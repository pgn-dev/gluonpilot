
#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

#include "microcontroller/microcontroller.h"
#include "uart1_queue/uart1_queue.h"

// This example uses 2 different FreeRTOS tasks:
static void vParseTask( void *pvParameters );
static void vTickTask( void *pvParameters );


int main()
{
	microcontroller_init();
	
	uart1_queue_init(38400l);
	
	xTaskCreate( vTickTask, ( signed portCHAR * ) "TickTask", ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 1, NULL );
	xTaskCreate( vParseTask, ( signed portCHAR * ) "ParseTask", ( configMINIMAL_STACK_SIZE * 4 ), NULL, tskIDLE_PRIORITY + 3, NULL );
	
	vTaskStartScheduler();
	uart1_puts("Not enough heap!\n\r");
	
	return 1;
}
	
	
/*
 *    This task will send "." directly to uart1 once every 2 seconds.
 */
static void vTickTask( void *pvParameters )
{
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Tick task initializing...");
	uart1_puts("done\n\r");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()
	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 2000 / portTICK_RATE_MS ) );

		uart1_putc('.');
	}
}

extern xQueueHandle xRxedChars;


/*
 *   This task will wake when a character is received on uart1.
 *   It will parse a line into the several tokens (separated by a space).
 */
static void vParseTask( void *pvParameters )
{
	char  buffer[20];
	int   buffer_position;
	int   token[5] = {0,0,0,0,0};
	int   current_token;
	
	int i, j;
	
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 
	
	char tmp;

	uart1_puts("Parse task initializing...");

	uart1_puts("done\n\r");
	
	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		if( xQueueReceive( xRxedChars, &tmp, portMAX_DELAY  ) )
        {
            // pcRxedMessage now points to the struct AMessage variable posted
            // by vATask.
            //uart1_putc('(');
            //uart1_putc(tmp);
            //uart1_putc(')');
            
            if (tmp == '\n' || tmp == '\r')
            {
	            buffer[buffer_position] = '\0';
	            token[current_token + 1] = buffer_position;
	            uart1_putc('{');
		        uart1_puts(buffer);
			    uart1_puts("}\n\r");
	            for (i = 0; i <= current_token && i < 5; i++)
	            {
		        	j = 0;
		        	uart1_putc('[');
		        	while (token[i]+j < token[i+1])
		        	{
		        		uart1_putc(buffer[token[i] + j++]);
		        	}	
		        	uart1_puts("]\n\r");
		        } 
            	buffer_position = 0;
            	current_token = 0;
            	token[0] = 0;
            	token[1] = 0;
            	token[2] = 0;
            	token[3] = 0;
            	token[4] = 0;
            	if (tmp == '\r')
            		uart1_puts("\n\r> ");
            }
            else if (tmp == ' ' && current_token < 5)
            {
	            //buffer[buffer_position++] = tmp;
	            token[++current_token] = buffer_position;
	            
	        } 
	        else if (buffer_position < 20)
	        	buffer[buffer_position++] = tmp;
        }
	}
}

	
	
void vApplicationIdleHook( void )
{
}

