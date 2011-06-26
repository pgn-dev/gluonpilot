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
#include "sensors.h"
#include "navigation.h"

#include "common.h"

void control_wing_manual();
void control_wing_stabilized(double dt, int h);
void control_wing_navigate(double dt, int altitude_controllable);
void control_wing_desired_to_servos();


//! Contains the last calculated servo position
int servo_out[6];

//! Different outputs before mixing. Set by any of the 3 control methods, and used by the servo mixing type.
int elevator_out = 0, aileron_out = 0, yaw_out = 0, motor_out = 0;

//! Mix xyz_out to servo_out
void control_mix_out();

//! Contains the currect state of the control loop
struct ControlState control_state;
      
	          

/*!
 *    Initializes the control module.
 *    It uses the current RC-transmitter's input to determine the servo's neutral settings.
 *    This shall only work when the stick of the transmitter are not touched during this method!
 */
void control_init()
{
	int i;
	
	// Manual trim mode: the servo's neutral settings are defined by the RC-transmitters trim settings. See wiki.
	if (1/*config.control.manual_trim*/)
	{	
		for (i = 0; i < 6; i++)
		{
			config.control.servo_neutral[i] = 1500;
			config.control.servo_max[i] = 2000;
			config.control.servo_min[i] = 1000;
		}	
		
		// The current position of the sticks on the RC-transmitter are 
		// saved as the neutral values
		for (i = 0; i < 8; i++)
			if (ppm.channel[i] > 800 && ppm.channel[i] < 2200)  // only take valid values
				config.control.channel_neutral[i] = ppm.channel[i];
		
		// This procedure calculates the servo's neutral settings using the RC-transmitter's input
		elevator_out = config.control.channel_neutral[config.control.channel_pitch] - 1500;
		aileron_out = config.control.channel_neutral[config.control.channel_roll] - 1500;
		yaw_out = config.control.channel_neutral[config.control.channel_yaw] - 1500;
		motor_out = config.control.channel_neutral[config.control.channel_motor] - 1500;
		
		control_mix_out();  // mix the neutral RC-channels so we can get a neutral setting for the servos
		
		// mixed outputs are our servo's neutral values
		for (i = 0; i < 6; i++)
			config.control.servo_neutral[i] = servo_out[i];
	}
	
	if (config.control.cruising_speed_ms < 0.5)  // not valid? change to 18 to avoid /0
		config.control.cruising_speed_ms = 18.0;  
}



/******************************************************************************
 *                                                                            *
 *                       Part for fixed-wing aircraft                         *
 *                                                                            *
 ******************************************************************************/


/*!
 *   FreeRTOS task for fixed wing aircraft (not QUAD mixing)
 */
void control_wing_task( void *parameters )
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
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 20 / portTICK_RATE_MS ) );   //!> 50Hz
		
		// Update RC link status
		if (config.control.use_pwm)
		{
			if (ppm.channel[config.control.channel_motor] < 900)  // We assume failsafe kicked in when motor channel < 900ms
			{
				//ppm.valid_frame = 0;
				ppm.connection_alive = 0;
			}
			else
				ppm.connection_alive = 1;
		} else
			ppm_in_update_status_ticks_50hz();

		if (!ppm.connection_alive || ppm.channel[config.control.channel_ap] < 1300)
		{
			control_state.flight_mode = AUTOPILOT;
			
			//control_state.desired_height = home_height + 65.0;
			if (lastMode != control_state.flight_mode)  // target altitude = altitude when switching from manual to stabilized
				control_state.desired_altitude = sensor_data.pressure_height;
				
			control_wing_navigate(0.020, config.control.stabilization_with_altitude_hold); // stabilized mode as long as navigation isn't available
		} 
		else if (ppm.channel[config.control.channel_ap] < 1666)
		{
			control_state.flight_mode = STABILIZED;
			if (lastMode != control_state.flight_mode)  // target altitude = altitude when switching from manual to stabilized
				control_state.desired_altitude = sensor_data.pressure_height; //home_height + 65.0;
			control_wing_stabilized(0.020, config.control.stabilization_with_altitude_hold); // stabilized mode
		} 
		else
		{
			control_state.flight_mode = MANUAL;
			control_wing_manual(); // manual mode
			
		}
		lastMode = control_state.flight_mode;
	}
}


/*!
 *   Reads the input from the RC-transmitter, mixes it and sends it to the servos.
 */
