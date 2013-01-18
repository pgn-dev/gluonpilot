 
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
#include "uart1_queue/uart1_queue.h"
#include "ppm_in/ppm_in.h"

#include "common.h"
#include "task_osd.h"
#include "configuration.h"
#include "sensors.h"
#include "handler_navigation.h"

extern xSemaphoreHandle xSpiSemaphore;


// Other characters
#define SATELLITE_1 0xB6
#define SATELLITE_2 0xB7
#define RC_TRANSMITTER 0xB5
#define HOME 0xB4
#define SATELLITE_DISH 0xB6
#define DISTANCE_M 0x5D
#define DISTANCE_KM 0xE3
#define DISTANCE_FT 0x5E
#define DISTANCE_MI 0xE2
#define HEIGHT 0x8D
#define ANTENNA 0xCA
#define KMH 0xCB
#define METER 0xCC

//const unsigned char heading[] = {0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF};
const unsigned char heading45[] = {219, 218, 217, 224, 223, 222, 221, 220};
const unsigned char number_large[] = {0x0A, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
const unsigned char number_small[] = {0x7A, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79};

int home_heading_deg = 0;

int osd_input_available();
int osd_charactermemory_busy();
void max7456_loadchars();
int osd_use_pal();
int osd_use_ntsc();
int osd_initialize(portTickType *xLastExecutionTime);
void osd_set_position(int row, int column);
void osd_write_char(unsigned char x);
void osd_write_string(const unsigned char *x);
void osd_write_ascii_char(unsigned char x, int small);
void osd_write_ascii_string(unsigned char *x, int small);
void osd_print_static_data();
void osd_print_integer(int num, int row, int col, int small);
void print_meters(int row, int col, int m, int small);

void osd_print_home_info();
void osd_print_satellites_in_view(int small);
void osd_print_vario(int small);
void osd_print_posted_message(int small);
void osd_print_centered(int row, char *str, int small);
void osd_print_altitude(int small);
void osd_print_rcinfo(int small);
void osd_print_speed(int small);
void osd_print_fly_time(int small);
void osd_print_voltage1(int small);
void osd_print_current1(int small);
void osd_print_mah1(int small);
void osd_print_voltage2(int small);
void osd_print_active_block(int small);
void osd_print_mode();
void osd_print_home_heading();
void osd_print_home_distance(int small);
void osd_print_artificial_horizon();
void osd_print_artificial_horizon2();
void osd_print_gluonpilot_logo(int, int);

void osd_menu_osd();
void osd_menu_blocks();

int use_metric = 1;

int do_clear_screen = 0;

enum menu { OSD, BLOCKS };

enum menu active_menu = BLOCKS;

int last_roll_ppm;

/*
 *
 *   Used stackspace: 260 / 430
 */
void osd_task( void *parameters )
{
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 
	int i;
	
	
	uart1_puts("OSD task initializing...\r\n");
	vTaskSetApplicationTaskTag( NULL, ( void * ) 7 );
#ifndef USE_TRACING
	if (osd_initialize(& xLastExecutionTime) == 0)
		vTaskDelete(xTaskGetCurrentTaskHandle());
#endif
	uart1_puts("OSD initialized\r\n");
	
	i = ppm.channel[config.control.channel_ap];
		
	//while (abs(i - ppm.channel[config.control.channel_ap]) < 200 && !button_down())
	{
		
		
	}
	
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();
	vTaskDelay( ( ( portTickType )100 / portTICK_RATE_MS ) );   // 5Hz
	
    spiWriteReg(0x04, 0x04); // clear


    osd_print_gluonpilot_logo(5, 8);
    spiWriteReg(0x04, 0x04); // clear
    osd_print_gluonpilot_logo(5, 8);
    osd_print_centered(10, "  Menu >", 0);
    osd_print_centered(14, "http://www.gluonpilot.com", 0);

    {
        osd_print_centered(8, " Waiting... ", 0);
        char ball = 0x80;
        for (i = 0; i < 16; i++)
        {
            //vTaskDelay( ( ( portTickType ) 300 / portTICK_RATE_MS ) );
            osd_set_position(8, 22);
            osd_write_char(ball++);
        }
    }

    spiWriteReg(0x04, 0x04); // clear
    //osd_print_static_data();

	for( ;; )
	{
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 200 / portTICK_RATE_MS ) );   // 5Hz

        if (xSemaphoreTake( xSpiSemaphore, ( portTickType ) 0 ) == pdTRUE )  // only execute this when SPI port is available
        {
            //vTaskDelay( ( ( portTickType ) 1 / portTICK_RATE_MS ) );   // 5Hz
            
            if (do_clear_screen)
            {
                spiWriteReg(0x04, 0x04);
                do_clear_screen = 0;
            }

            osd_set_position(1, 10);
            osd_write_ascii_char(' ', 0);

            osd_print_posted_message(1);

            for (i = 0; i < 30; i++)
            {
                osd_set_position(2, i);
                osd_write_char(0x70);
                osd_set_position(13, i);
                osd_write_char(0x9C);
            }

            if (active_menu == BLOCKS)
                osd_menu_blocks();
            else
                osd_menu_osd();

            last_roll_ppm = ppm.channel[config.control.channel_roll];
            
            xSemaphoreGive( xSpiSemaphore );
        } else
            printf("\r\nSPI OSD not available\r\n");
	}
}

