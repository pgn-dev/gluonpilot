
#include <stdio.h>

#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "adc/adc.h"

int main()
{
	microcontroller_init();
	uart1_open(115200L);
	
	adc_open();
	
	printf("\n\rADC demo\n\r");
	
	printf("x_acc \t y_acc \t z_acc \t x_rate \t y_rate \t z_rate \t idg500_vref\n\r");
	
	adc_start();
	
	while(1)
	{
		printf ("%u %u %u %u %u %u %u\r\n",
			adc_get_channel(0),  // x_acc
			adc_get_channel(6),  // y_acc
			adc_get_channel(1),  // z_acc
			adc_get_channel(7),  // idg500_xrate
			adc_get_channel(4),  // idg500_yrate
			adc_get_channel(5),  // zrate
			adc_get_channel(3)); // idg500_vref
		
		microcontroller_delay_ms(1);
		adc_start();
		microcontroller_delay_ms(1);
	}	
	
	return 0;
}
