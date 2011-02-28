#ifndef CONTROL_H
#define CONTROL_H

#include "pid/pid.h"


enum MixTypes { AILERON = 0, DELTA_PLUS = 1, DELTA_MIN = 2, QUADROCOPTER  = 3};

enum FlightModes { MANUAL = 0, STABILIZED = 1, AUTOPILOT = 2, LOITER = 3, RETURN = 4};

struct ControlState
{
	enum FlightModes flight_mode;
	double desired_roll;
	double desired_pitch;
	double desired_altitude;
};

extern struct ControlState control_state;

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
	unsigned int use_pwm : 1;

	enum MixTypes servo_mix;	
	double max_roll;
	double max_pitch;
	
	int aileron_differential;  //! Since 0.4
	
	//! Pid structs to control the stabilization
	struct pid pid_roll2aileron;
    struct pid pid_pitch2elevator;
    struct pid pid_heading2roll;
    struct pid pid_altitude2pitch;
    
    
    int waypoint_radius_m;
    int cruising_speed_ms;
    unsigned int stabilization_with_altitude_hold : 1;
};	


void control_init();

void control_copter_task( void *pvParameters );
void control_wing_task( void *pvParameters );


#endif // CONTROL_H
