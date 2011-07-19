 
#include <math.h>
#include <stdlib.h>

#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

#include "max7456/max7456.h"
#include "button/button.h"
#include "ppm_in/ppm_in.h"
#include "adc/adc.h"

#include "common.h"
#include "configuration.h"
#include "osd.h"
#include "sensors.h"
#include "navigation.h"

int osd_input_available();
int osd_charactermemory_busy();
void max7456_loadchars();
int osd_use_pal();
int osd_use_ntsc();
int osd_initialize(portTickType *xLastExecutionTime);
void osd_set_position(int row, int column);
void osd_write_char(unsigned char x);
void osd_write_string(unsigned char *x);
void osd_write_ascii_char(unsigned char x);
void osd_write_ascii_string(unsigned char *x);
void osd_print_static_data();


// Other characters
#define SATELLITE_1 0xC8
#define SATELLITE_2 0xC9
#define RC_TRANSMITTER 0xB3
#define HOME 0xB4
#define SATELLITE_DISH 0xB6
#define DISTANCE 0x8E
#define HEIGHT 0x8D
#define ANTENNA 0xCA
#define KMH 0xCB
#define METER 0xCC
const unsigned char heading[] = {0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF};
const unsigned char number[] = {0x0A, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};

#define DIR_0 0xA0
#define DIR_22 0xA1
#define DIR_45 0xA2
#define DIR_67 0xA3
#define DIR_90 0xA4
#define DIR_112 0xA5
#define DIR_135 0xA6
#define DIR_157 0xA7
#define DIR_180 0xA8
#define DIR_202 0xA9
#define DIR_225 0xAA
#define DIR_247 0xAB
#define DIR_270 0xAC
#define DIR_292 0xAD
#define DIR_315 0xAE
#define DIR_337 0xAF
#define DIR_360 0xA0

int home_heading_deg = 0;

void osd_print_home_info();
void osd_print_satellites_in_view();
void osd_print_compass();

int use_metric = 1;


void osd_task( void *parameters )
{
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 
	int i;
	
	
	uart1_puts("OSD task initializing...\r\n");
	
	if (osd_initialize(& xLastExecutionTime) == 0)
		vTaskDelete(xTaskGetCurrentTaskHandle());
	
	uart1_puts("OSD initialized\r\n");
	
	i = ppm.channel[config.control.channel_ap];
		
	//while (abs(i - ppm.channel[config.control.channel_ap]) < 200 && !button_down())
	{
		
		
	}
	
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();
	
	osd_print_static_data();
	
	for( ;; )
	{
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 200 / portTICK_RATE_MS ) );   // 5Hz
		spiWriteReg(0x04, 0x04); // clear
		osd_print_artificial_horizon();
		osd_print_home_info();
		//osd_print_static_data();
		osd_print_satellites_in_view();
		osd_print_compass();
	}
}

/*!
 *  Prints static data on the screen
 */
void osd_print_static_data()
{
	// print satellite
	osd_set_position(1, 25);
	osd_write_char(SATELLITE_1);
	osd_set_position(1, 26);
	osd_write_char(SATELLITE_2);
	
	// print home
	osd_set_position(1, 2);
	osd_write_char(HOME);
	
	osd_set_position(1, 6);
	osd_write_char(DISTANCE);
	osd_set_position(1, 14);
	osd_write_char(HEIGHT);
}

