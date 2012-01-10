
/*

100kz I2C test routine for HMC5843 on dsPIC33FJ256MC710
Internal FRC and EMUC2/EMUD2 are used see micro_frc.c for config and osc startup

Oscillator changes may require modification of I2C1BRG in function InitI2C
Standard connections to I2C 1 module at pins 56 and 57
Standard output on uart 1 at 38400 baud

Mitch

*/


#include <stdio.h>
#include <math.h>
#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "i2c/i2c.h"
#include "hmc5843/hmc5843.h"



int main ()
{	struct intvector mag;	// integer vector of magnetometer measurements
	#define magx mag.x.i16	// x axis
	#define magy mag.y.i16	// y axis
	#define magz mag.z.i16	// z axis

	microcontroller_init();

	uart1_open(57600l);		// open UART1

	printf ("Starting test...\n\r");
	microcontroller_delay_ms(5);
	i2c_init();				// Initialise the I2C(1) module
	hmc5843_init();			// initialize the HMC5843 chip
	test_HMC5843();			// display configuration and ID registers

	while(1)
	{
		hmc5843_read(&mag);		// update magnetometer data

		printf( "%5d %5d %5d -> %f\r\n",magx,magy,magz, atan2f(-magy,magx)/3.14*180.0);

		microcontroller_delay_ms(100);
		
	}	// end while
}		// end main


