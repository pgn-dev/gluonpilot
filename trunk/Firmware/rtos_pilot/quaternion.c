/*! 
 *  Implementation of the quaternion concept. 
 *
 *  Used to update the attitude without suffering gimbal lock.
 *  For more information, please see http://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation
 *
 *  @file     quaternion.c
 *  @author   Tom Pycke
 *  @date     25-oct-2008
 *  @since    0.1
 */
 
#include <errno.h>
#include <math.h>
#include "quaternion.h"

/*!
 *	 Initialize quaternion (q[4]) with roll, pitch and yaw euclidian angles.
 */
void quaternion_from_attitude (const float roll, const float pitch, const float yaw, float* q)
{
	float cos_roll_2 = cosf(roll/2.0f);
	float sin_roll_2 = sinf(roll/2.0f);
	float cos_pitch_2 = cosf(pitch/2.0f);
	float sin_pitch_2 = sinf(pitch/2.0f);
	float cos_yaw_2 = cosf(yaw/2.0f);
	float sin_yaw_2 = sinf(yaw/2.0f);

	q[0] = cos_roll_2 * cos_pitch_2 * cos_yaw_2 + sin_roll_2 * sin_pitch_2 * sin_yaw_2;
	q[1] = sin_roll_2 * cos_pitch_2 * cos_yaw_2 - cos_roll_2 * sin_pitch_2 * sin_yaw_2;
	q[2] = cos_roll_2 * sin_pitch_2 * cos_yaw_2 + sin_roll_2 * cos_pitch_2 * sin_yaw_2;
	q[3] = cos_roll_2 * cos_pitch_2 * sin_yaw_2 - sin_roll_2 * sin_pitch_2 * cos_yaw_2; // WAS cos_roll_2 * cos_pitch_2 * sin_yaw_2 - sin_roll_2 * sin_pitch_2 * sin_yaw_2

	quaternion_normalize(q);
}


/*!
 *	 Update quaternion with rates.
 *   @param p = along the x-axis = roll-rate
 *   @param q = along the y-axis = pitch-rate
 *   @param z = along the z-axis = yaw-rate
 */
void quaternion_update_with_rates (const float rollrate, const float pitchrate, const float headingrate, float *q, const float dt)
{
	const float w1 = rollrate;
	const float w2 = pitchrate;
	const float w3 = headingrate;

	float q0 = q[0];
	float q1 = q[1];
	float q2 = q[2];
	float q3 = q[3];
	
	q0 += 0.5f * (        - q[1]*w1 - q[2]*w2 - q[3]*w3)*dt;
	q1 += 0.5f * (q[0]*w1 +           q[2]*w3 - q[3]*w2)*dt;
	q2 += 0.5f * (q[0]*w2 - q[1]*w3 +           q[3]*w1)*dt;
	q3 += 0.5f * (q[0]*w3 + q[1]*w2 - q[2]*w1)*dt;
	
	q[0] = q0;
	q[1] = q1;
	q[2] = q2;
	q[3] = q3;

	quaternion_normalize(q);
}

float quaternion_to_roll (const float* q)
{
	float r;
	errno = 0;
	r = atan2f( 2.0 * ( q[2]*q[3] + q[0]*q[1] ) ,
	             (1.0 - 2.0 * (q[1]*q[1] + q[2]*q[2])) );  
	if (errno)
		return  0.0;
	else
		return r;
}	

float quaternion_to_pitch(const float* q)
{
	float r;
	errno = 0;
	r = asinf( -2.0 * (q[1]*q[3] - q[0]*q[2]) );	
	if (errno)
		return  0.0;
	else
		return r;
}


float quaternion_to_yaw(const float* q)
{
	float r;
	errno = 0;
	r = atan2f( 2.0 * ( q[0]*q[3] + q[1]*q[2] ) ,
	          (1.0 - 2.0 * (q[2]*q[2] + q[3]*q[3])) );  
	if (errno)
		return  0.0;
	else
		return r;
}	

void quaternion_normalize(float *q)
{
	float norm = sqrtf(q[0]*q[0] + q[1]*q[1] + q[2]*q[2] + q[3]*q[3]);

	q[0] /= norm;	
	q[1] /= norm;
	q[2] /= norm;
	q[3] /= norm;
}