#define COMPASS_LINE 12
void osd_print_compass()
{
	int h, x, startheading, endheading, rest;
	int heading = (int)RAD2DEG(sensor_data.gps.heading_rad);
		
	startheading = heading - 90;
	endheading = heading + 90;
	if (startheading < 0)
		startheading += 360;
	if (endheading > 360)
		endheading -= 360;

	for (h = startheading, x = 8; x < 21; h += 15, x++)
	{
		if (h >= 359)
			h -= 360;
		int t = h/15;
		osd_set_position(COMPASS_LINE+1, x);
		if (t == home_heading_deg/15 || t == (home_heading_deg+360)/15)
			osd_write_char(HOME);
		else if (t == 0)
			osd_write_char(0xD3);
		else if (t == 3 || t == 9 || t == 15 || t == 21)
			osd_write_char(0xD7);
		else if (t == 6)
			osd_write_char(0xD5);
		else if (t == 12)
			osd_write_char(0xD4);
		else if (t == 18)
			osd_write_char(0xD6);
		else
			osd_write_char(0xD8);
	}	
	
	x = 13;
	osd_set_position(COMPASS_LINE, x);
	osd_write_char(number[heading/100]);
	osd_set_position(COMPASS_LINE, ++x);
	rest = heading % 100;
	osd_write_char(number[rest/10]);
	osd_set_position(COMPASS_LINE, ++x);
	rest = rest % 10;
	osd_write_char(number[rest]);
	osd_set_position(COMPASS_LINE, ++x);
	osd_write_char(0xD0);  // degree
}	
double gravity_to_roll2(double a_y, double a_z)
{
	double roll_acc = atan(a_y / a_z);
	if (a_z > 0.0)
	{
		if (a_y < 0.0)	
			roll_acc =  roll_acc + 3.14159;
		else
			roll_acc =  roll_acc - 3.14159;
	}	
	return roll_acc;	
}	
double gravity_to_pitch2(double a_x, double a_z)
{
	double pitch_acc = -atan(a_x / a_z); // replace with asin?

	if (a_z > 0.0)
		pitch_acc =  -pitch_acc;

	return pitch_acc;
}

#define AH_LINE_START 5
void osd_print_artificial_horizon()
{
	int pitch_increment = (int)(sensor_data.pitch*(180.0/3.14/22.0*7.0));   // > 22° = out of screen
	// hor: 7..14..21    ver: 3.3 .. 7.1 (15 stappen) -> 1..8..15
	//// optimize me: precalculated tanf
	int y7 = 8 - (int)(tanf(sensor_data.roll)*-10.8) + pitch_increment;
	int y8 = 8 - (int)(tanf(sensor_data.roll)*-9.26) + pitch_increment;
	int y9 = 8 - (int)(tanf(sensor_data.roll)*-7.71) + pitch_increment;
	int y10 = 8 - (int)(tanf(sensor_data.roll)*-6.17) + pitch_increment;
	int y11 = 8 - (int)(tanf(sensor_data.roll)*-4.63) + pitch_increment;
	int y12 = 8 - (int)(tanf(sensor_data.roll)*-3.09) + pitch_increment;
	int y13 = 8 - (int)(tanf(sensor_data.roll)*-1.54) + pitch_increment;
	int y14 = 8 + pitch_increment;
	int y15 = 8 - (int)(tanf(sensor_data.roll)*1.54) + pitch_increment;
	int y16 = 8 - (int)(tanf(sensor_data.roll)*3.09) + pitch_increment;
	int y17 = 8 - (int)(tanf(sensor_data.roll)*4.63) + pitch_increment;
	int y18 = 8 - (int)(tanf(sensor_data.roll)*6.17) + pitch_increment;
	int y19 = 8 - (int)(tanf(sensor_data.roll)*7.71) + pitch_increment;
	int y20 = 8 - (int)(tanf(sensor_data.roll)*9.26) + pitch_increment;
	int y21 = 8 - (int)(tanf(sensor_data.roll)*10.8) + pitch_increment;
	
	if ((y7) < 16 && y7 >= 0) {
		osd_set_position(AH_LINE_START+y7/3, 7);   osd_write_char(0x4F - y7%3);
	}
	if ((y8) < 16 && y8 >= 0) {
		osd_set_position(AH_LINE_START+y8/3, 8);   osd_write_char(0x4F - y8%3);
	}	
	if ((y9) < 16 && y9 >= 0) {
		osd_set_position(AH_LINE_START+y9/3, 9);   osd_write_char(0x4F - y9%3);
	}	
	if ((y10) < 16 && y10 >= 0) {
		osd_set_position(AH_LINE_START+y10/3, 10); osd_write_char(0x4F - y10%3);
	}	
	if ((y11) < 16 && y11 >= 0) {
		osd_set_position(AH_LINE_START+y11/3, 11); osd_write_char(0x4F - y11%3);
	}	
	if ((y12) < 16 && y12 >= 0) {
		osd_set_position(AH_LINE_START+y12/3, 12); osd_write_char(0x4F - y12%3);
	}	
	if ((y13) < 16 && y13 >= 0) {
		osd_set_position(AH_LINE_START+y13/3, 13); osd_write_char(0x4F - y13%3);
	}	
	if ((y14) < 16 && y14 >= 0) {
		osd_set_position(AH_LINE_START+y14/3, 14); osd_write_char(0x4F - y14%3);
	}	
	if ((y15) < 16 && y15 >= 0) {
		osd_set_position(AH_LINE_START+y15/3, 15); osd_write_char(0x4F - y15%3);
	}	
	if ((y16) < 16 && y16 >= 0) {
		osd_set_position(AH_LINE_START+y16/3, 16); osd_write_char(0x4F - y16%3);
	}	
	if ((y17) < 16 && y17 >= 0) {
		osd_set_position(AH_LINE_START+y17/3, 17); osd_write_char(0x4F - y17%3);
	}	
	if ((y18) < 16 && y18 >= 0) {
		osd_set_position(AH_LINE_START+y18/3, 18); osd_write_char(0x4F - y18%3);
	}	
	if ((y19) < 16 && y19 >= 0) {
		osd_set_position(AH_LINE_START+y19/3, 19); osd_write_char(0x4F - y19%3);
	}	
	if ((y20) < 16 && y20 >= 0) {
		osd_set_position(AH_LINE_START+y20/3, 20); osd_write_char(0x4F - y20%3);
	}	
	if ((y21) < 16 && y21 >= 0) {
		osd_set_position(AH_LINE_START+y21/3, 21); osd_write_char(0x4F - y21%3);
	}	
	
	osd_set_position(AH_LINE_START+2, 14);
	osd_write_char(0xE5);
}	


