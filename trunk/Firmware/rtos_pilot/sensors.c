/*! 
 *  Reads raw sensor data and convert it to usefull data.
 *
 *  This uses the dsPic's ADC module to sample the sensor's analog data. After
 *  processing, usefull pitch, roll, yaw and angular rates are available.
 *  Sensors read:
 *     - 3-axis accelerometer  
 *     - 2-axis gyro + 1-axis gyro
 *     - SCP1000 for pressure and temperature (-> height)
 *     - GPS (separate task)
 *
 *  This is then all stored in global sensor_data variable.
 *
 *  @file     sensors.c
 *  @author   Tom Pycke
 *  @date     24-dec-2009
 *  @since    0.1
 */
 
 #include <math.h>
 
// Include all FreeRTOS header files
#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

// Include all gluon libraries
#include "scp1000/scp1000.h"
#include "adc/adc.h"
#include "gps/gps.h"
#include "button/button.h"
#include "pid/pid.h"
#include "uart1_queue/uart1_queue.h"
#include "led/led.h"
#include "i2c/i2c.h"
#include "bmp085/bmp085.h"

#include "sensors.h"
#include "configuration.h"
#include "ahrs.h"
#include "navigation.h"
#include "common.h"

#define INVERT_X -1.0   // set to -1 if front becomes back


//! Contains all usefull (processed) sensor data
struct SensorData sensor_data;

static const float acc_value_g = 6600.0;

extern xSemaphoreHandle xSpiSemaphore;

void read_raw_sensor_data();
void scale_raw_sensor_data();

float scale_z_gyro = 0.0;

/*!
 *   FreeRTOS task that reads all the sensor data and stored it in the
 *   sensor_data struct.
 *
 *   The current execution rate is 100Hz.
 */
void sensors_task( void *parameters )
{
	unsigned int temperature_10 = 200;
	float last_height = 0.0;
	double dt_since_last_height = 0.0;
	int low_update_counter = 0;

		
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Sensors task initializing...");
#ifdef ENABLE_QUADROCOPTER
	i2c_init();
	vTaskDelay(( portTickType ) 20 / portTICK_RATE_MS );
	hmc5843_init();
#endif
	adc_open();	

	if (HARDWARE_VERSION >= V01O)
	{
		i2c_init();
		bmp085_init();
	}
	else
		scp1000_init();

	read_raw_sensor_data();
	scale_raw_sensor_data();
	ahrs_init();
	
	if (HARDWARE_VERSION == V01N) // IDZ-500 gyroscope
		scale_z_gyro = (-0.02538315*3.14159/180.0)*2.0;
	else // ADXRS-613 gyroscope
		scale_z_gyro = (0.0062286*3.14159/180.0);  //(2^16-1 - (2^5-1)) / 3.3 * 0.0125*(22)/(22+12)
		


	uart1_puts("done\r\n");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
#ifdef ENABLE_QUADROCOPTER
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 4 / portTICK_RATE_MS ) );   // 250Hz
		dt_since_last_height += 0.004;
		low_update_counter += 1;
#else
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 20 / portTICK_RATE_MS ) );   // 50Hz
		dt_since_last_height += 0.02;
		low_update_counter += 5;
#endif
		
		
		read_raw_sensor_data();
				
		adc_start();  // restart ADC sampling to make sure we have our samples on the next loop iteration.

		scale_raw_sensor_data();
		
		if (low_update_counter % 50 == 0) // 5Hz
		{
			sensor_data.battery_voltage_10 = ((float)adc_get_channel(8) * (3.3 * 5.1 / 6550.0));
			if (HARDWARE_VERSION >= V01O)
			{
				if (low_update_counter/50 % 2 == 0)
				{
					long tmp = bmp085_read_temp();
					bmp085_convert_temp(tmp, &sensor_data.temperature_10);
					sensor_data.temperature = (double)sensor_data.temperature_10 / 10.0;
					bmp085_start_convert_pressure();
				} 
				else
				{
					long tmp = bmp085_read_pressure();
					long pressure;
					bmp085_convert_pressure(tmp, &pressure);
					sensor_data.pressure = (double)pressure;
					float height = scp1000_pressure_to_height(sensor_data.pressure, sensor_data.temperature);
					sensor_data.pressure_height = height;
					bmp085_start_convert_temp();
				}	
			}
			else
			{
				if (scp1000_dataready())   // New reading from the pressure sensor -> calculate vertical speed
				{
					// this should be at 9Hz ->0.11s
					if (xSemaphoreTake( xSpiSemaphore, ( portTickType ) 0 ))  // Spi1 is shared with SCP1000 and Dataflash
					{
						sensor_data.pressure = scp1000_get_pressure();
						sensor_data.temperature = scp1000_get_temperature();
						xSemaphoreGive( xSpiSemaphore );
					}	
					temperature_10 = (unsigned int)sensor_data.temperature * 10;
					float height = scp1000_pressure_to_height(sensor_data.pressure, sensor_data.temperature);
					if (height > -30000.0 && height < 30000.0)   // sometimes we get bad readings ~ -31000
						sensor_data.pressure_height = height;
					sensor_data.vertical_speed = sensor_data.vertical_speed * 0.8 + (sensor_data.pressure_height - last_height)/dt_since_last_height * 0.2; // too much noise otherwise
					
					if (fabs(sensor_data.vertical_speed) > MAX(5.0, sensor_data.gps.speed_ms))  // validity check
						sensor_data.vertical_speed = 0.0;
						
					last_height = sensor_data.pressure_height;
					dt_since_last_height = 0.0;
				}
			}	
				
		


			if (control_state.simulation_mode)
				vTaskDelete(xTaskGetCurrentTaskHandle());
		}	
		

		// x = (Pitch; Roll)'
