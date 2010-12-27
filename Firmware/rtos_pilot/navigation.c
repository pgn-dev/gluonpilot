/*! 
 *  Calculates the desired pitch and roll for navigation
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

#define NAVIGATION_HZ 5  // 5 Hz

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
double get_variable(enum navigation_variable i);
int waypoint_reached(struct NavigationCode *current_code);

unsigned int ticks_counter = 0;

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
	
	navigation_data.time_airborne_s = 0;
	
	navigation_load();
	
	// Testing:
	/*sensor_data.gps.latitude_rad = DEG2RAD(50.852200);
	sensor_data.gps.longitude_rad = DEG2RAD(3.670674);
	navigation_set_home();

	sensor_data.gps.latitude_rad = DEG2RAD(50.853371);
	sensor_data.gps.longitude_rad = DEG2RAD(3.671246);
	sensor_data.gps.speed_ms = 19;
	
	navigation_data.navigation_codes[1].x =  navigation_data.home_latitude_rad;
	navigation_data.navigation_codes[1].y = navigation_data.home_longitude_rad;
	
	navigation_do_circle(& navigation_data.navigation_codes[1]);*/
	

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
	ticks_counter++;
	if (ticks_counter == NAVIGATION_HZ)
	{
		ticks_counter = 0;
		navigation_data.time_airborne_s++;
	}	
	
	// Set the "home"-position
	if (!navigation_data.airborne)
	{ 
		if (sensor_data.gps.speed_ms >= 3 && sensor_data.gps.status == ACTIVE && sensor_data.gps.satellites_in_view >= 5)
		{
			navigation_data.time_airborne_s = 0.0;  // reset this to know the real time airborne
			navigation_data.airborne = 1;
			navigation_set_home();
			navigation_data.last_waypoint_latitude_rad = navigation_data.home_latitude_rad;
			navigation_data.last_waypoint_longitude_rad = navigation_data.home_longitude_rad;
			navigation_data.wind_heading = sensor_data.gps.heading_rad;
			navigation_calculate_relative_positions();
		}
		else
		{
			if (sensor_data.gps.status != ACTIVE)
			{
				navigation_data.home_pressure_height = sensor_data.pressure_height;  // as opposed to GPS height!!
			}
			navigation_set_home(); // set temporary home, not airborne
		}	
		return;
	}
	

	struct NavigationCode *current_code = & navigation_data.navigation_codes[navigation_data.current_codeline];
	
	switch(current_code->opcode)
	{
		case CLIMB:
			navigation_data.desired_pre_bank = 0.0;
			navigation_data.desired_heading_rad = navigation_data.wind_heading;
			navigation_data.desired_height_above_ground_m = current_code->x + 1000.0;
			if (sensor_data.pressure_height - navigation_data.home_pressure_height > current_code->x)
				navigation_data.current_codeline++;
			break;
		case FROM_TO_REL:
		case FROM_TO_ABS:
			navigation_data.desired_pre_bank = 0.0;
			
			float leg_x = (current_code->x - navigation_data.last_waypoint_latitude_rad) * latitude_meter_per_radian;  // lat
  			float leg_y = (current_code->y - navigation_data.last_waypoint_longitude_rad) * longitude_meter_per_radian;  // lon
  			float leg2 = MAX(leg_x * leg_x + leg_y * leg_y, 1.);
  			float nav_leg_progress = ((sensor_data.gps.latitude_rad - navigation_data.last_waypoint_latitude_rad) * latitude_meter_per_radian * leg_x + 
  			                          (sensor_data.gps.longitude_rad - navigation_data.last_waypoint_longitude_rad) * longitude_meter_per_radian * leg_y) / leg2;
  			float nav_leg_length = sqrt(leg2);

			  /** distance of carrot (in meter) */
			float carrot = 4.0 * sensor_data.gps.speed_ms;
			
			nav_leg_progress += MAX(carrot / nav_leg_length, 0.);
			
			if (nav_leg_progress >= 1.0)
			{
				navigation_data.desired_heading_rad = navigation_heading_rad_fromto(sensor_data.gps.longitude_rad - current_code->y,
	                                                         sensor_data.gps.latitude_rad - current_code->x);
			}
			else
			{
				navigation_data.desired_heading_rad = navigation_heading_rad_fromto(
					sensor_data.gps.longitude_rad - ( navigation_data.last_waypoint_longitude_rad + nav_leg_progress * leg_y / longitude_meter_per_radian),
		            sensor_data.gps.latitude_rad - ( navigation_data.last_waypoint_latitude_rad + nav_leg_progress * leg_x / latitude_meter_per_radian ) );
			}
				                                                         
	        navigation_data.desired_height_above_ground_m = current_code->a;
			
			if (waypoint_reached(current_code))
			{
				navigation_data.current_codeline++;
				navigation_data.last_waypoint_latitude_rad = current_code->x;
				navigation_data.last_waypoint_longitude_rad = current_code->y;
			}
			
			break;
		
		case FLY_TO_REL:
		case FLY_TO_ABS:
			navigation_data.desired_pre_bank = 0.0;
			navigation_data.desired_heading_rad = navigation_heading_rad_fromto(sensor_data.gps.longitude_rad - current_code->y,
	                                                         sensor_data.gps.latitude_rad - current_code->x);
	                                                         
	        navigation_data.desired_height_above_ground_m = current_code->a;
			
			if (waypoint_reached(current_code))
			{
				navigation_data.current_codeline++;
				navigation_data.last_waypoint_latitude_rad = current_code->x;
				navigation_data.last_waypoint_longitude_rad = current_code->y;
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
		case UNTIL_GR:
			if (get_variable(current_code->a) > current_code->x)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline--;
			break;
		case UNTIL_SM:
			//printf("-> %f < %f => %d\n\r", get_variable(current_code->a), current_code->x, (int)(get_variable(current_code->a) < current_code->x));
			if (get_variable(current_code->a) < current_code->x)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline--;
			break;
		case UNTIL_EQ:
			if (fabs(get_variable(current_code->a) - current_code->x) < 1e-6)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline--;
			break;
		case UNTIL_NE:
			if (fabs(get_variable(current_code->a) - current_code->x) > 1e-6)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline--;
			break;
		case IF_GR:
			if (get_variable(current_code->a) > current_code->x)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline += 2;
			break;
		case IF_SM:
			if (get_variable(current_code->a) < current_code->x)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline += 2;
			break;
		case IF_EQ:
			if (fabs(get_variable(current_code->a) - current_code->x) < 1e-6)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline += 2;
			break;
		case IF_NE:
			if (fabs(get_variable(current_code->a) - current_code->x) > 1e-6)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline += 2;
			break;
		case EMPTYCMD:
			navigation_data.desired_pre_bank = 0.0;
			navigation_data.current_codeline = 0;
			// also return home @ 100m height
			navigation_data.desired_heading_rad = navigation_heading_rad_fromto(sensor_data.gps.longitude_rad,
	                                                   		         sensor_data.gps.latitude_rad);
	        navigation_data.desired_height_above_ground_m = 100.0; 
			break;
		//default:
		
	}	
}


