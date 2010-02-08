/*! 
 *  @file     uart1.c
 *  @brief    Hardware functions for using the UART1 module
 *  @author   Tom Pycke
 *  @date     23-nov-2008
 *  @since    0.1
 */
 
#include "microcontroller/microcontroller.h"
 
 
void uart1_open(long baud)
{
	// configure U2MODE
	U1MODEbits.UARTEN = 0;	// Bit15 TX, RX DISABLED, ENABLE at end of func
	U1MODEbits.USIDL = 0;	// Bit13 Continue in Idle
	U1MODEbits.IREN = 0;	// Bit12 No IR translation
	U1MODEbits.RTSMD = 1;	// Bit11 Simplex Mode
	U1MODEbits.UEN = 0;		// Bits8,9 TX,RX enabled, CTS,RTS not
	U1MODEbits.WAKE = 0;	// Bit7 No Wake up (since we don't sleep here)
	U1MODEbits.LPBACK = 0;	// Bit6 No Loop Back
	U1MODEbits.ABAUD = 0;	// Bit5 No Autobaud (would require sending '55')
	U1MODEbits.URXINV = 0;	// Bit4 IdleState = 1  (for dsPIC)
	U1MODEbits.BRGH = 0;	// Bit3 16 clocks per bit period
	U1MODEbits.PDSEL = 0;	// Bits1,2 8bit, No Parity
	U1MODEbits.STSEL = 0;	// Bit0 One Stop Bit
	
	// Load a value into Baud Rate Generator.  Example is for 9600.
	// See section 19.3.1 of datasheet.
	//  U2BRG = (Fcy/(16*BaudRate))-1
	//  U2BRG = (40MHz/(16*38400))-1
	//  U2BRG = 65
	
	U1BRG = (int)(FCY / (16*baud) - 1);	
	
	// Load all values in for U1STA SFR
	U1STAbits.UTXISEL1 = 0;	//Bit15 Int when Char is transferred (1/2 config!)
	
	U1STAbits.UTXINV = 0;	//Bit14 N/A, IRDA config
	U1STAbits.UTXISEL0 = 0;	//Bit13 Other half of Bit15
	U1STAbits.UTXBRK = 0;	//Bit11 Disabled
	U1STAbits.UTXEN = 1;	//Bit10 TX pins controlled by periph
	U1STAbits.URXISEL = 0;	//Bits6,7 Int. on character recieved
	U1STAbits.ADDEN = 0;	//Bit5 Address Detect Disabled

	IPC7 = 0x4400;	// Mid Range Interrupt Priority level, no urgent reason

//	IFS1bits.U1TXIF = 0;	// Clear the Transmit Interrupt Flag
	//IEC1bits.U1TXIE = 1;	// Enable Transmit Interrupts
//  IFS1bits.U1RXIF = 0;	// Clear the Recieve Interrupt Flag
	//IEC1bits.U1RXIE = 1;	// Enable Recieve Interrupts

	U1MODEbits.UARTEN = 1;	// And turn the peripheral on

	U1STAbits.UTXEN = 1;
	// I think I have the thing working now.
}


char uart1_dataready()
{
	return U1STAbits.URXDA;
}

	
void uart1_puts(char *str)
{
	while(*str != '\0')
	{
		while(U1STAbits.UTXBF)
			;  /* wait if the buffer is full */
		U1TXREG = *str++;   /* transfer data byte to TX reg */
	}
}

void uart1_putc(char c)
{
	U1TXREG = c;
}

char uart1_getc()
{
	return U1RXREG;
}

