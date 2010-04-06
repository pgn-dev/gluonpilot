/*! 
 *  The PID-controller functions.
 *
 *  This implementation uses a generic PID controller with some
 *  simplifications. For more information on PID controllers, please
 *  check wikipedia: http://en.wikipedia.org/wiki/PID_controller
 *
 *  @file     pid.c
 *  @author   Tom Pycke
 *  @date     24-dec-2007
 *  @since    0.1
 */

#include <math.h>

#include "pid.h"
#include "configuration.h"


/*!
 *  Initializes the pid structure.
 *  @param pid    The to be initialized struct.
 *  @param d_gain The deriative gain.
 *  @param i_gain The integral gain.
 *  @param p_gain The proportional gain.
 *  @param i_min  Anti-windup for the integral term. Preferably -i_max.
 *  @param i_max  Positive anti-windup for the integral term.
 *  @param d_term_min_var Minimum difference required to active the derivative
 *                        term. Used to prevent jitter on the output.
 *  @todo  Add global min and max value for the output.
 */
void pid_init(struct pid *pid, double d_gain, double p_gain, double i_gain, double i_min, double i_max, double d_term_min_var)
{
	pid->last_error = 0.0;
	pid->i_state = 0.0;
	pid->d_state = 0.0;
	pid->i_max = i_max;
	pid->i_min = i_min;
	pid->d_gain = d_gain;
	pid->p_gain = p_gain;
	pid->i_gain = i_gain;
	pid->d_term_min_var = d_term_min_var;
}


/*!
 *  Updates the pid structure with all the available bells and whistles.
 *  @param pid The pid structure that has to be updated.
 *  @param position The reading (proportional term).
 *  @param dt Time since last update, needed for the integral and derivative term.
 *  @return The calculated pid output.
 */
double pid_update(struct pid *pid, double position, double dt)
{
	double tmp;
	
	tmp = (position - pid->d_state) * pid->d_gain / dt;   // D-term
	
	// to eliminate jittering which wears out servos
	if (abs(tmp) < pid->d_term_min_var)
		tmp = 0.0;
	
	// update d_state for next call.
	pid->d_state = position;
	
	pid->i_state += position * dt;                        // I-term
	if (pid->i_state > pid->i_max)
		pid->i_state = pid->i_max;
	else if(pid->i_state < pid->i_min)
		pid->i_state = pid->i_min;
	tmp += pid->i_state * pid->i_gain;

	
	tmp += pid->p_gain * position;                        // P-term
	return tmp;
}


/*!
 *  Update the pid struct using only the proportional term.
 *  @param position The reading (proportional term).
 *  @param dt Time since last update, needed for the integral and derivative term.
 *  @return The calculated pid output.
 */
double pid_update_only_p(struct pid *pid, double position, double dt)
{
	return pid->p_gain * position;                        // P-term	
}


double pid_update_only_p_and_i(struct pid *pid, double position, double dt)
{
	pid->i_state += position * dt;                        // I-term
	if (pid->i_state > pid->i_max)
		pid->i_state = pid->i_max;
	else if(pid->i_state < pid->i_min)
		pid->i_state = pid->i_min;
	
	return pid->p_gain * position + pid->i_state * pid->i_gain;                      // P-term	
}


/*float pid_roll_to_aileron(float desired_roll, struct ahrs_info *ahrs)
{
	static float last_error = 0.0;
	float pid = (desired_roll - ahrs->roll) - last_error;
	last_error = (desired_roll - ahrs->roll);
	pid *= PID_DGAIN_ROLL;
	pid += last_error * PID_PGAIN_ROLL;
	return pid;
}


float pid_pitch_to_elevator(float desired_pitch, struct ahrs_info *ahrs)
{
	// 45° pitch -> 45° elevator
	return (desired_pitch - ahrs->pitch) * PID_PGAIN_PITCH;
}


float pid_heading_to_roll(float desired_heading_rad, float current_heading_rad)
{
	float heading_error_rad = desired_heading_rad - current_heading_rad;
	// Choose shortest turn-direction
	if (heading_error_rad >= PI)
		heading_error_rad -= (PI*2.0);
	else if (heading_error_rad <= -PI)
		heading_error_rad += (PI*2.0);
		
	float p = heading_error_rad * PID_PGAIN_HEADING_TO_ROLL;

	if (p > PID_MAX_ROLL_RAD)
		p = PID_MAX_ROLL_RAD;
	else if (p < -PID_MAX_ROLL_RAD)
		p = -PID_MAX_ROLL_RAD;
		
	return p;
}
*/
