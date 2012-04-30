/*! 
 *  Filters the sensor input, calculates pitch and roll.
 *
 *  This method work very well for planes. It has been extensively tested on a funjet plane.
 *  x = state = [roll pitch]
 *
 *  y = measurement = [acc_x acc_y acc_z]
 *
 *  See http://www.gluonpilot.com/wiki/Matlab_attitude_estimation
 *
 *  @file     ahrs_kalman_2x3.c
 *  @author   Tom Pycke
 *  @date     30-dec-2009
 *  @since    0.1
 */
 
#ifdef ENABLE_QUADROCOPTER
#error Please use ahrs_simple_quaternion_c for multicopter use!
#endif

#include <math.h>
 
#include "button/button.h"
#include "matrix/matrix.h"
#include "pid/pid.h"
#include "quaternion/quaternion.h"

#include "sensors.h"
#include "configuration.h"
#include "common.h"


static float sin_lookup[181];  // lookup table to avoid sinus calculations

float gravity_to_roll(float a_y, float a_z);
float gravity_to_pitch(float a_x, float a_z);

inline float fast_sin(float x);
inline float fast_cos(float x);
__attribute__((__const__)) int isNaN (float* f) ;

static float pitch_rad = 0.0, roll_rad = 0.0;
float pitch_rad_sum_error = 0.0;
float roll_rad_sum_error = 0.0;


void ahrs_init()
{
	float x;
	int i;
	
	for (x=(-3.14159f), i = 0; x < 3.14159f; x+=(2.0f/180.0f*3.14159f), i++)
	{
		sin_lookup[i] = sinf(x);
	}	
	
	// initialize our attitude with the current accelerometer's data
	//printf("-> %f %f %f <-\r\n", sensor_data.acc_x, sensor_data.acc_y, sensor_data.acc_z);
    pitch_rad = gravity_to_pitch(sensor_data.acc_x, sensor_data.acc_z);
    roll_rad = gravity_to_roll(sensor_data.acc_y, sensor_data.acc_z);
        
    sensor_data.p_bias = 0.0f;
	sensor_data.q_bias = 0.0f;
}	

#define normalize(pitch, roll)              \
		if (pitch > DEG2RAD(100.0f)) \
		{                                   \
            pitch = pitch - DEG2RAD(90.0f);    \
            roll = roll + DEG2RAD(180.0f);          \
  		}                                   \
        if (pitch < DEG2RAD(-100.0f)) \
        {                                    \
            pitch = pitch + DEG2RAD(90.0f);     \
            roll = roll + DEG2RAD(180.0f);           \
        }                                    \
        if (roll > DEG2RAD(180.0f))     \
            roll = roll - DEG2RAD(180.0f); \
        if (roll < DEG2RAD(-180.0f))    \
            roll = roll + DEG2RAD(180.0f); \



