/*! 
 *  The main file of the Gluonpilot's FreeRTOS autopilot.
 *
 *  The functionality here is pretty limited:
 *   - Initialize primairy hardware
 *   - Load configuration from dataflash
 *   - Start all the tasks
 *
 *  @file     rtos_pilot.c
 *  @author   Tom Pycke
 *  @date     24-dec-2009
 *  @since    0.1
 */
 
 
#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

#include "microcontroller/microcontroller.h"
#include "uart1_queue/uart1_queue.h"
#include "dataflash/dataflash.h"
#include "control.h"
#include "sensors.h"
#include "communication.h"
#include "configuration.h"


extern xSemaphoreHandle xGpsSemaphore;

int main()
{
	microcontroller_init();
	uart1_queue_init(115200l);  // default baudrate: 115200
	
	uart1_puts("Gluonpilot v0.1\n\r");
	printf("Config: %d bytes\n\r", sizeof(struct Configuration));

	uart1_puts("Loading configuration");
	ppm_in_open();  // We need a complete frame (which takes at least 20ms) to start so never can start early enough!
	dataflash_open();
	configuration_load();
	gps_init(&(config.gps));
	vSemaphoreCreateBinary( xGpsSemaphore );
	uart1_puts("done\n\r");
	
	// Create our tasks. 
	xTaskCreate( control_task, ( signed portCHAR * ) "Control", ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 6, NULL );
	xTaskCreate( sensors_task, ( signed portCHAR * ) "Sensors", ( configMINIMAL_STACK_SIZE * 4 ), NULL, tskIDLE_PRIORITY + 5, NULL );
	xTaskCreate( sensors_gps_task, ( signed portCHAR * ) "Gps", ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 4, NULL );
	xTaskCreate( communication_input_task, ( signed portCHAR * ) "ConsoleInput", ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 3, NULL );
	xTaskCreate( communication_telemetry_task, ( signed portCHAR * ) "Telemetry", ( configMINIMAL_STACK_SIZE * 2 ), NULL, tskIDLE_PRIORITY + 2, NULL );
		
	// Order the scheduler to start scheduling our two tasks.
	vTaskStartScheduler();
	
	// We should only get here when the scheduler wasn't able to allocate enough memory.
	uart1_puts("Not enough heap!\r\n");
	
	return 1;
}


void vApplicationStackOverflowHook( xTaskHandle *pxTask, signed portCHAR *pcTaskName )
{
	uart1_puts("Stack overflow! ");
	uart1_puts(pcTaskName);
	uart1_puts("\n\r");
	while(1) ; 
}


void vApplicationIdleHook( void )
{
	//uart1_putc('.');
}