void osd_print_satellites_in_view()
{
	// print satellite
	osd_set_position(1, 25);
	osd_write_char(SATELLITE_1);
	osd_set_position(1, 26);
	osd_write_char(SATELLITE_2);
	
	
	osd_set_position(1, 27);
	if (sensor_data.gps.status==EMPTY)
		osd_write_char(0x3C);  // no GPS unit found
	else
	{
		if (sensor_data.gps.satellites_in_view == 0)
			osd_write_char(0x0A);
		else if (sensor_data.gps.satellites_in_view < 10)
			osd_write_char(sensor_data.gps.satellites_in_view);
		else
			osd_write_char(9);
	}		
}


/**
 *    A distance in meter, print it to screen using metric or imperial units.
 */
void print_meters(int row, int col, int m)
{
	if (use_metric)
	{
		if (m > 999)
		{
			osd_print_integer(m/1000, row, col);
			osd_write_char(0x41);
			if (m > 9999)
				col += 3;
			else
				col += 2;
			
			osd_print_integer((m % 1000) / 100, row, col);
			osd_write_char(0x8F); // km	
		}
		else
		{
			osd_print_integer(m, row, col);
			osd_write_char(0x8C); // m	
		}		
	} 
	else  // using feet and mile
	{
		// approx convertion from meter to feet
		m *= 3;
		m += m/11;
		
		if (m > 9999)
		{
			//m = m / 5280; // mile
			osd_print_integer(m/5280, row, col);
			osd_write_char(0x41);
			col += 2;
			
			osd_print_integer((m % 5280) / 528, row, col);
			osd_write_char(0x7F); // mi	
		}
		else
		{
			osd_print_integer(m, row, col);
			osd_write_char(0x7E); // ft	
		}		
	}	
}	


	
const char fourninemeter[] = {0x09,0x09,0x09,0x09,METER,0x00};

