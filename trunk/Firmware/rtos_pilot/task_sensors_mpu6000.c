/*!
 *  Reads raw sensor data and convert it to usefull data.
 *  This is then all stored in global sensor_data variable.
 *
 *  @author   Tom Pycke
 *  @date     05/10/2012
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
#include "mpu6000/mpu6000.h"

#include "sensors.h"
#include "task_sensors_mpu6000.h"
#include "configuration.h"
#include "ahrs.h"
#include "common.h"
#include "gluonscript.h"

#define INVERT_X -1.0   // set to -1 if front becomes back


extern xSemaphoreHandle xSpiSemaphore;

void read_mpu6000_sensor_data();
void bmp085_do_10Hz_2();


/*!
 *   FreeRTOS task that reads all the sensor data and stored it in the
 *   sensor_data struct.
 *
 *   The current execution rate is 50Hz.
 *
 *   Measured stackspace consumption: xxx bytes (2150 available)
 */
void sensors_mpu6000_task( void *parameters )
{
	float last_height = 0.0f;
	float dt_since_last_height = 0.0f;
	unsigned int low_update_counter = 0;

	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime;
    
    vTaskSetApplicationTaskTag( NULL, ( void * ) 2 );

	uart1_puts("Sensors task initializing...");
#if ENABLE_QUADROCOPTER || F1E_STEERING
	i2c_init();
	vTaskDelay(( portTickType ) 20 / portTICK_RATE_MS );
	hmc5843_init();
    uart1_puts("magnetometer...");
    test_HMC5843();
#else
    i2c_init();
#endif
    
	adc_open();

    vTaskDelay( ( ( portTickType ) 1 / portTICK_RATE_MS ) );   // 1ms

    mpu6000_init();

    //vTaskDelay( ( ( portTickType ) 1 / portTICK_RATE_MS ) );   // 1ms
    //mpu6000_init();

    bmp085_init();

	read_mpu6000_sensor_data();

    vTaskDelay( ( ( portTickType ) 1 / portTICK_RATE_MS ) );   // 1ms

    read_mpu6000_sensor_data();

	ahrs_init();

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

		adc_start();  // restart ADC sampling to make sure we have our samples on the next loop iteration.

		read_mpu6000_sensor_data();

		if (low_update_counter % 25 == 0) // 2Hz
		{
			if (control_state.simulation_mode)
            {
                uart1_puts("\r\nSimulation mode: disabling sensors task!\r\n");
				vTaskDelete(xTaskGetCurrentTaskHandle());
            }

			sensor_data.battery1_voltage_10 = (int)((float)adc_get_channel(8) * (3.3f * 5.1f / 6552.0f * 10.0f)) / 10;
            sensor_data.battery2_voltage_10 = (int)((float)adc_get_channel(9) * (3.3f * 5.1f / 6552.0f * 10.0f)) / 10;
            sensor_data.battery1_current = ((float)adc_get_channel(23) * (3.30f * (10.0f) / 65520.0f));
            sensor_data.battery1_mAh += sensor_data.battery1_current * 1000.0 / 60.0 / 60.0 * 0.5;
            //printf("\r\n%fA\r\n",sensor_data.battery1_current);
            //printf("\r\n%u %u %u %u %u\r\n",
            //        adc_get_channel(7), adc_get_channel(8), adc_get_channel(9),
            //        adc_get_channel(10), adc_get_channel(11));
			bmp085_do_10Hz_2();
            sensor_data.vertical_speed = sensor_data.vertical_speed * 0.9f + (sensor_data.pressure_height - last_height)/0.5 * 0.1f; // too much noise otherwise
            last_height = sensor_data.pressure_height;
            dt_since_last_height = 0;
		}

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


void bmp085_do_10Hz_2()
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

void read_mpu6000_sensor_data()
{
    mpu6000_update_sensor_readings();

    if (mpu6000_raw_sensor_readings.acc_x < 0)
        sensor_data.acc_x_raw = 32768 + (long)mpu6000_raw_sensor_readings.acc_x;
    else
        sensor_data.acc_x_raw = 32768 + (unsigned int)mpu6000_raw_sensor_readings.acc_x;

    if (mpu6000_raw_sensor_readings.acc_y < 0)
        sensor_data.acc_y_raw = 32768 + (long)mpu6000_raw_sensor_readings.acc_y;
    else
        sensor_data.acc_y_raw = 32768 + (unsigned int)mpu6000_raw_sensor_readings.acc_y;

    if (mpu6000_raw_sensor_readings.acc_z < 0)
        sensor_data.acc_z_raw = 32768 + (long)mpu6000_raw_sensor_readings.acc_z;
    else
        sensor_data.acc_z_raw = 32768 + (unsigned int)mpu6000_raw_sensor_readings.acc_z;

    if (mpu6000_raw_sensor_readings.gyro_x < 0)
        sensor_data.gyro_x_raw = 32768 + (long)mpu6000_raw_sensor_readings.gyro_x;
    else
        sensor_data.gyro_x_raw = 32768 + (unsigned int)mpu6000_raw_sensor_readings.gyro_x;

    if (mpu6000_raw_sensor_readings.gyro_y < 0)
        sensor_data.gyro_y_raw = 32768 + (long)mpu6000_raw_sensor_readings.gyro_y;
    else
        sensor_data.gyro_y_raw = 32768 + (unsigned int)mpu6000_raw_sensor_readings.gyro_y;

    if (mpu6000_raw_sensor_readings.gyro_z < 0)
        sensor_data.gyro_z_raw = 32768 + (long)mpu6000_raw_sensor_readings.gyro_z;
    else
        sensor_data.gyro_z_raw = 32768 + (unsigned int)mpu6000_raw_sensor_readings.gyro_z;

    
    if (config.sensors.imu_rotated == 1)  // 90° CCW
    {
        sensor_data.acc_y = -((float)config.sensors.acc_x_neutral - (float)sensor_data.acc_x_raw) / 4096.0;
        sensor_data.acc_x = ((float)sensor_data.acc_y_raw - (float)config.sensors.acc_y_neutral) / 4096.0;
        sensor_data.acc_z = ((float)config.sensors.acc_z_neutral - (float)sensor_data.acc_z_raw) / 4096.0;

        sensor_data.q = -((float)config.sensors.gyro_x_neutral - (float)sensor_data.gyro_x_raw) * (3.14159 / 180.0 / 32.8);
        sensor_data.p = ((float)sensor_data.gyro_y_raw - (float)config.sensors.gyro_y_neutral) * (3.14159 / 180.0 / 32.8);
        sensor_data.r = ((float)config.sensors.gyro_z_neutral - (float)sensor_data.gyro_z_raw) * (3.14159 / 180.0 / 32.8);
    }
    else if (config.sensors.imu_rotated == 3)  // 270° CCW
    {
        sensor_data.acc_y = ((float)config.sensors.acc_x_neutral - (float)sensor_data.acc_x_raw) / 4096.0;
        sensor_data.acc_x = -((float)sensor_data.acc_y_raw - (float)config.sensors.acc_y_neutral) / 4096.0;
        sensor_data.acc_z = ((float)config.sensors.acc_z_neutral - (float)sensor_data.acc_z_raw) / 4096.0;

        sensor_data.q = ((float)config.sensors.gyro_x_neutral - (float)sensor_data.gyro_x_raw) * (3.14159 / 180.0 / 32.8);
        sensor_data.p = -((float)sensor_data.gyro_y_raw - (float)config.sensors.gyro_y_neutral) * (3.14159 / 180.0 / 32.8);
        sensor_data.r = ((float)config.sensors.gyro_z_neutral - (float)sensor_data.gyro_z_raw) * (3.14159 / 180.0 / 32.8);
    }
    else if (config.sensors.imu_rotated == 2)  // 180°
    {
        sensor_data.acc_x = ((float)sensor_data.acc_x_raw - (float)config.sensors.acc_x_neutral) / 4096.0;
        sensor_data.acc_y = ((float)config.sensors.acc_y_neutral - (float)sensor_data.acc_y_raw) / 4096.0;
        sensor_data.acc_z = ((float)config.sensors.acc_z_neutral - (float)sensor_data.acc_z_raw) / 4096.0;

        sensor_data.p = ((float)sensor_data.gyro_x_raw - (float)config.sensors.gyro_x_neutral) * (3.14159 / 180.0 / 32.8);
        sensor_data.q = ((float)config.sensors.gyro_y_neutral - (float)sensor_data.gyro_y_raw) * (3.14159 / 180.0 / 32.8);
        sensor_data.r = ((float)config.sensors.gyro_z_neutral - (float)sensor_data.gyro_z_raw) * (3.14159 / 180.0 / 32.8);
    }
    else //if (config.sensors.imu_rotated == 0)
    {
        sensor_data.acc_x = ((float)config.sensors.acc_x_neutral - (float)sensor_data.acc_x_raw) / 4096.0;
        sensor_data.acc_y = ((float)sensor_data.acc_y_raw - (float)config.sensors.acc_y_neutral) / 4096.0;
        sensor_data.acc_z = ((float)config.sensors.acc_z_neutral - (float)sensor_data.acc_z_raw) / 4096.0;

        sensor_data.p = ((float)config.sensors.gyro_x_neutral - (float)sensor_data.gyro_x_raw) * (3.14159 / 180.0 / 32.8);
        sensor_data.q = ((float)sensor_data.gyro_y_raw - (float)config.sensors.gyro_y_neutral) * (3.14159 / 180.0 / 32.8);
        sensor_data.r = ((float)config.sensors.gyro_z_neutral - (float)sensor_data.gyro_z_raw) * (3.14159 / 180.0 / 32.8);
    }
}

