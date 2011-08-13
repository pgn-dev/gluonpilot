  

#include "microcontroller/microcontroller.h"
#include "i2c/i2c.h"


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
		if (x > 20) 
			break; 
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
		if (x > 20) 
			break; 
	} 
	I2C1CONbits.RCEN = 0;
	IFS1bits.MI2C1IF = 0; // Clear Interrupt
	I2C1STATbits.IWCOL = 0;
	I2C1STATbits.BCL = 0;
	microcontroller_delay_us(10);
}


// Initialize the I2C(1) peripheral.
void i2c_init(void)
{	unsigned char temp;

	// initialize ports
	TRISGbits.TRISG2 = 1;
	TRISGbits.TRISG3 = 1;
	//microcontroller_delay_ms(1);

	//First set the I2C(1) BRG Baud Rate.
	//Consult the dSPIC Data Sheet for information on how to calculate the
	//Baud Rate.
 
	//I2C1BRG = 0x0188; //40 mips 100khz
	//I2C1BRG = 0x0185; //39.6 mips 100khz
	I2C1BRG = 0x005D; //40 mips 400khz


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
	while (I2C1STATbits.TBF)
		;
	IFS1bits.MI2C1IF = 0; // Clear Interrupt
	I2CTRN = data; // load the outgoing data byte
	// wait for transmission
	for (i=0; i<500; i++)
	{
		if (!I2C1STATbits.TRSTAT) 
			break; 
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
		if (i > 2000)
			break; 
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
