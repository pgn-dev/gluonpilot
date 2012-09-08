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
	#ifndef F1E_STEERING
		#error Please use ahrs_kalman_2x3.c for fixed wing aircraft use!
	#endif
#endif

#include <math.h>
#include <stdio.h>

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

    pitch_rad = gravity_to_pitch(sensor_data.acc_x, sensor_data.acc_z);
    roll_rad = gravity_to_roll(sensor_data.acc_y, sensor_data.acc_z);
    hmc5843_read(&sensor_data.magnetometer_raw);
	quaternion_from_attitude(roll_rad, pitch_rad, atan2f(-(float)sensor_data.magnetometer_raw.y.i16,(float)sensor_data.magnetometer_raw.x.i16)/3.14*180.0, q);
	
	p_bias = 0.0;
	q_bias = 0.0;
}	


#define Kp 2.0f			// proportional gain governs rate of convergence to accelerometer/magnetometer  WAS 1
#define Ki 0.003f		// integral gain governs rate of convergence of gyroscope biases
#define halfT 0.002f		// half the sample period

float exInt = 0, eyInt = 0, ezInt = 0;	// scaled integral error

int i = 0;

//====================================================================================================
// Function
//====================================================================================================

void ahrs_filter() 
{
	float norm;
	float mx, my, mz;
	float hx, hy, hz, bx, bz;
	float vx, vy, vz, wx, wy, wz;
	float ex, ey, ez;

    if (button_down())
	{
		quaternion_from_attitude(0.0, 0.0, 0.0, q);
	}	

	// auxiliary variables to reduce number of repeated operations
	float q0q0 = q[0]*q[0];
	float q0q1 = q[0]*q[1];
	float q0q2 = q[0]*q[2];
	float q0q3 = q[0]*q[3];
	float q1q1 = q[1]*q[1];
	float q1q2 = q[1]*q[2];
	float q1q3 = q[1]*q[3];
	float q2q2 = q[2]*q[2];   
	float q2q3 = q[2]*q[3];
	float q3q3 = q[3]*q[3];          
	
	// normalise the measurements
	norm = sqrt(sensor_data.acc_x*sensor_data.acc_x + sensor_data.acc_y*sensor_data.acc_y + sensor_data.acc_z*sensor_data.acc_z);       
	if (fabs(norm) > 0.001)
	{
		sensor_data.acc_x = sensor_data.acc_x / norm;
		sensor_data.acc_y = sensor_data.acc_y / norm;
		sensor_data.acc_z = sensor_data.acc_z / norm;
	}
	mx = (float)sensor_data.magnetometer_raw.x.i16;
	my = (float)sensor_data.magnetometer_raw.y.i16;
	mz = (float)sensor_data.magnetometer_raw.z.i16;
	norm = sqrt(mx*mx + my*my + mz*mz);
	if (fabs(norm) > 0.001)
	{
		mx = mx / norm;
		my = my / norm;
		mz = mz / norm;         
	}
	// compute reference direction of flux
	hx = 2*mx*(0.5 - q2q2 - q3q3) + 2*my*(q1q2 - q0q3) + 2*mz*(q1q3 + q0q2);
	hy = 2*mx*(q1q2 + q0q3) + 2*my*(0.5 - q1q1 - q3q3) + 2*mz*(q2q3 - q0q1);
	hz = 2*mx*(q1q3 - q0q2) + 2*my*(q2q3 + q0q1) + 2*mz*(0.5 - q1q1 - q2q2);         
	bx = sqrt((hx*hx) + (hy*hy));
	bz = hz;        
	
	// estimated direction of gravity and flux (v and w)
	vx = 2*(q1q3 - q0q2);
	vy = 2*(q0q1 + q2q3);
	vz = q0q0 - q1q1 - q2q2 + q3q3;
	wx = 2*bx*(0.5 - q2q2 - q3q3) + 2*bz*(q1q3 - q0q2);
	wy = 2*bx*(q1q2 - q0q3) + 2*bz*(q0q1 + q2q3);
	wz = 2*bx*(q0q2 + q1q3) + 2*bz*(0.5 - q1q1 - q2q2);  
	
	// error is sum of cross product between reference direction of fields and direction measured by sensors
	ex = (-sensor_data.acc_y*vz - -sensor_data.acc_z*vy) + (my*wz - mz*wy);
	ey = (-sensor_data.acc_z*vx - sensor_data.acc_x*vz) + (mz*wx - mx*wz);
	ez = (sensor_data.acc_x*vy - -sensor_data.acc_y*vx) + (mx*wy - my*wx);
	
	// integral error scaled integral gain
	exInt = exInt + ex*Ki*halfT;
	eyInt = eyInt + ey*Ki*halfT;
	ezInt = ezInt + ez*Ki*halfT;
	
	// adjusted gyroscope measurements
	sensor_data.p = sensor_data.p + Kp*ex + exInt;
	sensor_data.q = -sensor_data.q + Kp*ey + eyInt;
	sensor_data.r = sensor_data.r + Kp*ez + ezInt;
	
	// integrate quaternion rate and normalise
	q[0] = q[0] + (-q[1]*sensor_data.p - q[2]*sensor_data.q - q[3]*sensor_data.r)*halfT;
	q[1] = q[1] + (q[0]*sensor_data.p + q[2]*sensor_data.r - q[3]*sensor_data.q)*halfT;
	q[2] = q[2] + (q[0]*sensor_data.q - q[1]*sensor_data.r + q[3]*sensor_data.p)*halfT;
	q[3] = q[3] + (q[0]*sensor_data.r + q[1]*sensor_data.q - q[2]*sensor_data.p)*halfT;  
	
	// normalise quaternion
	norm = sqrt(q[0]*q[0] + q[1]*q[1] + q[2]*q[2] + q[3]*q[3]);
	if (fabs(norm) > 0.001)
	{
		q[0] = q[0] / norm;
		q[1] = q[1] / norm;
		q[2] = q[2] / norm;
		q[3] = q[3] / norm;
	}

	sensor_data.roll = quaternion_to_roll(q);
	sensor_data.pitch = quaternion_to_pitch(q);
	sensor_data.yaw = quaternion_to_yaw(q);
	
	if (i++  % 210 == 0)
    {
        int heading = sensor_data.yaw/3.14*180.0;
        if (heading < 0)
            heading += 360;
		printf("\r\nCompass: %d\r\n", heading);
    }
}


void ahrs_filter2(double dt)
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
