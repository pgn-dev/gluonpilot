/*!
 *   Implements the servo PWM signal (pulse between 1ms and 2ms with a period of 20ms)
 *   using the dsPic's Output Capture module.
 *   This code uses TIMER2 as a reference, make sure not to reconfigure this timer
 *   when using this library!
 */
 

#include "microcontroller/microcontroller.h"
#include "servo/servo.h"


#define servo_raw_1ms 625
#define servo_raw_2ms 1250

void servo_turbopwm()
{
	PR2 = FCY/50/8;         //= 400Hz = 0.0025ms period
}	


void servo_init()
{	
	T2CONbits.TCS = 0;	     // Use internal clock source
    T2CONbits.TCKPS = 0b10;  // Prescale Select 1:64  -> 1,6us
    T2CONbits.TON = 1;	
	
	PR2 = FCY/50/64;         // period = 0.02s

	
	OC1R = 0;  // start pulse
	OC1CONbits.OCTSEL = 0;   // use TMR2
	OC1CONbits.OCM = 6;

	OC2R = 0;
	OC2CONbits.OCTSEL = 0;
	OC2CONbits.OCM = 6;

	OC3R = 0;
	OC3CONbits.OCTSEL = 0;
	OC3CONbits.OCM = 6;

	OC4R = 0;
	OC4CONbits.OCTSEL = 0;
	OC4CONbits.OCM = 6;

	OC5R = 0;
	OC5CONbits.OCTSEL = 0;
	OC5CONbits.OCM = 6;
	
	OC6R = 0;
	OC6CONbits.OCTSEL = 0;
	OC6CONbits.OCM = 6;
}


void servo_all_neutral()
{
	servo_set_us(1, 1500);
	servo_set_us(2, 1500);
	servo_set_us(3, 1500);
	servo_set_us(4, 1500);
	servo_set_us(5, 1500);
	servo_set_us(6, 1500);
}


/*!
 *  Helper function to convert milliseconds to raw timer ticks.
 *  @param us microseconds.
 *  @returns Raw timer ticks from us.
 */
unsigned int servo_us_to_raw(unsigned int us)
{
	us <<= 2;    // * 4, to prevent losing bits while /8 
	
	// scale from 625 to 1000
	us *= 5;
	us /= 8;
	
	us >>= 2; 
	
	return us;
}


/*!
 *  Assigns a new value to the servo-output buffer.
 *  @param index Servo number (1 to 5)
 *  @param us    Number of microseconds the PWM pulse to the servo must 
 *               last (preferable between 1000 and 2000).
 */
void servo_set_us(int servo, unsigned int us)
{
	unsigned int raw = servo_us_to_raw(us);
	
	switch (servo)
	{
		case 0: 
			OC1RS = raw;
			break;
		case 1: 
			OC2RS = raw;
			break;
		case 2: 
			OC3RS = raw;
			break;
		case 3: 
			OC4RS = raw;
			break;
		case 4: 
			OC5RS = raw;
			break;
		case 5:
			OC6RS = raw;
			break;
		default:
			break;
	}	
}	


/*!
 *  Assigns a new value to the servo-output buffer.
 *  @param index Servo number (1 to 5)
 *  @param ms    Number of microseconds the PWM pulse to the servo must 
 *               last (preferable between 1.0 and 2.0).
 */
void servo_set_ms(int servo, float ms)
{
	servo_set_us(servo, (unsigned int)(ms*1000.0));
}