void osd_print_home_info()
{
	static int last_flight_mode = -1;
	int index;
	
	
	// Pre-calculate some data used for OSD
	home_heading_deg = RAD2DEG(navigation_heading_rad_fromto(sensor_data.gps.longitude_rad - navigation_data.home_longitude_rad,
	                                                         sensor_data.gps.latitude_rad - navigation_data.home_latitude_rad));
	if (home_heading_deg < 0)
		home_heading_deg += 360;
	int home_distance = (int) navigation_distance_between_meter(sensor_data.gps.longitude_rad, navigation_data.home_longitude_rad,
	                                                            sensor_data.gps.latitude_rad, navigation_data.home_latitude_rad);
	int home_heading_index = (int)(home_heading_deg / 22);

	if (home_heading_index < 0)
		home_heading_index += 16; 
	//osd_set_position(1, 4);	
	//osd_write_char(heading[home_heading_index]);

	
	// rc-link
	osd_set_position(1, 2);	
	osd_write_char(0xB3);
	int no_frame_times_20_s = ppm_signal_quality() * 4;
	//if (no_frame_times_20_s > 50)
	//	no_frame_times_20_s = 50;
	osd_print_integer(100 - no_frame_times_20_s, 1, 4);
	osd_write_char(0x81);
	
	// print flight mode
	if (1)//control_state.flight_mode != last_flight_mode)
	{
		if (control_state.flight_mode == AUTOPILOT)
		{
			osd_set_position(1, 13);	
			osd_write_char(0x0B);
			osd_set_position(1, 14);	
			osd_write_char(0x49); // A AutoPilot
			osd_set_position(1, 15);	
			osd_write_char(0x1A); // P
		}	
		else if (control_state.flight_mode == STABILIZED)
		{
			osd_set_position(1, 13);	
			osd_write_char(0x1D); // S
			osd_set_position(1, 14); 	
			osd_write_char(0x1E); // T
			osd_set_position(1, 15);	
			osd_write_char(0x0C); // B
		}
		else
		{
			osd_set_position(1, 13);	
			osd_write_char(0x17); // M
			osd_set_position(1, 14); 	
			osd_write_char(0x0B); // A
			osd_set_position(1, 15);	
			osd_write_char(0x18); // N
		}
		last_flight_mode = control_state.flight_mode;
	}
	
	// distance to home 
	osd_set_position(2, 2);
	osd_write_char(DISTANCE);
	print_meters(2,4,home_distance);
	
	// altitude
	osd_set_position(2, 23);
	osd_write_char(HEIGHT);
	int alt = (int)(sensor_data.pressure_height - navigation_data.home_pressure_height);
	osd_set_position(2, 24);
	if (alt < 0)
		osd_write_char(0x49);
	else
		osd_write_char(0x00);
	print_meters(2, 25, abs(alt));
	
	// home heading
	osd_set_position(2, 14);	
	osd_write_char(heading[home_heading_index]);
	
	// vario  ^x.y[m/s]
	//            
	sensor_data.vertical_speed; // m/s
	osd_set_position(7, 24);
	if (sensor_data.vertical_speed > 5.0)
		osd_write_char(0xB7); 
	else if (sensor_data.vertical_speed > 2.0)
		osd_write_char(0xB8); 
	else if (sensor_data.vertical_speed > 1.0)
		osd_write_char(0xB9); 
	else if (sensor_data.vertical_speed > 0.1)
		osd_write_char(0xBA); 
	else if (sensor_data.vertical_speed < -5.0)
		osd_write_char(0xBF); 
	else if (sensor_data.vertical_speed < -2.0)
		osd_write_char(0xBE); 
	else if (sensor_data.vertical_speed < -1.0)
		osd_write_char(0xBD); 
	else if (sensor_data.vertical_speed < 0.1)
		osd_write_char(0xBC); 
	else
		osd_write_char(0xBB); 
	
	int vs10 = abs((int)(sensor_data.vertical_speed*10.0));
	osd_set_position(7, 25);
	osd_write_char(number[vs10/10]);
	osd_set_position(7, 26);
	osd_write_char(0x41);
	osd_set_position(7, 27);
	osd_write_char(number[vs10%10]);
	osd_set_position(7, 28);
	osd_write_char(0x8A);	
	
	// voltage
	int volt10 = sensor_data.battery_voltage_10;
	if (volt10 >= 100)
	{
		osd_set_position (COMPASS_LINE+1, 1);
		osd_write_char(number[1]);
		volt10 = volt10 % 100;
	}	
	osd_set_position (COMPASS_LINE+1, 2);
	osd_write_char(number[volt10/10]);
	osd_set_position (COMPASS_LINE+1, 3);
	osd_write_char(0x9E);
	osd_set_position (COMPASS_LINE+1, 4);
	osd_write_char(number[volt10%10]);
	
	// time airborne
	osd_set_position (COMPASS_LINE+1, 23);
	if (navigation_data.airborne)
		osd_write_char(0xD2);
	else
		osd_write_char(0xD1);
	
	if (navigation_data.time_airborne_s >= 6000) //mmm
	{
		osd_set_position (COMPASS_LINE+1, 25);
		osd_write_char(navigation_data.time_airborne_s/60/100);
		osd_set_position (COMPASS_LINE+1, 26);
		osd_write_char(number[((navigation_data.time_airborne_s/60)%100)/10]);
		osd_set_position (COMPASS_LINE+1, 27);
		osd_write_char(number[((navigation_data.time_airborne_s/60)%10)]);
		osd_set_position (COMPASS_LINE+1, 28);
		osd_write_char(0x31);
	}
	else if (navigation_data.time_airborne_s >= 600) //mm:ss
	{
		osd_set_position (COMPASS_LINE+1, 24);
		osd_write_char(navigation_data.time_airborne_s/60/10);
		osd_set_position (COMPASS_LINE+1, 25);
		osd_write_char(number[((navigation_data.time_airborne_s/60)%10)]);
		osd_set_position (COMPASS_LINE+1, 26);
		osd_write_char(0x44);
		osd_set_position (COMPASS_LINE+1, 27);
		osd_write_char(number[(navigation_data.time_airborne_s%60)/10]);
		osd_set_position (COMPASS_LINE+1, 28);
		osd_write_char(number[navigation_data.time_airborne_s%10]);
	} 
	else //m:ss
	{
		osd_set_position (COMPASS_LINE+1, 25);
		osd_write_char(number[((navigation_data.time_airborne_s/60)%10)]);
		osd_set_position (COMPASS_LINE+1, 26);
		osd_write_char(0x44);
		osd_set_position (COMPASS_LINE+1, 27);
		osd_write_char(number[(navigation_data.time_airborne_s%60)/10]);
		osd_set_position (COMPASS_LINE+1, 28);
		osd_write_char((number[navigation_data.time_airborne_s%10]));
	}	
	
	
	// speed
	float speed;
	if (use_metric)  // to kph
		speed = sensor_data.gps.speed_ms * 3.6;
	else  // to mph
		speed = sensor_data.gps.speed_ms * (3.6 * 0.62);
	osd_print_integer((int) speed, 7, 2);
	if (use_metric)
		osd_write_char(0x80);
	else
		osd_write_char(0x7D);
}

