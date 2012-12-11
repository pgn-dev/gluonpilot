#ifndef __OSD_H__
#define __OSD_H__

enum RssiMode { None = 0, Analog = 1, Ch1 = 2, Ch2 = 3, Ch3 = 4, Ch4 = 5, Ch5 = 6, Ch6 = 7, Ch7 = 8, Ch8 = 9, Ch9 = 10, Ch10 = 11 };

struct OsdConfig
{
    unsigned show_artificial_horizon : 1;
    unsigned show_flight_time : 1;
    unsigned show_mode : 1;
    unsigned show_gps_status : 1;
    unsigned show_rc_link : 1;
    unsigned show_altitude : 1;
    unsigned show_speed : 1;
    unsigned show_voltage1 : 1;
    unsigned show_voltage2 : 1;
    unsigned show_current : 1;
    unsigned show_mah : 1;
    unsigned show_vario : 1;
    unsigned show_distance_home : 1;
    unsigned show_arrow_home : 1;
    unsigned show_block_name : 1;
    enum RssiMode rssi;
    unsigned char voltage_low;
    unsigned char voltage_high;

};

void osd_task( void *parameters );
void osd_post_message (char *str, int blink);
void osd_clear();

#endif