int waypoint_reached(struct NavigationCode *current_code)
{
	if (navigation_distance_between_meter(sensor_data.gps.longitude_rad, current_code->y,
			                              sensor_data.gps.latitude_rad, current_code->x) < config.control.waypoint_radius_m)
	{
		double heading_error_rad = navigation_data.desired_heading_rad - sensor_data.gps.heading_rad;
		
		if (heading_error_rad >= DEG2RAD(180.0))
			heading_error_rad -= DEG2RAD(360.0);
		else if (heading_error_rad <= DEG2RAD(-180.0))
			heading_error_rad += DEG2RAD(360.0);
		
		if (fabs(heading_error_rad) > DEG2RAD(90.0)) // we are flying away from the waypoint AND we are close enough
		{
			return 1;
		}
	} 
	return 0;
}
	

double get_variable(enum navigation_variable i)
{
	switch (i)
	{
		case HEIGHT:
			return sensor_data.pressure_height - navigation_data.home_pressure_height;
		case SPEED_MS:
			return sensor_data.gps.speed_ms;
		case HEADING_DEG:
			return RAD2DEG(sensor_data.gps.heading_rad);
		case FLIGHT_TIME_S:
			return 0.0;
		case SATELLITES_IN_VIEW:
			return sensor_data.gps.satellites_in_view;
		case HOME_DISTANCE:
			return navigation_distance_between_meter(sensor_data.gps.longitude_rad, navigation_data.home_longitude_rad,
			                                         sensor_data.gps.latitude_rad, navigation_data.home_latitude_rad);
		case PPM_LINK_ALIVE:
			return ppm.connection_alive ? 1.0 : 0.0;
		case CHANNEL_1:
			return (double)ppm.channel[0];
		case CHANNEL_2:
			return (double)ppm.channel[1];
		case CHANNEL_3:
			return (double)ppm.channel[2];
		case CHANNEL_4:
			return (double)ppm.channel[3];
		case CHANNEL_5:
			return (double)ppm.channel[4];
		case CHANNEL_6:
			return (double)ppm.channel[5];
		case CHANNEL_7:
			return (double)ppm.channel[6];
		case CHANNEL_8:
			return (double)ppm.channel[7];
		default:
			return 0.0;
	}	
}	


