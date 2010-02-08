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

#include "pid.h"
#include "sensors.h"
#include "configuration.h"
#include "quaternion.h"
#include "ahrs.h"

#include <math.h>


//! Contains all usefull (processed) sensor data
struct SensorData sensor_data;

// Usefull constants
#define G 9.81f
#define DT 0.01f   // 100Hz
#define RAD2DEG (180.f/3.14159f)

static const float acc_value_g = 6600.0f;

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
	
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Sensors task initializing...");

	adc_open();	
	scp1000_init();
	ahrs_init();
	
	uart1_puts("done\n\r");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
	
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 10 / portTICK_RATE_MS ) );   // 100Hz

		// The SPC1000 pressure sensor is updated at 9Hz
		if (scp1000_dataready())
		{
			// this should be at 1.8Hz ->0.555s
			sensor_data.pressure = scp1000_get_pressure();
			sensor_data.temperature = scp1000_get_temperature();
			temperature_10 = (unsigned int)sensor_data.temperature * 10;
			sensor_data.pressure_height = scp1000_pressure_to_height(sensor_data.pressure, sensor_data.temperature);
			sensor_data.w = sensor_data.w * 0.4 + (sensor_data.pressure_height - last_height)/0.555 * 0.6;
			last_height = sensor_data.pressure_height;
		}
		
		
		// read accelerometer data and apply temperature compensation
		// (cfr. http://www.gluonpilot.com/wiki/Temperature_compensation)
		sensor_data.acc_x_raw = adc_get_channel(6)- temperature_10;
		sensor_data.acc_z_raw = adc_get_channel(1)- temperature_10;
		sensor_data.acc_y_raw = adc_get_channel(0)- temperature_10;

		sensor_data.gyro_x_raw = adc_get_channel(4);
		sensor_data.gyro_y_raw = adc_get_channel(7);
		sensor_data.gyro_z_raw = adc_get_channel(5);  //*0.6 = 3V max
				
		adc_start();

		// scale to "g" units. We prefer "g" over SI units (m/s^2) because this allows to cancel out the gravity constant as it is "1"
		sensor_data.acc_x = ((float)(sensor_data.acc_x_raw) - (float)config.sensors.acc_x_neutral) / (-acc_value_g);
		sensor_data.acc_z = ((float)(sensor_data.acc_z_raw) - (float)config.sensors.acc_z_neutral) / (-acc_value_g);
		sensor_data.acc_y = ((float)(sensor_data.acc_y_raw) - (float)config.sensors.acc_y_neutral) / (-acc_value_g);
		
		// scale to rad/sec
		sensor_data.p = ((float)(sensor_data.gyro_x_raw)-config.sensors.gyro_x_neutral) * (-0.02518315f*3.14159f/180.0f);  // 0.02518315f
		sensor_data.q = ((float)(sensor_data.gyro_y_raw)-config.sensors.gyro_y_neutral) * (-0.02538315f*3.14159f/180.0f);
		sensor_data.r = ((float)(sensor_data.gyro_z_raw)-config.sensors.gyro_z_neutral) * (0.0062286f*3.14159f/180.0f);  //(2^16-1 - (2^5-1)) / 3.3 * 0.0125*(22)/(22+12)
			
		
		// x = (Pitch; Roll)'
		ahrs_filter();	
	}
}



xSemaphoreHandle xGpsSemaphore = NULL;

#define LONG_TIME 0xffff
void sensors_gps_task( void *parameters )
{
	uart1_puts("Gps task initializing...");
	sensor_data.gps.status = EMPTY;	
	uart1_puts("done\n\r");

	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		if( xSemaphoreTake( xGpsSemaphore, LONG_TIME ) == pdTRUE )
		{
			//uart1_puts("OK\n\r");
			gps_update_info(&(sensor_data.gps));
			//navigation_update(&navigationinfo, &gpsinfo);
		}
	}
}
