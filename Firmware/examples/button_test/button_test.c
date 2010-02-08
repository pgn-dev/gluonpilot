/*!
 *   This is a demo for the button library.
 *
 *   Usage: View the output on uart1 and press the button!
 */


// include library files
#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "button/button.h"


int main()
{
	microcontroller_init();
	button_init();
	uart1_open(115200l);
	
	uart1_puts("\n\rTest application for the button on ImuBoard\n\r");
	
	while (1)
	{
		if (button_down())
			uart1_puts("Down\r");	
		else
			uart1_puts("Up  \r");

		microcontroller_delay_ms(100);	
	}	
}