void osd_menu_osd()
{
    if (ppm.channel[config.control.channel_roll] > 1750 && last_roll_ppm < 1700 && control_state.flight_mode == AUTOPILOT)
    {
        if (active_menu == OSD)
        {
            do_clear_screen = 1;
            active_menu = BLOCKS;
        }
    }
    
    if (config.osd.show_mode)
        osd_print_mode();
    if (config.osd.show_arrow_home)
        osd_print_home_heading();
    if (config.osd.show_distance_home)
        osd_print_home_distance(0);
    if (config.osd.show_gps_status)
        osd_print_satellites_in_view(0);
    //spiWriteReg(0x04, 0x04); // clear
    if (config.osd.show_artificial_horizon)
        osd_print_artificial_horizon2();

    if (config.osd.show_altitude)
        osd_print_altitude(0);
    if (config.osd.show_rc_link)
        osd_print_rcinfo(0);
    if (config.osd.show_speed)
        osd_print_speed(0);
    if (config.osd.show_flight_time)
        osd_print_fly_time(0);
    if (config.osd.show_voltage1)
        osd_print_voltage1(0);
    if (config.osd.show_current)
        osd_print_current1(0);
    if (config.osd.show_mah)
        osd_print_mah1(0);
    if (config.osd.show_voltage2)
        osd_print_voltage2(0);

    if (config.osd.show_block_name)
        osd_print_active_block(1);
    if (config.osd.show_vario)
        osd_print_vario(0);
}

void osd_menu_blocks()
{
    static int selected_blocknum = 0, max_block = 0, last_ppm;
    int blocknum = 0, i;
    char ptr[9];

    if (ppm.channel[config.control.channel_roll] > 1750 && last_roll_ppm < 1700)  // select mode
    {
        if (selected_blocknum == 0)
        {
            active_menu = OSD;
            do_clear_screen = 1;
            selected_blocknum = 0;
            //return;
        }
        else
            {
            for (i = 0; i < MAX_GLUONSCRIPTCODES; i++)
            {
                if (gluonscript_data.codes[i].opcode == BLOCK)
                {
                    selected_blocknum--;
                    if (selected_blocknum == 0)
                    {
                        printf("\r\nOSD: new block selected\r\n");
                        gluonscript_data.current_codeline = i;
                        active_menu = OSD;
                        do_clear_screen = 1;
                        selected_blocknum = 0;
                        break;//return;
                    }
                }
            }
        }
    }
    else if (ppm.channel[config.control.channel_roll] < 1250 && last_roll_ppm > 1300)  // select mode
    {
        active_menu = OSD;
        do_clear_screen = 1;
        //return;
    }


    // clear '>'
    for (i = 0; i <= max_block; i++)
    {
        osd_set_position(i + 5, 8);
        osd_write_ascii_char(' ', 0);
    }
    // set selected '>'
    if (ppm.channel[config.control.channel_pitch] > 1700)
        selected_blocknum = (selected_blocknum + 1) % (max_block + 1);
    else if (ppm.channel[config.control.channel_pitch] < 1300)
        selected_blocknum = (selected_blocknum + max_block) % (max_block + 1);

    osd_print_centered(14, "Aileron: right:OK, left:BACK ", 1);
    osd_print_centered(1, "Goto block...", 1);

    osd_print_centered(blocknum + 5, "Return  ", 0);
    if (blocknum == selected_blocknum)
    {
        osd_set_position(blocknum + 5, 8);
        osd_write_ascii_char('>', 0);
    }
    
    for (i = 0; i < MAX_GLUONSCRIPTCODES; i++)
    {
        if (gluonscript_data.codes[i].opcode == BLOCK)
        {
            blocknum++;
            struct GluonscriptCode *current_code = &(gluonscript_data.codes[i]);
            int x = (int)current_code->x;
            int y = (int)current_code->y;
            ptr[0] = ((char*)(& current_code->a))[1];
            ptr[1] = ((char*)(& current_code->a))[0];
            ptr[2] = ((char*)(& current_code->b))[1];
            ptr[3] = ((char*)(& current_code->b))[0];
            ptr[4] = ((char*)(& x))[1];
            ptr[5] = ((char*)(& x))[0];
            ptr[6] = ((char*)(& y))[1];
            ptr[7] = ((char*)(& y))[0];
            ptr[8] = '\0';
            osd_print_centered(blocknum + 5, ptr, 0);
            if (blocknum == selected_blocknum)
            {
                osd_set_position(blocknum + 5, 8);
                osd_write_ascii_char('>', 0);
            }
            max_block = blocknum;
        }
    }

}