void ahrs_filter(float dt)
{
	static int i = 0;
	static float sin_roll=0.0;
	static float cos_roll=1.0;
	static float sin_pitch=0.0;
	static float cos_pitch=1.0;
	static float tan_pitch=0.0;
	static float df_dx[4] = {0.0, 0.0, 0.0, 0.0};
	static float P[4] = {1.0, 0.0, 0.0, 1.0};
	static float tmp1[9];
	static float tmp2[9];
	static float dh_dx_3x2[6];
	static float L[6];


	if (button_down())
	{
		//printf("down\r\n");
	}	
				
	/* optimization: comment this: */
	/*sin_roll = sin(roll_rad);
    cos_roll = cos(roll_rad);
    sin_pitch = sin(pitch_rad);
    cos_pitch = cos(pitch_rad);
    tan_pitch = tan(pitch_rad);*/
    
    // correction from outer loop
	sensor_data.p -= sensor_data.p_bias;
	sensor_data.q -= sensor_data.q_bias;

	roll_rad += dt * (sensor_data.p + (sensor_data.q*sin_roll + sensor_data.r*cos_roll) * tan_pitch);
	pitch_rad += dt * (sensor_data.q*cos_roll - sensor_data.r*sin_roll);
	
	// pitch = (-90,90]; roll = (-180,180]
	if (pitch_rad > DEG2RAD(180.0f))
		pitch_rad -= DEG2RAD(360.0f);
	if (pitch_rad < DEG2RAD(-180.0f))
		pitch_rad += DEG2RAD(360.0f);

	if (roll_rad > DEG2RAD(180.0f))
		roll_rad -= DEG2RAD(360.0f);
	if (roll_rad < DEG2RAD(-180.0f))
		roll_rad += DEG2RAD(360.0f);

	
	sin_roll = fast_sin(roll_rad);
    cos_roll = fast_cos(roll_rad);
    sin_pitch = fast_sin(pitch_rad);
    cos_pitch = fast_cos(pitch_rad);
    if (fabs(cos_pitch) < 0.02f)  // to avoid /0
    	tan_pitch = 1.0;
    else 
    	tan_pitch = sin_pitch / cos_pitch; //tan(pitch_rad);
    
    
    df_dx[0] = (sensor_data.q*cos_roll - sensor_data.r*sin_roll) * tan_pitch;
    df_dx[1] = (sensor_data.q*sin_roll - sensor_data.r*cos_roll)/(cos_pitch*cos_pitch);
    df_dx[2] = -sensor_data.q*sin_roll - sensor_data.r*cos_roll;
    //df_dx[3] = 0.0;

	//    A = df_dx;
    //    P = P + dt * (A*P + P*A' + Q);
    
    /* optimize me: P is symmectric: P[1] = P[2] */
    matrix_2x2_mul(df_dx, P, tmp1);   // A * P = tmp1
    matrix_2x2_mul_transp(P, df_dx, tmp2);  //  P * A' = tmp2
    tmp2[0] += 0.1f + tmp1[0];     // Q(1) = 0.1 for roll
    tmp2[1] += tmp1[1];
    //tmp2[2] += tmp1[2];
    tmp2[3] += 0.04f + tmp1[3];    // Q(2) = 0.02 for pitch, because the accelerometer is undergoing more non-compensated accelerations (at take-off for example)
    P[0] += tmp2[0] * dt;
    P[1] += tmp2[1] * dt;
    //P[2] += tmp2[2] * dt;
    P[2] = P[1];
    P[3] += tmp2[3] * dt;
    
    ////////////////////////////////
    
    if (i++ % 2 == 0 &&    // only apply every other iteration
        fabs(sensor_data.acc_x) < 1.0f)   // only apply when the acceleration along the x-axis is not too large (take-off!)
    {
	    //dh_dx = [q(i)/G*w_droll                        cos_pitch + q(i)*w_dpitch/G;...
	    //         -cos_pitch*cos_roll + p(i)*w_droll/G   sin_roll*sin_pitch + (r(i)*u_dpitch - p(i)*w_dpitch)/G;...
	    //         sin_roll*cos_pitch-p(i)*w_droll/G      cos_roll*sin_pitch + (p(i)*w_dpitch - q(i)*u_dpitch)/G];
	  	
	  	float dh = -sensor_data.vertical_speed;
	  	/*float u = cos_pitch * sensor_data.gps.speed_ms - sin_pitch * dh;
		float w = cos_roll * sin_pitch * sensor_data.gps.speed_ms + cos_roll * cos_pitch * dh;
	
	    float w_droll = -sin_roll * (sin_pitch * sensor_data.gps.speed_ms + cos_pitch * dh);
	    float u_dpitch = -sin_pitch * sensor_data.gps.speed_ms - cos_pitch * dh;
	    float w_dpitch = cos_roll * (cos_pitch * sensor_data.gps.speed_ms - sin_pitch * dh);*/
	    
	    /* Without dh: */
	  	float u = sqrt(sensor_data.gps.speed_ms*sensor_data.gps.speed_ms + dh*dh);
		float w = dh*cos_pitch*cos_roll; //cos_roll * sin_pitch * sensor_data.gps.speed_ms;
	
	    //float w_droll = -sin_roll * (sin_pitch * sensor_data.gps.speed_ms);
	    //float u_dpitch = -sin_pitch * sensor_data.gps.speed_ms;
	    //float w_dpitch = cos_roll * (cos_pitch * sensor_data.gps.speed_ms);
	    float w_droll = 0.0; float u_dpitch = 0.0; float w_dpitch = 0.0;


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
	   	
	   	
	   	tmp2[0] += 40.0f;   // x-axis = forward acceleration (not compensated), so less thrustworthy
	   	tmp2[4] += 30.0f;
	   	tmp2[8] += 35.0f;   // z-axis = vertical acceleration (not compensated for the moment, possibly using barometer?)
	   	
	   	float d;
	   	INVERT_3X3(tmp1, d, tmp2); // result = tmp1
	   	if (fabs(d) < 0.01f)  // almost division by 0 
	   	{
		   	int j;
		   	for (j = 0; j < 9; j++)
	   			tmp1[j] = 9999.9;
	   	}	
	   	
	   	// P * C'  [2x3] = tmp2
	   	matrix_2x2_times_3x2_transp(P, dh_dx_3x2, tmp2);  // P * C' = tmp2
	   	
	   	// L = tmp2 * tmp1   [2x3]
	   	matrix_2x3_times_3x3(tmp2, tmp1, L);
	   	
	   	// P = (eye(2,2) - L*C)*P;
	   	matrix_2x3_times_3x2(L, dh_dx_3x2, tmp1);
	   	tmp2[0] = 1.0f - tmp1[0];
	   	tmp2[1] = -tmp1[1];
	   	tmp2[2] = -tmp1[2];
	   	tmp2[3] = 1.0f - tmp1[3];
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
		
		tmp2[0] = L[0] * tmp1[0] + L[1] * tmp1[1] +  L[2] * tmp1[2];  // roll "error"
		tmp2[1] = L[3] * tmp1[0] + L[4] * tmp1[1] +  L[5] * tmp1[2];  // pitch "error"
		
	    roll_rad = roll_rad + tmp2[0];
	    pitch_rad = pitch_rad + tmp2[1];
	    
	    if (fabs(roll_rad) < DEG2RAD(45) && fabs(pitch_rad) < DEG2RAD(45))
	    {
		    roll_rad_sum_error += tmp2[0];
			pitch_rad_sum_error += tmp2[1];
		}
		
		if (fabs(pitch_rad) < DEG2RAD(89.0)) // to overcome secans +-inf
		{
			sensor_data.yaw += (sin_roll * sensor_data.q / cos_pitch + cos_roll * sensor_data.r / cos_pitch) * 0.04 ;  // try to calculate yaw approx.
			if (sensor_data.yaw >= DEG2RAD(360.0))
				sensor_data.yaw -= DEG2RAD(360.0);
			else if (sensor_data.yaw < DEG2RAD(0.0))
				sensor_data.yaw += DEG2RAD(360.0);
				
			if (fabs(sensor_data.yaw - sensor_data.gps.heading_rad) < DEG2RAD(250.0))  // do not chang if e.g. yaw = 355° and heading = 2°
				sensor_data.yaw = sensor_data.yaw*0.99 + sensor_data.gps.heading_rad*0.01;
		}	
    }
	else if (i % 25 == 0) // outer loop at 2Hz
	{
		// change bias with a max of 0.1°/s per second
		sensor_data.p_bias -= BIND(roll_rad_sum_error/10.0f, DEG2RAD(-0.1f), DEG2RAD(0.1f));
		sensor_data.q_bias -= BIND(pitch_rad_sum_error/10.0f, DEG2RAD(-0.1f), DEG2RAD(0.1f));
		//printf("\r\n %f \r\n", (roll_rad_sum_error/20.0));
		roll_rad_sum_error = 0.0f;
		pitch_rad_sum_error = 0.0f;
		
	}
#ifdef F1E_STEERING
	if (i % 5 == 0)
	{
		float mx = ((float)sensor_data.magnetometer_raw.x.i16);
		float my = ((float)sensor_data.magnetometer_raw.y.i16);
		float mz = ((float)sensor_data.magnetometer_raw.z.i16);

		float YH =                my*cos_roll           - mz*sin_roll;
		float XH = mx*cos_pitch + my*sin_roll*sin_pitch + mz*cos_roll*sin_pitch;
		
		sensor_data.yaw = atan2f (-YH, XH);
		while (sensor_data.yaw < 0.0)
			sensor_data.yaw += DEG2RAD(360.0);
		//sensor_data.yaw /= 2.0f;

		/*printf("\r\n%5d %5d %5d -> %f\r\n",sensor_data.magnetometer_raw.x.i16, 
							sensor_data.magnetometer_raw.y.i16,
							sensor_data.magnetometer_raw.z.i16, 
							sensor_data.yaw*180.0/3.14159);*/
	}	
#endif	
   
    /*int p = (int)pitch_rad;   
    if (p == -1 || (int)roll_rad == -1)   // we have a NaN -> ALERT
    {
		// reset everything
		pitch_rad = 0.0;
		roll_rad = 0.0;
		sin_roll = 0.0;
		cos_roll = 1.0;
		sin_pitch = 0.0;
		cos_pitch = 1.0;
		tan_pitch = 0.0;
		P[0] = 1.0;
		P[1] = 0.0;
		P[2] = 0.0;
		P[3] = 1.0;
		//printf("\r\n!\r\n");
	}*/
	if (isNaN(&pitch_rad))
	{
		pitch_rad = sensor_data.pitch;
		sin_pitch = 0.0;
		cos_pitch = 1.0;
		tan_pitch = 0.0;
		P[0] = 1.0;
		P[1] = 0.0;
		P[2] = 0.0;
		P[3] = 1.0;
        sensor_data.p_bias = 0.0f;
        sensor_data.q_bias = 0.0f;
		//printf("\r\n!\r\n");
	}
	if (isNaN(&roll_rad))
	{
		roll_rad = sensor_data.roll;
		sin_roll = 0.0;
		cos_roll = 1.0;
		P[0] = 1.0;
		P[1] = 0.0;
		P[2] = 0.0;
		P[3] = 1.0;
        sensor_data.p_bias = 0.0f;
        sensor_data.q_bias = 0.0f;
		//printf("\r\n!\r\n");
	}	
	normalize(pitch_rad, roll_rad);
   	sensor_data.pitch = pitch_rad;
	sensor_data.roll = roll_rad;
}	


