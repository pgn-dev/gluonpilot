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

#include <math.h>
 
#include "button/button.h"
#include "matrix/matrix.h"
#include "pid/pid.h"
#include "quaternion/quaternion.h"

#include "sensors.h"
#include "configuration.h"


// Usefull constants
#define G 9.81
#define DT 0.02   // 50Hz
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

static double sin_lookup[181];
void ahrs_init()
{
	double x;
	int i;
	// I is 10 to 100 times smaller than P
	pid_init(&pid_p_bias, 0.0, 0.1, /*0.00001*/ 0.001, -100.0, 100.0, 0.0);
	pid_init(&pid_q_bias, 0.0, 0.1, /*0.00001*/ 0.001, -100.0, 100.0, 0.0);
	//pid_init(&r_bias, 0.0f, float p_gain, float i_gain, float i_min, float i_max, float d_term_min_var);
	
	quaternion_from_attitude(0.0, 0.0, 0.0, q);
	
	p_bias = 0.0;
	q_bias = 0.0;
	
	for (x=(-3.14159), i = 0; x < 3.14159; x+=(2.0/180.0*3.14159), i++)
	{
		sin_lookup[i] = sin(x);
	}	
}	


double sin_roll=0.0;
double cos_roll=1.0;
double sin_pitch=0.0;
double cos_pitch=1.0;
double tan_pitch=0.0;
double f0, f1;
double df_dx[4] = {0.0, 0.0, 0.0, 0.0};
double P[4] = {1.0, 0.0, 0.0, 1.0};
double tmp1[9];
double tmp2[9];
double dh_dx_3x2[6];
double L[6];
double h[3];


double fast_sin(double x)
{
	// -180..180 -> -60..60
	int i = (int)(x/(2.0/(180.0/3.14159))) + (180/2);
	return sin_lookup[i];
}	
double fast_cos(double x)
{
	// -180..180 -> -60..60
	return fast_sin((3.14159/2.0) - x);
}	


