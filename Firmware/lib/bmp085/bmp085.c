 
#include <stdio.h>
#include "i2c/i2c.h"
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

char bmp085Read(unsigned char address)
{
//	unsigned char data;
  
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
	i2c_wait_acken();

	lsb = i2c_read_byte();

	reset_i2c_bus();
  
  return (int) ((int) msb<<8 | lsb);
}

// Read 2 bytes from the BMP085
// First byte will be from 'address'
// Second byte will be from 'address'+1
long bmp085ReadUT(unsigned char address)
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
	i2c_wait_acken();

	lsb = i2c_read_byte();

	reset_i2c_bus();

  return (long) ((long) msb<<8 | (long)lsb);
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
i2c_wait_acken();

	lsb = i2c_read_byte();

I2C1CONbits.ACKDT = 0;
I2C1CONbits.ACKEN = 1;
i2c_wait_acken();

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
	//printf("\nCalibration Information:\n");
	//printf("------------------------\n");
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
	
	//printf("\rAC1\t%d\n", ac1);
	//printf("\rAC2\t%d\n", ac2);
	//printf("\rAC3\t%d\n", ac3);
	//printf("\rAC4\t%d\n", ac4);
	//printf("\rAC5\t%d\n", ac5);
	//printf("\rAC6\t%d\n", ac6);
	//printf("\rB1\t%d\n", b1);
	//printf("\rB2\t%d\n", b2);
	//printf("\rMB\t%d\n", mb);
	//printf("\rMC\t%d\n", mc);
	//printf("\rMD\t%d\n", md);
	//printf("------------------------\n\n");
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
	return (long) bmp085ReadUT(0xF6);
}

long bmp085_read_pressure(void)
{
	return (long) bmp085ReadLong3(0xF6);
}


static long x1, x2, b5, b6, x3, b3, p;
static unsigned long b4, b7;

void bmp085_convert_temp(long raw, int *temp)
{
	x1 = ((long)raw - (long)ac6) * (long)ac5 >> 15;
	x2 = ((long) mc << 11) / (x1 + (long)md);
	b5 = x1 + x2;
	//printf(" T x1: %ld   x2: %ld   b5: %ld\r\n", x1, x2, b5);
	*temp = (int)((b5 + 8L) >> 4);
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
