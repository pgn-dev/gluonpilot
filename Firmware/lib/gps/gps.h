#ifndef GPS_H
#define GPS_H



enum gps_status { ACTIVE = 1, VOID = 0, EMPTY = 2};

struct gps_info {
	long time;
	long date;
	long last_fix_time;
	int  satellites_in_view;
	int  height_m;
	
	double latitude_rad,
	       longitude_rad;
	      
	float speed_ms;      // meter per second
	float heading_rad;   // in radians
	
	int sentence_number_last_fix;
	enum gps_status status;
};


struct GpsConfig
{
	long initial_baudrate;
	long operational_baudrate;	
};	

void gps_init(struct GpsConfig *gpsconfig);

char gps_update_info(struct gps_info *gpsinfo);

void gps_wait_for_lock();

void gps_config_output();

void gps_open_port(struct GpsConfig *gpsconfig);

int gps_valid_frames_receiving();


#endif // GPS_H
