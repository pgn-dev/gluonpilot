/*! 
 *  Filters the sensor input, calculates pitch and roll.
 *
 *  This implementation uses quaternions to make sure there is no gimbal lock.
 *  Drift is compensated using a simple PID feedback.
 *
 *  @file     ahrs_simple_quat.c
 *  @author   Tom Pycke
 *  @date     30-dec-2009
 *  @since    0.1
 */
 
#include "button/button.h"

#include "pid.h"
#include "sensors.h"
#include "configuration.h"
#include "quaternion.h"
#include "kalman.h"


#include <math.h>



// Usefull constants
#define G 9.81f
#define DT 0.01f   // 100Hz
#define RAD2DEG (180.f/3.14159f)

static const float acc_value_g = 6600.0f;

static float pitch_rad = 0.0, roll_rad = 0.0;
static float pitch_acc = 0.0, roll_acc = 0.0;
static float q[4];
static float w; // speed along z axis

struct pid pid_p_bias;   // roll gyro bias
struct pid pid_q_bias;   // pitch gyro bias
struct pid pid_r_bias;   // yaw gyro bias

struct Gyro1DKalman filter_roll;
struct Gyro1DKalman filter_pitch;

static float p_bias = 0.0f;  // in rad/sec
static float q_bias = 0.0f;
static float r_bias = 0.0f;



inline float gravity_to_roll(float a_y, float a_z);
inline float gravity_to_pitch(float a_x, float a_z);


void ahrs_init()
{
	pid_init(&pid_p_bias, 0.0f, 0.15f, /*0.00001f*/ 0.001f, -15.0f /* guess */, 15.0f, 0.0f);
	pid_init(&pid_q_bias, 0.0f, 0.15f, /*0.00001f*/ 0.001f, -15.0f /* guess */, 15.0f, 0.0f);
	//pid_init(&r_bias, 0.0f, float p_gain, float i_gain, float i_min, float i_max, float d_term_min_var);
	
	//init_Gyro1DKalman(&filter_roll, 10e-10, 10e-6, 0.096);
	//init_Gyro1DKalman(&filter_pitch, 10e-10, 10e-6, 0.01);
	
	quaternion_from_attitude(0.0f, 0.0f, 0.0f, q);
	
	p_bias = 0.0f;
	q_bias = 0.0f;
}	


