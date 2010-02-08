
#include "microcontroller/microcontroller.h"
#include "led/led.h"

void led_init()
{
	// RE4 = LED1
	TRISEbits.TRISE4 = 0;
	
	// RE3 = LED2
	TRISEbits.TRISE3 = 0;
}


void led1_on()
{
	PORTEbits.RE4 = 0;
}
	
void led1_off()
{
	PORTEbits.RE4 = 1;
}
	
void led1_toggle()
{
	if (led1_is_off())
		led1_on();
	else
		led1_off();	
}
	
unsigned char led1_is_off()
{
	return PORTEbits.RE4;
}	

void led2_on()
{
	PORTEbits.RE3 = 0;
}
	
void led2_off()
{
	PORTEbits.RE3 = 1;
}
	
void led2_toggle()
{
	if (led2_is_off())
		led2_on();
	else
		led2_off();
}
	
unsigned char led2_is_off()
{
	return PORTEbits.RE3;
}	
