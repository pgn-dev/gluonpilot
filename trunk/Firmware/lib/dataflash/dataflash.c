/*!
 *    @title  AT45DB161B library for gluonpilot
 *    @description
 *            This code implements the AT45DB161B dataflash chip from Atmel. Only
 *            the basic commands are implemented. We assume the chip uses 524-byte pages.
 *    @author (c) Tom Pycke
 *    @date   04-dec-2009
 *            23-nov-2008
 */


#include "microcontroller/microcontroller.h"
#include "dataflash/dataflash.h"
#include <spi.h>

// Definition of bit banging the SPI
unsigned char spi_comm_bitbang(unsigned char outgoing_byte);
// Definition of using the SPI hardware module
unsigned char spi_comm_hw(unsigned char outgoing_byte);

// Let's use the hardware module!
#define spi_comm(x) spi_comm_hw(x)


inline void dataflash_disable_spi();
inline void dataflash_enable_spi();


// These are the pins our dataflash chip is connected to
#define SCL  PORTGbits.RG6 //SCK
#define SDI  PORTGbits.RG7 //MISO
#define SDO  PORTGbits.RG8 //MOSI
#define CS   PORTFbits.RF0 //CSB 

/**
 *   Initializes the SPI hardware
 */
void dataflash_open()
{
	TRISFbits.TRISF0 = 0;	// make this an output
	TRISGbits.TRISG6 = 0;	// make this an output
	TRISGbits.TRISG8 = 0;	// make this an output
	TRISGbits.TRISG9 = 0;   // SS2 -> voor SCP1000
	TRISGbits.TRISG7 = 1;	// make this an input
	
	// Open hardware SPI, as fast as possible, don't use hardware SS2
	OpenSPI2(ENABLE_SCK_PIN & ENABLE_SDO_PIN & SPI_MODE16_OFF & SPI_SMP_OFF & SPI_CKE_OFF &
	         SLAVE_ENABLE_OFF & MASTER_ENABLE_ON & PRI_PRESCAL_4_1 & SEC_PRESCAL_1_1,
	         FRAME_ENABLE_OFF, SPI_ENABLE & SPI_RX_OVFLOW_CLR); 

	dataflash_disable_spi();
}

inline void dataflash_enable_spi()
{
	PORTGbits.RG9 = 1;  // disable the CS for the sc1000 pressure sensor, which shared this SPI connection.
	CS = 0;
}

inline void dataflash_disable_spi()
{
	CS = 1;
}
	
/*!
 *    SPI communication basic operation using hardware SPI.
 */
unsigned char spi_comm_hw(unsigned char add)
{
	unsigned int i;

	// Take a look at the Errata for this microcontroller to see why we need to wait for the flags
	SPI2BUF = add;
	while(SPI2STATbits.SPITBF) ;	
	while(! SPI2STATbits.SPIRBF) ;

	i = SPI2BUF;
		
	return i;
}	


/*!
 *    SPI communication basic operation using bit-banged software SPI.
 *    0.00402s for 512 bytes!
 */
unsigned char spi_comm_bitbang(unsigned char outgoing_byte)
{
    unsigned char incoming_byte, x;

    for(x = 0 ; x < 8 ; x++)
    {
        SCL = 0; //Toggle the SPI clock

        SDO = outgoing_byte >> 7; //Put bit on SPI data bus
        outgoing_byte <<= 1; //Rotate byte 1 to the left

		asm("nop");asm("nop");asm("nop");
		
        SCL = 1;

        incoming_byte <<= 1; //Rotate byte 1 to the left
        incoming_byte |= SDI & 0x01; //Read bit on SPI data bus
		asm("nop");asm("nop");asm("nop");
    }
    
    return(incoming_byte);
}


/*!
 *   This operation will write buffer of maximum 528 bytes to the dataflash.
 *   Basically, I'm just following the datasheet:
 *      - Command 0x84 allows us to write to buffer 1
 *      - Command 0x83 command the chip to write buffer 1 to the flash memory
 *
 *   Some simple simulations showed that it takes 0.7ms to write a page.
 */
void dataflash_write(int page, int size, unsigned char *buffer)
{
	int add1, add2;
	int i;
	add1 = 0;
	add2 = 0;
	
	dataflash_disable_spi();
	
	microcontroller_delay_us(1);
	
	dataflash_enable_spi();
	
	// Write to buffer 1
	spi_comm(0x84);
	spi_comm(0x00);   // select buffer1
	spi_comm(0x00);   // select buffer1
	spi_comm(0x00);   // select buffer1
	
	for (i = 0; i < size; i++)
		spi_comm(buffer[i]);

	dataflash_disable_spi();
	
	// Write buffer 1 to memory
	// 3 don't care bits
	// 12 bits for page
	add1 = page;
	add1 >>= 6;
	add2 = page;
	add2 <<= 2;
	
	
	
	microcontroller_delay_us(1);
	dataflash_enable_spi();

	spi_comm(0x83); // select buffer1
	spi_comm(add1 & 0xFF); // select buffer1
	spi_comm(add2 & 0xFF); // select buffer1
	spi_comm(0x00); // select buffer1
	
	dataflash_disable_spi();

	// Now he's probably busy writing
}	


void dataflash_read_config(int size, unsigned char *configbuffer)
{
	dataflash_read(0, size, configbuffer);
}	


/*!
 *   This operation will read a page of maximum 524 byte
 *   Basically, I'm just following the datasheet:
 *      - Command 0xE8 allows us to read continuously from a certain page
 *
 *   Some simple simulations showed that it takes 0.7ms to read a page.
 */
void dataflash_read(int page, int size, unsigned char *buffer)
{
	int add1, add2;
	int i;
	
	// Write buffer 1 to memory
	// 3 don't care bits
	// 12 bits for page
	add1 = page;
	add1 >>= 6;
	add2 = page;
	add2 <<= 2;

	
	dataflash_disable_spi();
	dataflash_enable_spi();

	spi_comm(0xE8); 
	spi_comm(add1 & 0xFF);  // 3 addr bytes   : 12 bytes: page - 10 bits: starting address
	spi_comm(add2 & 0xFF);
	spi_comm(0);
	
	spi_comm(0);  // 4 don't care bytes
	spi_comm(0);
	spi_comm(0);
	spi_comm(0);

	for (i = 0; i < size; i++)
	{
		buffer[i] = spi_comm(0x00);
	}	
	
	dataflash_disable_spi();
}	