void osd_print_vario(int small)
{
    const unsigned char* number;
    if (small)
        number = number_small;
    else
        number = number_large;

    // vario  ^x.y[m/s]
	//
//	sensor_data.vertical_speed; // m/s
	osd_set_position(8, 24);
	if (sensor_data.vertical_speed > 5.0)
		osd_write_char(0xF6);
	else if (sensor_data.vertical_speed > 2.0)
		osd_write_char(0xF7);
	else if (sensor_data.vertical_speed > 1.0)
		osd_write_char(0xF8);
	else if (sensor_data.vertical_speed > 0.1)
		osd_write_char(0xF9);
	else if (sensor_data.vertical_speed < -5.0)
		osd_write_char(0xFB);
	else if (sensor_data.vertical_speed < -2.0)
		osd_write_char(0xFC);
	else if (sensor_data.vertical_speed < -1.0)
		osd_write_char(0xFD);
	else if (sensor_data.vertical_speed < 0.1)
		osd_write_char(0xFE);
	else
		osd_write_char(0xFA);

	int vs10 = abs((int)(sensor_data.vertical_speed*10.0));
	osd_set_position(8, 25);
	osd_write_char(number[vs10/10]);
	osd_set_position(8, 26);
	osd_write_char(0x41);
	osd_set_position(8, 27);
	osd_write_char(number[vs10%10]);
	osd_set_position(8, 28);
	osd_write_char(0x5F);
}

int message_duration = 0;
char message[17];
int blinkme = 0;

void osd_post_message (char *str, int blink)
{
    int i;
    for (i = 0; i < 16 && str[i] != 0; i++)
    {
        message[i] = str[i];
    }
    message[i] = '\0';
    message_duration = 16;
    blinkme = blink;
}

void osd_print_posted_message(int small)
{
    if (message_duration > 1)
    {
        if (message_duration % 2 == 0 && blinkme)
            osd_print_centered(4, "                ", 0);
        else
            osd_print_centered(4, message, small);
        
        //osd_set_position(5, 14);
        //osd_write_char(0x80 + 16 - message_duration);
        message_duration--;
    } else if (message_duration == 1)
    {
        osd_print_centered(4, "                ", 0);

        //osd_set_position(5, 14);
        //osd_write_char(0x00);
        message_duration--;
    }
}

int active_block = -1;
int show_block_timer = 16;
char enteringblock[] = "Entering \0\0\0\0\0\0\0\0\0\0";

void osd_print_active_block(int small)
{
    if (show_block_timer == 0)
    {
        int i;

        for (i = gluonscript_data.current_codeline; i > 0; i--)
        {
            if (gluonscript_data.codes[i].opcode == BLOCK)
            {
                struct GluonscriptCode *current_code = & gluonscript_data.codes[i];
                if (active_block != i)
                {
                    active_block = i;
                    char ptr[9];
                    int x = (int)current_code->x;
                    int y = (int)current_code->y;
                    ptr[0] = ((char*)(& current_code->a))[1];
                    ptr[1] = ((char*)(& current_code->a))[0];
                    ptr[2] = ((char*)(& current_code->b))[1];
                    ptr[3] = ((char*)(& current_code->b))[0];
                    ptr[4] = ((char*)(& x))[1];
                    ptr[5] = ((char*)(& x))[0];
                    ptr[6] = ((char*)(& y))[1];
                    ptr[7] = ((char*)(& y))[0];
                    ptr[8] = '\0';

                    for (i = 0; i < 8 && ptr[i] != 0 && ptr[i] != ' '; i++)
                        enteringblock[i+9] = ptr[i];
                    enteringblock[i+9] = '\0';
                    osd_print_centered(3, enteringblock, small);
                    show_block_timer = 16;
                }
                break;
            }
        }
    }
    else if (show_block_timer == 1)
    {
        osd_print_centered(3, "                 ", 0);
        show_block_timer = 0;
    }
    else
        show_block_timer--;
}

void osd_print_altitude(int small)
{
    osd_set_position(7, 24);
	osd_write_char(0x96);
	int altitude;
    if (config.control.altitude_mode == GPS_ABSOLUTE)
        altitude =  sensor_data.gps.height_m;
    else if (config.control.altitude_mode == GPS_RELATIVE)
        altitude = sensor_data.gps.height_m - navigation_data.home_gps_height;
    else //if (config.control.altitude_mode == PRESSURE)
        altitude = (int)(sensor_data.pressure_height - navigation_data.home_pressure_height);
	osd_set_position(7, 24);
	if (altitude < 0)
		osd_write_char(0x49);
	else
		osd_write_char(0x00);
	print_meters(7, 25, abs(altitude), small);

    if (altitude < 100)
    {
        osd_set_position (7, 29);
		osd_write_char(0x00);
    }
    if (altitude < 10)
    {
        osd_set_position (7, 28);
		osd_write_char(0x00);
    }
}

