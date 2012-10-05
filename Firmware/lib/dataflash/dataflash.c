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
#include "configuration.h"
#include <spi.h>


struct Dataflash dataflash;


/****************** OLD before v1q modules! *******************/
// Let's use the hardware module!
//#define gp1_spi_comm(x) gp1_spi_comm_hw(x)
#define gp1_spi_comm(x) gp1_spi_comm_bitbang(x)

inline void gp1_dataflash_disable_spi();
inline void gp1_dataflash_enable_spi();

// Definition of bit banging the SPI
unsigned char gp1_spi_comm_bitbang(unsigned char outgoing_byte);
// Definition of using the SPI hardware module
unsigned char gp1_spi_comm_hw(unsigned char outgoing_byte);


void gp1_dataflash_write_raw(int page, int size, unsigned char *buffer);
void gp1_dataflash_read_raw(int page, int size, unsigned char *buffer);
int gp1_dataflash_read_Mbit();
void gp1_dataflash_write(int page, int size, unsigned char *buffer);
void gp1_dataflash_open();
void gp1_dataflash_read(int page, int size, unsigned char *buffer);
 
#define v1o_CS   PORTFbits.RF0 //CSB

/**************************************************************/


/****************** NEW v1q and newer modules! *******************/
// Let's use the hardware module!
//#define gp2_spi_comm(x) gp2_spi_comm_hw(x)
#define gp2_spi_comm(x) gp2_spi_comm_bitbang(x)

inline void gp2_dataflash_disable_spi();
inline void gp2_dataflash_enable_spi();

// Definition of bit banging the SPI
unsigned char gp2_spi_comm_bitbang(unsigned char outgoing_byte);
// Definition of using the SPI hardware module
unsigned char gp2_spi_comm_hw(unsigned char outgoing_byte);


void gp2_dataflash_write_raw(int page, int size, unsigned char *buffer);
void gp2_dataflash_read_raw(int page, int size, unsigned char *buffer);
int gp2_dataflash_read_Mbit();
void gp2_dataflash_write(int page, int size, unsigned char *buffer);
void gp2_dataflash_open();
void gp2_dataflash_read(int page, int size, unsigned char *buffer);

#define v1o_CS   PORTFbits.RF0 //CSB

/**************************************************************/



// This became a variable so we can use different AT45xxx chips
int MAX_PAGE = 4095;
int PAGE_SIZE;

int START_LOG_PAGE = 5;
int	LOG_INDEX_PAGE = 4;
int	CONFIGURATION_PAGE = 0;
int	NAVIGATION_PAGE = 2;

#define STATUS_RDY 0b10000000


/**
 *   Initializes the SPI hardware
 */
void dataflash_open()
{
    if (HARDWARE_VERSION < V01Q)
    {
        dataflash.read_Mbit = gp1_dataflash_read_Mbit;
        dataflash.write = gp1_dataflash_write;
        dataflash.open = gp1_dataflash_open;
        dataflash.read = gp1_dataflash_read;
        gp1_dataflash_open();
    }
    else if (HARDWARE_VERSION == V01Q)
    {
        dataflash.read_Mbit = gp2_dataflash_read_Mbit;
        dataflash.write = gp2_dataflash_write;
        dataflash.open = gp2_dataflash_open;
        dataflash.read = gp2_dataflash_read;
        gp2_dataflash_open();
    }
}

/************************************ OLD GP1 *******************************/

