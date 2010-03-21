#ifndef DATALOGGER_H
#define DATALOGGER_H

#define MAX_INDEX 10 

struct LogIndex
{
	int  page_num;
	long date;
	long time;
	long boot_sequence; // Gps OK, Ppm OK, Reboot
};

extern struct LogIndex datalogger_index_table[MAX_INDEX];

struct LogLine
{
	float gps_latitude_rad;
	float gps_longitude_rad;
	int gps_height_m;
	int gps_speed_m_s;
	int gps_heading;
	
	int pitch;
	int roll;
	int yaw;
	int acc_x;
	int acc_y;
	int acc_z;
	int gyro_x;
	int gyro_y;
	int gyro_z;
	
	int control_state;
	
	char temperature_c;
	int  height_m;
};


void datalogger_init();
void datalogger_writeline(struct LogLine *line);
void datalogger_task( void *parameters );
void datalogger_format();
int datalogger_print_next_page(int index, void(*printer)(struct LogLine*));
void datalogger_enable();
void datalogger_disable();



#endif // DATALOGGER_H
