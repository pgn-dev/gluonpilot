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
 *
 *
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
#include "button/button.h"
#include "pid/pid.h"
#include "uart1_queue/uart1_queue.h"
#include "i2c/i2c.h"
#include "bmp085/bmp085.h"

#include "sensors.h"
#include "task_sensors_analog.h"
#include "configuration.h"
#include "ahrs.h"
#include "common.h"
#include "gluonscript.h"

#define INVERT_X -1.0   // set to -1 if front becomes back


static const float acc_value_g = 6600.0f;

extern xSemaphoreHandle xSpiSemaphore;

void read_raw_sensor_data();
void scale_raw_sensor_data();
void bmp085_do_10Hz();
unsigned int squared(unsigned int a, unsigned int b, unsigned int c);

float scale_z_gyro = 0.0f;

/*!
 *   FreeRTOS task that reads all the sensor data and stored it in the
 *   sensor_data struct.
 *
 *   The current execution rate is 100Hz.
 *
 *   Measured stackspace consumption: 576 bytes (2150 available)
 */
void sensors_analog_task( void *parameters )
{
	unsigned int temperature_10 = 200;
	float last_height = 0.0f;
	float dt_since_last_height = 0.0f;
	unsigned int low_update_counter = 0;

    unsigned int mean_gyro_x, mean_gyro_y, mean_gyro_z;
    unsigned long var_gyros, var_gyros_temp = 0;
		
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

    vTaskSetApplicationTaskTag( NULL, ( void * ) 2 );

	uart1_puts("Sensors task initializing...");
#if ENABLE_QUADROCOPTER || F1E_STEERING
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
	vTaskDelay( ( ( portTickType ) 20 / portTICK_RATE_MS ) );
	read_raw_sensor_data();
	scale_raw_sensor_data();
	ahrs_init();

     mean_gyro_x = sensor_data.gyro_x_raw;
     mean_gyro_y = sensor_data.gyro_y_raw;
     mean_gyro_z = sensor_data.gyro_z_raw;
	
	if (HARDWARE_VERSION >= V01N) // IDZ-500 gyroscope
		scale_z_gyro = (-0.02538315f*3.14159f/180.0f)*2.0f;
	else // ADXRS-613 gyroscope
		scale_z_gyro = (0.0062286f*3.14159f/180.0f);  //(2^16-1 - (2^5-1)) / 3.3 * 0.0125*(22)/(22+12)
		


	uart1_puts("done\r\n");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
#ifdef ENABLE_QUADROCOPTER
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 4 / portTICK_RATE_MS ) );   // 250Hz
		dt_since_last_height += 0.004f;
		low_update_counter += 1;
#else
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 20 / portTICK_RATE_MS ) );   // 50Hz
		dt_since_last_height += 0.02f;
		low_update_counter += 5;
#endif
		if (low_update_counter > 65000)
			low_update_counter = 0;
		read_raw_sensor_data();
				
		adc_start();  // restart ADC sampling to make sure we have our samples on the next loop iteration.

		scale_raw_sensor_data();
		
		if (low_update_counter % 25 == 0) // 10Hz
		{
            // detects when the module is not moving.
            mean_gyro_x = (mean_gyro_x >> 1) + (sensor_data.gyro_x_raw >> 1);
            mean_gyro_y = (mean_gyro_y >> 1) + (sensor_data.gyro_y_raw >> 1);
            mean_gyro_z = (mean_gyro_z >> 1) + (sensor_data.gyro_z_raw >> 1);
            var_gyros_temp = var_gyros_temp - (var_gyros_temp >> 2) +
                        squared((unsigned int)(mean_gyro_x - sensor_data.gyro_x_raw),
                                (unsigned int)(mean_gyro_y - sensor_data.gyro_y_raw),
                                (unsigned int)(mean_gyro_z - sensor_data.gyro_z_raw) );
            var_gyros = var_gyros_temp >> 2;
            //if (var_gyros <= 10)
            //    printf("\r\n still \r\n");
            //else if (low_update_counter % 300 == 0)
            //    printf("\r\n%lu [%u %u] [%u %u] [%u %u]\r\n", var_gyros, (unsigned int)(mean_gyro_x - sensor_data.gyro_x_raw), sensor_data.gyro_x_raw,mean_gyro_y, sensor_data.gyro_y_raw,mean_gyro_z, sensor_data.gyro_z_raw);

			if (control_state.simulation_mode)
            {
                uart1_puts("\r\nSimulation mode: disabling sensors task!\r\n");
				vTaskDelete(xTaskGetCurrentTaskHandle());
            }
				
			sensor_data.battery1_voltage_10 = ((float)adc_get_channel(8) * (3.3f * 5.1f / 6550.0f));
			if (HARDWARE_VERSION >= V01O)
			{
				bmp085_do_10Hz();
			}
		}	
		else
		{
			if (HARDWARE_VERSION < V01O && scp1000_dataready())   // New reading from the pressure sensor -> calculate vertical speed
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
				if (height > -30000.0f && height < 30000.0f)   // sometimes we get bad readings ~ -31000
					sensor_data.pressure_height = height;

				sensor_data.vertical_speed = sensor_data.vertical_speed * 0.9f + (sensor_data.pressure_height - last_height)/dt_since_last_height * 0.1f; // too much noise otherwise
				
				if (fabs(sensor_data.vertical_speed) > MAX(5.0f, sensor_data.gps.speed_ms))  // validity check
					sensor_data.vertical_speed = 0.0f;
					
				last_height = sensor_data.pressure_height;
				dt_since_last_height = 0.0f;
			}
		}


		// x = (Pitch; Roll)'