void control_wing_manual()
{
	// > 0 => Up
	elevator_out = ppm.channel[config.control.channel_pitch] - config.control.channel_neutral[config.control.channel_pitch];
	// > 0 => Right
	aileron_out = ppm.channel[config.control.channel_roll] - config.control.channel_neutral[config.control.channel_roll];
	// > 0 => Motor on
	motor_out = ppm.channel[config.control.channel_motor] - config.control.channel_neutral[config.control.channel_motor];
	// > 0 => Right
	yaw_out = ppm.channel[config.control.channel_yaw] - config.control.channel_neutral[config.control.channel_yaw];
	
	control_mix_out();
}


/*!
 *   The RC-transmitter's sticks define the "desired" attitude. The sensors that define
 *   the module's attitude are used in a PID loop to position the servo's so the
 *   desired attitude can be obtained.
 */
void control_wing_stabilized(double dt, int altitude_hold)
{
	control_state.desired_roll = (double)((int)ppm.channel[config.control.channel_roll]
	                             - config.control.channel_neutral[config.control.channel_roll]) / 500.0 * (config.control.max_roll);

	control_state.desired_pitch = (double)((int)ppm.channel[config.control.channel_pitch]
	                              - config.control.channel_neutral[config.control.channel_pitch]) / 500.0 * (config.control.max_pitch);


	// Comment this line if you want pitch stabilization instead of altitude hold
	if (altitude_hold)
	{
		if (fabs(control_state.desired_pitch) > (config.control.max_pitch / 5.0)) // elevator stick not in neutral position
		{
			// Keep RC-input desired pitch
			control_state.desired_altitude = sensor_data.pressure_height;  // keep height in case stick goes back to neutral
		}	
		else  // altitude hold
		  control_state.desired_pitch = (control_state.desired_altitude - sensor_data.pressure_height)  / 20.0 * config.control.max_pitch; 
	} 
       
 motor_out = ppm.channel[config.control.channel_motor] - config.control.channel_neutral[config.control.channel_motor];
	control_wing_desired_to_servos(dt);
}



/*!
 *   The RC-transmitter's sticks define the "desired" attitude. The sensors that define
 *   the module's attitude are used in a PID loop to position the servo's so the
 *   desired attitude can be obtained.
 */
void control_wing_navigate(double dt, int altitude_controllable)
{
	/* Calculate desired roll */
	double heading_error_rad = navigation_data.desired_heading_rad - sensor_data.gps.heading_rad;
	
	// Choose shortest turn-direction
	if (heading_error_rad >= PI)
		heading_error_rad -= (PI*2.0);
	else if (heading_error_rad <= -PI)
		heading_error_rad += (PI*2.0);
		
	
	control_state.desired_roll = navigation_data.desired_pre_bank +
	                             pid_update(&config.control.pid_heading2roll, heading_error_rad, dt);	
	
	// Not enough GPS satellites? Fly flat and hope to get a new lock :-)
	if (sensor_data.gps.satellites_in_view < 4)
		control_state.desired_roll = 0.0;
		
	// from paparazzi
	double speed_depend_nav = sensor_data.gps.speed_ms / config.control.cruising_speed_ms;
 	if (speed_depend_nav > 1.3)	{
 		control_state.desired_roll *= 1.3;
 	} else if (speed_depend_nav < 0.8) { // good idea for takeoff?
 		control_state.desired_roll *= 0.8;
 	} else
		control_state.desired_roll *= speed_depend_nav;

	
	
	/* Calculate desired pitch */
  	// altitude hold
  	
  	control_state.desired_altitude = navigation_data.desired_height_above_ground_m + navigation_data.home_pressure_height;
  	
	//control_state.desired_pitch = (control_state.desired_height - sensor_data.pressure_height) / 10.0 * config.control.max_pitch; 
	control_state.desired_pitch = pid_update(&config.control.pid_altitude2pitch, 
	                                         control_state.desired_altitude - sensor_data.pressure_height, dt);
	
	if (altitude_controllable)  // control altitude with pitch transmitter stick?
	{
		double manual_desired_pitch = (double)((int)ppm.channel[config.control.channel_pitch]
		                              - config.control.channel_neutral[config.control.channel_pitch]) / 500.0 * (config.control.max_pitch);
	    if (fabs(manual_desired_pitch) > (config.control.max_pitch / 5.0)) // elevator stick not in neutral position
	    {
			control_state.desired_pitch = manual_desired_pitch;
			control_state.desired_altitude = sensor_data.pressure_height;  // save current height in case stick goes back to neutral
		}
	}

        // auto-throttle
        if (config.control.autopilot_auto_throttle)
        {
            int d_altitude = control_state.desired_altitude - sensor_data.pressure_height;
            int target = config.control.auto_throttle_cruise_pct +
                        (d_altitude * config.control.auto_throttle_p_gain) / 10;
            if (target > config.control.auto_throttle_max_pct)
                target = config.control.auto_throttle_max_pct;
            else if (target < config.control.auto_throttle_min_pct)
                target = config.control.auto_throttle_min_pct;

            motor_out = 1000 + target*10;
        } else
            motor_out = ppm.channel[config.control.channel_motor] - config.control.channel_neutral[config.control.channel_motor];

	control_wing_desired_to_servos(dt);
}