void osd_print_rcinfo(int small)
{
	int no_frame_times_20_s = ppm_signal_quality() * 4;
    int link_pct = 100 - no_frame_times_20_s;

    if (config.osd.rssi == Analog)
    {
        link_pct = (int)(((float)adc_get_channel(3) / 65520.0f * 3.3f - (float)config.osd.voltage_low/50.0f) / ((float)config.osd.voltage_high/50.0f - (float)config.osd.voltage_low/50.0f) * 100.0);
        if (link_pct < 0)
            link_pct = 0;
        else if (link_pct > 100)
            link_pct = 100;
        //printf("\r\n%u %d\r\n", adc_get_channel(3), link_pct);
    }
    else if (config.osd.rssi >= Ch1)
    {
        int us = ppm.channel[config.osd.rssi - 2];
        if (us < 1000)
            us = 1000;
        if (us > 2000)
            us = 2000;
        link_pct = (us - 1000) / 10;
    }

    // rc-link
	osd_set_position(1, 1);
	osd_write_char(0xB8);

	//if (no_frame_times_20_s > 50)
	//	no_frame_times_20_s = 50;
	osd_print_integer(link_pct, 1, 3, small);
	osd_write_ascii_char('%', small);

    if (link_pct < 100)
    {
        osd_set_position (1, 6);
		osd_write_char(0x00);
    }
    if (link_pct < 10)
    {
        osd_set_position (1, 5);
		osd_write_char(0x00);
    } else if (link_pct == 100)
    {
        osd_set_position (1, 7);
		osd_write_char(0x00);
    }

}

#define VOLTAGE_LINE 14
void osd_print_voltage1(int small)
{
    const unsigned char* number;
    if (small)
        number = number_small;
    else
        number = number_large;
    
    osd_set_position(VOLTAGE_LINE, 1);
    osd_write_char(0xE4);

    int volt10 = sensor_data.battery1_voltage_10;
    int decrement = 1;
	if (volt10 >= 100)  // > 10v0
	{
		osd_set_position (VOLTAGE_LINE, 2);
		osd_write_char(number[1]);
		volt10 = volt10 % 100;
        decrement = 0;
	}
	osd_set_position (VOLTAGE_LINE, 3 - decrement);
	osd_write_char(number[volt10/10]);
	osd_set_position (VOLTAGE_LINE, 4 - decrement);
	osd_write_char(0x9E);
	osd_set_position (VOLTAGE_LINE, 5 - decrement);
	osd_write_char(number[volt10%10]);
}

void osd_print_voltage2(int small)
{
    const unsigned char* number;
    if (small)
        number = number_small;
    else
        number = number_large;

    osd_set_position(VOLTAGE_LINE-1, 1);
    osd_write_char(0xE5);

    int current10 = sensor_data.battery2_voltage_10;
    int decrement = 1;
	if (current10 >= 100)  // > 10v0
	{
		osd_set_position (VOLTAGE_LINE-1, 2);
		osd_write_char(number[1]);
		current10 = current10 % 100;
        decrement = 0;
	}
	osd_set_position (VOLTAGE_LINE-1, 3 - decrement);
	osd_write_char(number[current10/10]);
	osd_set_position (VOLTAGE_LINE-1, 4 - decrement);
	osd_write_char(0x9E);
	osd_set_position (VOLTAGE_LINE-1, 5 - decrement);
	osd_write_char(number[current10%10]);
}

void osd_print_current1(int small)
{
    const unsigned char* number;
    if (small)
        number = number_small;
    else
        number = number_large;

    int current10 = (int)(sensor_data.battery1_current * 10.0);
    int decrement = 1;
	if (current10 >= 100)  // > 10v0
	{
		osd_set_position (VOLTAGE_LINE, 15);
		osd_write_char(number[1]);
		current10 = current10 % 100;
        decrement = 0;
	}
	osd_set_position (VOLTAGE_LINE, 16 - decrement);
	osd_write_char(number[current10/10]);
	osd_set_position (VOLTAGE_LINE, 17 - decrement);
	//osd_write_char(0x5A);
    osd_write_char(0x9D);
	osd_set_position (VOLTAGE_LINE, 18 - decrement);
	osd_write_char(number[current10%10]);
}

void osd_print_mah1(int small)
{
    int mah = sensor_data.battery1_mAh;
    int decrement = 1;
    const unsigned char* number;
    if (small)
        number = number_small;
    else
        number = number_large;

	if (mah >= 1000)
	{
		osd_set_position (VOLTAGE_LINE, 8);
		osd_write_char(number[mah/1000]);
		mah = mah % 1000;
        decrement = 0;
	}
    if (mah >= 100)
	{
        osd_set_position (VOLTAGE_LINE, 9 - decrement);
        osd_write_char(number[mah/100]);
        mah = mah % 100;
    }
    if (mah >= 10)
    {
        osd_set_position (VOLTAGE_LINE, 10 - decrement);
        osd_write_char(number[mah/10]);
        mah = mah % 10;
    }
	osd_set_position (VOLTAGE_LINE, 11 - decrement);
	osd_write_char(number[mah]);
    osd_set_position (VOLTAGE_LINE, 12 - decrement);
	osd_write_char(0x64);
}

