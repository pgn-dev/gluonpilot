
// Include all FreeRTOS header files
#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

#include "gps/gps.h"
#include "uart1_queue/uart1_queue.h"
#include "task_gps.h"
#include "led/led.h"

#include "configuration.h"
#include "common.h"
#include "gluonscript.h"
#include "handler_navigation.h"


/*!
 *   FreeRTOS task that parses the received GPS sentence and calculates the navigation.
 *
 *   The inner loop contains a semaphore that is only released when a complete and
 *   valid NMEA sentence has been received
 *
 *   Use stackspace 312 / 1720 bytes
 *
 */

//! This semaphore is set in the uart2 interrupt routine when a new GPS message arrives
xSemaphoreHandle xGpsSemaphore = NULL;


#define LONG_TIME 0xffff
void sensors_gps_task( void *parameters )
{
	int i = 0;

#ifdef F1E_STEERING
	while(1)
    {
		vTaskDelay( ( ( portTickType ) 1000 / portTICK_RATE_MS ) );
        sensor_data.gps.speed_ms = 0.0;//config.control.cruising_speed_ms;
    }
#endif

    vTaskSetApplicationTaskTag( NULL, ( void * ) 3 );

	uart1_puts("Gps & Navigation task initializing...\r\n");
	sensor_data.gps.status = EMPTY;
	sensor_data.gps.latitude_rad = 0.0;
	sensor_data.gps.longitude_rad = 0.0;

	gluonscript_init();

	gps_open_port(&(config.gps));

	// Wait for GPS output. On some old EB85 devices, this can take over 2sec
	for (i = 10; i <= 1000; i *= 2)
	{
		if (! gps_valid_frames_receiving())
			vTaskDelay( ( ( portTickType ) i / portTICK_RATE_MS ) );
	}


	gps_config_output();  // configure sentences and switch to 115200 baud


	vTaskDelay(( ( portTickType ) 100 / portTICK_RATE_MS ) );

	uart1_puts("Gps & Navigation task initialized\r\n");
	if (sensor_data.gps.status == EMPTY)
		led2_off();
	else if (sensor_data.gps.status == VOID)
		led2_on();

	//portTickType xLastExecutionTime = xTaskGetTickCount();
	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		if (control_state.simulation_mode)
		{
			i++;
			vTaskDelay(( ( portTickType ) 100 / portTICK_RATE_MS ) );
			sensor_data.gps.satellites_in_view = 9;
			sensor_data.gps.status = ACTIVE;
		}
		else if( xSemaphoreTake( xGpsSemaphore, ( portTickType ) 205 / portTICK_RATE_MS ) == pdTRUE )
		{
			gps_update_info(&(sensor_data.gps)); // 5Hz (needed?)
			i++;
		}
		else
		{
			// alert: no message received from GPS!
			sensor_data.gps.status = EMPTY;
			led2_off();
			i = 0;
			sensor_data.gps.satellites_in_view = 0;
		}

		// Speed is use for calculating accelerations (in the attitude filter)
		// When the GPS is no longer locked, we don't know the speed -> no reliable attitude
		// Use pre-configured cruising speed as measured speed
		if (sensor_data.gps.satellites_in_view < 4 && navigation_data.airborne)
				sensor_data.gps.speed_ms = config.control.cruising_speed_ms;

		if (i % 2 == 0) // this is used for both RMC and GGA, so only update every other tick
			gluonscript_do();

		if ((i % 6 == 0 || (i+1) % 6 == 0 || (i+2) % 6 == 0) &&  sensor_data.gps.status == ACTIVE && sensor_data.gps.satellites_in_view > 5)
			led2_off();
		else if (sensor_data.gps.status != EMPTY)
			led2_on();


	}
}
