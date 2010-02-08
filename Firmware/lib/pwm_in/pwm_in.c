


#include "microcontroller/microcontroller.h"
#include "pwm_in/pwm_in.h"


static unsigned int NUM_CHANNELS = 8;

//! The maximum allowed servo pulse, in timer ticks.
unsigned int servo_pulse_max; 
//! The minimum allowed servo pulse, in timer ticks.
unsigned int servo_pulse_min;
//! The minimum length of the PPM sync pulse, in timer ticks.
unsigned int sync_pulse;

//! Global struct that contains the state of the PPM pulses.
volatile struct ppm_info ppm;

float dt_no_valid_frame = 1.0;
#define connection_lost_after_seconds  0.5



void ppm_in_update_status(float dt)
{
	if (ppm.valid_frame && dt_no_valid_frame > 0)
		dt_no_valid_frame -= dt;
	else if (dt_no_valid_frame < connection_lost_after_seconds)
		dt_no_valid_frame += dt;
	
	ppm.connection_alive = (dt_no_valid_frame < connection_lost_after_seconds);
}	

unsigned int ppm_in_us_to_raw(unsigned int us)
{
	//us <<= 2;    // * 4, to prevent losing bits while /8 
	// scale from 625 to 1000
	us *= 5;
	us /= 8;
	//us >>= 2; 
	
	return us;
}


int ppm_signal_quality()
{
	return (int)(dt_no_valid_frame * 20.0);
}
	

unsigned int ppm_in_raw_to_us(unsigned int raw)
{
	//raw <<= 2;    // * 4, to prevent losing bits while /8 
	// scale from 625 to 1000
	raw *= 8;
	raw /= 5;
	//raw >>= 2; 
	
	return raw;
}


void ppm_in_guess_num_channels()
{
	unsigned int i;
	while (! ppm.valid_frame)
	{
		for (i = 4; i <= 12; i++)
			NUM_CHANNELS = i;
		microcontroller_delay_ms(20);
		if (ppm.valid_frame)
		{
			microcontroller_delay_ms(20);
			if (ppm.valid_frame)
				return;
		}	
	}
}	


void ppm_in_open()
{
	ppm.connection_alive = 0;
	T3CONbits.TCS = 0;		// Use internal clock source
    T3CONbits.TCKPS = 0b10;	// Prescale Select 1:64
    PR3 = 0xFFFF;           // Timer 3 uses full 16 bit
    T3CONbits.TON = 1;	    // Enable timer 3

	TRISD |= 0b111100000000;   // IC4 = RD11 = in

	// Interrupt capture:
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
	
	servo_pulse_max = ppm_in_us_to_raw(2200);
	printf ("-> %u\n\r", servo_pulse_max);
	servo_pulse_min = ppm_in_us_to_raw(800);	
	sync_pulse = ppm_in_us_to_raw(4500);
}


// shadow: fast context save DONT USE IT HERE!!!
// no_auto_psv: code does not access string literals or const vars
void __attribute__((__interrupt__)) _IC4Interrupt(void)
{
	static unsigned int raw_in, 
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
			ppm.channel[0] = ppm_in_raw_to_us(in);
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
	static unsigned int raw_in, 
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
			ppm.channel[1] = ppm_in_raw_to_us(in);
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
	static unsigned int raw_in, 
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
			ppm.channel[2] = ppm_in_raw_to_us(in);
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
	static unsigned int raw_in, 
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
			ppm.channel[3] = ppm_in_raw_to_us(in);
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