/*!
 *   Takes control_state.desired_roll and control_state.desired_pitch as input,
 *   and calculates elevator_out and aileron_out.
 */
void control_wing_desired_to_servos(double dt)
{
	double elevator_out_radians,
	       aileron_out_radians;     
	static int counter = 0;
	
	// Keep pitch & roll within limits
	control_state.desired_pitch = MIN(control_state.desired_pitch, config.control.max_pitch);
	control_state.desired_pitch = MAX(control_state.desired_pitch, -config.control.max_pitch);
	control_state.desired_roll = MIN(control_state.desired_roll, config.control.max_roll);
	control_state.desired_roll = MAX(control_state.desired_roll, -config.control.max_roll);

	// compensate the loss in lift
	//control_state.desired_pitch += (1.0/cosf(sensor_data.roll) - 1.0)*0.25; // (0.5: 12� up at 45� roll)
	
	elevator_out_radians = pid_update(&config.control.pid_pitch2elevator, 
	                                         control_state.desired_pitch - sensor_data.pitch, dt);
	aileron_out_radians = pid_update(&config.control.pid_roll2aileron, 
	                                        control_state.desired_roll - sensor_data.roll, dt);
	yaw_out = ppm.channel[config.control.channel_yaw] - config.control.channel_neutral[config.control.channel_yaw];

	
	// Experimental: when flying with the wind, the elevons become less effective. Avoid have a too large roll angle!
	// Fixme: gain scheduling depending on V_air
	/*if (sensor_data.roll > config.control.max_roll)
		aileron_out_radians *= 1.2;
	if (sensor_data.roll < -config.control.max_roll)
		aileron_out_radians *= 1.2;
	if (sensor_data.pitch > config.control.max_pitch)
		elevator_out_radians *= 1.2;*/
	

	//motor_out = ppm.channel[config.control.channel_motor] - config.control.channel_neutral[config.control.channel_motor];
	
	elevator_out = (int)(elevator_out_radians * 630.0); // +-45� -> +- 500
	aileron_out = (int)(aileron_out_radians * 630.0);

	control_mix_out();
}	


/******************************************************************************
 *                                                                            *
 *                     Part for multicopter aircraft                          *
 *                                                                            *
 ******************************************************************************/

/*!
 *   FreeRTOS task for multicopter aircraft (e.g. QUAD mixing)
 */
void control_copter_task( void *parameters )
{
	enum FlightModes lastMode = MANUAL;
	
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Control task initializing...");
	
	servo_init();
	control_init();

	vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 1000 / portTICK_RATE_MS ) );
	servo_turbopwm();
	int i = 0;

	uart1_puts("done\r\n");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 4 / portTICK_RATE_MS ) );    //!> 250Hz
	
		if (i++ == 5)
			ppm_in_update_status_ticks_50hz();

		if (!ppm.connection_alive || ppm.channel[config.control.channel_ap] < 1300)
		{
			control_state.flight_mode = AUTOPILOT;
			
			//control_state.desired_height = home_height + 65.0;
			if (lastMode != control_state.flight_mode)  // target altitude = altitude when switching from manual to stabilized
				control_state.desired_altitude = sensor_data.pressure_height;
				
			//control_navigate(0.004, config.control.stabilization_with_altitude_hold); // stabilized mode as long as navigation isn't available
		} 
		else if (ppm.channel[config.control.channel_ap] < 1666)
		{
			control_state.flight_mode = STABILIZED;
			control_copter_stabilized(0.004, 0); // stabilized mode
		} 
		else
		{
			control_state.flight_mode = MANUAL;
			control_wing_manual(); // manual mode
			
		}
		lastMode = control_state.flight_mode;
	}
}


/*!
 *   The RC-transmitter's sticks define the "desired" attitude. The sensors that define
 *   the module's attitude are used in a PID loop to position the servo's so the
 *   desired attitude can be obtained.
 */