__attribute__((__const__)) int isNaN (float* f)
 {
        int* rep = ((int*) f) + 1;
        return ((*rep & 0x7F00) == 0x7F00);
 }


/*!
 *   Calculates roll using accelerometer input.
 *
 *   Makes sure the output is similar to the quaternion's output.
 */
float gravity_to_roll(float a_y, float a_z)
{
	float roll_acc = atan(a_y / a_z);
	if (a_z > 0.0f)
	{
		if (a_y < 0.0f)	
			roll_acc =  roll_acc + 3.14159f;
		else
			roll_acc =  roll_acc - 3.14159f;
	}	
	return roll_acc;	
}	


/*!
 *   Calculates pitch using accelerometer input.
 *
 *   Makes sure the output is similar to the quaternion's output.
 */
float gravity_to_pitch(float a_x, float a_z)
{
	float pitch_acc = -atan(a_x / a_z); // replace with asin?

	if (a_z > 0.0f)
		pitch_acc =  -pitch_acc;

	return pitch_acc;
}


inline float fast_sin(float x)
{
	// -180..180 -> -60..60
	//int i = (int)(x/(2.0f/(180.0f/3.14159f))) + (180/2);
	int i = (int)(x/0.034906585f) + 90;
	return sin_lookup[i];
}	
inline float fast_cos(float x)
{
	// -180..180 -> -60..60
	return fast_sin((1.5707963f) - x);
}	
