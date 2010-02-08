/*
 *   This simple example will print out 8 channels from the PPM input pin. 
 *   When the frame is valid, the line begin with a happy smiley :-), an invalid 
 *   frame will be indicated by a sad smiley!
 *
 */


#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "ppm_in/ppm_in.h"


int main()
{
	int i;
	
	microcontroller_init();
	uart1_open(38400L);
	
	printf ("Testing PPM input functionality\n\r");
	ppm_in_open();
	ppm_in_guess_num_channels();
	
	while (1)
	{
		if (ppm.valid_frame)
			printf (":-) ");
		else
			printf (":-( ");
		for (i = 0; i < 8; i++)
			printf (" %u ", ppm.channel[i]);
			
		printf ("\n\r");
		microcontroller_delay_ms(30);	
	}
		
	return 0;	
}
