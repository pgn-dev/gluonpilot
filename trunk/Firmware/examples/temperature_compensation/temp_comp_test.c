
#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "adc/adc.h"
#include "scp1000/scp1000.h"


int main()
{
	microcontroller_init();
	uart1_open(115200l);
	
	adc_open();
	adc_start();
	scp1000_init();
	
	while (1)
	{
		long x = 0;
		int i;
		for (i=0; i < 10; i++)
		{
			x += adc_get_channel(0);
			adc_start();
			microcontroller_delay_ms(100);
		}	
		x /= 11;
		
		printf("%.1f %u\n\r", scp1000_get_temperature(), (unsigned int)x);
		microcontroller_delay_ms(1000);
		microcontroller_delay_ms(1000);

	}	
}	
