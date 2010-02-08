
#include "microcontroller/microcontroller.h"
#include "led/led.h"


void main()
{
	microcontroller_init();
	led_init();
	
	led1_on();
	led2_on();
	microcontroller_delay_ms(500);	
	led1_off();
	microcontroller_delay_ms(500);	
	
	while (1)
	{
		microcontroller_delay_ms(500);	
		led1_toggle();
	}	
	
	
}
	