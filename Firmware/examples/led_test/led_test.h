
#include "led/led.h"

void main()
{
	microcontroller_init();
	
	while (1)
	{
		microcontroller_delay_ms(500);	
		led1_toggle();
	}	
	
	
}
	