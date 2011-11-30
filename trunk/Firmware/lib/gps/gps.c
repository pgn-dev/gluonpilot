/*
 *   This code parses the GPS NMEA lines it receives on uart2.
 *   All this is done as efficient as possible on order to use
 *   at least possible cycles.
 *
 *   There are 2 ways to use this:
 *     - Manually calling gps_update_info() to see whether a new 
 *       GPS line has been received. Make sure to enable the "TEST"
 *       preprocessor define.
 *     - Using FreeRTOS and blocking the task on xGpsSemaphore. Make
 *       sure to disable the "TEST" preprocessor define.
 *
 */


#include <math.h>

#include "gps/gps.h"
#include "microcontroller/microcontroller.h"
#include "uart2/uart2.h"
#include "uart1_queue/uart1_queue.h"


#define KTS2MS       0.5144f                // knots to meter per second
#define RAD2DEG      57.29577951308232     // radians to degrees 
#define DEG2RAD      0.01745329251994      // degrees to radians

#ifndef TEST
	#include "FreeRTOS/FreeRTOS.h"
	#include "FreeRTOS/semphr.h"

	extern xSemaphoreHandle xGpsSemaphore;
#endif


int rmc_sentence_number = -1;  // making these volatile gives me an address error
int gga_sentence_number = -1;
volatile char nmea_buffer_RMC[100];
volatile int  nmea_buffer_RMC_counter = 0;

volatile char nmea_buffer_GGA[100];
volatile int  nmea_buffer_GGA_counter = 0;


//! Contains the state of the RMC parser.
// state   1234567                                                                98|99|100
//         $GPRMC,235955.505,V,8960.000000,N,00000.000000,E,0.000,0.00,050180,,,N*40 
volatile unsigned int state = 0;
//! Contains the current checksum of the RMC sentence.
volatile unsigned int checksum = 0;


void gps_init(struct GpsConfig *gpsconfig)
{
	gps_open_port(gpsconfig);
		
	// Wait for GPS output. On some old EB85 devices, this can take over 2sec
	if (rmc_sentence_number == -1 && nmea_buffer_RMC_counter == 0)
		microcontroller_delay_ms(10);
	if (rmc_sentence_number == -1 && nmea_buffer_RMC_counter == 0)
		microcontroller_delay_ms(50);
	if (rmc_sentence_number == -1 && nmea_buffer_RMC_counter == 0)
		microcontroller_delay_ms(100);
	if (rmc_sentence_number == -1 && nmea_buffer_RMC_counter == 0)
		microcontroller_delay_ms(200);		
	if (rmc_sentence_number == -1 && nmea_buffer_RMC_counter == 0)
		microcontroller_delay_ms(400);	 // 760ms
	if (rmc_sentence_number == -1 && nmea_buffer_RMC_counter == 0)
		microcontroller_delay_ms(800);	 // 1560ms
	if (rmc_sentence_number == -1 && nmea_buffer_RMC_counter == 0)
		microcontroller_delay_ms(1000);	 // 2560ms

	if (rmc_sentence_number == -1 && nmea_buffer_RMC_counter == 0)
	{
		uart1_puts("timeout...");
		uart2_open(115200l);
		return;	// Don't return? If module was rebooted, but GPS not, then we need to change the speed for the GPS module to become accessible
	}

	// First we configure which sentences we want. If the unit outputs all sentences at 5Hz by default, then 38400 will be too slow 
	// and the unit won't allow us to change the baudrate.
	
	gps_config_output();
}


// Valid frames received/receiving if RMC counter >= 0
int gps_valid_frames_receiving()
{
	return rmc_sentence_number != -1 || nmea_buffer_RMC_counter != 0;
}	


// Open the GPS uart port and start receiving messages
void gps_open_port(struct GpsConfig *gpsconfig)
{
	//uart2_open(4800l);    // For Locosys module
	//uart2_open(38400l);    // For Old EB-85
	//uart2_open(38400l);    // For San Jose EB-85
	
	uart2_open(gpsconfig->initial_baudrate);


	IFS1bits.U2RXIF = 0;	// Clear the Recieve Interrupt Flag
	IEC1bits.U2RXIE = 1;
}	


// Configs the GPS using MTK sentences to use RMC & GGA sentences at 5Hz, and switch to 115200 baud
void gps_config_output()
{
	// Change to 115200 baud
	
	uart2_puts("$PMTK251,115200*1F\r\n");  // this can take a while if no GPS is connected
	microcontroller_delay_ms(10);
	uart2_open(115200l);
	
	// only RMC and GGA
	// RMC & GGA
	uart2_puts("$PMTK314,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0*28\r\n");  // this can take a while if no GPS is connected
	microcontroller_delay_ms(10);	

	// 5Hz mode
	microcontroller_delay_ms(10);
	uart2_puts("$PMTK220,200*2C\r\n");
}


void gps_wait_for_lock()
{
	struct gps_info gpsinfo;
	while (1)
	{
		if (gps_update_info(&gpsinfo))   // We got a new GPS reading
		{
			if (gpsinfo.status == ACTIVE)
				return; // OK, we got a lock
		}	
	}	
}


