
// DEFINE TEST if you don't wanna use FreeRTOS with gps.h!

#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "uart2/uart2.h"
#include "gps/gps.h"
#include <stdio.h>


int main()
{
	int last_sentence_number;
	struct gps_info gpsinfo;
	struct GpsConfig config;
	
	config.initial_baudrate = 57600l;
	
	microcontroller_init();
	
	uart1_open(115200l);
	
	microcontroller_delay_ms(10);
	printf("Initializing GPS...");
	gps_init(&config);
	printf("done\n\r");
	
	//gps_wait_for_lock();
	
	while (1)
	{
		if (gps_update_info(&gpsinfo))
		{
			printf ("GPS: %ld %ld [%u] %f %f | %d\n\r", gpsinfo.date, gpsinfo.time, gpsinfo.satellites_in_view, gpsinfo.latitude_rad, gpsinfo.longitude_rad, gpsinfo.height_m);
		}
	}
	return 0;
}
