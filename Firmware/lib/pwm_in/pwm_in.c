/*
 *   The library decodes the PWM impulses the module receives
 *   from the RC-receiver on PPM1..5. 
 *   This decoding will mainly be done by the Input Capture hardware
 *   of the dsPic. This code uses TIMER3, so make sure your code doesn't
 *   modify the settings of this timer!
 *
 *   This code looks a bit messy because it needs to co-exist with ppm_in.
 *   Basically, pwm_in uses the same interrupt (IC4), but decodes the signal
 *   differently. That's why pwm_in uses the normal vector table, and ppm_in
 *   uses the alternate vector table. On boot, the software chooses wich one to
 *   initialize, based on the configuration.
 *
 */
 
 


#include "microcontroller/microcontroller.h"
#include "ppm_in/ppm_in.h"
#include "pwm_in/pwm_in.h"

// from ppm_in
extern unsigned int servo_pulse_max; 
extern unsigned int servo_pulse_min;

extern unsigned int ppm_in_us_to_raw(unsigned int us);


// Wait for RC-receiver to boot and give PWM pulses
void pwm_in_wait_for()
{
	int i;
	for (i = 0; i < 150; i++) // 25ms * 60 = 1.5 second, which is how long it can take for a 2.4GHz receiver to come online
	{
		uart1_putc('.');
		if (! (ppm.channel[1] > 900 && ppm.channel[1] < 2100)) // valid signal
			microcontroller_delay_ms(25);  // wait for RC-receiver to boot (long enough?)
		else
			break;
	}
	microcontroller_delay_ms(40);  // wait for whole frame to finish (worst case)
}	


void pwm_in_open()
{
	INTCON2bits.ALTIVT = 0;	
	
	ppm.connection_alive = 0;
	T3CONbits.TCS = 0;		// Use internal clock source
    T3CONbits.TCKPS = 0b10;	// Prescale Select 1:64
    PR3 = 0xFFFF;           // Timer 3 uses full 16 bit
    T3CONbits.TON = 1;	    // Enable timer 3

	TRISD |= 0b1111100000000;   // IC4 = RD11 = in

	// Interrupt capture:
	_IC6IF = 0;             // Clear interrupt flag
	_IC6IE = 1;             // Enable interrupts
	IC6CON = 1;             // start module
	IC6CONbits.ICTMR = 0;   // TMR3
	//IC6CONbits.ICI = 0b11;  // Interrupt on every 4th capture event
	IC6CONbits.ICM = 0b001; // Capture every edge 
	_IC6IP = 5;

	_IC5IF = 0;             // Clear interrupt flag
	_IC5IE = 1;             // Enable interrupts
	IC5CON = 1;             // start module
	IC5CONbits.ICTMR = 0;   // TMR3
	//IC5CONbits.ICI = 0b11;  // Interrupt on every 4th capture event
	IC5CONbits.ICM = 0b001; // Capture every edge 
	_IC5IP = 5;
	
	_IC4IF = 0;             // Clear interrupt flag
	_IC4IE = 1;             // Enable interrupts
	IC4CON = 1;             // start module
	IC4CONbits.ICTMR = 0;   // TMR3
	//IC4CONbits.ICI = 0b11;  // Interrupt on every 4th capture event
	IC4CONbits.ICM = 0b001; // Capture every edge 
	_IC4IP = 5;

	_IC3IF = 0;             // Clear interrupt flag
	_IC3IE = 1;             // Enable interrupts
	IC3CON = 1;             // start module
	IC3CONbits.ICTMR = 0;   // TMR3
	//IC3CONbits.ICI = 0b11;  // Interrupt on every 4th capture event
	IC3CONbits.ICM = 0b001; // Capture every edge 
	_IC3IP = 5;

	_IC2IF = 0;             // Clear interrupt flag
	_IC2IE = 1;             // Enable interrupts
	IC2CON = 1;             // start module
	IC2CONbits.ICTMR = 0;   // TMR3
	//IC2CONbits.ICI = 0b11;  // Interrupt on every 4th capture event
	IC2CONbits.ICM = 0b001; // Capture every edge 
	_IC2IP = 5;
	
	_IC1IF = 0;             // Clear interrupt flag
	_IC1IE = 1;             // Enable interrupts
	IC1CON = 1;             // start module
	IC1CONbits.ICTMR = 0;   // TMR3
	//IC1CONbits.ICI = 0b11;  // Interrupt on every 4th capture event
	IC1CONbits.ICM = 0b001; // Capture every edge 
	_IC1IP = 5;
	
	servo_pulse_max = ppm_in_us_to_raw(2300);
	servo_pulse_min = ppm_in_us_to_raw(700);	
}


unsigned int pwm_in_raw_to_us(unsigned int raw)
{
	//raw <<= 2;    // * 4, to prevent losing bits while /8 
	// scale from 625 to 1000
	raw *= 8;
	raw /= 5;
	//raw >>= 2; 
	
	return raw;
}


