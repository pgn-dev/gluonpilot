/*! 
 *  Calculates the desired pitch and roll for navigation
 *
 *
 *  @file     navigation.c
 *  @author   Tom Pycke
 *  @date     28-apr-2010
 *  @since    0.2
 */
 
#include <math.h>
 
#include "dataflash/dataflash.h"
#include "ppm_in/ppm_in.h"
 
#include "configuration.h"
#include "sensors.h"
#include "navigation.h"
#include "common.h"


struct NavigationData navigation_data;

double cos_latitude;
//! Convert latitude coordinates from radians into meters.
float latitude_meter_per_radian = 6363057.32484;

//! Convert longitude coordinates from radians into meters. 
//! This current value is only valid when you live around 50° N.
float longitude_meter_per_radian = 4107840.76433121;   // = Pi/180*(a / (1-e^2*sin(lat)^2)^(1/2))*cos(lat)
                                                                    // ~ cos(latitude) * latitude_meter_per_radian
void navigation_set_home();
double heading_rad_fromto (double diff_long, double diff_lat);
float distance_between_meter(float long1, float long2, float lat1, float lat2);
void navigation_do_circle(struct NavigationCode *current_code);



/*!
 *  Initializes the navigation.
 */
void navigation_init ()
{
	navigation_data.home_longitude_rad = 0.0;
	navigation_data.home_latitude_rad = 0.0;
	navigation_data.home_gps_height = 0.0;
	navigation_data.home_pressure_height = sensor_data.pressure_height;  // as opposed to GPS height!!

	navigation_data.desired_heading_rad = 0.0;
	//navigation_data.distance_next_waypoint = 0.0;
	
	navigation_data.airborne = 0;
	
	navigation_data.current_codeline = 0;
	navigation_data.last_code = 0;
	
	navigation_load();
}


/*!
 *    Calculate absolute lat/lon positions for relative waypoints.
 */
void navigation_calculate_relative_positions()
{
	int i;
	for (i = 0; i < MAX_NAVIGATIONCODES; i++)
	{
		switch (navigation_data.navigation_codes[i].opcode)
		{
			case FROM_TO_REL:
			case FLY_TO_REL:
			case CIRCLE_REL:
				navigation_data.navigation_codes[i].x /= latitude_meter_per_radian;
				navigation_data.navigation_codes[i].x += navigation_data.home_latitude_rad;
				navigation_data.navigation_codes[i].y /= longitude_meter_per_radian;
				navigation_data.navigation_codes[i].y += navigation_data.home_longitude_rad;
				break;
			default:
				break;
		}	
	}		
}	


void navigation_burn()
{
	dataflash_write(NAVIGATION_PAGE, sizeof(navigation_data.navigation_codes), (unsigned char*) & (navigation_data.navigation_codes));
}

	
void navigation_load()
{
	dataflash_read(NAVIGATION_PAGE, sizeof(navigation_data.navigation_codes), (unsigned char*) & (navigation_data.navigation_codes));
}	


void navigation_update()
{
	// Set the "home"-position
	if (!navigation_data.airborne)
	{ 
		if (sensor_data.gps.speed_ms >= 2 && sensor_data.gps.status == ACTIVE && sensor_data.gps.satellites_in_view >= 5)
		{
			navigation_data.airborne = 1;
			navigation_set_home();
			navigation_data.wind_heading = sensor_data.gps.heading_rad;
			navigation_calculate_relative_positions();
		}
		else
		{
			if (sensor_data.gps.status != ACTIVE)
				navigation_data.home_pressure_height = sensor_data.pressure_height;  // as opposed to GPS height!!
			
			// calibrate gyros, FIXME: outer feedback loop would no longer require this
			if (ppm.channel[config.control.channel_yaw] < 1100 &&   // yaw = left, roll = right while not airborne
			    ppm.channel[config.control.channel_roll] > 1900)
			{
				// Test: use neutral value ad-hoc		
				config.sensors.gyro_x_neutral = config.sensors.gyro_x_neutral/2 + sensor_data.gyro_x_raw/2;
				config.sensors.gyro_y_neutral = config.sensors.gyro_y_neutral/2 + sensor_data.gyro_y_raw/2;
				config.sensors.gyro_z_neutral = config.sensors.gyro_z_neutral/2 + sensor_data.gyro_z_raw/2;			    
			} 
		}	
		return;
	}

	navigation_data.desired_pre_bank = 0.0;
	struct NavigationCode *current_code = & navigation_data.navigation_codes[navigation_data.current_codeline];
	
	switch(current_code->opcode)
	{
		case CLIMB:
			navigation_data.desired_heading_rad = navigation_data.wind_heading;
			navigation_data.desired_height_above_ground_m = current_code->x;
			if (sensor_data.pressure_height - navigation_data.home_pressure_height > navigation_data.desired_height_above_ground_m)
				navigation_data.current_codeline++;
			break;
		case FROM_TO_REL:
		case FROM_TO_ABS:
		
			break;
		
		case FLY_TO_REL:
		case FLY_TO_ABS:
			navigation_data.desired_heading_rad = heading_rad_fromto(sensor_data.gps.longitude_rad - current_code->y,
	                                                         sensor_data.gps.latitude_rad - current_code->x);
	                                                         
	        navigation_data.desired_height_above_ground_m = current_code->a;

			if (distance_between_meter(sensor_data.gps.longitude_rad, current_code->y,
			                           sensor_data.gps.latitude_rad, current_code->x) < config.control.waypoint_radius_m)
			{
				double heading_error_rad = navigation_data.desired_heading_rad - sensor_data.gps.heading_rad;
				
				if (heading_error_rad >= DEG2RAD(180.0))
					heading_error_rad -= DEG2RAD(360.0);
				else if (heading_error_rad <= DEG2RAD(-180.0))
					heading_error_rad += DEG2RAD(360.0);
				
				if (fabs(heading_error_rad) > DEG2RAD(90.0)) // we are flying away from the waypoint AND we are close enough
				{
					navigation_data.current_codeline++;
				}	
			} 
			break;
		
		case CIRCLE_REL:
		case CIRCLE_ABS:
			navigation_do_circle(current_code);
			navigation_data.desired_height_above_ground_m = current_code->b;
			navigation_data.current_codeline++;
			break;
		case GOTO:
			navigation_data.current_codeline = current_code->a;
			break;
		case EMPTYCMD:
			navigation_data.current_codeline = 0;
			break;
		//default:
		
	}	
}