void control_copter_stabilized(double dt, int altitude_hold)
{
	control_state.desired_roll = (double)((int)ppm.channel[config.control.channel_roll]
	                             - config.control.channel_neutral[config.control.channel_roll]) / 500.0 * (config.control.max_roll);

	control_state.desired_pitch = (double)((int)ppm.channel[config.control.channel_pitch]
	                              - config.control.channel_neutral[config.control.channel_pitch]) / 500.0 * (config.control.max_pitch);



	// Comment this line if you want pitch stabilization instead of altitude hold
	if (altitude_hold)
	{
		if (fabs(control_state.desired_pitch) > (config.control.max_pitch / 5.0)) // elevator stick not in neutral position
		{
			// Keep RC-input desired pitch
			control_state.desired_altitude = sensor_data.pressure_height;  // keep height in case stick goes back to neutral
		}	
		else  // altitude hold
		  control_state.desired_pitch = (control_state.desired_altitude - sensor_data.pressure_height)  / 20.0 * config.control.max_pitch; 
	} 

	control_wing_desired_to_servos(dt);
}


/*!
 *   Takes control_state.desired_roll and control_state.desired_pitch as input,
 *   and calculates elevator_out and aileron_out.
 */
void control_copter_desired_to_servos(double dt)
{
	double elevator_out_radians,
	       aileron_out_radians;     
	static int counter = 0;
	static double desired_yaw = 0.0;
	
	motor_out = ppm.channel[config.control.channel_motor] - config.control.channel_neutral[config.control.channel_motor];	
	if (motor_out < 100)  // when the quad is on the ground, keep this yaw angle when taking off
		desired_yaw = sensor_data.yaw;
	
	// Keep pitch & roll within limits
	control_state.desired_pitch = MIN(control_state.desired_pitch, config.control.max_pitch);
	control_state.desired_pitch = MAX(control_state.desired_pitch, -config.control.max_pitch);
	control_state.desired_roll = MIN(control_state.desired_roll, config.control.max_roll);
	control_state.desired_roll = MAX(control_state.desired_roll, -config.control.max_roll);

	elevator_out_radians = pid_update(&config.control.pid_pitch2elevator, 
	                                   control_state.desired_pitch - sensor_data.pitch, dt);
	
	aileron_out_radians = pid_update(&config.control.pid_roll2aileron, 
	                                  control_state.desired_roll - sensor_data.roll, dt);
	
	double desired_yaw_rate  = (double)((int)ppm.channel[config.control.channel_yaw]
		                            - config.control.channel_neutral[config.control.channel_yaw]) / 500.0 * (DEG2RAD(30.0)); // max 30�/s

	if (fabs(desired_yaw_rate) < DEG2RAD(5.0)) // stick in the middle
		desired_yaw_rate = 0.0;		
	desired_yaw += desired_yaw_rate*dt;
	if (desired_yaw > DEG2RAD(360.0))
		desired_yaw	 -= DEG2RAD(360.0);
	if (desired_yaw < -DEG2RAD(360.0))
		desired_yaw	 += DEG2RAD(360.0);
	
	yaw_out = pid_update(&config.control.pid_heading2roll, desired_yaw - sensor_data.yaw, dt);

	yaw_out = MAX(yaw_out, -800);
	yaw_out = MIN(yaw_out, 800);	

	elevator_out = (int)(elevator_out_radians * 630.0); // +-45� -> +- 500
	aileron_out = (int)(aileron_out_radians * 630.0);

	control_mix_out();
}	







/*!
 *   Mixes variables aileron_out and elevator_out into correct servo positions, according to the configured mixing type.
 * 
 *   Input: 
 *      aileron_out: > 0 means right [-500..500]
 *      elevator_out: > 0 means up   [-500..500]
 *      motor_out: > 0 means on/more gas [0..1000]
 *
 */
