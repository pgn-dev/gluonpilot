/*! 
 *   Test application for the servo lib.
 *   This will make all servos connected to the outputs
 *   move back and forward and then back again!
 */  

#include "microcontroller/microcontroller.h"
#include "servo/servo.h"


int main()
{
	unsigned int i;
	
	microcontroller_init();

	uart1_open(115200l);
	uart1_puts ("starting\r\n");
		
	servo_init();
	
	servo_all_neutral();
	uart1_puts ("neutral\r\n");
	while (1)
	{
		// send servo PWM pulse from 1ms to 2ms (1000us to 2000us)
		for (i = 1000; i < 2000; i++)
		{
			uart1_puts(".\n\r");
			servo_set_us(0, i);
			servo_set_us(1, i);
			//servo_set_ms(2, 0.001 * (float)i);
			servo_set_us(2, i);
			servo_set_us(3, i);
			servo_set_us(4, i);
			servo_set_us(5, i);
			microcontroller_delay_ms(1);
		}	
		for (i = 2000; i > 1000; i--)
		{
			servo_set_us(0, i);
			servo_set_us(1, i);
			servo_set_ms(2, 0.001 * (float)i);
			servo_set_us(3, i);
			servo_set_us(4, i);
			servo_set_us(5, i);
			microcontroller_delay_ms(1);
		}	
	}

}
