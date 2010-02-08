#ifndef SENSORS_H
#define SENSORS_H

#include "gps/gps.h"


struct SensorData
{
	unsigned int acc_x_raw, acc_y_raw, acc_z_raw;
	unsigned int gyro_x_raw, gyro_y_raw, gyro_z_raw;
	float acc_x, acc_y, acc_z;
	float p, q, r;
	float roll, pitch, yaw;
	float roll_acc, pitch_acc;
	float w; // estimated speed along z axis
	
	float pressure;
	float temperature;
	float pressure_height;
	int temperature_10;
	
	struct gps_info gps;
};

struct SensorConfig
{
	float acc_x_neutral;	
	float acc_y_neutral;	
	float acc_z_neutral;	

	float gyro_x_neutral;	
	float gyro_y_neutral;	
	float gyro_z_neutral;
};

extern struct SensorData sensor_data;

void sensors_task( void *parameters );
void sensors_gps_task( void *parameters );


#endif // SENSORS_H
