/*! 
 *  Contains the memory allocation and method implementations for the configuration struct.
 *
 *  This code handles the reading and writing of the configuration struct to the dataflash chip.
 
 *  @file     configuration.c
 *  @author   Tom Pycke
 *  @date     24-dec-2009
 *  @since    0.1
 */
 
 
#include "dataflash/dataflash.h"
#include "gps/gps.h"
#include "sensors.h"
#include "communication.h"
#include "configuration.h"

//! Memory allocation for the configuration data.
struct Configuration config;


/*!
 *  Loads the configuration struct from the 1st dataflash page.
 *  @todo  Add global min and max value for the output.
 */
void configuration_load()
{
	dataflash_read(0, sizeof(struct Configuration), (unsigned char*)&config);
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
	dataflash_write(0, sizeof(struct Configuration), (unsigned char*)&config);
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
	
	for (i = 0; i < 6; i++)
	{
		config.control.servo_max[i] = 2000;
		config.control.servo_min[i] = 1000;
		config.control.servo_neutral[i] = 1500;
	}
	
	config.control.servo_mix = AILERON;
	
	config.gps.initial_baudrate = 57600l;
	
	config.sensors.acc_x_neutral = 32000;
	config.sensors.acc_y_neutral = 32000;
	config.sensors.acc_z_neutral = 32000;
	
	config.sensors.gyro_x_neutral = 27180.0f;
	config.sensors.gyro_y_neutral = 26304.0f;
	config.sensors.gyro_z_neutral = 31850.0f;
	
	config.telemetry.stream_GpsBasic = 30;
	config.telemetry.stream_GyroAccProc = 10;
	config.telemetry.stream_GyroAccRaw = 10;
	config.telemetry.stream_PPM = 10;
	config.telemetry.stream_PressureTemp = 30;
}