void control_mix_out()
{
	static int j = 0;
	int i;
	int aileron_out_left, aileron_out_right;
	int number_of_controlled_channels = 4;
	
	// aileron differential
	/*if (aileron_out > 0)
	{
		aileron_out_right = aileron_out + (aileron_out / 10) * config.control.aileron_differential;
		aileron_out_left = aileron_out - (aileron_out / 10) * config.control.aileron_differential;
	} 
	else
	{
		aileron_out_right = aileron_out - (aileron_out / 10) * config.control.aileron_differential;
		aileron_out_left = aileron_out + (aileron_out / 10) * config.control.aileron_differential;		
	}
	*/
	// no differential
	aileron_out_right = aileron_out;
	aileron_out_left = aileron_out;		
		
	
	switch(config.control.servo_mix)
	{
		case DELTA_PLUS:
			number_of_controlled_channels = 4;
			if (config.control.reverse_servo1)
				servo_out[0] = +aileron_out_right + elevator_out + config.control.servo_neutral[0];
			else
				servo_out[0] = -aileron_out_right - elevator_out + config.control.servo_neutral[0];
				
			if (config.control.reverse_servo2)
				servo_out[1] = +aileron_out_left - elevator_out + config.control.servo_neutral[1];
			else
				servo_out[1] = -aileron_out_left + elevator_out + config.control.servo_neutral[1];
				
			if (config.control.reverse_servo4)
				servo_out[3] = -motor_out + config.control.servo_neutral[3];
			else 
				servo_out[3] = motor_out + config.control.servo_neutral[3];

			break;
		case DELTA_MIN:
			number_of_controlled_channels = 4;
			if (config.control.reverse_servo1)
				servo_out[0] = +aileron_out_right - elevator_out + config.control.servo_neutral[0];
			else
				servo_out[0] = -aileron_out_right + elevator_out + config.control.servo_neutral[0];
				
			if (config.control.reverse_servo2)
				servo_out[1] = +aileron_out_left + elevator_out + config.control.servo_neutral[1];
			else
				servo_out[1] = -aileron_out_left - elevator_out + config.control.servo_neutral[1];
			
			if (config.control.reverse_servo4)
				servo_out[3] = -motor_out + config.control.servo_neutral[3];
			else 
				servo_out[3] = motor_out + config.control.servo_neutral[3];

			break;
		case QUADROCOPTER:
		{
			number_of_controlled_channels = 4;
			/*          >
			 *          0
			 *      < /   \ <
			 *      3       1        FRONT
			 *        \ > /
			 *          2
			 *    
			 */
			
			/*if (motor_out < 100) // about zero throttle
			{
				servo_out[0] = config.control.servo_neutral[3];
				servo_out[1] = config.control.servo_neutral[3];
				servo_out[2] = config.control.servo_neutral[3];
				servo_out[3] = config.control.servo_neutral[3];
			} 
			else*/
			{	
				// channel[motor] neutral = 1200
				// servo_neutral(init) = 1500
				// motor_out(init) = 1200 - 1500 = -300 -> in mix -> 
				// servo_neutral(mix) = 1200
				
				// P:0.4 (convergeert traag), D: 0.45 (0.6 oscilleert)

				servo_out[0] = motor_out + aileron_out/5 + yaw_out/5 + config.control.servo_neutral[0];
				servo_out[1] = motor_out + elevator_out/5 - yaw_out/5 + config.control.servo_neutral[1];
				servo_out[2] = motor_out - aileron_out/5  + yaw_out/5 + config.control.servo_neutral[2];
				servo_out[3] = motor_out - elevator_out/5 - yaw_out/5 + config.control.servo_neutral[3];
				//safety:
				if (motor_out < 100)
				{
					servo_out[0] = 0;
					servo_out[1] = 0;
					servo_out[2] = 0;
					servo_out[3] = 0;
				}	
			}
			break;
		}	
		default:  // aileron
			number_of_controlled_channels = 6;
                        if (config.control.reverse_servo6)
				servo_out[5] = -(int)(sensor_data.roll*636.0) + 1500;//config.control.servo_neutral[6];
			else
				servo_out[5] = +-(int)(sensor_data.roll*636.0) + 1500;//config.control.servo_neutral[6];

			if (config.control.reverse_servo1)
				servo_out[0] = -aileron_out_right + config.control.servo_neutral[0];
			else
				servo_out[0] = +aileron_out_right + config.control.servo_neutral[0];
				
			if (config.control.reverse_servo2)
				servo_out[1] = +aileron_out_left + config.control.servo_neutral[1];
			else
				servo_out[1] = -aileron_out_left + config.control.servo_neutral[1];
				
			if (config.control.reverse_servo3)
				servo_out[2] = -elevator_out + config.control.servo_neutral[2];
			else
				servo_out[2] = elevator_out + config.control.servo_neutral[2];
				
			if (config.control.reverse_servo4)
				servo_out[3] = -motor_out + config.control.servo_neutral[3];
			else
				servo_out[3] = motor_out + config.control.servo_neutral[3];
			
			if (config.control.reverse_servo5)
				servo_out[4] = +yaw_out + config.control.servo_neutral[4];
			else 
				servo_out[4] = -yaw_out + config.control.servo_neutral[4];

			break;
	}
	
	
	
	for(i = 0; i < number_of_controlled_channels; i++)
	{	
		if (servo_out[i] > config.control.servo_max[i])
			servo_out[i] = config.control.servo_max[i];
		if (servo_out[i] < config.control.servo_min[i])
			servo_out[i] = config.control.servo_min[i];

		servo_set_us(i, servo_out[i]);
	}
}
