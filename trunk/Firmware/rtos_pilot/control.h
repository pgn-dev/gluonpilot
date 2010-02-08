#ifndef CONTROL_H
#define CONTROL_H

#include "pid.h"


enum MixTypes { AILERON, DELTA_PLUS, DELTA_MIN, QUADROCOPTER };

/*!
 *   This struct assigns channel input (from the rc transmitter - see ppm_in or pwm_in) to a function.
 */
struct ControlConfig
{
	int channel_motor;
	int channel_ap;
	int channel_pitch;
	int channel_roll;
	int channel_yaw;
	
	int channel_neutral[8];
	int servo_neutral[6];
	int servo_max[6];
	int servo_min[6];
	
	unsigned int reverse_servo1 : 1;
	unsigned int reverse_servo2 : 1;
	unsigned int reverse_servo3 : 1;
	unsigned int reverse_servo4 : 1;
	unsigned int reverse_servo5 : 1;
	unsigned int reverse_servo6 : 1;
	
	unsigned int manual_trim : 1;

	enum MixTypes servo_mix;	
	float max_roll;
	float max_pitch;
	
	//! Pid structs to control the stabilization
	struct pid pid_roll2aileron;
    struct pid pid_pitch2elevator;
    struct pid pid_heading2roll;
};	


void control_init();

void control_task( void *pvParameters );


#endif // CONTROL_H
