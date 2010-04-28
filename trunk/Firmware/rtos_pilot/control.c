/*! 
 *  Takes care of manual, stabilized and autopilot control!
 *
 *  Input: RC-transmitter, sensor data
 *  Output: Servo positions
 *
 *  Conventions: For RC-transmitter and mixer input:
 *               Right roll > 0 -> input > 1500ms
 *               Up pitch > 0 -> input > 1500ms
 *
 *
 *  @file     control.c
 *  @author   Tom Pycke
 *  @date     24-dec-2009
 *  @since    0.1
 */

#include <math.h>

#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

#include "ppm_in/ppm_in.h"
#include "servo/servo.h"
#include "pid/pid.h"
#include "uart1_queue/uart1_queue.h"

#include "control.h"
#include "configuration.h"


void control_manual();
void control_stabilized(float dt);
void control_mix_aileron_uint(int pitch, int roll, int yaw, int motor);


//! Contains the last calculated servo position
int servo_out[6];

//! Different outputs before mixing. Set by any of the 3 control methods, and used by the servo mixing type.
int pitch_out = 0, roll_out = 0, yaw_out = 0, motor_out = 0;

//! Mix xyz_out to servo_out
void control_mix_out();

//! Contains the currect state of the control loop
struct ControlState control_state;


//! Temporary until we have navigation
static double home_height = 0.0;
	
	          

/*!
 *    Initializes the control module.
 *    It uses the current RC-transmitter's input to determine the servo's neutral settings.
 *    This shall only work when the stick of the transmitter are not touched during this method!
 */
void control_init()
{
	int i;
	
	//ppm_in_open(); MOVED TO MAIN
	
	// Manual trim mode: the servo's neutral settings are defined by the RC-transmitters trim settings. See wiki.
	if (config.control.manual_trim)
	{	
		for (i = 0; i < 3; i++)
			config.control.servo_neutral[i] = 1500;
			
		// The current position of the sticks on the RC-transmitter are 
		// saved as the neutral values
		for (i = 0; i < 8; i++)
			config.control.channel_neutral[i] = ppm.channel[i];
	
		// This procedure calculates the servo's neutral settings using the RC-transmitter's input
		pitch_out = config.control.channel_neutral[config.control.channel_pitch] - 1500;
		roll_out = config.control.channel_neutral[config.control.channel_roll] - 1500;
		yaw_out = config.control.channel_neutral[config.control.channel_yaw] - 1500;
		motor_out = config.control.channel_neutral[config.control.channel_motor] - 1500;
		
		control_mix_out();  // mix the neutral RC-channels so we can get a neutral setting for the servos
		
		// mixed outputs are our servo's neutral values
		for (i = 0; i < 6; i++)
			config.control.servo_neutral[i] = servo_out[i];
	}
}


/*!
 *   
 */
void control_task( void *parameters )
{
	enum FlightModes lastMode = MANUAL;
	
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Control task initializing...");
	
	servo_init();
	control_init();
	
	uart1_puts("done\r\n");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{		
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 10 / portTICK_RATE_MS ) );   //!> 100Hz
		
		if (ppm.channel[config.control.channel_ap] < 1333)
		{
			control_state.flight_mode = AUTOPILOT;
			control_state.desired_height = home_height + 65.0;
			control_stabilized(0.01); // stabilized mode as long as navigation isn't available
		} 
		else if (ppm.channel[config.control.channel_ap] < 1666)
		{
			control_state.flight_mode = STABILIZED;
			if (home_height == 0.0)
				home_height = sensor_data.pressure_height;
			if (lastMode != control_state.flight_mode)  // target altitude = altitude when switching from manual to stabilized
				control_state.desired_height = sensor_data.pressure_height; //home_height + 65.0;
			control_stabilized(0.01); // stabilized mode
		} 
		else
		{
			control_state.flight_mode = MANUAL;
			control_manual(); // manual mode
			
		}
		lastMode = control_state.flight_mode;
	}
}


/*!
 *   Reads the input from the RC-transmitter, mixes it and sends it to the servos.
 */
void control_manual()
{
	// > 0 => Up
	pitch_out = ppm.channel[config.control.channel_pitch] - config.control.channel_neutral[config.control.channel_pitch];
	// > 0 => Right
	roll_out = ppm.channel[config.control.channel_roll] - config.control.channel_neutral[config.control.channel_roll];
	// > 0 => Motor on
	motor_out = ppm.channel[config.control.channel_motor] - config.control.channel_neutral[config.control.channel_motor];
	
	control_mix_out();
}


/*!
 *   The RC-transmitter's sticks define the "desired" attitude. The sensors that define
 *   the module's attitude are used in a PID loop to position the servo's so the
 *   desired attitude can be obtained.
 */
