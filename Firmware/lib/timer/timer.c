
#include "microcontroller/microcontroller.h"
#include "timer/timer.h"

static const float ticks_per_second = (float)FCY;

void timer_init()
{
	
	T5CONbits.TCS = 0;   // Internal clock (FOSC/2)
	//T5CONbits.TCKPS = 0b00; // 1/256 prescaler
	//T5CONbits.TON = 1;
	//T5CONbits.T32 = 1;
	TMR5 = 0;            // Reset counter

	T4CONbits.TCS = 0;   // Internal clock (FOSC/2)
	T4CONbits.TCKPS = 0b00; // 1/256 prescaler
	T4CONbits.TON = 1;
	T4CONbits.T32 = 1;
	TMR4 = 0;            // Reset counter
}


void timer_reset()
{
	T4CONbits.TON = 0;
	TMR4 = 0;            // Reset counter
	TMR5 = 0;            // Reset counter
	T4CONbits.TON = 1;
	
}


float timer_dt()
{
	long t = TMR5;
	t <<= 16;
	t += TMR4;
	t <<= 8;       // we use a 1/256 prescaler!
	float ticks = (float)t;
	timer_reset();	
	return ticks/ticks_per_second;
}
