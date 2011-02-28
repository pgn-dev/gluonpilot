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
 
#ifndef ENABLE_QUADROCOPTER
#error Please use ahrs_kalman_2x3.c for fixed wing aircraft use!
#endif

#include <math.h>
 
#include "button/button.h"
#include "matrix/matrix.h"
#include "pid/pid.h"
#include "quaternion/quaternion.h"

#include "sensors.h"
#include "configuration.h"


// Usefull constants
#define G 9.81
#define RAD2DEG (180.0/3.14159)

static double pitch_rad = 0.0, roll_rad = 0.0;
static double pitch_acc = 0.0, roll_acc = 0.0;
static double q[4];
static double w; // speed along z axis

struct pid pid_p_bias;   // roll gyro bias
struct pid pid_q_bias;   // pitch gyro bias

static double p_bias = 0.0;  // in rad/sec
static double q_bias = 0.0;


inline double gravity_to_roll(double a_y, double a_z);
inline double gravity_to_pitch(double a_x, double a_z);

void ahrs_init()
{
	// I is 10 to 100 times smaller than P
	pid_init(&pid_p_bias, 0.0, 0.6, /*0.00001*/ 0.001, -100.0, 100.0, 0.0);
	pid_init(&pid_q_bias, 0.0, 0.6, /*0.00001*/ 0.001, -100.0, 100.0, 0.0);
	//pid_init(&r_bias, 0.0f, float p_gain, float i_gain, float i_min, float i_max, float d_term_min_var);
	
	quaternion_from_attitude(0.0, 0.0, 0.0, q);
	
	p_bias = 0.0;
	q_bias = 0.0;
}	


void ahrs_filter(double dt)
{
	//static int counter_since_last_update = 0;
	//static int last_gps_sentence_number_last_fix = 0;
	
	if (button_down())
	{
		quaternion_from_attitude(0.0, 0.0, 0.0, q);
	}	

	// for PID:
	sensor_data.p += p_bias;
	sensor_data.q += q_bias;

	quaternion_update_with_rates(sensor_data.p, sensor_data.q, sensor_data.r, q, dt);
	
	roll_rad = quaternion_to_roll(q);
	pitch_rad = quaternion_to_pitch(q);
	sensor_data.yaw = quaternion_to_yaw(q);
	
	
	double u = sensor_data.gps.speed_ms;
	
	// calculate the gravity-component from the accelerometers by substracting the dynamics
	w = 0.0;
	double g_ax = sensor_data.acc_x;// - sensor_data.q * w / G;
	double g_ay = sensor_data.acc_y - (sensor_data.r * u/* - sensor_data.p * w*/) / G;
	double g_az = sensor_data.acc_z + (sensor_data.q * u / G);
	
	roll_acc = gravity_to_roll(g_ay, g_az);
	pitch_acc = gravity_to_pitch(g_ax, g_az);
		
		/* Do not do this update when:
		 *   - Acceleration is too strong (saturation, strong vibration, shock)
		 *   - Accelerometer data is too small: a_x ~ 0, a_z ~ 0 => atan(g_ax / g_az) ?
		 */
		     
		int abs_roll = (int)fabs(roll_rad*RAD2DEG);
		int abs_pitch = (int)fabs(pitch_rad*RAD2DEG);
		if (abs_pitch < 75 || abs_pitch > 105)  		// don't trust accelerometer pitch when roll is around +-90°
		{
			double roll_error = (roll_acc - roll_rad);
			
			// tricky behaviour when acc is +175° and gyro one is -175°
			if (roll_error > (250.0/RAD2DEG))
				roll_error -= 2.0 * 3.14159;
			else if (roll_error < (-250.0f/RAD2DEG))
				roll_error += 2.0 * 3.14159; 
			
			p_bias = pid_update_only_p_and_i(&pid_p_bias, roll_error, dt);
		}	
		
		double pitch_error;
		if (abs_roll < (75) || abs_roll > (105)) // don't trust accelerometer roll when pitch is around +-90°
		{
			pitch_error = (pitch_acc - pitch_rad);
			q_bias =  pid_update_only_p_and_i(&pid_q_bias, pitch_error, dt);
		}
	
	sensor_data.pitch = pitch_rad;
	sensor_data.roll = roll_rad;
	
	sensor_data.pitch_acc = pitch_acc;
	sensor_data.roll_acc = roll_acc;
}


/*!
 *   Calculates roll using accelerometer input.
 *
 *   Makes sure the output is similar to the quaternion's output.
 */
inline double gravity_to_roll(double a_y, double a_z)
{
	double roll_acc = atan(a_y / a_z);
	if (a_z > 0.0)
	{
		if (a_y < 0.0)	
			roll_acc =  roll_acc + 3.14159;
		else
			roll_acc =  roll_acc - 3.14159;
	}	
	return roll_acc;	
}	


/*!
 *   Calculates pitch using accelerometer input.
 *
 *   Makes sure the output is similar to the quaternion's output.
 */
inline double gravity_to_pitch(double a_x, double a_z)
{
	double pitch_acc = -atan(a_x / a_z); // replace with asin?

	if (a_z > 0.0)
		pitch_acc =  -pitch_acc;

	return pitch_acc;
}
