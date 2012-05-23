
#include "microcontroller/microcontroller.h"
#include "uart1_queue/uart1_queue.h"

#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/task.h"

xQueueHandle xRxedChars;

void uart1_queue_init(long baud)
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

	IEC0bits.U1RXIE = 1;	// Enable Recieve Interrupts

	U1MODEbits.UARTEN = 1;	// And turn the peripheral on

	U1STAbits.UTXEN = 1;
	// I think I have the thing working now.	
	
	
	xRxedChars = xQueueCreate( 150, ( unsigned portBASE_TYPE ) sizeof( char ) );
}	


void __attribute__((__interrupt__, auto_psv)) _U1RXInterrupt( void )
{
	char cChar;
	portBASE_TYPE xHigherPriorityTaskWoken = pdFALSE;

	/* Get the character and post it on the queue of Rxed characters.
	If the post causes a task to wake force a context switch as the woken task
	may have a higher priority than the task we have interrupted. */

	
	while( U1STAbits.URXDA )
	{
		cChar = U1RXREG;
		xQueueSendFromISR( xRxedChars, &cChar, &xHigherPriorityTaskWoken );
	}
	IFS0bits.U1RXIF = 0;
	// NO YIELDING! We are in an interrupt routine, and parsing input is not urgent anyway
	/*if( xHigherPriorityTaskWoken != pdFALSE )
	{
		taskYIELD();
	}*/
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

void uart1_put(char *str, int len)
{
	while(len-- > 0)
	{
		while(U1STAbits.UTXBF)
			;  /* wait if the buffer is full */
		U1TXREG = *str++;   /* transfer data byte to TX reg */
	}
}

void uart1_putc(char c)
{
	while(U1STAbits.UTXBF)
			;
	U1TXREG = c;
}
