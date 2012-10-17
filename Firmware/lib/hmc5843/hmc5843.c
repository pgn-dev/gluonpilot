
/*	
	hmc5843.c
	A set i2c routines and functions for the Honeywell HMC5843
	triad magnetometer.

	C30 I2C periperal libraries are not used

	Note - the HMC5843 breakout board distributed by sparkfun 
	electronics	(as of Jan 2010) does not function well with the 
	existing capacitor.	It can be replaced with a 10uF ceramic.  
	See the forums at sparkfun.com.

	I2C functions derived from code at http://www.engscope.com/
	
	Mitch
*/

#include <stdio.h>
#include <math.h>
#include "i2c/i2c.h"
#include "hmc5843.h"
#include "microcontroller/microcontroller.h"


// initialize HMC5843
void hmc5843_init()
{
	// The default (factory) HMC5843 7-bit slave address is 0x3C for write operations, 
	// or 0x3D for read operations.  Put the HMC5843 into continuous mode by sending 
	// 0x3C 0x02 0x00 to write the 00 into the second register or mode register

    // HMC5843
	//I2Cwrite(0x03c, 0, 20); 	// put hmc at 10hz
	//I2Cwrite(0x03c, 1, 0); 	// put hmc .7Ga mode
	//I2Cwrite(0x03c, 2, 0); 	// put hmc in continuous mode

    // HMC5883L
  	I2Cwrite(0x03c, 0, 0b10000); 	// put hmc at 15hz
	I2Cwrite(0x03c, 1, 0b100000); 	// put hmc .7Ga mode
	I2Cwrite(0x03c, 2, 0); 		// put hmc in continuous mode

	// note that you need to wait 100ms after this before first calling recieve
	//microcontroller_delay_ms(100);
}

// Read HMC5843 data registers
// This can be called at 100ms intervals to get new vector
void hmc5843_read(struct intvector *magdata) 
{
	// To clock out the new data, send:
	// 0x3D, and clock out DXRA, DXRB, DYRA, DYRB, DZRA, DZRB located in registers 3 through 8. 
	// The HMC5843 will automatically re-point back to register 3 for the next 0x3D query, 
	// expected 100 milli-seconds or later
	// note Mitch - automatic indexing is flakey - use direct register access and read all 6 bytes


	/*magdata->y.b2.hbyte = I2Cread(0x03C, 3);
	magdata->y.b2.lbyte = I2Cread(0x03C, 4);
	magdata->x.b2.hbyte = I2Cread(0x03C, 5);
	magdata->x.b2.lbyte = I2Cread(0x03C, 6);
	magdata->z.b2.hbyte = I2Cread(0x03C, 7);
	magdata->z.b2.lbyte = I2Cread(0x03C, 8);
	magdata->x.i16 = -magdata->x.i16;*/
    
    magdata->x.b2.hbyte = I2Cread(0x03C, 3);
	magdata->x.b2.lbyte = I2Cread(0x03C, 4);
	magdata->z.b2.hbyte = I2Cread(0x03C, 5);
	magdata->z.b2.lbyte = I2Cread(0x03C, 6);
	magdata->y.b2.hbyte = I2Cread(0x03C, 7);
	magdata->y.b2.lbyte = I2Cread(0x03C, 8);
	magdata->z.i16 = -magdata->z.i16;
    magdata->y.i16 = -magdata->y.i16;
}

// exhibit the status of the HMC5843
void test_HMC5843() 
{
	printf( "rega %u \r\n", I2Cread(0x03C,0));
	printf( "regb %u \r\n", I2Cread(0x03C,1));
	printf( "mode %u \r\n", I2Cread(0x03C,2));
	printf( "stat %u \r\n", I2Cread(0x03C,9));
	printf( "id   %1c%1c%1c \r\n",I2Cread(0x03C,10),I2Cread(0x03C,11),I2Cread(0x03C,12));
}
