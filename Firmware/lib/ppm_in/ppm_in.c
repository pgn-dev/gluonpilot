/*
 *   The library decodes the PPM impulse train the module receives
 *   from the RC-receiver. It will also check the validity of the 
 *   frame (same number of channels, all channels within the valid
 *   range, ...).
 *   This decoding will mainly be done by the Input Capture hardware
 *   of the dsPic. This code uses TIMER3, so make sure your code doesn't
 *   modify the settings of this timer!
 *
 */
 
#include <stdio.h>

#include "microcontroller/microcontroller.h"
#include "ppm_in/ppm_in.h"


static unsigned int NUM_CHANNELS = 7; //0..7 = 8

//! The maximum allowed servo pulse, in timer ticks.
unsigned int servo_pulse_max; 
//! The minimum allowed servo pulse, in timer ticks.
unsigned int servo_pulse_min;
//! The minimum length of the PPM sync pulse, in timer ticks.
unsigned int sync_pulse;

//! Global struct that contains the state of the PPM pulses.
volatile struct ppm_info ppm;

volatile unsigned int frame_counter = 0;

float dt_no_valid_frame = 1.0;
int ticks_no_valid_frame = 1;

#define CONNECTION_LOST_AFTER_SECONDS_NO_FRAME  0.5
#define CONNECTION_LOST_AFTER_MS_NO_FRAME  500

unsigned int ppm_in_us_to_raw(unsigned int us);


/**
 *   Opens input capture 4 (PPM1) to capture the pulse train from the receiver
 */
void ppm_in_open()
{
	// Use alternate vector able. (normal vector table is for pwm_in)
	INTCON2bits.ALTIVT = 1;	
		
	ppm.connection_alive = 0;
	T3CONbits.TCS = 0;		// Use internal clock source
    T3CONbits.TCKPS = 0b10;	// Prescale Select 1:64
    PR3 = 0xFFFF;           // Timer 3 uses full 16 bit
    T3CONbits.TON = 1;	    // Enable timer 3

    //         9876543210
	TRISD |= 0b100000000000;   // IC4 = RD11 = in

	// Interrupt capture:
	_IC4IF = 0;             // Clear interrupt flag
	_IC4IE = 1;             // Enable interrupts
	IC4CON = 1;             // start module
	IC4CONbits.ICTMR = 0;   // TMR3
	IC4CONbits.ICI = 0b11;  // Interrupt on every 4th capture event
	IC4CONbits.ICM = 0b010; // Capture falling edge 

	_IC4IP = 5;
	
	servo_pulse_max = ppm_in_us_to_raw(2200);
	servo_pulse_min = ppm_in_us_to_raw(800);	
	sync_pulse = ppm_in_us_to_raw(4500);
	
	ppm.valid_frame = 0;
	ppm.channel[0] = 0;
	ppm.channel[1] = 0;
	ppm.channel[2] = 0;
	ppm.channel[3] = 0;
	ppm.channel[4] = 0;
	ppm.channel[5] = 0;
	ppm.channel[6] = 0;
	ppm.channel[7] = 0;
	
	ppm_in_guess_num_channels();
	//while (ppm.channel[0] == 0 || !ppm.valid_frame) // wait for valid frame
	//	;
}



void ppm_in_update_status(float dt)
{
	if (ppm.valid_frame && dt_no_valid_frame > 0)
		dt_no_valid_frame -= dt;
	else if (dt_no_valid_frame <= CONNECTION_LOST_AFTER_SECONDS_NO_FRAME)
		dt_no_valid_frame += dt;
	
	ppm.connection_alive = (dt_no_valid_frame < CONNECTION_LOST_AFTER_SECONDS_NO_FRAME);
}	


/**
 *   Integer version of ppm_in_update_status()
 */
static unsigned int last_frame_counter = 0;

