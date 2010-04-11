#ifndef FREERTOS_CONFIG_H
#define FREERTOS_CONFIG_H

#include <p33FJ256MC710.h>

/*-----------------------------------------------------------
 * Application specific definitions.
 *
 * These definitions should be adjusted for your particular hardware and
 * application requirements.
 *
 * THESE PARAMETERS ARE DESCRIBED WITHIN THE 'CONFIGURATION' SECTION OF THE
 * FreeRTOS API DOCUMENTATION AVAILABLE ON THE FreeRTOS.org WEB SITE. 
 *
 * See http://www.freertos.org/a00110.html.
 *----------------------------------------------------------*/


#define portCONFIGURE_TIMER_FOR_RUN_TIME_STATS()  \
T5CONbits.TCS = 0;    \
	T5CONbits.TCKPS = 0b11;  \
	T5CONbits.TON = 1; \
	TMR5 = 0;           

#define portGET_RUN_TIME_COUNTER_VALUE()  TMR5
#define configGENERATE_RUN_TIME_STATS 0


#define configUSE_PREEMPTION			1
#define configUSE_IDLE_HOOK				0
#define configUSE_TICK_HOOK				0
#define configTICK_RATE_HZ				( ( portTickType ) 1000 )
#define configCPU_CLOCK_HZ				( ( unsigned portLONG ) 40000000 )  /* Fosc / 2 */
#define configMAX_PRIORITIES			( ( unsigned portBASE_TYPE ) 8 )
//#define configMINIMAL_STACK_SIZE		( 115 )
#define configMINIMAL_STACK_SIZE		( 215 )
#define configTOTAL_HEAP_SIZE			( ( size_t ) 16384 )
#define configMAX_TASK_NAME_LEN			( 8 )
#define configUSE_TRACE_FACILITY		1
#define configUSE_16_BIT_TICKS			1
#define configIDLE_SHOULD_YIELD			1

/* Co-routine definitions. */
#define configUSE_CO_ROUTINES 		1
#define configMAX_CO_ROUTINE_PRIORITIES ( 2 )

#define configCHECK_FOR_STACK_OVERFLOW 0

/* Set the following definitions to 1 to include the API function, or zero
to exclude the API function. */

#define INCLUDE_vTaskPrioritySet		1
#define INCLUDE_uxTaskPriorityGet		0
#define INCLUDE_vTaskDelete				0
#define INCLUDE_vTaskCleanUpResources	0
#define INCLUDE_vTaskSuspend			1
#define INCLUDE_vTaskDelayUntil			1
#define INCLUDE_vTaskDelay				1
#define INCLUDE_uxTaskGetStackHighWaterMark 0



#define configKERNEL_INTERRUPT_PRIORITY	0x01

#endif /* FREERTOS_CONFIG_H */