void ahrs_filter()
{
	static int counter_since_last_update = 0;
	static int last_gps_sentence_number_last_fix = 0;
	
	if (button_down())
	{
		quaternion_from_attitude(0.0f, 0.0f, 0.0f, q);
	}	
		
	/*roll_rad += sensor_data.p + ((sensor_data.q * sin_roll + sensor_data.r * cos_roll) * tanf(pitch_rad)) * DT;
	pitch_rad += (sensor_data.q * cos_roll - sensor_data.r * sin_roll) * DT;
	sensor_data.yaw += sensor_data.r * DT;
	MODULO_ANGLE(roll_rad);
	MODULO_ANGLE(pitch_rad);*/
	
	// for PID:
	sensor_data.p += p_bias;
	sensor_data.q += q_bias;

	//ars_predict(&filter_roll, sensor_data.p /*+ ((sensor_data.q * sinf(filter_roll.x_angle) + sensor_data.r * cosf(filter_roll.x_angle)) * tanf(filter_pitch.x_angle))*/, DT);
	//ars_predict(&filter_pitch, (sensor_data.q * cosf(filter_roll.x_angle))/* - sensor_data.r * sinf(filter_roll.x_angle))*/, DT);
		
	//quaternion_update_with_rates(sensor_data.p- filter_roll.x_bias, sensor_data.q-filter_pitch.x_bias/cosf(filter_roll.x_angle), sensor_data.r, q, DT);
	quaternion_update_with_rates(sensor_data.p, sensor_data.q, sensor_data.r, q, DT);
	
	roll_rad = quaternion_to_roll(q);
	pitch_rad = quaternion_to_pitch(q);
	sensor_data.yaw = quaternion_to_yaw(q);
	//filter_roll.x_angle = roll_rad;
	//filter_pitch.x_angle = pitch_rad;
	
	
	float u = sensor_data.gps.speed_ms;
	
	//float a_x = sensor_data.q * w / G + sin_pitch;
	//float a_y = (sensor_data.r * u - sensor_data.p * w) / G - cos_pitch*sin_roll;
	//float a_z = (-sensor_data.q * u) / G - cos_pitch*cos_roll;
	
	// calculate the gravity-component from the accelerometers by substracting the dynamics
	float g_ax = sensor_data.acc_x - sensor_data.q * w / G;
	float g_ay = sensor_data.acc_y - (sensor_data.r * u - sensor_data.p * w) / G;
	float g_az = sensor_data.acc_z - (-sensor_data.q * u);
	
	roll_acc = gravity_to_roll(g_ay, g_az);
	pitch_acc = gravity_to_pitch(g_ax, g_az);
	
	
	counter_since_last_update++;
	// Update bias using accelerometer data on evert new GPS sentence (5Hz) or at 4Hz otherwise.
	if (sensor_data.gps.sentence_number_last_fix != last_gps_sentence_number_last_fix ||
	    counter_since_last_update == 25) 
	{
		counter_since_last_update = 0;
		last_gps_sentence_number_last_fix = sensor_data.gps.sentence_number_last_fix;
		
		/* Do not do this update when:
		 *   - Acceleration is too strong (saturation, strong vibration, shock)
		 *   - Accelerometer data is too small: a_x ~ 0, a_z ~ 0 => atan(g_ax / g_az) ?
		 */
		     
		int fabs_roll = (int)fabs(roll_acc*RAD2DEG);
		if (fabs_roll < 80.f || fabs_roll > 100.0f)  		// don't trust accelerometer pitch when roll is around +-90°
		{
			float error = (roll_acc - roll_rad);    // roll_rad += sensor_data.p + ((sensor_data.q * sin_roll + sensor_data.r * cos_roll) * tanf(pitch_rad)) * DT;
			
			// tricky behaviour when acc is +175° and gyro one is -175°
			if (error > (250.0f/RAD2DEG))
				error -= 2.0f*3.14159f;
			else if (error < (-250.0f/RAD2DEG))
				error += 2.0f*3.14159f; 
			
			p_bias = pid_update_only_p_and_i(&pid_p_bias, error, 0.2f);
		}	
		
		float fabs_pitch = (int)fabs(pitch_acc*RAD2DEG);
		if (fabs_pitch < (80.f) || fabs_pitch > (100.0f)) // don't trust accelerometer roll when pitch is around +-90°
		{
			float error = (pitch_acc - pitch_rad) * cosf(roll_rad);    // because pitch_rad += (sensor_data.q * cos_roll - sensor_data.r * sin_roll) * DT
			q_bias = pid_update_only_p_and_i(&pid_q_bias, error, 0.2f);
		}
		
		ars_update(&filter_roll, roll_acc);
		ars_update(&filter_pitch, pitch_acc);
	}
	
	sensor_data.pitch = pitch_rad;
	sensor_data.roll = roll_rad;
	//sensor_data.pitch = filter_pitch.x_angle;
	//sensor_data.roll = filter_roll.x_angle;
	
	sensor_data.pitch_acc = pitch_acc;
	sensor_data.roll_acc = roll_acc;
}

/*!
 *   Calculates roll using accelerometer input.
 *
 *   Makes sure the output is similar to the quaternion's output.
 */
inline float gravity_to_roll(float a_y, float a_z)
{
	float roll_acc = atanf(a_y / a_z);
	if (a_y < 0.0f)
	{
		if (a_z > 0.0f)
			roll_acc =  roll_acc + 3.14159f;
	}		
	else 
	{
		if (a_z > 0.0f)
			roll_acc =  roll_acc - 3.14159f;
	}
	return roll_acc;	
}	


/*!
 *   Calculates pitch using accelerometer input.
 *
 *   Makes sure the output is similar to the quaternion's output.
 */
inline float gravity_to_pitch(float a_x, float a_z)
{
	float pitch_acc = -atanf(a_x / a_z);
	if (a_x < 0.0f)
	{
		if (a_z > 0.0f)
			pitch_acc =  -pitch_acc;
		
	}		
	else
	{
		if (a_z > 0.0f)
			pitch_acc =  -pitch_acc;
	}
	return pitch_acc;
}