/*!
 *  Helper function to convert a character in hexadecimal notation to 
 *  an integer.
 */
int hexchar2int(char c)
{
	if (c >= '0' && c <= '9')
		return (c-'0');
	else if (c >= 'A' && c <= 'F')
		return (c-'A'+10);
	else if (c >= 'a' && c <= 'f')
		return (c-'a'+10);
	else
		return 0;
}


/*!
 *  Hot restarts the GPS module.
 */
void gps_hot_restart()
{
	uart2_puts("$PMTK101*32");
	uart2_putc(0x0D); uart2_putc(0x0A);
}


/*!
 *  Helper function to read a positive long variable.
 *  @param str Pointer to a string starting with a long number.
 */
long read_positive_long (const char *str)
{
	int i = 0;
	long result = 0;
	char c;
	while (1)
	{
		c = str[i++];
		if (c >= '0' && c <= '9')
			result = result*10 + (long)(c-'0');
		else 
			break;
	}
	return result;
}


/*!
 *  Helper function to read a positive floating point variable.
 *  @param str Pointer to a string starting with a positive number.
 */
float read_positive_float (const char *str)
{
	int i = 0;
	float integer = 0,
	      fract = 0,
	      mantissa = 10.0;
	char c;
	
	while (1)
	{
		c = str[i++];
		if (c >= '0' && c <= '9')
			integer = integer*10.0 + (float)(c-'0');
		else 
			break;
	}
	if (c == '.')
	{
		while (1)
		{
			c = str[i++];
			if (c >= '0' && c <= '9')
				fract = fract + ((float)(c-'0')) / mantissa;
			else 
				break;
			mantissa *= 10.0;
		}
	}
	
	return integer  + fract;
}


/*!
 *  Helper function to read a positive double point variable.
 *  @param str Pointer to a string starting with a positive number.
 */
double read_positive_double (const char *str)
{
	int i = 0;
	double integer = 0,
	       fract = 0,
	       mantissa = 10.0;
	char c;
	
	while (1)
	{
		c = str[i++];
		if (c >= '0' && c <= '9')
			integer = integer*10.0 + (double)(c-'0');
		else 
			break;
	}
	if (c == '.')
	{
		while (1)
		{
			c = str[i++];
			if (c >= '0' && c <= '9')
				fract = fract + ((double)(c-'0')) / mantissa;
			else 
				break;
			mantissa *= 10.0;
		}
	}
	
	return integer  + fract;
}


/*!
 *  Convert default NMEA latitude or langitude to radians.
 *  NMEA positions are formatted as 4916.46 when the actual position
 *  is 49° 16.45'. This function transforms this to radians (49.2711666/180.0/3.14159).
 *  @param p NMEA-formatted position.
 *  @return p converted to radians.
 */
double position_NMEA_to_rad(double p)
{
	double minutes_decimal = fmod(p, 100.0) / 60.0;
	double degrees = floor(p/100.0);
	
	return (degrees + minutes_decimal) * DEG2RAD;
}




/*!
 *  When the GPS input buffer contains a new RMC sentence, this function
 *  parses the sentence and stores it in the gps_info struct.
 *  @param gpsinfo The struct where the parsed information needs to be saved to.
 *  @return 1 if the RMC-status field is "Active".
 *          0 when there is no new GPS data available.
 */
