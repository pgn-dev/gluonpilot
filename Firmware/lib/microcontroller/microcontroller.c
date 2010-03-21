	
#include "microcontroller/microcontroller.h"

RCONBITS RCONbits_backup;


_FOSCSEL(FNOSC_PRIPLL);
_FOSC(FCKSM_CSDCMD & OSCIOFNC_OFF  & POSCMD_XT);  

//_FWDT(FWDTEN_OFF);       // Watchdog Timer Enabled/disabled by user software	




void microcontroller_init()
{	
	INTCON2bits.ALTIVT = 0;	 // Don't use alternate interrupt vector
	
	// Disable Watch Dog Timer
	RCONbits.SWDTEN=0;
	
	
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
        printf("Oscillator error!\n\r");
        //while(1);
}

void _trapISR _AddressError(void)
{
        INTCON1bits.ADDRERR = 0;
        printf("Address error!\n\r");
        //while(1);
        asm("reset");
}

void _trapISR _StackError(void)
{
        INTCON1bits.STKERR = 0;
        printf("Stack error!\n\r");
        //while(1);
        asm("reset");
}

void _trapISR _MathError(void)
{
        INTCON1bits.MATHERR = 0;
        printf("Math error!\n\r");
        //while(1);
        asm("reset");
}
