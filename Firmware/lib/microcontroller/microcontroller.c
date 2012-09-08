	
#include "microcontroller/microcontroller.h"
#include "uart1_queue/uart1_queue.h"

RCONBITS RCONbits_backup;


_FOSCSEL(FNOSC_PRIPLL);
_FOSC(FCKSM_CSDCMD & OSCIOFNC_OFF  & POSCMD_XT);  

//_FWDT(FWDTEN_OFF);       // Watchdog Timer Enabled/disabled by user software	



void microcontroller_init()
{	
	INTCON2bits.ALTIVT = 0;	 // Don't use alternate interrupt vector
	
	// Disable Watch Dog Timer
	RCONbits.SWDTEN = 0;
		
	/*  Configure Oscillator to operate the device at 80Mhz (40MIPS)
    	Fosc= Fin*M/(N1*N2), Fcy=Fosc/2
    	Fosc= 4M*80/(2*2)=80Mhz for 4MHz input clock */
	CLKDIVbits.PLLPRE = 0;    // N1=2  
	CLKDIVbits.PLLPOST = 0;   // N2=2
	PLLFBDbits.PLLDIV = 78;   // ->F_XT / 2 * (78 + 2) / 2
	
	RCONbits_backup = RCONbits;
	RCON = 0;  // we have to do this to make sure we can read RCON correctly on a (next) reset
}
	


/*!
 *  Delay function
 *  @param ms Number of milliseconds (0.001s) the function needs to wait.
 */
void microcontroller_delay_ms(unsigned long ms)
{
	//__delay32((long)FCY/1000L*ms);
	unsigned long i = FCY/1000/12*ms;
    for(;i > 0 ;i--)
        asm("clrwdt");
}


/*!
 *  Delay function
 *  @param us Number of microseconds (0.000001s) the function needs to wait.
 */
void microcontroller_delay_us(unsigned long us)
{
	long i = FCY/1000/1000/12*us;
    for(;i > 0 ;i--)
        asm("clrwdt");
}


void microcontroller_reset_type()
{
	if (RCONbits_backup.BOR)
	{
		uart1_puts("\r\nA Brown-out Reset has occurred\r\n");
	}
	if (RCONbits_backup.EXTR)
	{
		uart1_puts("\r\nA Master Clear (pin) Reset has occurred\r\n");
	}
	if (RCONbits_backup.IDLE)
	{
		uart1_puts("\r\nDevice was in Idle mode\r\n");
	}
	if (RCONbits_backup.IOPUWR)
	{
		uart1_puts("\r\nAn illegal opcode detection, an illegal address mode or uninitialized W register used as an Address Pointer caused a Reset\r\n");
	}	
	if (RCONbits_backup.POR)
	{
		uart1_puts("\r\nA Power-up Reset has occurred\r\n");
	}
	if (RCONbits_backup.SLEEP)
	{
		uart1_puts("\r\nDevice has been in Sleep mode\r\n");
	}
	if (RCONbits_backup.SWDTEN)
	{
		uart1_puts("\r\nWDT is enabled\r\n");
	}
	if (RCONbits_backup.SWR)
	{
		uart1_puts("\r\nA RESET instruction has been executed\r\n");
	}
	if (RCONbits_backup.TRAPR)
	{
		uart1_puts("\r\nA Trap Conflict Reset has occurred\r\n");
	}
	/*if (RCONbits_backup.VREGS)
	{
		uart1_puts("Voltage regulator is active during Sleep\r\n");
	}*/
	if (RCONbits_backup.WDTO)
	{
		uart1_puts("\r\nWDT time-out has occurred\r\n");
	}										
}
	

int microcontroller_after_reboot()
{
	return !(RCONbits_backup.EXTR || RCONbits_backup.POR); // We are in a reboot if there was no external reset
}


/* ****************************************************************
* Standard Exception Vector handlers if ALTIVT (INTCON2<15>) = 0  *
*                                                                 *
* Not required for labs but good to always include                *
******************************************************************/
#define _trapISR __attribute__((interrupt,no_auto_psv))

void _trapISR _OscillatorFail(void)
{
        INTCON1bits.OSCFAIL = 0;
        uart1_puts("\r\nOscillator error!\n\r");
        //while(1);
}

void _trapISR _AddressError(void)
{
        INTCON1bits.ADDRERR = 0;
        uart1_puts("\r\nAddress error!\n\r");
        //while(1);
        asm("reset");
}

void _trapISR _StackError(void)
{
        INTCON1bits.STKERR = 0;
        uart1_puts("\r\nStack error!\n\r");
        //while(1);
        asm("reset");
}

void _trapISR _MathError(void)
{
        INTCON1bits.MATHERR = 0;
        uart1_puts("\r\nMath error!\n\r");
        //while(1);
        //asm("reset");
}




void __attribute__((interrupt, no_auto_psv)) _AltOscillatorFail(void)
{
        INTCON1bits.OSCFAIL = 0;
        uart1_puts("\r\nOscillator error!\n\r");
        //while(1);
}

void __attribute__((interrupt, no_auto_psv)) _AltAddressError(void)
{
        INTCON1bits.ADDRERR = 0;
        uart1_puts("Address error!\n\r");
        //while(1);
        asm("reset");
}

void __attribute__((interrupt, no_auto_psv)) _AltStackError(void)
{
        INTCON1bits.STKERR = 0;
        uart1_puts("Stack error!\n\r");
        //while(1);
        //asm("reset");
}

void __attribute__((interrupt, no_auto_psv)) _AltMathError(void)
{
        INTCON1bits.MATHERR = 0;
        uart1_puts("Math error!\n\r");
        //while(1);
        //asm("reset");
}
