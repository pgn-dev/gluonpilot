
// DEFINE TEST if you don't wanna use FreeRTOS with gps.h!

/*
 * Used for testing the GPS
 *
 * Use this together with http://realterm.sourceforge.net (@115200 baud)
 *
 * Everything you send to to module is forwarded to the GPS
 * (usefull for sending MTK messages to the unit)
 * (see http://diydrones.com/profiles/blogs/using-the-5hz-locosys-gps-with)
 */

 

#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "uart2/uart2.h"
//#include "gps/gps.h"
#include <stdio.h>


int main()
{
	int last_sentence_number;
	//struct gps_info gpsinfo;
	//struct GpsConfig config;
	
	//config.initial_baudrate =  ;
	
	microcontroller_init();
	
	uart1_open(57600l);
	
	
	microcontroller_delay_ms(10);
	
	printf("\r\nInitializing GPS...");
	//gps_init(&config);
	uart2_open(9600L);
	
	printf("done\n\r");
	
	IFS1bits.U2RXIF = 0;	// Clear the Recieve Interrupt Flag
	//IEC1bits.U2RXIE = 1;
	
	//gps_wait_for_lock();
			
	while (1)
	{
		//if (gps_update_info(&gpsinfo))
		//{
		//	printf ("GPS: %ld %ld [%u] %f %f | %d\r\n", gpsinfo.date, gpsinfo.time, gpsinfo.satellites_in_view, gpsinfo.latitude_rad, gpsinfo.longitude_rad, gpsinfo.height_m);
		//}
		
		/*if (U1STAbits.URXDA)
		{
			U2TXREG = U1RXREG;
			printf(".");
			while(U2STAbits.UTXBF)
				;
		}*/
		while(U2STAbits.URXDA)
			uart1_putc(U2RXREG);	
	}
	return 0;
}
