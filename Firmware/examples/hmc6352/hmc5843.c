
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
#include "micro_frc.h"
#include "hmc5843.h"




// iniate a start condition on bus
void i2c_start(void)
{
	int x = 0;
	I2C1CONbits.ACKDT = 0; //Reset any previous Ack
	microcontroller_delay_us(10);
	I2C1CONbits.SEN = 1; //Initiate Start condition
	Nop();
	//the hardware will automatically clear Start Bit
	//wait for automatic clear before proceding
	while (I2C1CONbits.SEN)
	{ 
		microcontroller_delay_us(1);
		x++;
		if (x > 20) break;
	}
	microcontroller_delay_us(2); 
} 


//iniate a restart condition on bus
void i2c_restart(void)
{
	int x = 0;
	I2C1CONbits.RSEN = 1; //Initiate restart condition
	Nop();
	//the hardware will automatically clear restart bit
	//wait for automatic clear before proceding
	while (I2C1CONbits.RSEN)
	{ 
		microcontroller_delay_us(1);
		x++;
		if (x > 20) break;
	}
	microcontroller_delay_us(2);
} 


//Reset the I2C bus to Idle
void reset_i2c_bus(void) 
{
	int x = 0;
	//initiate stop bit
	I2C1CONbits.PEN = 1;
	//wait for hardware clear of stop bit
	while (I2C1CONbits.PEN)
	{
		microcontroller_delay_us(1); 
		x ++; 
		if (x > 20) break; 
	} 
	I2C1CONbits.RCEN = 0;
	IFS1bits.MI2C1IF = 0; // Clear Interrupt
	I2C1STATbits.IWCOL = 0;
	I2C1STATbits.BCL = 0;
	microcontroller_delay_us(10);
}


// Initialize the I2C(1) peripheral.
void InitI2C(void)
{	unsigned char temp;

	// initialize ports
	TRISGbits.TRISG2 = 1;
	TRISGbits.TRISG3 = 1;
	//microcontroller_delay_ms(1);

	//First set the I2C(1) BRG Baud Rate.
	//Consult the dSPIC Data Sheet for information on how to calculate the
	//Baud Rate.
 
	//I2C1BRG = 0x0188; //40 mips 100khz
	I2C1BRG = 0x0185; //39.6 mips 100khz
	//I2C1BRG = 0x005D; //40 mips 400khz


	I2C1CONbits.I2CEN = 0; // Disable I2C Mode
	I2C1CONbits.DISSLW = 1; // Disable slew rate control
	IFS1bits.MI2C1IF = 0; // Clear Interrupt
	I2C1CONbits.I2CEN = 1; // Enable I2C Mode
	temp = I2CRCV; // read buffer to clear buffer full
	reset_i2c_bus(); // set bus to idle 
}

// basic I2C byte send
char send_i2c_byte(int data)
{
	int i;
	while (I2C1STATbits.TBF) { }

	IFS1bits.MI2C1IF = 0; // Clear Interrupt
	I2CTRN = data; // load the outgoing data byte
	// wait for transmission
	for (i=0; i<500; i++)
	{
		if (!I2C1STATbits.TRSTAT) break; 
		microcontroller_delay_us(1); 
	}
	if (i == 500)
	{
		return(1); 
	}
	// Check for NO_ACK from slave, abort if not found
	if (I2C1STATbits.ACKSTAT == 1)
	{
		reset_i2c_bus(); 
		return(1); 
	}
	microcontroller_delay_us(2);
	return(0);
}


//function reads one byte, returns the read data
char i2c_read_byte(void)
{
	int i = 0;
	char data = 0;
	//set I2C module to receive
	I2C1CONbits.RCEN = 1;
	//if no response, break
	while (!I2C1STATbits.RBF)
	{
		i ++; 
		if (i > 2000) break; 
	}
	//get data from I2CRCV register
	data = I2CRCV;
	//return data
	return data;
}


// write to an address
void I2Cwrite(char addr, char subaddr, char value)
{
	char tmp;
	i2c_start();
	tmp = send_i2c_byte(addr);
	tmp = send_i2c_byte(subaddr);
	tmp = send_i2c_byte(value);
	reset_i2c_bus();
}

//read from an address
char I2Cread(char addr, char subaddr)
{
	char temp;
	i2c_start();
	send_i2c_byte(addr);
	send_i2c_byte(subaddr);
	microcontroller_delay_us(10);
	i2c_restart();
	send_i2c_byte(addr | 0x01);
	temp = i2c_read_byte();
	reset_i2c_bus();
	return temp;
}

// initialize HMC5843
void init_HMC5843()
{
	// The default (factory) HMC5843 7-bit slave address is 0x3C for write operations, 
	// or 0x3D for read operations.  Put the HMC5843 into continuous mode by sending 
	// 0x3C 0x02 0x00 to write the 00 into the second register or mode register

	//I2Cwrite(0x03c, 0, 24); 	// put hmc at 50hz
	//I2Cwrite(0x03c, 1, 0); 	// put hmc .7Ga mode
	I2Cwrite(0x03c, 2, 0); 		// put hmc in continuous mode

	// note that you need to wait 100ms after this before first calling recieve
	microcontroller_delay_ms(100);
}

// Read HMC5843 data registers
// This can be called at 100ms intervals to get new vector
void HMC5843(struct intvector *magdata) 
{
	// To clock out the new data, send:
	// 0x3D, and clock out DXRA, DXRB, DYRA, DYRB, DZRA, DZRB located in registers 3 through 8. 
	// The HMC5843 will automatically re-point back to register 3 for the next 0x3D query, 
	// expected 100 milli-seconds or later
	// note Mitch - automatic indexing is flakey - use direct register access and read all 6 bytes
	
	magdata->x.b2.hbyte = I2Cread(0x03C, 3);
	magdata->x.b2.lbyte = I2Cread(0x03C, 4);

	magdata->y.b2.hbyte = I2Cread(0x03C, 5);
	magdata->y.b2.lbyte = I2Cread(0x03C, 6);
	
	magdata->z.b2.hbyte = I2Cread(0x03C, 7);
	magdata->z.b2.lbyte = I2Cread(0x03C, 8);
}

// exhibit the status of the HMC5843
void test_HMC5843() 
{
	printf( "rega %u \n\r",I2Cread(0x03C,0));
	printf( "regb %u \n\r",I2Cread(0x03C,1));
	printf( "mode %u \n\r",I2Cread(0x03C,2));
	printf( "stat %u \n\r",I2Cread(0x03C,9));
	printf( "id   %1c%1c%1c \n\r",I2Cread(0x03C,10),I2Cread(0x03C,11),I2Cread(0x03C,12));
	microcontroller_delay_ms(100);
}



int main ()
{	struct intvector mag;	// integer vector of magnetometer measurements
	#define magx mag.x.i16	// x axis
	#define magy mag.y.i16	// y axis
	#define magz mag.z.i16	// z axis

	microcontroller_init();

	uart1_open(38400);		// open UART1
	printf ("Starting test...\n\r");

	InitI2C();				// Initialise the I2C(1) module
	init_HMC5843();			// initialize the HMC5843 chip
	test_HMC5843();			// display configuration and ID registers

	while(1)
	{
		HMC5843(&mag);		// update magnetometer data

		printf( "%5d %5d %5d\r",magx,magy,magz);

		microcontroller_delay_ms(100);
		
	}	// end while
}		// end main