void navigation_do_circle(struct NavigationCode *current_code)
{
	float r = (float)current_code->a; // meter
	float rad_s = sensor_data.gps.speed_ms / r;   // rad/s for this circle
#define carrot 5.0
	float distance_ahead = carrot * sensor_data.gps.speed_ms;
		
	// heading towards center of circle
	navigation_data.desired_heading_rad = heading_rad_fromto(sensor_data.gps.longitude_rad - current_code->y,
	                                                         sensor_data.gps.latitude_rad - current_code->x);
	float current_alpha = navigation_data.desired_heading_rad - DEG2RAD(180.0);  // 0° = top of circle

	float distance_center = 
	 	distance_between_meter(sensor_data.gps.longitude_rad, current_code->y,
	                           sensor_data.gps.latitude_rad, current_code->x);
	
	float next_alpha;
	if (distance_center > r + distance_ahead ||
	    distance_center < r - distance_ahead)  // too far in or out of the circle?
	{
		next_alpha = current_alpha + DEG2RAD(90.0); // fly to point where you would touch the circle
	}
	else
	{
		float rad_ahead = rad_s*carrot;
		/*if (rad_ahead > 3.14159/4.0)	
			next_alpha = current_alpha + 3.14159/4.0; // go to the position one second ahead
		if (rad_ahead < 3.14159/16.0)	
			next_alpha = current_alpha + 3.14159/16.0; // go to the position one second ahead
		else*/
			next_alpha = current_alpha + atan(distance_ahead/r);
	}	

	
	navigation_data.desired_pre_bank =(distance_center > r + distance_ahead || 
	                                   distance_center < r + distance_ahead) ? 0 :
  				                          atan(sensor_data.gps.speed_ms*sensor_data.gps.speed_ms / (G*r));

	float next_r = sqrt(r*r + distance_ahead*distance_ahead);
			
	// max desired_heading
	float pointlon = current_code->y + sin(next_alpha) * next_r / longitude_meter_per_radian;
	float pointlat = current_code->x + cos(next_alpha) * next_r / latitude_meter_per_radian;
	
	
	navigation_data.desired_heading_rad = heading_rad_fromto(sensor_data.gps.longitude_rad - pointlon,
		                                                     sensor_data.gps.latitude_rad - pointlat);
		                                                     
	if (navigation_data.desired_heading_rad > DEG2RAD(360.0))
		navigation_data.desired_heading_rad -= DEG2RAD(360.0);
	else if (navigation_data.desired_heading_rad < 0.0)
		navigation_data.desired_heading_rad += DEG2RAD(360.0);
		
	navigation_data.desired_height_above_ground_m = current_code->b;
}	


/*!
 *  Save the current position as the GPS position.
 */
void navigation_set_home()
{
	navigation_data.home_longitude_rad = sensor_data.gps.longitude_rad;
	navigation_data.home_latitude_rad = sensor_data.gps.latitude_rad;
	navigation_data.home_gps_height = sensor_data.gps.height_m;
	
	cos_latitude = cos(sensor_data.gps.latitude_rad);
	longitude_meter_per_radian = latitude_meter_per_radian * cos_latitude;  // approx
}



/*!
 *  Calculates the heading from waypoint a to waypoint b.
 *  @param diff_long Origin longitude - destination longitude.
 *  @param diff_lat Origin latitude - destination latitude.
 */
double heading_rad_fromto (double diff_long, double diff_lat)
{
	//diff_lat *= cos_latitude;   // Local, flat earth approximation!
	diff_long *= cos_latitude;   // Local, flat earth approximation!
	
	double waypointHeading = atan2(diff_long, -diff_lat);

	// make clockwise direction positive (CCW is +ve as is)
	if(sin(diff_long) > 0.0)
		waypointHeading = (2.0*PI) - waypointHeading;
	else
		waypointHeading = -waypointHeading;

	return waypointHeading;
}


/*!
 *  Calculates the distance between 2 waypoints.
 *  Won't give good results if the waypoints are several 100 kms apart.
 */
float distance_between_meter(float long1, float long2, float lat1, float lat2)
{
	/* Haversine */
	//return acosf(sinf(lat1)*sinf(lat2) + cosf(lat1)*cosf(lat2)*cosf(long2-long1)) * 6371.0

	/*
		float difflong = (long1 - long2) * cos(lat1) * latitude_meter_per_radian;
		float difflat = (lat1 - lat2) * latitude_meter_per_radian;
	*/	
	
	/* simple: */
	float difflong = (long1 - long2) * longitude_meter_per_radian;
	float difflat = (lat1 - lat2) * latitude_meter_per_radian;
	return sqrt(difflong*difflong + difflat*difflat);
}
