
#include <stdio.h>
#include <i2c.h> 

#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"

#define HMC6352_READ_ADDR  0x43
#define HMC6352_WRITE_ADDR 0x42


// iniate a start condition on bus
void i2c_start(void)
{
	int x = 0;
//	I2C1CONbits.ACKDT = 0; //Reset any previous Ack
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
	I2C1BRG = 0x0188; //40 mips 100khz
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

//function reads one byte, returns the read data
unsigned int i2c_read_2bytes(void)
{
	int i = 0;
	int data = 0;
	//set I2C module to receive
	I2C1CONbits.RCEN = 1;
AckI2C1();
	//if no response, break
	while (!I2C1STATbits.RBF)
	{
		i ++; 
		if (i > 2000) break; 
	}
	//get data from I2CRCV register
	data = I2CRCV;
	data <<= 8;
	

// from http://chungyan5.no-ip.org/vc/linlike8/trunk/arch/dsPic/drivers/i2c.c?view=markup&root=freertos_posix&pathrev=726
IdleI2C1();
	I2C1CONbits.ACKDT = 0;
	I2C1CONbits.ACKEN = 1;		//Send Acknowledgement
	IdleI2C1();					//I2C bus at idle state, awaiting transimission
	//Receive Low Byte===============================================================
    I2C1CONbits.RCEN = 1;		//Enable Receive
    while(I2C1CONbits.RCEN);


	data += I2CRCV;

	//return data
	return data;
}


void start_calibration()
{
	i2c_start();
	
	send_i2c_byte(HMC6352_WRITE_ADDR);
	send_i2c_byte('C');

	i2c_restart();
}

void stop_calibration()
{
	i2c_start();
	
	send_i2c_byte(HMC6352_WRITE_ADDR);
	send_i2c_byte('E');

	i2c_restart();
}

void AckI2C1(void)
{
	I2C1CONbits.ACKDT = 0;
	I2C1CONbits.ACKEN = 1;
}
void NotAckI2C1(void)
{
    I2C1CONbits.ACKDT = 1;
    I2C1CONbits.ACKEN = 1;
}
void StopI2C1(void)
{
     I2C1CONbits.PEN = 1;	/* initiate Stop on SDA and SCL pins */
}
void IdleI2C1(void)
{
    /* Wait until I2C Bus is Inactive */
    while(I2C1CONbits.SEN || I2C1CONbits.PEN || I2C1CONbits.RCEN || 
          I2C1CONbits.RSEN || I2C1CONbits.ACKEN || I2C1STATbits.TRSTAT);	
}
char WriteI2C1(unsigned char data_out)
{
    I2C1TRN = data_out;

    if(I2C1STATbits.IWCOL)        /* If write collision occurs,return -1 */
        return -1;
    else
    {
        return 0;
    }
}
unsigned char ReadI2C1(void)
{
    I2C1CONbits.RCEN = 1;
    while(I2C1CONbits.RCEN);
    I2C1STATbits.I2COV = 0;
    return(I2C1RCV);
}


void main()
{
	int i = 0;

	microcontroller_init();
	uart1_open(115200l);

	printf("Starting...\r\n");
	
	InitI2C();

	//start_calibration();
	//for(i=0; i < 20; i++)
	//{
	//	microcontroller_delay_ms(1000);
	//	uart1_putc('.');
	//}
	//stop_calibration();

	while (1)
	{
		unsigned int temp;
		/*StartI2C1(); 
        IdleI2C1(); 

        WriteI2C1(0x42);    //Send address 
        //IdleI2C(); 

        WriteI2C1(0x41);    //Send command "A" 
        //IdleI2C(); 

        StopI2C1(); 
        microcontroller_delay_ms(10);    //wait at least 6000 uS 

        StartI2C1();        
        IdleI2C1(); 

        WriteI2C1(0x43);    //Write read command 
        //IdleI2C(); 
        
        temp = ReadI2C1() << 8;    //Read first byte 
        IdleI2C1(); 

        AckI2C1(); 
        IdleI2C1(); 

        temp += ReadI2C1();    //Read second byte 
        //IdleI2C1(); 
        //AckI2C(); 
        //IdleI2C(); 
        //NotAckI2C1(); 
        //IdleI2C1(); 
        
        //StopI2C1(); */

i2c_start();
send_i2c_byte(HMC6352_WRITE_ADDR);
send_i2c_byte('A');
i2c_restart();
microcontroller_delay_ms(10);  
send_i2c_byte(HMC6352_READ_ADDR);
temp = i2c_read_2bytes();
//AckI2C1(); 
	reset_i2c_bus();
	 	printf("-> %u\r\n", temp/10);
		microcontroller_delay_ms(500);
	}

}
