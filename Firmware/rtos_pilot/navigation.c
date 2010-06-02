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
 
#include "configuration.h"
#include "sensors.h"
#include "navigation.h"

#define PI 3.14159

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




/*!
 *  Initializes the navigation.
 */
void navigation_init ()
{
	navigation_data.home_longitude_rad = 0.0;
	navigation_data.home_latitude_rad = 0.0;
	navigation_data.home_gps_height = 0.0;
	navigation_data.home_pressure_height = sensor_data.pressure_height;

	navigation_data.desired_heading_rad = 0.0;
	navigation_data.height_error = 0.0;
	navigation_data.distance_next_waypoint = 0.0;
	
	navigation_data.airborne = 0;
	
//	navigation_data.waypoint[0].latitude = 60.0;
//	navigation_data.waypoint[0].longitude = 60.0;
//	navigation_data.waypoint[0].type = RELATIVE_HOME_METERS;
//	navigation_data.waypoint[1].latitude = -60.0;
//	navigation_data.waypoint[1].longitude = 60.0;
//	navigation_data.waypoint[1].type = RELATIVE_HOME_METERS;
//	navigation_data.waypoint[2].latitude = -60.0;
//	navigation_data.waypoint[2].longitude = -60.0;
//	navigation_data.waypoint[2].type = RELATIVE_HOME_METERS;
//	navigation_data.waypoint[3].latitude = 60.0;
//	navigation_data.waypoint[3].longitude = -60.0;
//	navigation_data.waypoint[3].type = RELATIVE_HOME_METERS;
//	navigation_data.waypoint[4].latitude = 0.0;
//	navigation_data.waypoint[4].longitude = 0.0;
//	navigation_data.waypoint[4].type = REWIND;
	
	navigation_data.current_waypoint = 0;
	navigation_data.last_waypoint = 3;
}

	
/*!
 *  Update the navigation state.
 */
void navigation_update()
{
	// Set the "home"-position
	if (!navigation_data.airborne)
	{ 
		if (sensor_data.gps.speed_ms >= 2.0)
		{
			navigation_data.airborne = 1;
			navigation_set_home();		
			// pre-calculate waypoints
			//navigation_calculate(navigation);
		}
		else
			return;
	}

	
	if (1)   // One waypoint: "home"
	{
		float carrot = 5.0;
		
		float r = 90.0; // meter
		float rad_s = sensor_data.gps.speed_ms / r;   // rad/s for this circle
		
		// heading towards center of circle
		navigation_data.desired_heading_rad = heading_rad_fromto(sensor_data.gps.longitude_rad - navigation_data.home_longitude_rad,
		                                                         sensor_data.gps.latitude_rad - navigation_data.home_latitude_rad);
		float current_alpha = navigation_data.desired_heading_rad - 3.14159;  // 0° = top of circle

		float distance_center = 
		 	distance_between_meter(sensor_data.gps.longitude_rad, navigation_data.home_longitude_rad,
		                           sensor_data.gps.latitude_rad, navigation_data.home_latitude_rad);
		
		float next_alpha;
		if (distance_center > r + carrot*sensor_data.gps.speed_ms ||
		    distance_center < r - carrot*sensor_data.gps.speed_ms)  // too far in or out of the circle?
		{
			next_alpha = current_alpha + 3.14159/2.0; // fly to point where you would touch the circle
		}
		else
		{
			float rad_ahead = rad_s*carrot;
			/*if (rad_ahead > 3.14159/4.0)	
				next_alpha = current_alpha + 3.14159/4.0; // go to the position one second ahead
			if (rad_ahead < 3.14159/16.0)	
				next_alpha = current_alpha + 3.14159/16.0; // go to the position one second ahead
			else*/
				next_alpha = current_alpha + rad_ahead; // go to the position one second ahead

		}	
		
		// max desired_heading
		float pointlon = navigation_data.home_longitude_rad + sin(next_alpha) * r / longitude_meter_per_radian;
		float pointlat = navigation_data.home_latitude_rad + cos(next_alpha) * r / latitude_meter_per_radian;
	
		navigation_data.desired_heading_rad = heading_rad_fromto(sensor_data.gps.longitude_rad - pointlon,
			                                                     sensor_data.gps.latitude_rad - pointlat);
	}
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
	diff_lat *= cos_latitude;   // Local, flat earth approximation!
	
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