void osd_print_speed(int small)
{
    float speed;
	if (use_metric)  // to kph
		speed = sensor_data.gps.speed_ms * 3.6;
	else  // to mph
		speed = sensor_data.gps.speed_ms * (3.6 * 0.62);

	osd_print_integer((int) speed, 7, 2, small);
	if (use_metric)
    {
		osd_write_char(0x65);
    }
	else
    {
		osd_write_char(0x66);
    }
        
    if (speed < 10)
    {
        osd_set_position (7, 4);
        osd_write_char(0x00);
    }
    if (speed < 100)
    {
        osd_set_position (7, 5);
        osd_write_char(0x00);
    }
}

void osd_print_fly_time(int small)
{
    const unsigned char* number;
    if (small)
        number = number_small;
    else
        number = number_large;

    osd_set_position (VOLTAGE_LINE, 23);
	if (navigation_data.airborne)
		osd_write_char(0xE7);
	else
		osd_write_char(0xE6);

	if (navigation_data.time_airborne_s >= 6000) //mmm
	{
		osd_set_position (VOLTAGE_LINE, 25);
		osd_write_char(navigation_data.time_airborne_s/60/100);
		osd_set_position (VOLTAGE_LINE, 26);
		osd_write_char(number[((navigation_data.time_airborne_s/60)%100)/10]);
		osd_set_position (VOLTAGE_LINE, 27);
		osd_write_char(number[((navigation_data.time_airborne_s/60)%10)]);
		osd_set_position (VOLTAGE_LINE, 28);
		osd_write_char(0x31);
	}
	else if (navigation_data.time_airborne_s >= 600) //mm:ss
	{
		osd_set_position (VOLTAGE_LINE, 24);
		osd_write_char(navigation_data.time_airborne_s/60/10);
		osd_set_position (VOLTAGE_LINE, 25);
		osd_write_char(number[((navigation_data.time_airborne_s/60)%10)]);
		osd_set_position (VOLTAGE_LINE, 26);
		osd_write_char(0x44);
		osd_set_position (VOLTAGE_LINE, 27);
		osd_write_char(number[(navigation_data.time_airborne_s%60)/10]);
		osd_set_position (VOLTAGE_LINE, 28);
		osd_write_char(number[navigation_data.time_airborne_s%10]);
	}
	else //m:ss
	{
		osd_set_position (VOLTAGE_LINE, 25);
		osd_write_char(number[((navigation_data.time_airborne_s/60)%10)]);
		osd_set_position (VOLTAGE_LINE, 26);
		osd_write_char(0x44);
		osd_set_position (VOLTAGE_LINE, 27);
		osd_write_char(number[(navigation_data.time_airborne_s%60)/10]);
		osd_set_position (VOLTAGE_LINE, 28);
		osd_write_char((number[navigation_data.time_airborne_s%10]));
	}
}

void osd_print_home_distance(int small)
{
    int home_distance = (int) navigation_distance_between_meter(sensor_data.gps.longitude_rad, navigation_data.home_longitude_rad,
	                                                            sensor_data.gps.latitude_rad, navigation_data.home_latitude_rad);
    //osd_set_position(12, 16);
	//osd_write_char(DISTANCE_M);
	print_meters(12,13,home_distance, small);
}

void osd_print_home_heading()
{
    static int old_symbol = -1;
    static int symbol_mapping[] = {0xA8, 0xA6, 0xA4, 0xA2, 0xA0, 0xAE, 0xAC, 0xAA };

    // Pre-calculate some data used for OSD
	int home_heading_deg = (int) RAD2DEG(navigation_heading_rad_fromto(sensor_data.gps.longitude_rad - navigation_data.home_longitude_rad,
	                                                         sensor_data.gps.latitude_rad - navigation_data.home_latitude_rad)
	                           - sensor_data.gps.heading_rad);
	if (home_heading_deg < 0)
		home_heading_deg += 360;
	else if (home_heading_deg > 360)
		home_heading_deg -= 360;
    int symbol = (home_heading_deg + 22) / 45;
    
    //if (old_symbol != symbol)
    {
        osd_set_position(11, 14);
        osd_write_char(symbol_mapping[symbol % 8]);
        osd_set_position(11, 15);
        osd_write_char(symbol_mapping[symbol % 8] + 1);
        old_symbol = symbol;
    }
}


