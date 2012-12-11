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
 *
 */
 
#include <stdio.h>

// FreeRTOS includes
#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

// Gluonpilot library includes
#include "microcontroller/microcontroller.h"
#include "uart1_queue/uart1_queue.h"
#include "dataflash/dataflash.h"
#include "pwm_in/pwm_in.h"
#include "ppm_in/ppm_in.h"
#include "led/led.h"
#include "servo/servo.h"

// rtos_pilot includes
#include "task_control.h"
#include "task_sensors_analog.h"
#include "task_sensors_mpu6000.h"
#include "communication.h"
#include "configuration.h"
#include "task_datalogger.h"
#include "handler_navigation.h"
#include "gluonscript.h"
#include "osd.h"
#include "task_gps.h"

#include "common.h"

extern xSemaphoreHandle xGpsSemaphore;
extern xSemaphoreHandle xSpiSemaphore;

static char version[] = "0.9Beta";

unsigned long idle_counter = 0;

void setup_trace_pins();

int main()
{
	microcontroller_init();

	uart1_queue_init(57600l);  // default baudrate: 57600 due to XBee bi-direction communication


	printf("Gluonpilot v%s ", version);
#ifdef LIMITED  // Limited version is pre-loaded on modules sent to Non-European countries
	printf("Limited version");
#endif
	
	printf(" [%s %s, config: %dB, logline: %dB, navigation: %dB, double: %dB]\r\n\r\n",
                __DATE__, __TIME__, sizeof(struct Configuration), sizeof(struct LogLine), sizeof(gluonscript_data.codes), sizeof(double));
	
	microcontroller_reset_type();  // printf out reason of reset; for debugging
	led_init();

	// Create semaphores needed for FreeRTOS synchronization (better to do it know, they are changed in interrupts of uart2 and ppm)
	vSemaphoreCreateBinary( xSpiSemaphore );
	vSemaphoreCreateBinary( xGpsSemaphore );

	// What hardware version are we using?
	configuration_determine_hardware_version();
	if (HARDWARE_VERSION == V01N)
		printf("Found hardware version v0.1n\r\n");
	else if (HARDWARE_VERSION == V01O)
		printf("Found hardware version v0.1o\r\n");
    else if (HARDWARE_VERSION == V01Q)
		printf("Found hardware version v0.1q (GP2)\r\n");
	else
		printf("Found hardware version v0.1j or earlier\r\n");
	
	// Open flash & load configuration
	dataflash_open();
	printf("%d MB flash found \r\n", (int)PAGE_SIZE/264);
	printf("Loading configuration...");
	configuration_load();
	printf("done\r\n");

	
	// Open RC receiver input: pwm_in/ppm_in task: in ppm_in/pwm_in.c
	// This is too low level to do it in the control task
	if (config.control.use_pwm)
	{
		pwm_in_open(); 
		uart1_puts("Waiting for pwm");
		pwm_in_wait_for();
		if (! (ppm.channel[0] > 900 && ppm.channel[0] < 2100))
			uart1_puts("not found!\r\n");
		else
			uart1_puts("done\r\n");
	} 
	else
	{
		uart1_puts("Opening ppm...");
		ppm_in_open(); // We need a complete frame (which takes at least 20ms) to start so never can start early enough!
		uart1_puts(" done\r\n");
	}	
	

	// Create our tasks. 
	if (config.control.servo_mix == QUADROCOPTER)
		xTaskCreate( control_copter_task,            ( signed portCHAR * ) "CControl",      ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 7, NULL );
	else
		xTaskCreate( control_wing_task,            ( signed portCHAR * ) "WControl",      ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 7, NULL );

    if (HARDWARE_VERSION == V01Q)
    	xTaskCreate( sensors_mpu6000_task,                 ( signed portCHAR * ) "Sensors",      ( configMINIMAL_STACK_SIZE * 5 ), NULL, tskIDLE_PRIORITY + 6, NULL );
    else
        xTaskCreate( sensors_analog_task,                 ( signed portCHAR * ) "Sensors",      ( configMINIMAL_STACK_SIZE * 5 ), NULL, tskIDLE_PRIORITY + 6, NULL );

    xTaskCreate( sensors_gps_task,             ( signed portCHAR * ) "GpsNavi",      ( configMINIMAL_STACK_SIZE * 4 ), NULL, tskIDLE_PRIORITY + 5, NULL );
	xTaskCreate( communication_input_task,     ( signed portCHAR * ) "ConsoleInput", ( configMINIMAL_STACK_SIZE * 5 ), NULL, tskIDLE_PRIORITY + 4, NULL );
	xTaskCreate( datalogger_task,              ( signed portCHAR * ) "Dataflash",    ( configMINIMAL_STACK_SIZE * 3 ), NULL, tskIDLE_PRIORITY + 3, NULL );
	xTaskCreate( communication_telemetry_task, ( signed portCHAR * ) "Telemetry",    ( configMINIMAL_STACK_SIZE * 2 ), NULL, tskIDLE_PRIORITY + 2, NULL );
    xTaskCreate( osd_task,                     ( signed portCHAR * ) "OSD",          ( configMINIMAL_STACK_SIZE * 1 ), NULL, tskIDLE_PRIORITY + 1, NULL );

#ifdef USE_TRACING
    printf("\r\nENABLING TRACING\r\n");
    setup_trace_pins();
#endif

	// Order the scheduler to start scheduling our two tasks.
	vTaskStartScheduler();
	
	// We should only get here when the scheduler wasn't able to allocate enough memory.
	uart1_puts("Not enough heap!\r\n");
	
	return 1;
}


void vApplicationStackOverflowHook( xTaskHandle *pxTask, signed portCHAR *pcTaskName )
{
	uart1_puts("\n\rStack overflow! ");
	uart1_puts((char*)pcTaskName);
	uart1_puts("\n\r");
	while(1) ; 
}


void vApplicationIdleHook( void )
{
    vTaskSetApplicationTaskTag( NULL, ( void * ) 8 );
    TRISBbits.TRISB2 = 0;  // use OSD SPI CS
    while (1)
    {
        PORTBbits.RB2 = 0;
        idle_counter+=2;
        PORTBbits.RB2 = 1;
        idle_counter-=1;
    }
}


// TRACE functionality for tasks

void setup_trace_pins()
{
    //TRISCbits.TRISC13 = 0;  // PGD
    //TRISCbits.TRISC14 = 0;  // PGC
    servo_set_logical_1(0);
    servo_set_logical_1(1);
    servo_set_logical_1(2);
    servo_set_logical_1(3);
    servo_set_logical_1(4);
    servo_set_logical_1(5);
    servo_set_logical_1(6);
    servo_set_logical_1(7);
}