void osd_print_integer(int num, int row, int col)
{
	int index = col;
	if (num > 9999)
	{
		osd_set_position(row, index);
		osd_write_string(fourninemeter);
	}	
	else
	{
		int rest = 0;
		osd_set_position(row, index);
		if (num >= 1000)
		{
			osd_write_char(number[num/1000]);
			osd_set_position(row, ++index);
		}	
		rest = num % 1000;
		if (num >= 100)
		{
			osd_write_char(number[rest/100]);
			osd_set_position(row, ++index);
		} 
		rest = rest % 100;
		if (num >= 10)
		{	
			osd_write_char(number[rest/10]);
			osd_set_position(row, ++index);
		}	
		rest = rest % 10;
		osd_write_char(number[rest]);
		osd_set_position(row, ++index);
	}
}	

/*!
 *    Initialize the OSD. Return 0 if no OSD is found.
 */
int osd_initialize(portTickType *xLastExecutionTime)
{
	char x;
	
	max7456_init();
	
	spiWriteReg(VIDEO_MODE_0_WRITE, VIDEO_MODE_0_40_PAL);
	vTaskDelayUntil( xLastExecutionTime, ( ( portTickType ) 100 / portTICK_RATE_MS ) );
	if (spiReadReg(VIDEO_MODE_0_READ) != VIDEO_MODE_0_40_PAL)
	{
		uart1_puts("no OSD found!\r\n");
		return 0;
	}
	
	while (osd_charactermemory_busy()) // wait for character memory to become available
		vTaskDelayUntil( xLastExecutionTime, ( ( portTickType ) 200 / portTICK_RATE_MS ) );
	
	max7456_loadchars();
	
	if (osd_use_pal())
	{
		uart1_puts("found PAL, using PAL...");
		spiWriteReg(VIDEO_MODE_0_WRITE, VIDEO_MODE_0_40_PAL | OSD_EN);
	}	
	else if (osd_use_ntsc())
	{
		uart1_puts("found NTSC, using NTSC...");
		spiWriteReg(VIDEO_MODE_0_WRITE, VIDEO_MODE_0_40_PAL | OSD_EN);
	}		
	else
	{
		uart1_puts("no video input, using PAL...");
		spiWriteReg(VIDEO_MODE_0_WRITE, VIDEO_MODE_0_40_PAL | OSD_EN);
	}	
	
	x = spiReadReg(0xEC);
	vTaskDelayUntil( xLastExecutionTime, ( ( portTickType ) 1 / portTICK_RATE_MS ) );
	x &= 0xEF;
	spiWriteReg(0x6C, x);
	vTaskDelayUntil( xLastExecutionTime, ( ( portTickType ) 1 / portTICK_RATE_MS ) );
	spiWriteReg(0x04, 0x00);
	
	return 1;
}




