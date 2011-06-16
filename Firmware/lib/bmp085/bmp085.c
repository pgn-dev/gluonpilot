 
#include "bmp085/bmp085.h"
#include "microcontroller/microcontroller.h"

#define BMP085_ADDRESS 0xEE  // I2C address of BMP085

#define OSS 3

int ac1;
int ac2; 
int ac3; 
unsigned int ac4;
unsigned int ac5;
unsigned int ac6;
int b1; 
int b2;
int mb;
int mc;
int md;

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

char bmp085Read(unsigned char address)
{
	unsigned char data;
  
	i2c_start();
	send_i2c_byte(BMP085_ADDRESS);
	send_i2c_byte(address);
	microcontroller_delay_us(10);
	i2c_restart();
    send_i2c_byte(BMP085_ADDRESS | 0x01);
	char temp = i2c_read_byte();
	reset_i2c_bus();
	return temp;
}

// Read 2 bytes from the BMP085
// First byte will be from 'address'
// Second byte will be from 'address'+1
int bmp085ReadInt(unsigned char address)
{
	unsigned char msb, lsb;
  
	i2c_start();

	send_i2c_byte(BMP085_ADDRESS);
	send_i2c_byte(address);
	microcontroller_delay_us(10);
	i2c_restart();
	send_i2c_byte(BMP085_ADDRESS | 0x01);

	msb = i2c_read_byte();

I2C1CONbits.ACKDT = 0;
I2C1CONbits.ACKEN = 1;
while(I2C1CONbits.ACKEN == 1);

	lsb = i2c_read_byte();

	reset_i2c_bus();
  
  return (int) ((int) msb<<8 | lsb);
}

long bmp085ReadLong3(unsigned char address)
{
	unsigned char msb, lsb, xlsb;
  
	i2c_start();

	send_i2c_byte(BMP085_ADDRESS);
	send_i2c_byte(address);
	microcontroller_delay_us(10);
	i2c_restart();
	send_i2c_byte(BMP085_ADDRESS | 0x01);
	msb = i2c_read_byte();

I2C1CONbits.ACKDT = 0;
I2C1CONbits.ACKEN = 1;
while(I2C1CONbits.ACKEN == 1);

	lsb = i2c_read_byte();

I2C1CONbits.ACKDT = 0;
I2C1CONbits.ACKEN = 1;
while(I2C1CONbits.ACKEN == 1);

	xlsb = i2c_read_byte();

	reset_i2c_bus();
  
	long result = msb;
	result <<= 8;
	result += lsb;
	result <<= 8;
	result += xlsb;
	result >>= 8-OSS;
	return result;
  	//return (long) ((long) msb<<16 | (long)lsb << 8 | (long)xlsb) >> (8-OSS);
}

void bmp085_Calibration(void)
{
	printf("\nCalibration Information:\n");
	printf("------------------------\n");
	ac1 = bmp085ReadInt(0xAA);
	ac2 = bmp085ReadInt(0xAC);
	ac3 = bmp085ReadInt(0xAE);
	ac4 = bmp085ReadInt(0xB0);
	ac5 = bmp085ReadInt(0xB2);
	ac6 = bmp085ReadInt(0xB4);
	b1 = bmp085ReadInt(0xB6);
	b2 = bmp085ReadInt(0xB8);
	mb = bmp085ReadInt(0xBA);
	mc = bmp085ReadInt(0xBC);
	md = bmp085ReadInt(0xBE);
	
	printf("\rAC1\t%d\n", ac1);
	printf("\rAC2\t%d\n", ac2);
	printf("\rAC3\t%d\n", ac3);
	printf("\rAC4\t%d\n", ac4);
	printf("\rAC5\t%d\n", ac5);
	printf("\rAC6\t%d\n", ac6);
	printf("\rB1\t%d\n", b1);
	printf("\rB2\t%d\n", b2);
	printf("\rMB\t%d\n", mb);
	printf("\rMC\t%d\n", mc);
	printf("\rMD\t%d\n", md);
	printf("------------------------\n\n");
}


void bmp085_start_convert_pressure()
{
	// pressure
	i2c_start();
	send_i2c_byte(BMP085_ADDRESS);
	send_i2c_byte(0xF4);
	send_i2c_byte(0x34 + (OSS<<6));
	microcontroller_delay_us(10);
	reset_i2c_bus();
}