void gp1_dataflash_open()
{
	TRISFbits.TRISF0 = 0;	// make this an output
	TRISGbits.TRISG6 = 0;	// make this an output
	TRISGbits.TRISG8 = 0;	// make this an output
	TRISGbits.TRISG9 = 0;   // SS2 -> voor SCP1000
	TRISGbits.TRISG7 = 1;	// make this an input
	
	// Open hardware SPI, as fast as possible, don't use hardware SS2
	/*OpenSPI2(ENABLE_SCK_PIN & ENABLE_SDO_PIN & SPI_MODE16_OFF & SPI_SMP_OFF & SPI_CKE_OFF &
	         SLAVE_ENABLE_OFF & MASTER_ENABLE_ON & PRI_PRESCAL_4_1 & SEC_PRESCAL_1_1,
	         FRAME_ENABLE_OFF, SPI_ENABLE & SPI_RX_OVFLOW_CLR); 
*/
	gp1_dataflash_disable_spi();
	
	switch (gp1_dataflash_read_Mbit())
	{
		case 8:
			MAX_PAGE = 4095;
			break;
		case 7:
			MAX_PAGE = 4095;
			break;
		case 6:    // This 16Mbit chip is the default
			MAX_PAGE = 4095;
			PAGE_SIZE = 528;
			START_LOG_PAGE = 5;
			LOG_INDEX_PAGE = 4;
			CONFIGURATION_PAGE = 0;  // page 1=reserve
			NAVIGATION_PAGE = 2;  // page 3=reserve
			break;
		case 5:
			START_LOG_PAGE = 9;
			LOG_INDEX_PAGE = 8;
			CONFIGURATION_PAGE = 0;  // page 1+2+3=reserve
			NAVIGATION_PAGE = 4;  // page 6+7=reserve

			MAX_PAGE = 4095;
			PAGE_SIZE = 264;
			break;
		default:
			MAX_PAGE = 4095;
			break;
	}	
}



int gp1_dataflash_read_Mbit()
{
	gp1_dataflash_disable_spi();
	
	microcontroller_delay_us(1);
	
	gp1_dataflash_enable_spi();
	
	// Write to buffer 1
	gp1_spi_comm(0x9F);
	/*int manu = */gp1_spi_comm(0x00);
	int size = gp1_spi_comm(0x00) & 31;
	gp1_spi_comm(0x00);
	gp1_spi_comm(0x00);

	gp1_dataflash_disable_spi();
	
	return size;
}	


int gp1_dataflash_read_status()
{
	gp1_dataflash_disable_spi();
	
	microcontroller_delay_us(1);
	
	gp1_dataflash_enable_spi();
	
	// Write to buffer 1
	gp1_spi_comm(0xD7);
	return gp1_spi_comm(0x00);
}	

inline void gp1_dataflash_enable_spi()
{
    if (HARDWARE_VERSION < V01Q)
        PORTGbits.RG9 = 1;  // disable the CS for the sc1000 pressure sensor, which shared this SPI connection.
	v1o_CS = 0;
}

inline void gp1_dataflash_disable_spi()
{
	v1o_CS = 1;
}

/*!
 *    SPI communication basic operation using hardware SPI.
 */
unsigned char gp1_spi_comm_hw(unsigned char add)
{
	unsigned int i;

INTERRUPT_PROTECT (
	while(SPI2STATbits.SPITBF) ;
	
	i = SPI2BUF;
	
	// Take a look at the Errata for this microcontroller to see why we need to wait for the flags
	SPI2BUF = add;
	while(SPI2STATbits.SPITBF) ;
	while(! SPI2STATbits.SPIRBF) ;
	i = SPI2BUF;
)
	return i;
}


/*!
 *    SPI communication basic operation using bit-banged software SPI.
 *    0.00402s for 512 bytes!
 */
