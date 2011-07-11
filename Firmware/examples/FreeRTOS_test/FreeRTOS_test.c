/*
 *   This example shows the basics of FreeRTOS: how to create tasks.
 *   For more info, I encourage you to visit the FreeRTOS
 *   website (http://www.freertos.org) and read the documentation!
 *
 */


#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"

// This example uses 2 different FreeRTOS tasks:
static void vTask1( void *pvParameters );
static void vTask2( void *pvParameters );


int main()
{
	microcontroller_init();
	
	uart1_open(115200l);
	
	// Create our two tasks. They both have 3 times default stack size. Make sure you understand this when
	// designing your own tasks. The stack size INCLUDES possible stack usage by the interrupt routines.
	// They have the same priority.
	xTaskCreate( vTask1, ( signed portCHAR * ) "Task1", ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 1, NULL );
	xTaskCreate( vTask2, ( signed portCHAR * ) "Task2", ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 1, NULL );
	
	// Order the scheduler to start scheduling our two tasks.
	vTaskStartScheduler();
	
	// We should only get here when the scheduler wasn't able to allocate enough memory.
	uart1_puts("Not enough heap!\n\r");
	
	return 1;
}
	
	
/*
 *    This task will send a line directly to uart1 once every 1 second.
 */
static void vTask1( void *pvParameters )
{
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Task1 initializing...");
	uart1_puts("done\n\r");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()
	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 1000 / portTICK_RATE_MS ) );

		uart1_puts("I am task 1\n\r");
	}
}

extern xQueueHandle xRxedChars;


/*
 *    This task will send a line directly to uart1 once every 5 seconds.
 */
static void vTask2( void *pvParameters )
{
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Task2 initializing...");
	uart1_puts("done\n\r");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()
	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 5000 / portTICK_RATE_MS ) );

		uart1_puts("I am task 2\n\r");
	}
}

	
	
void vApplicationIdleHook( void )
{
}