void navigation_do_circle(struct NavigationCode *current_code)
{
	float r = (float)current_code->a; // meter
	float rad_s = sensor_data.gps.speed_ms / r;   // rad/s for this circle
#define carrot 4.0
	float distance_ahead = carrot * sensor_data.gps.speed_ms;
	float abs_r = fabs(r);

	// heading towards center of circle
	float current_alpha = navigation_heading_rad_fromto(current_code->y - sensor_data.gps.longitude_rad,
	                                                    current_code->x - sensor_data.gps.latitude_rad);  // 0° = top of circle

	float distance_center = 
	 	navigation_distance_between_meter(sensor_data.gps.longitude_rad, current_code->y,
	                                      sensor_data.gps.latitude_rad, current_code->x);
	float next_alpha;
	float rad_ahead = rad_s*carrot;
	
	//if (distance_center > abs_r + distance_ahead ||
	//    distance_center < abs_r - distance_ahead)  // too far in or out of the circle?
	//{
	//	next_alpha = current_alpha + DEG2RAD(90.0); // fly to point where you would touch the circle
	//}
	//else
	//{
		/*if (rad_ahead > 3.14159/4.0)	
			next_alpha = current_alpha + 3.14159/4.0; // go to the position one second ahead
		if (rad_ahead < 3.14159/16.0)	
			next_alpha = current_alpha + 3.14159/16.0; // go to the position one second ahead
		else*/
			next_alpha = current_alpha + rad_ahead; //atan(distance_ahead/r);   CHANGE
	//}	

	
	navigation_data.desired_pre_bank = (distance_center > abs_r + distance_ahead*2.0 || 
	                                   distance_center < abs_r - distance_ahead) ? 0 :
  				                          atan(sensor_data.gps.speed_ms*sensor_data.gps.speed_ms / (G*r));

	float next_r = abs_r / cos(rad_ahead); // CHANGE sqrt(r*r + distance_ahead*distance_ahead);
			
	// max desired_heading
	float pointlon = current_code->y + sin(next_alpha) * next_r / longitude_meter_per_radian;
	float pointlat = current_code->x + cos(next_alpha) * next_r / latitude_meter_per_radian;
	
	
	navigation_data.desired_heading_rad = navigation_heading_rad_fromto(sensor_data.gps.longitude_rad - pointlon,
		                                                                sensor_data.gps.latitude_rad - pointlat);
		                                                     
	if (navigation_data.desired_heading_rad > DEG2RAD(360.0))
		navigation_data.desired_heading_rad -= DEG2RAD(360.0);
	else if (navigation_data.desired_heading_rad < 0.0)
		navigation_data.desired_heading_rad += DEG2RAD(360.0);
		
	navigation_data.desired_height_above_ground_m = current_code->b;
	
	/*printf("-> %f | %f", distance_center, current_alpha);
	printf("(%f) %f\r\n", navigation_data.desired_pre_bank/3.14159*180.0, navigation_data.desired_heading_rad/3.14159*180.0);
	printf("(%f, %f) @ %d\r\n", RAD2DEG(current_code->x), RAD2DEG(current_code->y), current_code->a);*/
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
double navigation_heading_rad_fromto (double diff_long, double diff_lat)
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
float navigation_distance_between_meter(float long1, float long2, float lat1, float lat2)
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
