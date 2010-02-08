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
#include "uart1_queue/uart1_queue.h"
#include "scp1000/scp1000.h"

// This example uses 2 different FreeRTOS tasks:
static void vDataStreamingTask( void *pvParameters );
static void vScp1000( void *pvParameters );

struct Sensors
{
	float pressure;
	float height;
	float temperature;
	
} sensor_data;	


/*!
 *  Sends a signed integer as a string to a function.
 *  This function converts integer x to a series of characters and sends
 *  them one by one to function printer. Faster than printf, obviously.
 *  @param x The signed integer
 *  @param printer The function that the signed integer will be send to as a
 *                 string. It must have one argument: a pointer to an array 
 *                 of chars.
 */
void PrintSignedInteger(int x, void (*printer)(char[]))
{
	static char tmpChar[2] = "0";
	unsigned int radix,
                 i, converted;
	char emptyChar = ' ';
	
	if (x < 0)
	{
		printer("-");
		x*=-1;
	}
	else if (x == 0)
	{
		printer("0");
		return;	
	}
	converted = (unsigned int) x;
	// auto-align on 16bit size (655.. = 5 positions)
	for (radix=10000, i=0; i < 5; radix/=10, i++)
	{
		if (converted > radix-1) {
			tmpChar[0] =  '0' + (converted/radix);
			printer(tmpChar);
			converted = converted % radix;
			emptyChar = '0';
		} else if (emptyChar == '0') {
			printer("0");
		}
	}
}



int main()
{
	microcontroller_init();
	
	uart1_queue_init(115200l);
	
	// Create our two tasks. They both have 3 times default stack size. Make sure you understand this when
	// designing your own tasks. The stack size INCLUDES possible stack usage by the interrupt routines.
	// They have the same priority.
	xTaskCreate( vDataStreamingTask, ( signed portCHAR * ) "DataStreaming", ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 1, NULL );
	xTaskCreate( vScp1000, ( signed portCHAR * ) "Scp1000", ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 1, NULL );
	
	// Order the scheduler to start scheduling our two tasks.
	vTaskStartScheduler();
	
	// We should only get here when the scheduler wasn't able to allocate enough memory.
	uart1_puts("Not enough heap!\n\r");
	
	return 1;
}
	

struct DataStreamingConfig
{
	unsigned char stream_PPM;
	unsigned char stream_GyroAcc;
	unsigned char stream_PressureTemp;
} data_streaming_config;	

/*
 *    This task will send a line directly to uart1 20 times a second.
 */
static void vDataStreamingTask( void *pvParameters )
{
	struct DataStreamingConfig counters;
	counters.stream_PPM = 0;
	counters.stream_GyroAcc = 0;
	counters.stream_PressureTemp = 10;
	
	
	// MOVEME
	data_streaming_config.stream_PressureTemp = 10;
	
	
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("DataStreaming task initializing...");
	uart1_puts("done\n\r");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()
	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 50 / portTICK_RATE_MS ) );
		counters.stream_PPM++;
		counters.stream_GyroAcc++;
		counters.stream_PressureTemp++;
		
		if (counters.stream_PPM == data_streaming_config.stream_PPM)
		{
			printf("TR\r\n");
			counters.stream_PPM = 0;
		}	
		if (counters.stream_GyroAcc == data_streaming_config.stream_GyroAcc)
		{
			printf("TA;\r\n");
			counters.stream_GyroAcc = 0;
		}
		if (counters.stream_PressureTemp == data_streaming_config.stream_PressureTemp)
		{
			uart1_puts("TP;");
			//printf("TP;%f;%f\r\n", sensor_data.pressure, sensor_data.temperature);
			PrintSignedInteger((int)(sensor_data.pressure/10), &uart1_puts);
			uart1_putc(';');
			PrintSignedInteger((int)sensor_data.temperature, &uart1_puts);
			uart1_puts("\n\r");
			counters.stream_PressureTemp = 0;
		}
	}
}

extern xQueueHandle xRxedChars;


/*
 *    This task will send a line directly to uart1 once every 5 seconds.
 */
static void vScp1000( void *pvParameters )
{
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Scp1000 initializing...");
	scp1000_init();
	uart1_puts("done\n\r");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()
	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 115 / portTICK_RATE_MS ) );

		sensor_data.pressure = scp1000_get_pressure();
		sensor_data.temperature = scp1000_get_temperature();
	}
}

	
	
void vApplicationIdleHook( void )
{
}