#define TICKS_FOR_NO_CONNECTION CONNECTION_LOST_AFTER_MS_NO_FRAME/20
void ppm_in_update_status_ticks_50hz()
{
    if (frame_counter == last_frame_counter)
    {
        if (ticks_no_valid_frame < TICKS_FOR_NO_CONNECTION)
        {
            ticks_no_valid_frame++;
        }
    }
    else
    {
        last_frame_counter = frame_counter;
        if (ticks_no_valid_frame > 0)
            ticks_no_valid_frame--;
    }

    
	// PPM
	/*if (ppm.valid_frame && ticks_no_valid_frame > 0)
	{
		ticks_no_valid_frame -= 1;
	} else if (!ppm.valid_frame && ticks_no_valid_frame < TICKS_FOR_NO_CONNECTION)
		ticks_no_valid_frame += 1;
	*/
	ppm.connection_alive = (ticks_no_valid_frame < TICKS_FOR_NO_CONNECTION);
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
	return ticks_no_valid_frame; //(int)(dt_no_valid_frame * 20.0);
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


/**
 *   Guesses the number of channels in the PPM pulse train.
 *
 *   Times out after about 2 seconds
 */
void ppm_in_guess_num_channels()
{
	unsigned int i;
	unsigned int tries = 0;
	
	while (! ppm.valid_frame && tries < 6)
	{
		tries++;
		for (i = 4; i <= 12; i++)
		{
			NUM_CHANNELS = i;
			microcontroller_delay_ms(50);
			if (ppm.valid_frame)
			{
				microcontroller_delay_ms(24);
				if (ppm.valid_frame)
				{
					printf ("channels: %d", NUM_CHANNELS+1);
					return;
				}	
			}	
		}	
	}
	printf("timeout...");
}	


// shadow: fast context save DONT USE IT HERE!!!
// no_auto_psv: code does not access string literals or const vars
/*!
 *  Interrupt routing that decodes the PPM signal.
 *  This routing is called on every 4th falling edge, and this to save
 *  processing power. The PPM frame is accepted when:
 *    - Every channel is between a certain minimum and maximum
 *    - If it ends with a synch pulse
 *    - If it contains a predefined number of channels.
 *
 *  If these conditions are not met, the frame is discared. The results
 *  are saved in the global ppm_info struct.
 *
 *
 *  This routine uses the alternate interrupt vector table pwm_in uses the normal one.
 */
void __attribute__((__interrupt__, __auto_psv__)) _AltIC4Interrupt(void)
{
	static volatile unsigned int counter = 0;
	static volatile unsigned char invalid_pulse = 1;
	static volatile int ppm_in[14];

	unsigned int raw_in, 
	             last_raw_in = 0,
	             in;
	TMR3 = 0;
	_IC4IF = 0;		
	while (IC4CONbits.ICBNE)
	{
		raw_in = IC4BUF;
		
		if (last_raw_in < raw_in)
			in = raw_in - last_raw_in;
		else
			in = 0xFFFF - last_raw_in + raw_in;
		
		if (in > sync_pulse)
		{
			// this is a valid frame if 
			//  - the number of received channels is the same as last time
			//  - no invalid pulses encountered
			ppm.valid_frame = (NUM_CHANNELS == counter) && !invalid_pulse;
			counter = 0;
			invalid_pulse = 0;
			if (ppm.valid_frame) //
            {
				for (counter=0; counter < NUM_CHANNELS; counter++)
					ppm.channel[counter] = ppm_in[counter];
                frame_counter++;
            }
			counter = 0;

		}
		else if (in < servo_pulse_max && in > servo_pulse_min && !invalid_pulse)
		{
			if (ppm.valid_frame) 
			{ // last frame was valid?
				ppm_in[counter] = ppm_in_raw_to_us(in);
			}
			counter++;
		} else
		{
			counter++;
			invalid_pulse = 1;
			ppm.valid_frame = 0;
		}
			
		if (counter > NUM_CHANNELS)
		{
			counter = 0;
			invalid_pulse = 1;
			ppm.valid_frame = 0;
		}
			
		last_raw_in = raw_in;
	}	
	
}
