/*!
 *  Demo application for the scp1000 sensor.
 *  The sensor is initialized as described in the manual.
 *  Reads temperature, pressure and derived height.
 */


// We use printf
#include <stdio.h>

#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "scp1000/scp1000.h"


int main()
{
	microcontroller_init();	
	uart1_open(115200L);
	scp1000_init();
	
	while(1)
	{
		printf ("Temp: %.1f   Height: %.1f   Pressure: %f [Status %u]\n\r", scp1000_get_temperature(), 
		                                                                    scp1000_get_height(), 
		                                                                    (double)scp1000_get_pressure(), 
		                                                                    scp1000_get_status());
		microcontroller_delay_ms(500);
	}	
	
	return 0;
}