#if (ENABLE_QUADROCOPTER || F1E_STEERING)
		if (low_update_counter % 25 == 0)
		{
			hmc5843_read(&sensor_data.magnetometer_raw); 
		}
#endif

#ifdef ENABLE_QUADROCOPTER
		ahrs_filter(0.005f);	
#else
		ahrs_filter(0.02f);	
#endif
	}
}

unsigned int squared(unsigned int a, unsigned int b, unsigned int c)
{
    unsigned long i;
    if (a > 60000)
        a = !a;
    if (b > 60000)
        b = !b;
    if (c > 60000)
        c = !c;

    if (a > 200)
        return 60000;
    if (b > 200)
        return 60000;
    if (c > 200)
        return 60000;

    i = (unsigned long)(a*a) + (unsigned long)(b*b) + (unsigned long)(c*c);
    if (i > 65000)
        return 65000;
    else
        return (unsigned int)i;
}


void bmp085_do_10Hz()
{
	static int state = 0;
	long tmp;

	switch (state)
	{
		case 0:
			bmp085_start_convert_temp();
			state = 1;
			break;
		case 1:
		{
			tmp = bmp085_read_temp();
			bmp085_convert_temp(tmp, &sensor_data.temperature_10);
			sensor_data.temperature = (float)sensor_data.temperature_10 / 10.0f;
		}
			bmp085_start_convert_pressure();
			state = 2;
			break;
		case 2:
            {
                long pressure;
                tmp = bmp085_read_pressure();
                bmp085_convert_pressure(tmp, &pressure);

                sensor_data.pressure = (float)pressure;
            }
            sensor_data.pressure_height = scp1000_pressure_to_height(sensor_data.pressure, sensor_data.temperature);

            bmp085_start_convert_temp();
            state = 1;
            break;
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
	sensor_data.acc_x = ((float)(sensor_data.acc_x_raw) - (float)config.sensors.acc_x_neutral) / (-acc_value_g*INVERT_X);
	sensor_data.acc_y = ((float)(sensor_data.acc_y_raw) - (float)config.sensors.acc_y_neutral) / (-acc_value_g*INVERT_X);
	sensor_data.acc_z = ((float)(sensor_data.acc_z_raw) - (float)config.sensors.acc_z_neutral) / (-acc_value_g);
			
	// scale to rad/sec
	sensor_data.p = ((float)(sensor_data.gyro_x_raw)-config.sensors.gyro_x_neutral) * (-0.02518315f*3.14159f/180.0f * INVERT_X);  // 0.02518315f
	sensor_data.q = ((float)(sensor_data.gyro_y_raw)-config.sensors.gyro_y_neutral) * (-0.02538315f*3.14159f/180.0f * INVERT_X);
	sensor_data.r = ((float)(sensor_data.gyro_z_raw)-config.sensors.gyro_z_neutral) * scale_z_gyro;
}	