void control_stabilized(float dt)
{
	double elevator_out_radians,
	       aileron_out_radians;
	
	control_state.desired_roll = (double)((int)ppm.channel[config.control.channel_roll]
	                             - config.control.channel_neutral[config.control.channel_roll]) / 500.0 * (config.control.max_roll);
	control_state.desired_pitch = (double)((int)ppm.channel[config.control.channel_pitch]
	                              - config.control.channel_neutral[config.control.channel_pitch]) / 500.0 * (config.control.max_pitch);

	// Comment this line if you want pitch stabilization instead of altitude hold
	control_state.desired_pitch = (control_state.desired_height - sensor_data.pressure_height)  / 20.0 * config.control.max_pitch; 

	// Keep pitch & roll within limits
	if (control_state.desired_pitch > config.control.max_pitch)
		control_state.desired_pitch = config.control.max_pitch;
	else if (control_state.desired_pitch < -config.control.max_pitch)
		control_state.desired_pitch = -config.control.max_pitch;
	if (control_state.desired_roll > config.control.max_roll)
		control_state.desired_roll = config.control.max_roll;
	else if (control_state.desired_roll < -config.control.max_roll)
		control_state.desired_roll = -config.control.max_roll;

	// compensate the loss in lift
	control_state.desired_pitch += (1.0/cosf(sensor_data.roll) - 1.0)*0.20; // (0.5: 12° up at 45° roll)
	
	elevator_out_radians = pid_update_only_p_and_i(&config.control.pid_pitch2elevator, 
	                                         control_state.desired_pitch - sensor_data.pitch, dt); //pid_pitch_to_elevator(desired_pitch, ahrs);
	aileron_out_radians = pid_update_only_p_and_i(&config.control.pid_roll2aileron, 
	                                        control_state.desired_roll - sensor_data.roll, dt);  //pid_roll_to_aileron(desired_roll, ahrs);
	
	// Experimental: when flying with the wind, the elevons become less effective. Avoid have a too large roll angle!
	// Fixme: gain scheduling depending on V_air
	if (sensor_data.roll > config.control.max_roll)
		aileron_out_radians *= 2.0;
	if (sensor_data.roll < -config.control.max_roll)
		aileron_out_radians *= 2.0;
	if (sensor_data.pitch > config.control.max_pitch)
		elevator_out_radians *= 2.0;


	motor_out = ppm.channel[config.control.channel_motor] - config.control.channel_neutral[config.control.channel_motor];

	pitch_out = (int)(elevator_out_radians * 630.0); // +-45° -> +- 500
	roll_out = (int)(aileron_out_radians * 630.0);
	//printf ("%f %f %d\n\r", desired_pitch, desired_pitch - ahrs->pitch, elevator_out);

	control_mix_out();
}


/*!
 *   Mixes variables xyz_out into correct servo positions, according to the configured mixing type.
 * 
 *   Input: 
 *      roll_out: > 0 means right
 *      pitch_out: > 0 means up
 *      motor_out: > 0 means on/more gas
 *
 */
void control_mix_out()
{
	int i;
	
	switch(config.control.servo_mix)
	{
		case DELTA_PLUS:
			if (config.control.reverse_servo1)
				servo_out[0] = +roll_out + pitch_out + config.control.servo_neutral[0];
			else
				servo_out[0] = -roll_out - pitch_out + config.control.servo_neutral[0];
				
			if (config.control.reverse_servo2)
				servo_out[1] = +roll_out - pitch_out + config.control.servo_neutral[1];
			else
				servo_out[1] = -roll_out + pitch_out + config.control.servo_neutral[1];
				
			if (config.control.reverse_servo4)
				servo_out[3] = -motor_out + config.control.servo_neutral[3];
			else 
				servo_out[3] = motor_out + config.control.servo_neutral[3];

			break;
		case DELTA_MIN:
			if (config.control.reverse_servo1)
				servo_out[0] = +roll_out - pitch_out + config.control.servo_neutral[0];
			else
				servo_out[0] = -roll_out + pitch_out + config.control.servo_neutral[0];
				
			if (config.control.reverse_servo2)
				servo_out[1] = +roll_out + pitch_out + config.control.servo_neutral[1];
			else
				servo_out[1] = -roll_out - pitch_out + config.control.servo_neutral[1];
				
			if (config.control.reverse_servo4)
				servo_out[3] = -motor_out + config.control.servo_neutral[3];
			else 
				servo_out[3] = motor_out + config.control.servo_neutral[3];

			break;
		default:  // aileron
			if (config.control.reverse_servo1)
				servo_out[0] = -roll_out + config.control.servo_neutral[0];
			else
				servo_out[0] = roll_out + config.control.servo_neutral[0];
			if (config.control.reverse_servo2)
				servo_out[1] = roll_out + config.control.servo_neutral[1];
			else
				servo_out[1] = -roll_out + config.control.servo_neutral[1];
			if (config.control.reverse_servo3)
				servo_out[2] = -pitch_out + config.control.servo_neutral[2];
			else
				servo_out[2] = pitch_out + config.control.servo_neutral[2];
				
			if (config.control.reverse_servo4)
				servo_out[3] = -motor_out + config.control.servo_neutral[3];
			else
				servo_out[3] = motor_out + config.control.servo_neutral[3];
			break;
	}
	
	for(i = 0; i < 6; i++)
	{	
		if (servo_out[i] > config.control.servo_max[i])
			servo_out[i] = config.control.servo_max[i];
		if (servo_out[i] < config.control.servo_min[i])
			servo_out[i] = config.control.servo_min[i];

		servo_set_us(i, servo_out[i]);
	}
}
