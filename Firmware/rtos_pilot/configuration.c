/*! 
 *  Contains the memory allocation and method implementations for the configuration struct.
 *
 *  This code handles the reading and writing of the configuration struct to the dataflash chip.
 
 *  @file     configuration.c
 *  @author   Tom Pycke
 *  @date     24-dec-2009
 *  @since    0.1
 */
 

#include "microcontroller/microcontroller.h"
#include "dataflash/dataflash.h"
#include "gps/gps.h"
#include "sensors.h"
#include "communication.h"
#include "configuration.h"

//! Memory allocation for the configuration data.
struct Configuration config;

//! Contains the hardware version (defined in configuration.h)
int HARDWARE_VERSION;


/*!
 *  Loads the configuration struct from the 1st dataflash page.
 *  @todo  Add global min and max value for the output.
 */
void configuration_load()
{
	dataflash_read(CONFIGURATION_PAGE, sizeof(struct Configuration), (unsigned char*)&config);
}


void configuration_determine_hardware_version()
{
	// output
	TRISGbits.TRISG14 = 0;
	// input
	TRISGbits.TRISG12 = 1;
	
	// are they connected? --> v0.1n or newer
	PORTGbits.RG14 = 1;
	microcontroller_delay_us(10);
	if (PORTGbits.RG12 == 1)
	{
		PORTGbits.RG14 = 0;
		microcontroller_delay_us(10);
		if (PORTGbits.RG12 == 0)
		{
			//printf("RG12 and RG14 connected!\r\n");
			HARDWARE_VERSION = V01N;
		}	
		else
			HARDWARE_VERSION = V01J;
	} 
	else
	{
		HARDWARE_VERSION = V01J;
	}	
}	


/*!
 *  Writes the configuration struct to the 1st dataflash page.
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
void configuration_write()
{
	dataflash_write(CONFIGURATION_PAGE, sizeof(struct Configuration), (unsigned char*)&config);
}


/*!
 *  Called on user request. Usefull when an upgrade causes the "struct config" to change.
 */
void configuration_default()
{
	int i;
	config.control.channel_ap = 4;
	config.control.channel_motor = 3;
	config.control.channel_pitch = 0;
	config.control.channel_roll = 1;
	config.control.channel_yaw = 2;
	
	for (i = 0; i < 8; i++)
		config.control.channel_neutral[i] = 1500;
		
	config.control.manual_trim = 1;
	config.control.reverse_servo1 = 0;
	config.control.reverse_servo2 = 0;
	config.control.reverse_servo3 = 0;
	config.control.reverse_servo4 = 0;
	config.control.reverse_servo5 = 0;
	config.control.reverse_servo6 = 0;
	
	config.control.manual_trim = 0;
	config.control.use_pwm = 0;
	
	for (i = 0; i < 6; i++)
	{
		config.control.servo_max[i] = 2000;
		config.control.servo_min[i] = 1000;
		config.control.servo_neutral[i] = 1500;
	}
	
	config.control.servo_mix = AILERON;
	config.control.aileron_differential = 0;
	config.control.cruising_speed_ms = 15;
	config.control.max_pitch = 15.0/180.0*3.14;
	config.control.max_roll = 30.0/180.0*3.14;
	config.control.servo_mix = AILERON;
	
	config.control.stabilization_with_altitude_hold = 0;
        config.control.auto_throttle_cruise_pct = 90;
        config.control.auto_throttle_min_pct = 30;
        config.control.auto_throttle_max_pct = 100;
        config.control.auto_throttle_p_gain = 8;  // 0.8
        

	pid_init(&config.control.pid_heading2roll, 0.0, 0.7, 0.0, -1.0, 1.0, 0.0);
	pid_init(&config.control.pid_pitch2elevator , 0.0, 0.7, 0.0, -1.0, 1.0, 0.0);
	pid_init(&config.control.pid_roll2aileron, 0.0, 0.5, 0.0, -1.0, 1.0, 0.0);
	pid_init(&config.control.pid_altitude2pitch, 0.0, 0.03, 0.0, -1.0, 1.0, 0.0);
	
	config.control.waypoint_radius_m = 30;
	
        config.control.autopilot_auto_throttle = 90;

	config.gps.operational_baudrate = 115200l;
	config.gps.initial_baudrate = 38400l;
	
	
	config.sensors.acc_x_neutral = 32000;
	config.sensors.acc_y_neutral = 32000;
	config.sensors.acc_z_neutral = 32000;
	
	config.sensors.gyro_x_neutral = 27180.0f;
	config.sensors.gyro_y_neutral = 26304.0f;
	config.sensors.gyro_z_neutral = 31850.0f;
	
	
	config.telemetry.stream_GpsBasic = 5;
	config.telemetry.stream_GyroAccProc = 20;
	config.telemetry.stream_GyroAccRaw = 30;
	config.telemetry.stream_PPM = 35;
	config.telemetry.stream_PressureTemp = 30;
	config.telemetry.stream_Attitude = 5;
	config.telemetry.stream_Control = 20;
}
