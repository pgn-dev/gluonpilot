#ifndef __OSD_H__
#define __OSD_H__

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
};

void osd_task( void *parameters );
void osd_post_message (char *str, int blink);
void osd_clear();

#endif