// shadow: fast context save DONT USE IT HERE!!!
// no_auto_psv: code does not access string literals or const vars
void __attribute__((__interrupt__)) _IC6Interrupt(void)
{
	static volatile unsigned int raw_in, 
	                    last_raw_in = 0,
	                    in;
	
	_IC6IF = 0;		
	raw_in = IC6BUF;
	if (PORTDbits.RD12 == 0)
	{	
		if (last_raw_in < raw_in)
			in = raw_in - last_raw_in;
		else
			in = 0xFFFF - last_raw_in + raw_in;  // 16 bit counter
		
		if (in < servo_pulse_max && in > servo_pulse_min)// && !invalid_pulse)
		{
			ppm.channel[5] = pwm_in_raw_to_us(in);
			ppm.valid_frame = 1;
		}
		else
		{
			ppm.valid_frame = 0;
		}
	}
	else
		last_raw_in = raw_in;
}


void __attribute__((__interrupt__)) _IC5Interrupt(void)
{
	static volatile unsigned int raw_in, 
	                    last_raw_in = 0,
	                    in;
	
	_IC5IF = 0;		
	raw_in = IC5BUF;
	if (PORTDbits.RD12 == 0)
	{	
		if (last_raw_in < raw_in)
			in = raw_in - last_raw_in;
		else
			in = 0xFFFF - last_raw_in + raw_in;  // 16 bit counter
		
		if (in < servo_pulse_max && in > servo_pulse_min)// && !invalid_pulse)
		{
			ppm.channel[4] = pwm_in_raw_to_us(in);
			ppm.valid_frame = 1;
		}
		else
		{
			ppm.valid_frame = 0;
		}
	}
	else
		last_raw_in = raw_in;
}


void __attribute__((__interrupt__)) _IC4Interrupt(void)
{
	static volatile unsigned int raw_in, 
	                    last_raw_in = 0,
	                    in;
	
	_IC4IF = 0;		
	raw_in = IC4BUF;
	if (PORTDbits.RD11 == 0)
	{	
		if (last_raw_in < raw_in)
			in = raw_in - last_raw_in;
		else
			in = 0xFFFF - last_raw_in + raw_in;  // 16 bit counter
		
		if (in < servo_pulse_max && in > servo_pulse_min)// && !invalid_pulse)
		{
			ppm.channel[0] = pwm_in_raw_to_us(in);
			ppm.valid_frame = 1;
		}
		else
		{
			ppm.valid_frame = 0;
		}
	}
	else
		last_raw_in = raw_in;
}


void __attribute__((__interrupt__)) _IC3Interrupt(void)
{
	static volatile unsigned int raw_in, 
	                    last_raw_in = 0,
	                    in;
	
	_IC3IF = 0;		
	raw_in = IC3BUF;
	if (PORTDbits.RD10 == 0)
	{	
		if (last_raw_in < raw_in)
			in = raw_in - last_raw_in;
		else
			in = 0xFFFF - last_raw_in + raw_in;  // 16 bit counter
		
		if (in < servo_pulse_max && in > servo_pulse_min)// && !invalid_pulse)
		{
			ppm.channel[3] = pwm_in_raw_to_us(in);
			ppm.valid_frame = 1;
		}
		else
		{
			ppm.valid_frame = 0;
		}
	}
	else
		last_raw_in = raw_in;
}


void __attribute__((__interrupt__)) _IC2Interrupt(void)
{
	static volatile unsigned int raw_in, 
	                    last_raw_in = 0,
	                    in;
	
	_IC2IF = 0;		
	raw_in = IC2BUF;
	if (PORTDbits.RD9 == 0)
	{	
		if (last_raw_in < raw_in)
			in = raw_in - last_raw_in;
		else
			in = 0xFFFF - last_raw_in + raw_in;  // 16 bit counter
		
		if (in < servo_pulse_max && in > servo_pulse_min)// && !invalid_pulse)
		{
			ppm.channel[2] = pwm_in_raw_to_us(in);
			ppm.valid_frame = 1;
		}
		else
		{
			ppm.valid_frame = 0;
		}
	}
	else
		last_raw_in = raw_in;
}



void __attribute__((__interrupt__)) _IC1Interrupt(void)
{
	static volatile unsigned int raw_in, 
	                    last_raw_in = 0,
	                    in;
	
	_IC1IF = 0;		
	raw_in = IC1BUF;
	if (PORTDbits.RD8 == 0)
	{	
		if (last_raw_in < raw_in)
			in = raw_in - last_raw_in;
		else
			in = 0xFFFF - last_raw_in + raw_in;  // 16 bit counter
		
		if (in < servo_pulse_max && in > servo_pulse_min)// && !invalid_pulse)
		{
			ppm.channel[1] = pwm_in_raw_to_us(in);
			ppm.valid_frame = 1;
		}
		else
		{
			ppm.valid_frame = 0;
		}
	}
	else
		last_raw_in = raw_in;
}
