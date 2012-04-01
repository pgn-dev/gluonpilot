
// Include all FreeRTOS header files
#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

#include "servo/servo.h"

#include "common.h"
#include "trigger.h"
#include "navigation.h"
#include "gluonscript.h"


struct trigger_state trigger = { .mode = SINGLE_SHOT, .is_triggering = 0, .servo_channel = 5,
                                 .usec_pulse = 2000, .delay_s = 0.5, .period_s = 2, .trigger_counter = 0};
enum trigger_mode mode;

ScriptHandlerReturn trigger_handle_gluonscriptcommand (struct GluonscriptCode *code)
{
    static int counter_5hz = 0;

	if (code->opcode == SERVO_TRIGGER)
	{
		trigger_servo(code->a, code->b, code->x);
        trigger.trigger_counter++;
		return HANDLED_FINISHED;
	} 
    else if (code->opcode == SERVO_START_TRIGGER)
    {
        trigger.servo_channel = code->a;
        trigger.usec_pulse = code->b;
        trigger.delay_s = code->x;
        trigger.is_triggering = 1;
        return HANDLED_FINISHED;
    }
    else if (code->opcode == SERVO_STOP_TRIGGER)
    {
        trigger.is_triggering = 0;
        return HANDLED_FINISHED;
    }

    if (trigger.is_triggering)
    {
        counter_5hz++;
        if ((float)counter_5hz >= trigger.period_s * 5.0)
        {
            trigger_servo(trigger.servo_channel, trigger.usec_pulse, trigger.delay_s);
            trigger.trigger_counter++;
            counter_5hz = 0;
        }
    }

    return HANDLED_UNFINISHED;
}


void trigger_servo(int servo, int usec_pulse, float delay_s)
{
	unsigned int us = servo_read_us(servo);

    if (usec_pulse >= 2500)
        servo_set_logical_1(servo);
    else if (usec_pulse == 0)
        servo_set_logical_0(servo);
    else
        servo_set_us(servo, usec_pulse);  // a = channel(0..7), b = microseconds (1000...2000)
    
	unsigned int ms_delay = (unsigned int)(delay_s * 1000.0f);
	ms_delay = MIN(ms_delay, 3000);  // lets limit this to 3 seconds.
	vTaskDelay(( ( portTickType ) ms_delay / portTICK_RATE_MS ) );

	// set back to original position
    if (us >= 2500)
        servo_set_logical_1(servo);
    else if (us == 0)
        servo_set_logical_0(servo);
    else
        servo_set_us(servo, us);
}

void trigger_do()
{
}	

void trigger_start()
{
}	

void trigger_stop()
{
}	