char osd_get_char(int c)
{
	switch(c)
	{
		case ' ': return 0x00; break;
		case 'a': return 0x25; break;
		case 'b': return 0x26; break;
		case 'c': return 0x27; break;
		case 'd': return 0x28; break;
		case 'e': return 0x29; break;
		case 'f': return 0x2A; break;
		case 'g': return 0x2B; break;
		case 'h': return 0x2C; break;
		case 'i': return 0x2D; break;
		case 'j': return 0x2E; break;
		case 'k': return 0x2F; break;
		case 'l': return 0x30; break;
		case 'm': return 0x31; break;
		case 'n': return 0x32; break;
		case 'o': return 0x33; break;
		case 'p': return 0x34; break;
		case 'q': return 0x35; break;
		case 'r': return 0x36; break;
		case 's': return 0x37; break;
		case 't': return 0x38; break;
		case 'u': return 0x39; break;
		case 'v': return 0x3A; break;
		case 'w': return 0x3B; break;
		case 'x': return 0x3C; break;
		case 'y': return 0x3D; break;
		case 'z': return 0x3E; break;
		default: return 0x00;
	}		
}	


/*!
 *    Set the cursor of the OSD.
 *    Row = 0..15, Column = 0..29
 */
void osd_set_position(int row, int column)
{
	int position = (row * 30) + column;
	unsigned char high = (position >= 256)?1:0;
	unsigned char low = (unsigned char)(position % 256);
	spiWriteReg(DM_ADDRH_WRITE, high);
	spiWriteReg(DM_ADDRL_WRITE, low);
}	


void osd_write_char(unsigned char x)
{
	spiWriteReg(DM_CODE_IN_WRITE, x);	
}	


/*!
 *   Write string x (ended with '\0');
 */
void osd_write_string(unsigned char *x)
{
	unsigned char l = spiReadReg(DM_ADDRL_READ);
	unsigned char h = spiReadReg(DM_ADDRH_READ);
	int pos = ((int)h)*256 + (int)l;
	while (*x != 0x00)
	{
		spiWriteReg(DM_CODE_IN_WRITE, *x++);
		pos++;
		if (pos == 256)
			spiWriteReg(DM_ADDRH_WRITE, (unsigned char)(pos>255?1:0));
		spiWriteReg(DM_ADDRL_WRITE, (unsigned char)(pos%256));
	}	
}


void osd_write_ascii_char(unsigned char x)
{
	spiWriteReg(DM_CODE_IN_WRITE, osd_get_char(x));	
}


void osd_write_ascii_string(unsigned char *x)
{
	unsigned char l = spiReadReg(DM_ADDRL_READ);
	unsigned char h = spiReadReg(DM_ADDRH_READ);
	int pos = ((int)h)*256 + (int)l;
	while (*x != 0x00)
	{
		spiWriteReg(DM_CODE_IN_WRITE, osd_get_char(*x++));
		pos++;
		if (pos == 256)
			spiWriteReg(DM_ADDRH_WRITE, (unsigned char)(pos>255?1:0));
		spiWriteReg(DM_ADDRL_WRITE, (unsigned char)(pos%256));
	}	
}


/*!
 * Is there an input video signal?
 */
int osd_input_available()
{
	return max756_read_status() & (STATUS_02_PAL_DETECTED | STATUS_01_NTSC_DETECTED);	
}
	
int osd_charactermemory_busy()
{
	return 	max756_read_status() & STATUS_20_NVRAM_BUSY;
}	

int osd_use_pal()
{
	return max756_read_status() & (STATUS_02_PAL_DETECTED);	
}

int osd_use_ntsc()
{
	return max756_read_status() & (STATUS_01_NTSC_DETECTED);	
}	