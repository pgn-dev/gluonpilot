#ifndef TRIGGER_H
#define TRIGGER_H

#include "handler_navigation.h"
#include "gluonscript.h"


enum trigger_mode 
{
    TRIGGER_PWM_INTERVAL_MODE = 0,
    TRIGGER_CHDK_MODE = 1,
    TRIGGER_PWM_DISTANCE_MODE = 2
};


struct trigger_state
{
	enum trigger_mode mode;
	unsigned int is_triggering : 1;
    int servo_channel;
    int usec_pulse;
    float delay_s;
    float period_s;
    float distance_m;
    unsigned int trigger_counter;
};	

extern struct trigger_state trigger;
 
void trigger_set_mode(enum trigger_mode mode);
void trigger_servo(int servo, int usec_pulse, float delay_s);

ScriptHandlerReturn trigger_handle_gluonscriptcommand (struct GluonscriptCode *code);

#endif //TRIGGER_H