unsigned char gp1_spi_comm_bitbang(unsigned char outgoing_byte)
{
    unsigned char incoming_byte = 0, x;
    

    /*for(x = 0 ; x < 8 ; x++)
    {
        PORTGbits.RG6 = 0; //Toggle the SPI clock

        PORTGbits.RG8 = outgoing_byte >> 7; //Put bit on SPI data bus , SDO
        outgoing_byte <<= 1; //Rotate byte 1 to the left

		asm("nop");asm("nop");asm("nop");
		
        PORTGbits.RG6 = 1;

        incoming_byte <<= 1; //Rotate byte 1 to the left
        incoming_byte |= PORTGbits.RG7 & 0x01; //Read bit on SPI data bus, SDI
		asm("nop");asm("nop");asm("nop");
    }*/

    PORTGbits.RG6 = 0;
    for (x = 0 ; x < 8 ; x++)
    {
        PORTGbits.RG8 = outgoing_byte & 0x80; //Put bit on SPI data bus , SDO
        outgoing_byte <<= 1; //Rotate byte 1 to the left
        
        PORTGbits.RG6 = 1;                                         // Raise the clock to clock the data out of the MAX7456
        incoming_byte <<=1;                                       // Rotate the data
        incoming_byte += PORTGbits.RG7;                                // Read the data bit
        PORTGbits.RG6 = 0;                                         // Drop the clock ready for th enext bit
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
void gp1_dataflash_write(int page, int size, unsigned char *buffer)
{
	while ((gp1_dataflash_read_status()  & STATUS_RDY) == 0)
		;
		
	gp1_dataflash_write_raw(page, PAGE_SIZE, buffer);
	
	if  (size > PAGE_SIZE)
	{
		while ((gp1_dataflash_read_status()  & STATUS_RDY) == 0)
			;
		gp1_dataflash_write_raw(page+1, PAGE_SIZE, (unsigned char*) &(buffer[PAGE_SIZE]));

        if (size > PAGE_SIZE*2)
            gp1_dataflash_write_raw(page+2, size - PAGE_SIZE*2, (unsigned char*) &(buffer[PAGE_SIZE*2]));
	}	
	/*
	while (size > PAGE_SIZE)
	{
		size -= PAGE_SIZE;
		buffer += PAGE_SIZE;
		page += 1;
		dataflash_write_raw(page, size > PAGE_SIZE ? PAGE_SIZE : size, buffer);
	}*/	
}

	
void gp1_dataflash_write_raw(int page, int size, unsigned char *buffer)
{
	int add1, add2;
	int i;
	add1 = 0;
	add2 = 0;
	
	gp1_dataflash_disable_spi();
	
	microcontroller_delay_us(1);
	
	gp1_dataflash_enable_spi();
	
	// Write to buffer 1
	gp1_spi_comm(0x84);
	gp1_spi_comm(0x00);   // select buffer1
	gp1_spi_comm(0x00);   // select buffer1
	gp1_spi_comm(0x00);   // select buffer1
	
	for (i = 0; i < size; i++)
		gp1_spi_comm(buffer[i]);

	gp1_dataflash_disable_spi();
	
	if (PAGE_SIZE == 528)
	{
		// For a page size of 528 bytes (16Mbit)
		//     12 bits
		// xxPPPPPP|PPPPPPxx|xxxxxxxx
		// Write buffer 1 to memory
		// 3 don't care bits
		// 12 bits for page
		add1 = page;
		add1 >>= 6;
		add2 = page;
		add2 <<= 2;
	}
	else if (PAGE_SIZE == 264)
	{
		// For a page size of 264 bytes (8Mbit)
		//      12 bits
		// xxxPPPPP|PPPPPPPx|xxxxxxxx
		// Write buffer 1 to memory
		// 12 bits for page
		add1 = page;
		add1 >>= 7;
		add2 = page;
		add2 <<= 1;
	}	
	
	
	microcontroller_delay_us(1);
	gp1_dataflash_enable_spi();

	gp1_spi_comm(0x83); // select buffer1
	gp1_spi_comm(add1 & 0xFF); // select buffer1
	gp1_spi_comm(add2 & 0xFF); // select buffer1
	gp1_spi_comm(0x00); // select buffer1
	
	gp1_dataflash_disable_spi();

	// Now he's probably busy writing
}	


/*!
 *   This operation will read a page of maximum 524 byte
 *   Basically, I'm just following the datasheet:
 *      - Command 0xE8 allows us to read continuously from a certain page
 *
 *   Some simple simulations showed that it takes 0.7ms to read a page.
 */
 void gp1_dataflash_read(int page, int size, unsigned char *buffer)
{
	/*if (size > PAGE_SIZE)
	{
		dataflash_read_raw(page, PAGE_SIZE, buffer);
		while (size > PAGE_SIZE)
		{
			size -= PAGE_SIZE;
			buffer += PAGE_SIZE;
			page += 1;
			dataflash_read_raw(page, size > PAGE_SIZE ? PAGE_SIZE : size, buffer);
		}	
	} 
	else*/
	{
		gp1_dataflash_read_raw(page, size, buffer);
	}
}


void gp1_dataflash_read_raw(int page, int size, unsigned char *buffer)
{
	int add1=0, add2=0;
	int i;
	
	// For 528 bytes page size: xxPPPPPP | PPPPPPBB | BBBBBBBB
	// For 264 bytes page size: xxxPPPPP | PPPPPPPB | BBBBBBBB
	if (PAGE_SIZE == 528)
	{
		// Write buffer 1 to memory
		// 3 don't care bits
		// 12 bits for page
		add1 = page;
		add1 >>= 6;
		add2 = page;
		add2 <<= 2;
	} 
	else //if (PAGE_SIZE == 264)
	{
		add1 = page;
		add1 >>= 7;
		add2 = page;
		add2 <<= 1;
	}

	
	gp1_dataflash_disable_spi();
	gp1_dataflash_enable_spi();
	
	gp1_spi_comm(0xE8);
	gp1_spi_comm(add1 & 0xFF);  // 3 addr bytes   : 12 bytes: page - 10 bits: starting address
	gp1_spi_comm(add2 & 0xFF);
	gp1_spi_comm(0);
	
		
	gp1_spi_comm(0);  // 4 don't care bytes
	gp1_spi_comm(0);
	gp1_spi_comm(0);
	gp1_spi_comm(0);

	for (i = 0; i < size; i++)
	{
		buffer[i] = gp1_spi_comm(0x00);
	}	
	
	gp1_dataflash_disable_spi();
}	





/************************************ NEW GP2 *******************************/

void gp2_dataflash_open()
{
	/*TRISGbits.TRISG7 = 0;	// make this an output
	TRISGbits.TRISG6 = 0;	// CLK
	//TRISGbits.TRISG8 = 0;	// make this an output
	TRISGbits.TRISG9 = 0;   // SS
	TRISGbits.TRISG8 = 1;	// make this an input*/

    TRISFbits.TRISF6 = 0;  // SCK1
    TRISFbits.TRISF7 = 0;  // output
    TRISFbits.TRISF0 = 0;  // CS
    TRISFbits.TRISF8 = 1;  // input

	// Open hardware SPI, as fast as possible, don't use hardware SS2
	/*OpenSPI2(ENABLE_SCK_PIN & ENABLE_SDO_PIN & SPI_MODE16_OFF & SPI_SMP_OFF & SPI_CKE_OFF &
	         SLAVE_ENABLE_OFF & MASTER_ENABLE_ON & PRI_PRESCAL_4_1 & SEC_PRESCAL_1_1,
	         FRAME_ENABLE_OFF, SPI_ENABLE & SPI_RX_OVFLOW_CLR);*/

	gp2_dataflash_disable_spi();

	switch (gp2_dataflash_read_Mbit())
	{
		case 8:
			MAX_PAGE = 4095;
			break;
		case 7:
			MAX_PAGE = 4095;
			break;
		case 6:    // This 16Mbit chip is the default
			MAX_PAGE = 4095;
			PAGE_SIZE = 528;
			START_LOG_PAGE = 5;
			LOG_INDEX_PAGE = 4;
			CONFIGURATION_PAGE = 0;  // page 1=reserve
			NAVIGATION_PAGE = 2;  // page 3=reserve
			break;
		case 5:
			START_LOG_PAGE = 9;
			LOG_INDEX_PAGE = 8;
			CONFIGURATION_PAGE = 0;  // page 1+2+3=reserve
			NAVIGATION_PAGE = 4;  // page 6+7=reserve

			MAX_PAGE = 4095;
			PAGE_SIZE = 264;
			break;
		default:
			MAX_PAGE = 4095;
			break;
	}
}



int gp2_dataflash_read_Mbit()
{
	gp2_dataflash_disable_spi();

	microcontroller_delay_us(1);

	gp2_dataflash_enable_spi();

	// Write to buffer 1
	gp2_spi_comm(0x9F);
	/*int manu = */gp2_spi_comm(0x00);
	int size = gp2_spi_comm(0x00) & 31;
	gp2_spi_comm(0x00);
	gp2_spi_comm(0x00);

	gp2_dataflash_disable_spi();
	return size;
}


int gp2_dataflash_read_status()
{
	gp2_dataflash_disable_spi();

	microcontroller_delay_us(1);

	gp2_dataflash_enable_spi();

	// Write to buffer 1
	gp2_spi_comm(0xD7);
	return gp2_spi_comm(0x00);
}

inline void gp2_dataflash_enable_spi()
{
    //if (HARDWARE_VERSION == V01Q)
        PORTFbits.RF0 = 0;
}

inline void gp2_dataflash_disable_spi()
{
	PORTFbits.RF0 = 1;
}

/*!
 *    SPI communication basic operation using hardware SPI.
 */
unsigned char gp2_spi_comm_hw(unsigned char add)
{
	unsigned int i;

INTERRUPT_PROTECT (
	while(SPI2STATbits.SPITBF) ;

	i = SPI2BUF;

	// Take a look at the Errata for this microcontroller to see why we need to wait for the flags
	SPI2BUF = add;
	while(SPI2STATbits.SPITBF) ;
	while(! SPI2STATbits.SPIRBF) ;
	i = SPI2BUF;
)
	return i;
}


/*!
 *    SPI communication basic operation using bit-banged software SPI.
 *    0.00402s for 512 bytes!
 */
unsigned char gp2_spi_comm_bitbang(unsigned char outgoing_byte)
{
    unsigned char incoming_byte = 0, x;
/*
    PORTFbits.RF6 = 0;
    for (x = 0 ; x < 8 ; x++)
    {
        PORTFbits.RF7 = outgoing_byte & 0x80; //Put bit on SPI data bus , SDO
        outgoing_byte <<= 1; //Rotate byte 1 to the left

        PORTFbits.RF6 = 1;                                         // Raise the clock to clock the data out of the MAX7456
        incoming_byte <<=1;                                       // Rotate the data
        incoming_byte += PORTFbits.RF8;                                // Read the data bit
        PORTFbits.RF6 = 0;                                         // Drop the clock ready for th enext bit
    }
 * */


    for(x = 0 ; x < 8 ; x++)
    {
        PORTFbits.RF6 = 0; //Toggle the SPI clock

        PORTFbits.RF7 = outgoing_byte >> 7; //Put bit on SPI data bus , SDO
        outgoing_byte <<= 1; //Rotate byte 1 to the left

		//asm("nop");asm("nop");

        PORTFbits.RF6 = 1;

        incoming_byte <<= 1; //Rotate byte 1 to the left
        incoming_byte |= PORTFbits.RF8 & 0x01; //Read bit on SPI data bus, SDI
		//asm("nop");asm("nop");
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
void gp2_dataflash_write(int page, int size, unsigned char *buffer)
{
	while ((gp2_dataflash_read_status() & STATUS_RDY) == 0)
		;

	gp2_dataflash_write_raw(page, PAGE_SIZE, buffer);

	if  (size > PAGE_SIZE)
	{
		while ((gp2_dataflash_read_status()  & STATUS_RDY) == 0)
			;
		gp2_dataflash_write_raw(page+1, PAGE_SIZE, (unsigned char*) &(buffer[PAGE_SIZE]));

        if (size > PAGE_SIZE*2)
            gp2_dataflash_write_raw(page+2, size - PAGE_SIZE*2, (unsigned char*) &(buffer[PAGE_SIZE*2]));
	}
	/*
	while (size > PAGE_SIZE)
	{
		size -= PAGE_SIZE;
		buffer += PAGE_SIZE;
		page += 1;
		dataflash_write_raw(page, size > PAGE_SIZE ? PAGE_SIZE : size, buffer);
	}*/
}


void gp2_dataflash_write_raw(int page, int size, unsigned char *buffer)
{
	int add1, add2;
	int i;
	add1 = 0;
	add2 = 0;

	gp2_dataflash_disable_spi();

	microcontroller_delay_us(1);

	gp2_dataflash_enable_spi();

	// Write to buffer 1
	gp2_spi_comm(0x84);
	gp2_spi_comm(0x00);   // select buffer1
	gp2_spi_comm(0x00);   // select buffer1
	gp2_spi_comm(0x00);   // select buffer1

	for (i = 0; i < size; i++)
		gp2_spi_comm(buffer[i]);

	gp2_dataflash_disable_spi();

	if (PAGE_SIZE == 528)
	{
		// For a page size of 528 bytes (16Mbit)
		//     12 bits
		// xxPPPPPP|PPPPPPxx|xxxxxxxx
		// Write buffer 1 to memory
		// 3 don't care bits
		// 12 bits for page
		add1 = page;
		add1 >>= 6;
		add2 = page;
		add2 <<= 2;
	}
	else if (PAGE_SIZE == 264)
	{
		// For a page size of 264 bytes (8Mbit)
		//      12 bits
		// xxxPPPPP|PPPPPPPx|xxxxxxxx
		// Write buffer 1 to memory
		// 12 bits for page
		add1 = page;
		add1 >>= 7;
		add2 = page;
		add2 <<= 1;
	}


	microcontroller_delay_us(1);
	gp2_dataflash_enable_spi();

	gp2_spi_comm(0x83); // select buffer1
	gp2_spi_comm(add1 & 0xFF); // select buffer1
	gp2_spi_comm(add2 & 0xFF); // select buffer1
	gp2_spi_comm(0x00); // select buffer1

	gp2_dataflash_disable_spi();

	// Now he's probably busy writing
}


/*!
 *   This operation will read a page of maximum 524 byte
 *   Basically, I'm just following the datasheet:
 *      - Command 0xE8 allows us to read continuously from a certain page
 *
 *   Some simple simulations showed that it takes 0.7ms to read a page.
 */
 void gp2_dataflash_read(int page, int size, unsigned char *buffer)
{
	/*if (size > PAGE_SIZE)
	{
		dataflash_read_raw(page, PAGE_SIZE, buffer);
		while (size > PAGE_SIZE)
		{
			size -= PAGE_SIZE;
			buffer += PAGE_SIZE;
			page += 1;
			dataflash_read_raw(page, size > PAGE_SIZE ? PAGE_SIZE : size, buffer);
		}
	}
	else*/
	{
		gp2_dataflash_read_raw(page, size, buffer);
	}
}


void gp2_dataflash_read_raw(int page, int size, unsigned char *buffer)
{
	int add1=0, add2=0;
	int i;

	// For 528 bytes page size: xxPPPPPP | PPPPPPBB | BBBBBBBB
	// For 264 bytes page size: xxxPPPPP | PPPPPPPB | BBBBBBBB
	if (PAGE_SIZE == 528)
	{
		// Write buffer 1 to memory
		// 3 don't care bits
		// 12 bits for page
		add1 = page;
		add1 >>= 6;
		add2 = page;
		add2 <<= 2;
	}
	else //if (PAGE_SIZE == 264)
	{
		add1 = page;
		add1 >>= 7;
		add2 = page;
		add2 <<= 1;
	}


	gp2_dataflash_disable_spi();
	gp2_dataflash_enable_spi();

	gp2_spi_comm(0xE8);
	gp2_spi_comm(add1 & 0xFF);  // 3 addr bytes   : 12 bytes: page - 10 bits: starting address
	gp2_spi_comm(add2 & 0xFF);
	gp2_spi_comm(0);


	gp2_spi_comm(0);  // 4 don't care bytes
	gp2_spi_comm(0);
	gp2_spi_comm(0);
	gp2_spi_comm(0);

	for (i = 0; i < size; i++)
	{
		buffer[i] = gp2_spi_comm(0x00);
	}

	gp2_dataflash_disable_spi();
}