void osd_print_mode()
{
    static enum FlightModes last_mode = -1;
    //if (last_mode == control_state.flight_mode)
    //    return;
    
    if (control_state.flight_mode == AUTOPILOT)
    {
        osd_set_position(1, 14);
        osd_write_ascii_char('A', 0);
        osd_set_position(1, 15);
        osd_write_char(0x6A);
        osd_set_position(1, 16);
        osd_write_char(0x6B);
    }
    else if (control_state.flight_mode == STABILIZED)
    {
        osd_set_position(1, 14);
        osd_write_ascii_char('S', 0);
        osd_set_position(1, 15);
        osd_write_char(0x6C);
        osd_set_position(1, 16);
        osd_write_char(0x6D);
    }
    else
    {
        osd_set_position(1, 14);
        osd_write_ascii_char('M', 0);
        osd_set_position(1, 15);
        osd_write_char(0x6E);
        osd_set_position(1, 16);
        osd_write_char(0x6F);
    }

    last_mode = control_state.flight_mode;
}

void osd_print_centered(int row, char *str, int small)
{
    int len = 0;
    int i;

    while (str[len] != 0)
        len++;

    int start_pos = 15 - len / 2;

    
    for (i = 0; i <= len; i++)
    {
        osd_set_position(row, i + start_pos + 1);
        osd_write_ascii_char(str[i], small);
    }
}


void osd_print_gluonpilot_logo(int row, int start_col) // 5, 8
{
    int x;
    for (x = start_col; x < start_col + 16; x++)
    {
        osd_set_position(row, x);
        osd_write_char(0xC0 + x - start_col);
        osd_set_position(row+1, x);
        osd_write_char(0xD0 + x - start_col);
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
	osd_write_char(DISTANCE_M);
	osd_set_position(1, 14);
	osd_write_char(HEIGHT);
}


float gravity_to_roll2(float a_y, float a_z)
{
	float roll_acc = atan(a_y / a_z);
	if (a_z > 0.0)
	{
		if (a_y < 0.0)	
			roll_acc =  roll_acc + 3.14159;
		else
			roll_acc =  roll_acc - 3.14159;
	}	
	return roll_acc;	
}	
float gravity_to_pitch2(float a_x, float a_z)
{
	float pitch_acc = -atan(a_x / a_z); // replace with asin?

	if (a_z > 0.0)
		pitch_acc =  -pitch_acc;

	return pitch_acc;
}

#define AH_LINE_START 4
void osd_print_artificial_horizon2()
{
    static int previous_positions[16] = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    int i;
    for (i = 0; i < 16; i++)
    {
        osd_set_position(AH_LINE_START+previous_positions[i], 7+i);
        osd_write_char(0x00);
    }
    float FOV_V = 28.0;

    // 6 vertical positions => 6 * 6 = 36 possible positions (18 up & down)
    int pitch_increment = (int)(sensor_data.pitch*(180.0/3.14/FOV_V*18.0)) + 3;

    //double FOV_H =
    float tanroll = tanf(sensor_data.roll);
    for (i = -7; i < 8; i++) // -0.7..0.7 -> -18..18
    {
        if (i == 0)
            continue;
        int y = 18 - (int)(tanroll / 1.0f * 18.0f * ((float)i/7.0f) ) + pitch_increment;  // -45..45 -> 0..36
        // y = -18..18 => 3..7 = 3 + (y+18)
        // hor: 7..14..21    ver: 3.3 .. 7.1 (15 stappen) -> 1..8..15
        if (y <= 36 && y >= 0)
        {
            previous_positions[i+7] = y / 6;
            osd_set_position(AH_LINE_START +  y / 6, 14 + i); // AH_LINE_START + 0..6
            osd_write_char(0x56 - y % 6);
        }
    }

    osd_set_position(AH_LINE_START+3, 14);
	osd_write_char(0x4E);
}

void osd_print_artificial_horizon()
{
    static int previous_positions[] = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    int i;

    for (i = 0; i < 15; i++)
    {
        osd_set_position(AH_LINE_START+previous_positions[i], 7+i);
        osd_write_char(0x00);
    }

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
        previous_positions[0] = y7/3;
		osd_set_position(AH_LINE_START+y7/3, 7);   osd_write_char(0x4F - y7%3);
	}
	if ((y8) < 16 && y8 >= 0) {
        previous_positions[1] = y8/3;
		osd_set_position(AH_LINE_START+y8/3, 8);   osd_write_char(0x4F - y8%3);
	}	
	if ((y9) < 16 && y9 >= 0) {
        previous_positions[2] = y9/3;
		osd_set_position(AH_LINE_START+y9/3, 9);   osd_write_char(0x4F - y9%3);
	}	
	if ((y10) < 16 && y10 >= 0) {
        previous_positions[3] = y10/3;
		osd_set_position(AH_LINE_START+y10/3, 10); osd_write_char(0x4F - y10%3);
	}	
	if ((y11) < 16 && y11 >= 0) {
        previous_positions[4] = y11/3;
		osd_set_position(AH_LINE_START+y11/3, 11); osd_write_char(0x4F - y11%3);
	}	
	if ((y12) < 16 && y12 >= 0) {
        previous_positions[5] = y12/3;
		osd_set_position(AH_LINE_START+y12/3, 12); osd_write_char(0x4F - y12%3);
	}	
	if ((y13) < 16 && y13 >= 0) {
        previous_positions[6] = y13/3;
		osd_set_position(AH_LINE_START+y13/3, 13); osd_write_char(0x4F - y13%3);
	}	
	if ((y14) < 16 && y14 >= 0) {
        previous_positions[7] = y14/3;
		osd_set_position(AH_LINE_START+y14/3, 14); osd_write_char(0x4F - y14%3);
	}	
	if ((y15) < 16 && y15 >= 0) {
        previous_positions[8] = y15/3;
		osd_set_position(AH_LINE_START+y15/3, 15); osd_write_char(0x4F - y15%3);
	}	
	if ((y16) < 16 && y16 >= 0) {
        previous_positions[9] = y16/3;
		osd_set_position(AH_LINE_START+y16/3, 16); osd_write_char(0x4F - y16%3);
	}	
	if ((y17) < 16 && y17 >= 0) {
        previous_positions[10] = y17/3;
		osd_set_position(AH_LINE_START+y17/3, 17); osd_write_char(0x4F - y17%3);
	}	
	if ((y18) < 16 && y18 >= 0) {
        previous_positions[11] = y18/3;
		osd_set_position(AH_LINE_START+y18/3, 18); osd_write_char(0x4F - y18%3);
	}	
	if ((y19) < 16 && y19 >= 0) {
        previous_positions[12] = y19/3;
		osd_set_position(AH_LINE_START+y19/3, 19); osd_write_char(0x4F - y19%3);
	}	
	if ((y20) < 16 && y20 >= 0) {
        previous_positions[13] = y20/3;
		osd_set_position(AH_LINE_START+y20/3, 20); osd_write_char(0x4F - y20%3);
	}	
	if ((y21) < 16 && y21 >= 0) {
        previous_positions[14] = y21/3;
		osd_set_position(AH_LINE_START+y21/3, 21); osd_write_char(0x4F - y21%3);
	}	
	
	osd_set_position(AH_LINE_START+2, 14);
	osd_write_char(0x70);
}	