void bmp085_start_convert_temp()
{
	// temperature
	i2c_start();
	send_i2c_byte(BMP085_ADDRESS);
	send_i2c_byte(0xF4);
	send_i2c_byte(0x2E);
	microcontroller_delay_us(10);
	reset_i2c_bus();
}


long bmp085_read_temp(void)
{
	return (long) bmp085ReadInt(0xF6);
}

long bmp085_read_pressure(void)
{
	return (long) bmp085ReadLong3(0xF6);
}


static long x1, x2, b5, b6, x3, b3, p;
static unsigned long b4, b7;

void bmp085_convert_temp(long raw, long *temp)
{
	x1 = ((long)raw - (long)ac6) * (long)ac5 >> 15;
	x2 = ((long) mc << 11) / (x1 + (long)md);
	b5 = x1 + x2;
	//printf(" T x1: %ld   x2: %ld   b5: %ld\r\n", x1, x2, b5);
	*temp = (b5 + 8L) >> 4;
}

void bmp085_convert_pressure(long up, long* pressure)
{
	// bmp085_convert_temp required before this call!!
/*
	b6 = b5 - 4000L;
	x1 = ((long)b2 * (b6 * b6 >> 12)) >> 11;
	x2 = (long)ac2 * b6 >> 11;
	x3 = x1 + x2;
	b3 = (((long) ac1 * 4L + x3) << OSS + 2)/4;
	x1 = (long)ac3 * b6 >> 13;
	x2 = ((long)b1 * (b6 * b6 >> 12)) >> 16;
	x3 = ((x1 + x2) + 2) >> 2;
	b4 = ((unsigned long)ac4 * (unsigned long) (x3 + 32768L)) >> 15;
	b7 = ((unsigned long) up - (unsigned long)b3) * (50000L >> OSS);
	p = b7 < 0x80000000 ? (b7 * 2L) / b4 : (b7 / b4) * 2L;
	x1 = (p >> 8) * (p >> 8);
	x1 = (x1 * 3038L) >> 16;
	x2 = (-7357L * p) >> 16;
	*pressure = p + ((x1 + x2 + 3791L) >> 4);  */

       
        // Calculate the true pressure.
        b6 = b5 - 4000L;
		//printf(" b6: %ld\r\n", b6);
        x1 = ((signed long)b2 * (b6 * b6 >> 12)) >> 11;
		//printf(" x1: %ld\r\n", x1);
        x2 = (signed long)ac2 * b6 >> 11;
		//printf(" x2: %ld\r\n", x2);
        x3 = x1 + x2;
        //printf(" x3: %ld\r\n", x3);
		b3 = ((((signed long)ac1 * 4L + x3) << OSS) + 2) >> 2;
        x1 = (signed long)ac3 * b6 >> 13;
        x2 = ((signed long)b1 * (b6 * b6 >> 12)) >> 16;
        x3 = ((x1 + x2) + 2) >> 2;
        b4 = ((unsigned long)ac4 * (unsigned long)(x3 + 32768L)) >> 15;
        b7 = ((unsigned long)up - (unsigned long)b3) * (50000L >> OSS);
        p = (signed long)((b7 < 0x80000000) ? (b7 * 2L) / b4 : (b7 / b4) * 2L);
        x1 = (p >> 8) * (p >> 8);
        x1 = (x1 * 3038L) >> 16;
        x2 = (-7357 * p) >> 16;
		//printf(" - %ld %ld %ld - \r\n", p, x1, x2);
        *pressure = p + ((x1 + x2 + 3791L) >> 4);
}

/*
long bmp085ReadPressure(void)
{
	long pressure = 0;
	
	i2cSendStart();
	i2cWaitForComplete();
	
	i2cSendByte(BMP085_W);	// write 0xEE
	i2cWaitForComplete();
	
	i2cSendByte(0xF4);	// write register address
	i2cWaitForComplete();
	
	i2cSendByte(0x34);	// write register data for temp
	i2cWaitForComplete();
	
	i2cSendStop();
	
	delay_ms(10);	// max time is 4.5ms
	
	pressure = bmp085ReadShort(0xF6);
	pressure &= 0x0000FFFF;
	
	return pressure;
	
	//return (long) bmp085ReadShort(0xF6);
}*/

void bmp085_init()
{
	bmp085_Calibration();
}
