


#include <stdio.h>
#include <math.h>
#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "bmp085/bmp085.h"



int main ()
{
	long temp, p, rawt, rawp;

	microcontroller_init();

	uart1_open(115200l);		// open UART1

	printf ("Starting test...\n\r");
	microcontroller_delay_ms(150);
	i2c_init();				// Initialise the I2C(1) module
	bmp085_init();			// initialize the HMC5843 chip
	//test_HMC5843();			// display configuration and ID registers

	while(1)
	{
		bmp085_start_convert_temp();
		microcontroller_delay_ms(5);
		rawt = bmp085_read_temp();
		bmp085_convert_temp(rawt, &temp);

		bmp085_start_convert_pressure();
		microcontroller_delay_ms(150);
		rawp = bmp085_read_pressure();
		bmp085_convert_pressure(rawp, &p);

		printf( "%ld (%ld | %d %d %d) - %ld (%ld | %d %d) \r\n", p, rawp, (int)((rawp>>16)&0xFF), (int)((rawp>>8)&0xFF), (int)(rawp&0xFF), temp, rawt, (int)((rawt>>8)&0xFF), (int)(rawt&0xFF));

		microcontroller_delay_ms(500);
		
	}	// end while
}		// end main