void osd_print_satellites_in_view(int small)
{
    const unsigned char* number;
    if (small)
        number = number_small;
    else
        number = number_large;

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
			osd_write_char(number[0]);
		else if (sensor_data.gps.satellites_in_view < 10)
			osd_write_char(number[sensor_data.gps.satellites_in_view]);
		else
			osd_write_char(number[9]);
	}		
}


/**
 *    A distance in meter, print it to screen using metric or imperial units.
 */
void print_meters(int row, int col, int m, int small)
{
	if (use_metric)
	{
		if (m > 999)
		{
			osd_print_integer((int)m/1000, row, col, small);
			osd_write_char(0x41);
			if (m > 9999)
				col += 3;
			else
				col += 2;
			
			osd_print_integer((m % 1000) / 100, row, col, small);
			osd_write_char(DISTANCE_KM); // km
            spiWriteReg(DM_ADDRL_WRITE, spiReadReg(DM_ADDRL_READ)+1);
			osd_write_char(0x00);
		}
		else
		{
			osd_print_integer(m, row, col, small);
			osd_write_char(DISTANCE_M); // m
            spiWriteReg(DM_ADDRL_WRITE, spiReadReg(DM_ADDRL_READ)+1);
			osd_write_char(0x00);
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
			osd_print_integer(m/5280, row, col, small);
			osd_write_char(0x41);
			col += 2;
			
			osd_print_integer((m % 5280) / 528, row, col, small);
			osd_write_char(DISTANCE_MI); // mi
            spiWriteReg(DM_ADDRL_WRITE, spiReadReg(DM_ADDRL_READ)+1);
			osd_write_char(0x00);
		}
		else
		{
			osd_print_integer(m, row, col, small);
			osd_write_char(DISTANCE_FT); // ft
            spiWriteReg(DM_ADDRL_WRITE, spiReadReg(DM_ADDRL_READ)+1);
			osd_write_char(0x00); 
		}		
	}	
}	


	
const unsigned char fourninemeter_large[] = {0x09,0x09,0x09,0x09,METER,0x00};
const unsigned char fourninemeter_small[] = {0x79,0x79,0x79,0x79,METER,0x00};


void osd_clear()
{
    do_clear_screen = 1;
}