char gps_update_info(struct gps_info *gpsinfo)
{
	static int last_call_rmc_sentence_number = -1; // the sentence number
	static int last_call_gga_sentence_number = -1; // the sentence number
	
	if (gga_sentence_number != last_call_gga_sentence_number)
	{
		//  $GPGGA,110917.000,5051.0242,N,00340.1555,E,1,6,1.16,41.5,M,47.3,M,,*65
		//printf("new gga");
		char *stringpointer = (char*) nmea_buffer_GGA;   // overriding the volatileness
		
		//Time
		while (*(stringpointer++) != ',') ;
		// Lat
		while (*(stringpointer++) != ',') ;
		// Lat sector
		while (*(stringpointer++) != ',') ;
		// Lng
		while (*(stringpointer++) != ',') ;
		// Lng sector
		while (*(stringpointer++) != ',') ;
		// GPS quality
		while (*(stringpointer++) != ',') ;
		// Satellites in view
		gpsinfo->satellites_in_view = (int)read_positive_long(stringpointer);
		while (*(stringpointer++) != ',') ;
		// HDOP
		while (*(stringpointer++) != ',') ;
		// Altitude
		gpsinfo->height_m = (int)read_positive_long(stringpointer);
		
		// We don't need anything else!
		
		last_call_gga_sentence_number = gga_sentence_number;
		
		return 0;   // only do this on RMC
	}	
	else if (rmc_sentence_number != last_call_rmc_sentence_number)  // not really an else...
	{
		char *stringpointer = (char*) nmea_buffer_RMC;  // overriding the volatileness
		
		gpsinfo->time = read_positive_long(stringpointer);
		
		while (*(stringpointer++) != ',') 
			;
	
		// now i is at the status field
		if (*(stringpointer) == 'A')
		{
			gpsinfo->last_fix_time = gpsinfo->time;
			gpsinfo->sentence_number_last_fix = rmc_sentence_number;
			gpsinfo->status = ACTIVE;
		}
		else
		{
			gpsinfo->status = VOID;	
			
			// !!!!! ONLY DO THIS FOR TESTING !!!!!!
			//gpsinfo->sentence_number_last_fix = rmc_sentence_number;
		}
		
		while (*(stringpointer++) != ',') 
			;
				
		gpsinfo->latitude_rad = position_NMEA_to_rad(read_positive_double(stringpointer));
		
		while (*(stringpointer++) != ',') 
			;
		
		// N or S
		if (*(stringpointer) == 'S')
			gpsinfo->latitude_rad *= -1.0;
			
		while (*(stringpointer++) != ',') 
			;
			
		gpsinfo->longitude_rad = position_NMEA_to_rad(read_positive_double(stringpointer));
		
		while (*(stringpointer++) != ',') 
			;
		
		if (*(stringpointer) == 'W')
			gpsinfo->longitude_rad *= -1.0;
			
		while (*(stringpointer++) != ',') 
			;
	
		gpsinfo->speed_ms = read_positive_float(stringpointer) * KTS2MS;   // convert knots to meter per second
		
		while (*(stringpointer++) != ',') 
			;
	
		gpsinfo->heading_rad = read_positive_float(stringpointer) * DEG2RAD;
		
		while (*(stringpointer++) != ',') 
			;
	
		gpsinfo->date = read_positive_long(stringpointer);
		
		
		last_call_rmc_sentence_number = rmc_sentence_number;
		return 1;
	}
	else
		return 0;
}


/*!
 *  Interrupt routine notifying us a new character is available from the
 *  GPS's uart module.
 *  This function buffers a valid (structure and checksum) RMC sentence. The
 *  used buffer is nmea_buffer_RMC.
 */
void __attribute__((__interrupt__, __shadow__)) _U2RXInterrupt(void)
{
	unsigned char c = U2RXREG;
	//uart1_putc(c);
	
 
	if (c == '$')   // Beginnng of new sequence
	{
		state = 1;
		checksum = 0;
	} 
	else if (c == '*')
	{
		if (state == 7)
			state = 98;	
		else if (state == 11)
			state = 90;
	} 
	else if (state == 90)
	{
		checksum -= (hexchar2int(c)*16);
		state = 91;			
	}	
	else if (state == 91)
	{
		nmea_buffer_GGA[nmea_buffer_GGA_counter++] = '\0';
		checksum -= (hexchar2int(c));
		if (checksum == 0)
			gga_sentence_number++;
		state = 92;
#ifndef TEST
		static portBASE_TYPE xHigherPriorityTaskWoken = pdFALSE; 
		xSemaphoreGiveFromISR( xGpsSemaphore, &xHigherPriorityTaskWoken );
#endif		
	}	
	else if (state == 98)
	{
		checksum -= (hexchar2int(c)*16);
		state = 99;	
	}
	else if (state == 99)
	{
		nmea_buffer_RMC[nmea_buffer_RMC_counter++] = '\0';
		checksum -= (hexchar2int(c));
		if (checksum == 0)
			rmc_sentence_number++;

		state = 100;

// small test programs don't use FreeRTOS so we need a way to avoid 
#ifndef TEST
		static portBASE_TYPE xHigherPriorityTaskWoken = pdFALSE; 
		xSemaphoreGiveFromISR( xGpsSemaphore, &xHigherPriorityTaskWoken );
#endif
	}
	else 
	{
		checksum ^= c;
		
		switch(state)
		{
			case 1:
				if (c == 'G')
					state = 2;
				break;
			case 2:
				if (c == 'P')
					state = 3;
				break;
			case 3:
				if (c == 'R')
					state = 4;
				else if (c == 'G')
					state = 8;
				break;
			case 4:
				if (c == 'M')
					state = 5;
				break;
			case 5:
				if (c == 'C')
					state = 6;
				break;
			case 6:
				if (c == ',')
				{
					state = 7;
					nmea_buffer_RMC_counter = 0;
				}
				break;
			case 7:
				if (nmea_buffer_RMC_counter < 100)
					nmea_buffer_RMC[nmea_buffer_RMC_counter++] = c;
				break;
			case 8:
				if (c == 'G')
					state = 9;
				break;
			case 9:
				if (c == 'A')
					state = 10;
				break;
			case 10:
				if (c == ',')
					state = 11;
					nmea_buffer_GGA_counter = 0;
				break;
			case 11:
				if (nmea_buffer_GGA_counter < 100)
					nmea_buffer_GGA[nmea_buffer_GGA_counter++] = c;
				break;
			default:
				nmea_buffer_RMC_counter = 0;
				nmea_buffer_GGA_counter = 0;
		}
	}
	_U2RXIF = 0;
}
