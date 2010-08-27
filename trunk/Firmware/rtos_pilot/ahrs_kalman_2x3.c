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

#include <math.h>
 
#include "button/button.h"
#include "matrix/matrix.h"
#include "pid/pid.h"
#include "quaternion/quaternion.h"

#include "sensors.h"
#include "configuration.h"
#include "common.h"


// Usefull constants
static double pitch_rad = 0.0, roll_rad = 0.0;
static double pitch_acc = 0.0, roll_acc = 0.0;

static double sin_lookup[181];

void ahrs_init()
{
	double x;
	int i;
	
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

#define normalize(pitch, roll)              \
		if (pitch > 100.0/180.0*3.14159) \
		{                                   \
            pitch = pitch - 3.14159/2.0;    \
            roll = roll + 3.14159;          \
  		}                                   \
        if (pitch < -100.0/180.0*3.14159) \
        {                                    \
            pitch = pitch + 3.14159/2.0;     \
            roll = roll + 3.14159;           \
        }                                    \
        if (roll > 3.14159)     \
            roll = roll - 3.14159; \
        if (roll < -3.14159)    \
            roll = roll + 3.14159; \



void ahrs_filter(double dt)
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
	
	roll_rad += f0*dt;
	pitch_rad += f1*dt;
	
	// pitch = (-90,90]; roll = (-180,180]
	if (pitch_rad > DEG2RAD(180.0))
		pitch_rad -= DEG2RAD(360.0);
	if (pitch_rad < DEG2RAD(-180.0))
		pitch_rad += DEG2RAD(360.0);

	if (roll_rad > DEG2RAD(180.0))
		roll_rad -= DEG2RAD(360.0);
	if (roll_rad < DEG2RAD(-180.0))
		roll_rad += DEG2RAD(360.0);

	
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
    tmp2[0] += 0.1 + tmp1[0];     // Q(1) = 0.1 for roll
    tmp2[1] += tmp1[1];
    //tmp2[2] += tmp1[2];
    tmp2[3] += 0.03 + tmp1[3];    // Q(2) = 0.02 for pitch, because the accelerometer is undergoing more non-compensated accelerations (at take-off for example)
    P[0] += tmp2[0] * dt;
    P[1] += tmp2[1] * dt;
    //P[2] += tmp2[2] * dt;
    P[2] = P[1];
    P[3] += tmp2[3] * dt;
    
    ////////////////////////////////
    
    if (i++ % 2 == 0 &&    // only apply every other iteration
        fabs(sensor_data.acc_x) < 1.0)   // only apply when the acceleration along the x-axis is not too large (take-off!)
    {
	    //dh_dx = [q(i)/G*w_droll                        cos_pitch + q(i)*w_dpitch/G;...
	    //         -cos_pitch*cos_roll + p(i)*w_droll/G   sin_roll*sin_pitch + (r(i)*u_dpitch - p(i)*w_dpitch)/G;...
	    //         sin_roll*cos_pitch-p(i)*w_droll/G      cos_roll*sin_pitch + (p(i)*w_dpitch - q(i)*u_dpitch)/G];
	  	double dh = 0.0; //-sensor_data.vertical_speed;
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
	   	
	   	
	   	tmp2[0] += 40.0;   // x-axis = forward acceleration (not compensated), so less thrustworthy
	   	tmp2[4] += 30.0;
	   	tmp2[8] += 35.0;   // z-axis = vertical acceleration (not compensated for the moment, possibly using barometer?)
	   	
	   	double d;
	   	INVERT_3X3(tmp1, d, tmp2); // result = tmp1
	   	if (abs(d) < 0.01)  // almost division by 0 
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
    
    int p = (int)pitch_rad;   
    //if (pitch_rad != pitch_rad || roll_rad != roll_rad || abs(pitch_rad) > 999999.0 || abs(roll_rad) > 999999.9) // we have a NaN -> ALERT
    if (p == -1 && (int)roll_rad == -1)
    {
		// reset everything
		pitch_rad = 0.0;
		roll_rad = 0.0;
		pitch_acc = 0.0;
		roll_acc = 0.0;    
		sin_roll = 0.0;
		cos_roll = 1.0;
		sin_pitch = 0.0;
		cos_pitch = 1.0;
		tan_pitch = 0.0;
		P[0] = 1.0;
		P[1] = 0.0;
		P[2] = 0.0;
		P[3] = 1.0;
		printf("\r\n!\r\n");
	}
	normalize(pitch_rad, roll_rad);
   	sensor_data.pitch = pitch_rad;
	sensor_data.roll = roll_rad;
}	
