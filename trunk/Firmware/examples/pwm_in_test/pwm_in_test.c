/*
 *   This example works almost the same of the ppm_in one, except
 *   that it uses 4 PWM inputs from the receiver instead of the
 *   PPM pulse train.
 */


#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "ppm_in/ppm_in.h"
#include "pwm_in/pwm_in.h"

int main()
{
	int i;
	
	microcontroller_init();
	uart1_open(115200L);
	
	printf ("Testing PWM input functionality\n\r");
	pwm_in_open();

	
	while (1)
	{
		if (ppm.valid_frame)
			printf (":-) ");
		else
			printf (":-( ");

		printf (" %u %u %u %u %u", ppm.channel[0], ppm.channel[1], ppm.channel[2], ppm.channel[3], ppm.channel[4]);
			
		printf ("\n\r");
		microcontroller_delay_ms(100);	
	}
		
	return 0;	
}