void ahrs_filter()
{
	static int i = 0;
	if (button_down())
	{
		;
	}	
				
	/* optimization: comment this: */
	/*sin_roll = sin(roll_rad);
    cos_roll = cos(roll_rad);
    sin_pitch = sin(pitch_rad);
    cos_pitch = cos(pitch_rad);
    tan_pitch = tan(pitch_rad);*/
    
	f0 = sensor_data.p + sensor_data.q*sin_roll*tan_pitch + sensor_data.r*cos_roll*tan_pitch;
	f1 = sensor_data.q*cos_roll - sensor_data.r*sin_roll;
	
	roll_rad += f0*DT;
	pitch_rad += f1*DT;
	
	// pitch = (-90,90]; roll = (-180,180]
	while (pitch_rad > 3.14159)
		pitch_rad -= 3.14159*2.0;
	while (pitch_rad < -3.14159)
		pitch_rad += 3.14159*2.0;

	while (roll_rad > 3.14159)
		roll_rad -= 3.14159*2.0;
	while (roll_rad < -3.14159)
		roll_rad += 3.14159*2.0;

	
	sin_roll = fast_sin(roll_rad);
    cos_roll = fast_cos(roll_rad);
    sin_pitch = fast_sin(pitch_rad);
    cos_pitch = fast_cos(pitch_rad);
    tan_pitch = sin_pitch / cos_pitch; //tan(pitch_rad);
    
    
    df_dx[0] = sensor_data.q*cos_roll*tan_pitch - sensor_data.r*sin_roll*tan_pitch;
    df_dx[1] = (sensor_data.q*sin_roll - sensor_data.r*cos_roll)/(cos_pitch*cos_pitch);
    df_dx[2] = -sensor_data.q*sin_roll - sensor_data.r*cos_roll;
    //df_dx[3] = 0.0;

	//    A = df_dx;
    //    P = P + dt * (A*P + P*A' + Q);
    
    /* optimize me: P is symmectric: P[1] = P[2] */
    matrix_2x2_mul(df_dx, P, tmp1);   // A * P = tmp1
    matrix_2x2_mul_transp(P, df_dx, tmp2);  //  P * A' = tmp2
    tmp2[0] += 0.001 + tmp1[0]; // Q
    tmp2[1] += tmp1[1];
    //tmp2[2] += tmp1[2];
    tmp2[3] += 0.001 + tmp1[3];
    P[0] += tmp2[0] * DT;
    P[1] += tmp2[1] * DT;
    //P[2] += tmp2[2] * DT;
    P[2] = P[1];
    P[3] += tmp2[3] * DT;
    
    ////////////////////////////////
    
    if (i++ % 2 == 0)
    {
	    //dh_dx = [q(i)/G*w_droll                        cos_pitch + q(i)*w_dpitch/G;...
	    //         -cos_pitch*cos_roll + p(i)*w_droll/G   sin_roll*sin_pitch + (r(i)*u_dpitch - p(i)*w_dpitch)/G;...
	    //         sin_roll*cos_pitch-p(i)*w_droll/G      cos_roll*sin_pitch + (p(i)*w_dpitch - q(i)*u_dpitch)/G];
	  	double dh = -sensor_data.vertical_speed;
	  	double u = cos_pitch * sensor_data.gps.speed_ms - sin_pitch * dh;
		double w = cos_roll * sin_pitch * sensor_data.gps.speed_ms + cos_roll * cos_pitch * dh;
	
	    double w_droll = (-sin_roll * sin_pitch * sensor_data.gps.speed_ms - sin_roll * cos_pitch * dh);
	    double u_dpitch = -sin_pitch * sensor_data.gps.speed_ms - cos_pitch * dh;
	    double w_dpitch = cos_roll * cos_pitch * sensor_data.gps.speed_ms - cos_roll * sin_pitch * dh;
	            
	    dh_dx_3x2[0] = sensor_data.q/G*w_droll;
	    dh_dx_3x2[1] = cos_pitch + sensor_data.q*w_dpitch/G;
	    dh_dx_3x2[2] = -cos_pitch*cos_roll + sensor_data.p*w_droll/G;
	    dh_dx_3x2[3] = sin_roll*sin_pitch + (sensor_data.r*u_dpitch - sensor_data.p*w_dpitch)/G;
	    dh_dx_3x2[4] = sin_roll*cos_pitch - sensor_data.p*w_droll/G;
	    dh_dx_3x2[5] = cos_roll*sin_pitch + (sensor_data.p*w_dpitch - sensor_data.q*u_dpitch)/G;
	    
	    /*
	    C = dh_dx;  %C:3x2   P:2x2
		L = P*C'*(R + C*P*C')^-1;  % 2x3
	   	P = (eye(2,2) - L*C)*P;  
	   	*/
	   	matrix_3x2_times_2x2(dh_dx_3x2, P, tmp1);  // C * P = tmp1
	   	matrix_3x2_times_3x2_transp(tmp1, dh_dx_3x2, tmp2);  // tmp1 * C' = tmp2
	   	//R = diag([0.25 0.25 0.25]);
	   	tmp2[0] += 0.25;
	   	tmp2[4] += 0.25;
	   	tmp2[8] += 0.25;
	   	
	   	double d;
	   	INVERT_3X3(tmp1, d, tmp2); // result = tmp1
	   	if (fabs(d) < 0.01)  // almost division by 0 
	   		return;
	   	
	   	// P * C'  [2x3] = tmp2
	   	matrix_2x2_times_3x2_transp(P, dh_dx_3x2, tmp2);  // P * C' = tmp2
	   	
	   	// L = tmp2 * tmp1   [2x3]
	   	matrix_2x3_times_3x3(tmp2, tmp1, L);
	   	
	   	// P = (eye(2,2) - L*C)*P;
	   	matrix_2x3_times_3x2(L, dh_dx_3x2, tmp1);
	   	tmp2[0] = 1.0 - tmp1[0];
	   	tmp2[1] = -tmp1[1];
	   	tmp2[2] = -tmp1[2];
	   	tmp2[3] = 1.0 - tmp1[3];
	   	matrix_2x2_mul(tmp2, P, tmp1);
	   	P[0] = tmp1[0];
	   	P[1] = tmp1[1];
	   	P[2] = tmp1[2];
	   	P[3] = tmp1[3];
	   	
	   	/*
	   	h = [q(i)*w/G + sin_pitch; ...
	         (r(i)*u - p(i)*w )/G - cos_pitch*sin_roll; ...
	         (p(i)*w - q(i)*u)/G  - cos_pitch*cos_roll]; 
	    */
		    
	    tmp1[0] = sensor_data.acc_x - (sensor_data.q*w/G + sin_pitch);
	    tmp1[1] = sensor_data.acc_y - ((sensor_data.r*u - sensor_data.p*w)/G - cos_pitch*sin_roll);
	    tmp1[2] = sensor_data.acc_z - ((sensor_data.p*w - sensor_data.q*u)/G  - cos_pitch*cos_roll);
		
		// x = x + L*([a_x(i);a_y(i);a_z(i)] - h);    
	    roll_rad = roll_rad + L[0] * tmp1[0] + L[1] * tmp1[1] +  L[2] * tmp1[2];
	    pitch_rad = pitch_rad + L[3] * tmp1[0] + L[4] * tmp1[1] +  L[5] * tmp1[2];
    }
    sensor_data.pitch = pitch_rad;
	sensor_data.roll = roll_rad;
	sensor_data.pitch_acc = w;
}	


void ahrs_filter2()
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

	quaternion_update_with_rates(sensor_data.p, sensor_data.q, sensor_data.r, q, DT);
	
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
			
			p_bias = pid_update_only_p_and_i(&pid_p_bias, roll_error, DT);
		}	
		
		double pitch_error;
		if (abs_roll < (75) || abs_roll > (105)) // don't trust accelerometer roll when pitch is around +-90°
		{
			pitch_error = (pitch_acc - pitch_rad);
			q_bias =  pid_update_only_p_and_i(&pid_q_bias, pitch_error, DT);
		}
	
	sensor_data.pitch = pitch_rad;
	sensor_data.roll = roll_rad;
	
	sensor_data.pitch_acc = pid_q_bias.i_state;
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