#ifdef ENABLE_QUADROCOPTER
		if (low_update_counter % 5 == 0)
		{
			hmc5843_read(&sensor_data.magnetometer_raw); 
		}

		ahrs_filter(0.005);	
#else
		ahrs_filter(0.02);	
#endif
	}
}

void read_raw_sensor_data()
{
	sensor_data.acc_x_raw = adc_get_channel(6);
	sensor_data.acc_z_raw = adc_get_channel(1);
	sensor_data.acc_y_raw = adc_get_channel(0);

	sensor_data.gyro_x_raw = adc_get_channel(4);
	sensor_data.gyro_y_raw = adc_get_channel(7);
	sensor_data.gyro_z_raw = adc_get_channel(5);  //*0.6 = 3V max	
	
	sensor_data.idg500_vref = adc_get_channel(3);
}	


void scale_raw_sensor_data()
{
	// scale to "g" units. We prefer "g" over SI units (m/s^2) because this allows to cancel out the gravity constant as it is "1"
	sensor_data.acc_x = ((double)(sensor_data.acc_x_raw) - (double)config.sensors.acc_x_neutral) / (-acc_value_g*INVERT_X);
	sensor_data.acc_y = ((double)(sensor_data.acc_y_raw) - (double)config.sensors.acc_y_neutral) / (-acc_value_g*INVERT_X);
	sensor_data.acc_z = ((double)(sensor_data.acc_z_raw) - (double)config.sensors.acc_z_neutral) / (-acc_value_g);
			
	// scale to rad/sec
	sensor_data.p = ((double)(sensor_data.gyro_x_raw)-config.sensors.gyro_x_neutral) * (-0.02518315*3.14159/180.0 * INVERT_X);  // 0.02518315f
	sensor_data.q = ((double)(sensor_data.gyro_y_raw)-config.sensors.gyro_y_neutral) * (-0.02538315*3.14159/180.0 * INVERT_X);
	sensor_data.r = ((double)(sensor_data.gyro_z_raw)-config.sensors.gyro_z_neutral) * scale_z_gyro;
}	



/*!
 *   FreeRTOS task that parses the received GPS sentence and calculates the navigation.
 *
 *   The inner loop contains a semaphore that is only released when a complete and
 *   valid NMEA sentence has been received
 */

//! This semaphore is set in the uart2 interrupt routine when a new GPS message arrives
xSemaphoreHandle xGpsSemaphore = NULL; 


#define LONG_TIME 0xffff
void sensors_gps_task( void *parameters )
{
	int i = 0;
	
	uart1_puts("Gps & Navigation task initializing...\r\n");
	sensor_data.gps.status = EMPTY;	
	navigation_init ();
		
	gps_open_port(&(config.gps));
		
	// Wait for GPS output. On some old EB85 devices, this can take over 2sec
	for (i = 10; i <= 1000; i *= 2)
	{
		if (! gps_valid_frames_receiving())
			vTaskDelay( ( ( portTickType ) i / portTICK_RATE_MS ) );
	}		

	
	gps_config_output();  // configure sentences and switch to 115200 baud

	
	vTaskDelay(( ( portTickType ) 3000 / portTICK_RATE_MS ) );   // 3s
	
	uart1_puts("Gps & Navigation task initialized\r\n");
	if (sensor_data.gps.status == EMPTY)
		led2_off();	
	else if (sensor_data.gps.status == VOID)
		led2_on();
	
	portTickType xLastExecutionTime = xTaskGetTickCount();
	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		if( xSemaphoreTake( xGpsSemaphore, LONG_TIME ) == pdTRUE )
		{
			gps_update_info(&(sensor_data.gps)); // 5Hz (needed?)
			
			// Speed is use for calculating accelerations (in the attitude filter)
			// When the GPS is no longer locked, we don't know the speed -> no reliable attitude
			// Use pre-configured cruising speed as measured speed
			if (sensor_data.gps.satellites_in_view < 4 && 
				navigation_data.airborne)
					sensor_data.gps.speed_ms = config.control.cruising_speed_ms;
				
			i++;
			if (i % 2 == 0)
				navigation_update();
			
			if (i % 6 == 0 || (i+1) % 6 == 0 || (i+2) % 6 == 0)  // this is used for both RMC and GGA, so only update every other tick
			{
				if (sensor_data.gps.status == ACTIVE && sensor_data.gps.satellites_in_view > 5)
					led2_off();
			}	
			else if (sensor_data.gps.status != EMPTY)
				led2_on();

		}
	}
}
