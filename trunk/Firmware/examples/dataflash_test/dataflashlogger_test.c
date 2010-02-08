
#include "../microcontroller.h"
#include "../uart1/uart1.h"
#include "../gps/gps.h"
#include "../scp1000/scp1000.h"

#include "dataflashlogger.h"


struct datalog
{
	float latitude_rad;
	float longitude_rad;
	float speed_ms;
	int gps_height;
	float scp_height;
	int  satellites_in_view;
	long date;
	long time;
	unsigned manual:1;
	int pitch_acc; 
	int pitch_filter;
};		



int main()
{
	struct gps_info gpsinfo;
	struct dataflashlogger_info dfli;
	int i;
	unsigned long t;
	struct datalog log;
	
	
	microcontroller_init();
	uart1_open(38400L);
	uart1_puts ("Demo for dataflashlogger\r\n");
	
	dataflash_open();
	
	scp1000_open();
	
	gps_init();
	
	i = 0;
	t = 0;
	char last_char, last_last_char;

	while (t++ < 3000000)
	{
		i--;
		microcontroller_delay_us(1);
		
		if (uart1_dataready())
		{
			char c = uart1_getc();
			if (c == '\n' || c == '\r')
			{
				if (last_char == 'f' && last_last_char == 'f')
				{
					uart1_puts("Formatting flash...\r\n");
					dataflashlogger_format();
					uart1_puts("Done\r\n");
				} 
				else if (last_char == 'f' && last_last_char == 'r')
				{
					uart1_puts("Reading flash...\r\n");
					uart1_puts("HEADER;Date;Time;Satellites;Latitude;Longitude;GPDHeight;GPSSpeed;BaroHeight;Manual;Pitch_acc;Pitch\r\n");
					long last_time = -1;
					dfli.time = 0;
					dataflashlogger_read(&log, &dfli, sizeof(struct datalog));
					do
					{
						printf ("DATA;%ld;%ld;%u;%f;%f;%d;%f;%.1f;%u;%d;%d\r\n", log.date, log.time, log.satellites_in_view, log.latitude_rad*(180.0/3.14159), log.longitude_rad*(180.0/3.14159),log.gps_height, log.speed_ms, log.scp_height, log.manual, log.pitch_acc, log.pitch_filter);
						last_time = dfli.time;
						dataflashlogger_read(&log, &dfli, sizeof(struct datalog));
					} while (last_time == dfli.time);
					//printf ("[%ld] %ld;%ld;%u;%f;%f;%d;%f\r\n", dfli.time, gpsinfo.date, gpsinfo.time, gpsinfo.satellites_in_view, gpsinfo.latitude_rad*(180.0/3.14159), gpsinfo.longitude_rad*(180.0/3.14159), gpsinfo.height_m, gpsinfo.speed_ms);
					uart1_puts("Done\r\n");
				} 
			} 
			else
			{
				last_last_char = last_char;
				last_char = c;
			}	
		}		
	}	
	
	uart1_puts("Start logging...\r\n");
	uart1_puts("Wait for GPS lock...\r\n");
	
	while (1)
	{
		if (gps_update_info(&gpsinfo) && i++ % 5 == 0)
		{
			if (gpsinfo.status == ACTIVE)
				break;
		}	
	}
	
	printf("Opening file %ld %ld\r\n", gpsinfo.date, gpsinfo.time);
	
	dataflashlogger_init(gpsinfo.time, gpsinfo.date);
	
	while (1)
	{
		if (gps_update_info(&gpsinfo) && i++ % 2 == 0)
		{
			printf ("GPS: %ld %ld [%u] %f %f | %d %.1f\r\n", gpsinfo.date, gpsinfo.time, gpsinfo.satellites_in_view, gpsinfo.latitude_rad, gpsinfo.longitude_rad, gpsinfo.height_m, log.scp_height);
			log.date = gpsinfo.date;
			log.gps_height = gpsinfo.height_m;
			log.latitude_rad = gpsinfo.latitude_rad;
			log.longitude_rad = gpsinfo.longitude_rad;
			log.satellites_in_view = gpsinfo.satellites_in_view;
			log.scp_height = scp1000_get_height();
			log.speed_ms = gpsinfo.speed_ms;
			log.time = gpsinfo.time;
			dataflashlogger_add(&log, sizeof(struct datalog));
		}
	}	
	
	return 0;	
}