void osd_print_integer(int num, int row, int col, int small)
{
	int index = col;
    const unsigned char* number;
    if (small)
        number = number_small;
    else
        number = number_large;
    
	if (num > 9999)
	{
		osd_set_position(row, index);
        if (small)
            osd_write_string(fourninemeter_small);
        else
            osd_write_string(fourninemeter_large);
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

    if (xSemaphoreTake( xSpiSemaphore, ( portTickType ) 1000 ) == pdTRUE )
    {
        max7456_init();

        spiWriteReg(0x00, 0x02);  // software reset
        vTaskDelay( ( portTickType ) 50 / portTICK_RATE_MS );

        spiWriteReg(VIDEO_MODE_0_WRITE, VIDEO_MODE_0_40_PAL);
        vTaskDelay( ( portTickType ) 100 / portTICK_RATE_MS );
        if (spiReadReg(VIDEO_MODE_0_READ) != VIDEO_MODE_0_40_PAL)
        {
            printf("no OSD found!\r\n");
            xSemaphoreGive( xSpiSemaphore );
            return 0;
        }

        while (osd_charactermemory_busy()) // wait for character memory to become available
            vTaskDelay( ( ( portTickType ) 200 / portTICK_RATE_MS ) );

        max7456_loadchars();

        if (osd_use_pal())
        {
            uart1_puts("found PAL, using PAL...");
            spiWriteReg(VIDEO_MODE_0_WRITE, VIDEO_MODE_0_40_PAL | OSD_EN);
        }
        else if (osd_use_ntsc())
        {
            uart1_puts("found NTSC, using NTSC...");
            spiWriteReg(VIDEO_MODE_0_WRITE, VIDEO_MODE_0_40_NTSC | OSD_EN);
        }
        else
        {
    #ifdef ENABLE_OSD_PAL_DEFAULT
            uart1_puts("no video input, using PAL...");
            spiWriteReg(VIDEO_MODE_0_WRITE, VIDEO_MODE_0_40_PAL | OSD_EN);
    #else
            uart1_puts("no video input, using NTSC...");
            spiWriteReg(VIDEO_MODE_0_WRITE, VIDEO_MODE_0_40_NTSC | OSD_EN);
    #endif
        }

        // black level
        x = spiReadReg(0xEC);
        vTaskDelayUntil( xLastExecutionTime, ( ( portTickType ) 1 / portTICK_RATE_MS ) );
        x &= 0xEF;
        spiWriteReg(0x6C, x);
        vTaskDelayUntil( xLastExecutionTime, ( ( portTickType ) 1 / portTICK_RATE_MS ) );
        spiWriteReg(0x04, 0x00);
        xSemaphoreGive( xSpiSemaphore );
    }
    else
    {
        printf("\r\nCould not initialize OSD\r\n");
    }
	return 1;
}





char osd_get_char_large(int c)
{
    if (c >= 'a' && c <= 'z')
        return 0x25 + (c - 'a');
    if (c >= 'A' && c <= 'Z')
        return 0x0B + (c - 'A');
    if (c >= '1' && c <= '9')
        return c - '1' + 1;

	switch(c)
	{
		case ' ': return 0x00; break;
		case '.': return 0x41; break;
        case '?': return 0x42; break;
        case '<': return 0x4A; break;
        case '>': return 0x4B; break;
        case '-': return 0x49; break;
        case '/': return 0x47; break;
        case ':': return 0x44; break;
        case '(': return 0x3F; break;
        case ')': return 0x40; break;
        case '0': return 0x0A; break;
        case '%': return 0x95; break;
        case ',': return 0x45; break;
		default: return 0x00;
	}		
}
char osd_get_char_small(int c)
{
    if (c > 'Z')
        c = c - 'a' + 'A';
    
    if (c >= 'A' && c <= 'Z')
        return 0x7B + (c - 'A');
    if (c >= '1' && c <= '9')
        return c - '1' + 0x71;

	switch(c)
	{
		case ' ': return 0x00; break;
		case '.': return 0x41; break;
        case '?': return 0x42; break;
        case '<': return 0x4A; break;
        case '>': return 0x4B; break;
        case '-': return 0x49; break;
        case '/': return 0x47; break;
        case ':': return 0x44; break;
        case '(': return 0x3F; break;
        case ')': return 0x40; break;
        case '0': return 0x0A; break;
        case ',': return 0x45; break;
        case '%': return 0x95; break;
		default: return 0x00;
	}
}


char osd_get_char(int c, int small)
{
    if (small)
        return osd_get_char_small(c);
    else
        return osd_get_char_large(c);
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
void osd_write_string(const unsigned char *x)
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


void osd_write_ascii_char(unsigned char x, int small)
{
	spiWriteReg(DM_CODE_IN_WRITE, osd_get_char(x, small));
}


void osd_write_ascii_string(unsigned char *x, int small)
{
	unsigned char l = spiReadReg(DM_ADDRL_READ);
	unsigned char h = spiReadReg(DM_ADDRH_READ);
	int pos = ((int)h)*256 + (int)l;
	while (*x != 0x00)
	{
		spiWriteReg(DM_CODE_IN_WRITE, osd_get_char(*x++, small));
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
