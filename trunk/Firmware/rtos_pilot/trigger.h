#ifndef TRIGGER_H
#define TRIGGER_H

#include "navigation.h"
#include "gluonscript.h"

enum trigger_mode 
{
	SINGLE_SHOT,
	CONTINUOUS_SHOOTING	
};


struct trigger_state
{
	enum trigger_mode mode;
	unsigned int is_triggering : 1;
    int servo_channel;
    int usec_pulse;
    float delay_s;
    float period_s;
};	

extern struct trigger_state trigger;
 
void trigger_set_mode(enum trigger_mode mode);
void trigger_do();
void trigger_start();
void trigger_stop();
void trigger_servo(int servo, int usec_pulse, float delay_s);

ScriptHandlerReturn trigger_handle_gluonscriptcommand (struct GluonscriptCode *code);

#endif //TRIGGER_H
