
#include "microcontroller/microcontroller.h"

#include <spi.h>
#include <math.h>

#include "scp1000.h"

#define SCL  PORTGbits.RG6 //SCK This is clock input into SCP1000
#define SDI  PORTGbits.RG8 //MOSI This is input into the SCP1000 from the PIC
#define SDO  PORTGbits.RG7 //MISO This is output from SCP1000 into the PIC
#define CS   PORTGbits.RG9 //CSB This is chip select input - when low, the SCP1000 is selected

unsigned char read_register(unsigned char register_name);
unsigned int read_register16(unsigned char register_name);
void write_register(unsigned char register_name, unsigned char register_value);
unsigned char spi_comm_software(unsigned char outgoing_byte);


unsigned int Write8(unsigned int add, unsigned int val);
unsigned int Read8(unsigned int add);
unsigned int Read16(unsigned int add);

void scp1000_init()
{
	TRISGbits.TRISG6 = 0;	// just make this an output
	TRISGbits.TRISG8 = 0;	// just make this an output
	TRISGbits.TRISG9 = 0;	// just make this an output
	TRISGbits.TRISG7 = 1;	// just make this an input
	TRISEbits.TRISE8 = 1;	// just make this an input (dataready)
	
	TRISEbits.TRISE3 = 0;	// just make this an output
	TRISEbits.TRISE4 = 0;	// just make this an output
	PORTEbits.RE3 = 1;
	PORTEbits.RE4 = 1;
	
	OpenSPI2(ENABLE_SCK_PIN & ENABLE_SDO_PIN & SPI_MODE16_OFF & SPI_SMP_OFF & SPI_CKE_OFF &
         SLAVE_ENABLE_OFF & MASTER_ENABLE_ON & PRI_PRESCAL_64_1 & SEC_PRESCAL_8_1,
         FRAME_ENABLE_OFF,
         SPI_ENABLE & SPI_RX_OVFLOW_CLR); 

	microcontroller_delay_ms(90);
	
	
	Write8(0x02, 0x00);   // indirect access to 0x00
	microcontroller_delay_us(100);
	//Write8(0x01, 0x05);   // high resolution to data write register
	Write8(0x01, 0x0D);    // high speed to data write register
	microcontroller_delay_us(100);
	Write8(0x03, 0x02);   // write data to 0x00
	microcontroller_delay_ms(50);
	Write8(0x03, 0x0A);   // start high resolution
	Write8(0x03, 0x09);   // start high speed
}


inline int scp1000_dataready()
{
	return PORTEbits.RE8;	
}
	

float scp1000_get_height()
{
	return scp1000_pressure_to_height(scp1000_get_pressure(), scp1000_get_temperature());
}


float scp1000_pressure_to_height(float pressure, float temperature)
{
	//return 44330.0 * (1.0 - powf(pressure / 101325.0, 0.19));
	//return logf(pressure / 101000.f) * (273.f + temperature) * (287.05f / 9.81f);
	return logf(pressure / 101000.f) * (273.f + 20.f) * (-287.05f / 9.81f);
}	


float scp1000_get_pressure()
{
	unsigned int d1, d2;
	unsigned long pressure;
	
	//microcontroller_delay_us(1);
	//Write8(0x03, 0x0A);
	//microcontroller_delay_us(1);

	d2 = Read8(0x1F);
	d1 = Read16(0x20);

	pressure = d2;
	pressure <<= 16;
	pressure |= d1;
	return pressure / 4.0;
}


float scp1000_get_temperature()
{
	unsigned int t = Read16(0x21);
	
	return ((float)t) / 20.0;
}


unsigned int scp1000_get_status()
{
	return Read8(0x07);
}


unsigned int Read16(unsigned int register_name)
{
	unsigned int i;
	
	register_name <<= 2;
    register_name &= 0b11111100; //Read command
	
	//CS = 0;
	//microcontroller_delay_us(10);
	CS = 0;
	i = SPI2BUF;
	SPI2BUF = register_name;  // address
	while(SPI2STATbits.SPITBF) ;	
	while(! SPI2STATbits.SPIRBF) ;
	i = SPI2BUF;
	
	SPI2BUF = 0x00;
	while(SPI2STATbits.SPITBF) ;	
	while(! SPI2STATbits.SPIRBF) ;
	i = SPI2BUF;

	i <<= 8;
	SPI2BUF = 0x00;
	while(SPI2STATbits.SPITBF) ;	
	while(! SPI2STATbits.SPIRBF) ;
	unsigned int tmp = SPI2BUF;
	CS = 1;
		
	i |= tmp & 0xFF;
	
	return i;
}	


unsigned int Read8(unsigned int add)
{
	unsigned int i;
	
	add <<= 1;
	add |= 0x00;  // read
	add <<= 1; 
	add |= 0x00;
	
	while(SPI2STATbits.SPITBF) ;
	
	i = SPI2BUF;

	CS = 0;
	microcontroller_delay_us(10);
	
	SPI2BUF = add;
	while(SPI2STATbits.SPITBF) ;	
	while(! SPI2STATbits.SPIRBF) ;
	microcontroller_delay_us(1);
	i = SPI2BUF;

	SPI2BUF = 0x00;
	while(SPI2STATbits.SPITBF) ;
	while(! SPI2STATbits.SPIRBF) ;
	microcontroller_delay_us(1);
	i = SPI2BUF;
	CS = 1;	
	//PORTGbits.RG1 = 1;
	
	return i;
}	


unsigned int Write8(unsigned int add, unsigned int val)
{
	int i;
	
	add <<= 1;
	add |= 0x01;  // read
	add <<= 1; 
	add |= 0x00;
	
	i = SPI2BUF;
	CS = 0;
	microcontroller_delay_us(10);
	
	SPI2BUF = add;
	while(SPI2STATbits.SPITBF) ;
	while(! SPI2STATbits.SPIRBF) ;
	i = SPI2BUF;
	
	SPI2BUF = val;
	while(SPI2STATbits.SPITBF) ;
	while(! SPI2STATbits.SPIRBF) ;
	CS = 1;
	i = SPI2BUF;
		
	return i;
}
