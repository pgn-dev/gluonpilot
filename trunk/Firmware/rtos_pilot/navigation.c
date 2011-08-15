/*! 
 *  Calculates the desired pitch and roll for navigation
 *
 *  @file     navigation.c
 *  @author   Tom Pycke
 *  @date     28-apr-2010
 *  @since    0.2
 */
 
#include <math.h>

// Include all FreeRTOS header files
#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

// Gluonpilot libraries
#include "dataflash/dataflash.h"
#include "ppm_in/ppm_in.h"

#include "configuration.h"
#include "sensors.h"
#include "navigation.h"
#include "common.h"

#define NAVIGATION_HZ 5  // 5 Hz

volatile struct NavigationData navigation_data;

float cos_latitude;
//! Convert latitude coordinates from radians into meters.
float latitude_meter_per_radian = 6363057.32484;

//! Convert longitude coordinates from radians into meters. 
//! This current value is only valid when you live around 50� N.
float longitude_meter_per_radian = 4107840.76433121;   // = Pi/180*(a / (1-e^2*sin(lat)^2)^(1/2))*cos(lat)
                                                                    // ~ cos(latitude) * latitude_meter_per_radian
void navigation_set_home();
float heading_rad_fromto (float diff_long, float diff_lat);
float distance_between_meter(float long1, float long2, float lat1, float lat2);
void navigation_do_circle(struct NavigationCode *current_code);
float get_variable(enum navigation_variable i);
int waypoint_reached(struct NavigationCode *current_code);
void convert_parameters_to_abs(int i);

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
        navigation_data.time_block_s = 0;
	navigation_data.wind_heading_set = 0;
	navigation_data.relative_positions_calculated = 0;
	navigation_data.desired_throttle_pct = -1;
	navigation_load();
}


void navigation_calculate_relative_position(int i)
{
	switch (navigation_data.navigation_codes[i].opcode)
	{
		case FROM_TO_REL:
                           navigation_data.navigation_codes[i].opcode = FROM_TO_ABS;
                           convert_parameters_to_abs(i);
                           break;
		case FLY_TO_REL:
                           navigation_data.navigation_codes[i].opcode = FLY_TO_ABS;
                           convert_parameters_to_abs(i);
                           break;
		case CIRCLE_REL:
                           navigation_data.navigation_codes[i].opcode = CIRCLE_ABS;
                           convert_parameters_to_abs(i);
                           break;
		default:
                           break;
	}	
}	

/*!
 *    Calculate absolute lat/lon positions for relative waypoints.
 */
void navigation_calculate_relative_positions()
{
	int i;
	for (i = 0; i < MAX_NAVIGATIONCODES; i++)
	{
		navigation_calculate_relative_position(i);
	}
	navigation_data.relative_positions_calculated = 1;
}


void convert_parameters_to_abs(int i)
{
    navigation_data.navigation_codes[i].x /= latitude_meter_per_radian;
    navigation_data.navigation_codes[i].x += navigation_data.home_latitude_rad;
    navigation_data.navigation_codes[i].y /= longitude_meter_per_radian;
    navigation_data.navigation_codes[i].y += navigation_data.home_longitude_rad;
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
        navigation_data.time_block_s++;
	}	
	
	// Set the "home"-position
	if (!navigation_data.airborne)
	{ 
		if (/*(ppm.channel[config.control.channel_motor] > 1600 || control_state.simulation_mode) &&*/
		    /*sensor_data.gps.speed_ms >= 2.0 &&*/ sensor_data.gps.status == ACTIVE && sensor_data.gps.satellites_in_view >= 5)
		{
			navigation_data.time_airborne_s = 0.0;  // reset this to know the real time airborne
			navigation_data.airborne = 1;
			navigation_set_home();
			navigation_data.last_waypoint_latitude_rad = navigation_data.home_latitude_rad;
			navigation_data.last_waypoint_longitude_rad = navigation_data.home_longitude_rad;
			navigation_calculate_relative_positions();  // we should send the new waypoints or calculate relative positions on the fly
		}
		else
		{
			if (sensor_data.gps.status != ACTIVE || sensor_data.gps.satellites_in_view < 5)
			{
				navigation_data.home_pressure_height = sensor_data.pressure_height;  // as opposed to GPS height!!
				//printf("\r\nHome altitude set\r\n");
			}
			navigation_set_home(); // set temporary home, not airborne
			
			navigation_data.desired_pre_bank = 0.0;
			//navigation_data.current_codeline = 0;
			// also return home @ 100m height
			navigation_data.desired_heading_rad = navigation_heading_rad_fromto(sensor_data.gps.longitude_rad,
	                                                   		                    sensor_data.gps.latitude_rad);
            navigation_data.desired_height_above_ground_m = 100.0;
		}	
		//return;
	}
	// set initial heading on take-off (fixed wing)
	if (!navigation_data.wind_heading_set && sensor_data.gps.speed_ms >= 2.0)
	{
		navigation_data.wind_heading_set = 1;
		navigation_data.wind_heading = sensor_data.gps.heading_rad;
		navigation_data.time_airborne_s = 0.0;  // reset this to know the real time airborne
	}
	

	struct NavigationCode *current_code = & navigation_data.navigation_codes[navigation_data.current_codeline];
	switch(current_code->opcode)
	{
		case CLIMB:
			navigation_data.desired_pre_bank = 0.0f;
			navigation_data.desired_throttle_pct = -1;

			if (navigation_data.wind_heading_set)
				navigation_data.desired_heading_rad = navigation_data.wind_heading;
			else 
				navigation_data.desired_heading_rad = sensor_data.gps.heading_rad;

			navigation_data.desired_height_above_ground_m = current_code->x + 1000.0f;
			if (sensor_data.pressure_height - navigation_data.home_pressure_height > current_code->x)
				navigation_data.current_codeline++;
			break;
		case FROM_TO_REL:
		case FROM_TO_ABS:
		{
			navigation_data.desired_pre_bank = 0.0f;
			navigation_data.desired_throttle_pct = -1;
			
			float leg_x = (current_code->x - navigation_data.last_waypoint_latitude_rad) * latitude_meter_per_radian;  // lat
  			float leg_y = (current_code->y - navigation_data.last_waypoint_longitude_rad) * longitude_meter_per_radian;  // lon
  			float leg2 = MAX(leg_x * leg_x + leg_y * leg_y, 1.f);
  			float nav_leg_progress = ((sensor_data.gps.latitude_rad - navigation_data.last_waypoint_latitude_rad) * latitude_meter_per_radian * leg_x + 
  			                          (sensor_data.gps.longitude_rad - navigation_data.last_waypoint_longitude_rad) * longitude_meter_per_radian * leg_y) / leg2;
  			float nav_leg_length = sqrtf(leg2);

			  /** distance of carrot (in meter) */
			float carrot = 4.0f * sensor_data.gps.speed_ms;
			
			nav_leg_progress += MAX(carrot / nav_leg_length, 0.f);
			
			if (nav_leg_progress >= 1.0f)
			{
				navigation_data.desired_heading_rad = navigation_heading_rad_fromto((float)(sensor_data.gps.longitude_rad - (double)current_code->y),
		                                                                            (float)(sensor_data.gps.latitude_rad - (double)current_code->x));
			}
			else
			{
				navigation_data.desired_heading_rad = navigation_heading_rad_fromto(
					(float)(sensor_data.gps.longitude_rad - (double)( navigation_data.last_waypoint_longitude_rad + nav_leg_progress * leg_y / longitude_meter_per_radian)),
		            (float)(sensor_data.gps.latitude_rad - (double)( navigation_data.last_waypoint_latitude_rad + nav_leg_progress * leg_x / latitude_meter_per_radian ) ) );
			}
				                                                         
	        navigation_data.desired_height_above_ground_m = current_code->a;
			
			if (waypoint_reached(current_code))
			{
				navigation_data.current_codeline++;
				navigation_data.last_waypoint_latitude_rad = current_code->x;
				navigation_data.last_waypoint_longitude_rad = current_code->y;
			}
			
			break;
		}
		case FLY_TO_REL:
		case FLY_TO_ABS:
			navigation_data.desired_pre_bank = 0.0;
			navigation_data.desired_throttle_pct = -1;
			
			navigation_data.desired_heading_rad = navigation_heading_rad_fromto((float)(sensor_data.gps.longitude_rad - (double)current_code->y),
	                                                                            (float)(sensor_data.gps.latitude_rad - (double)current_code->x));
	                                                         
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
			navigation_data.desired_throttle_pct = -1;
			navigation_do_circle(current_code);
			navigation_data.desired_height_above_ground_m = current_code->b;
			navigation_data.last_waypoint_latitude_rad = current_code->x;
			navigation_data.last_waypoint_longitude_rad = current_code->y;
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
			if (fabs(get_variable(current_code->a) - current_code->x) < 1e-6f)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline--;
			break;
		case UNTIL_NE:
			if (fabs(get_variable(current_code->a) - current_code->x) > 1e-6f)
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
			if (fabs(get_variable(current_code->a) - current_code->x) < 1e-6f)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline += 2;
			break;
		case IF_NE:
			if (fabs(get_variable(current_code->a) - current_code->x) > 1e-6f)
				navigation_data.current_codeline++;
			else
				navigation_data.current_codeline += 2;
			break;
		case SERVO_SET:
			servo_set_us(current_code->a, current_code->b);  // a = channel(0..7), b = microseconds (1000...2000)
			navigation_data.current_codeline++;
			break;
		case SERVO_TRIGGER:
		{
			unsigned int us = servo_read_us(current_code->a);
			servo_set_us(current_code->a, current_code->b);  // a = channel(0..7), b = microseconds (1000...2000)
			unsigned int ms_delay = (unsigned int)(current_code->x * 1000.0f);
			ms_delay = MIN(ms_delay, 3000);  // lets limit this to 3 seconds.
			vTaskDelay(( ( portTickType ) ms_delay / portTICK_RATE_MS ) );
			servo_set_us(current_code->a, us);  // set back to original position
		}	
			navigation_data.current_codeline++;
			break;
		case EMPTYCMD:
			navigation_data.desired_pre_bank = 0.0f;
			navigation_data.desired_throttle_pct = -1;
			navigation_data.current_codeline = 0;
			// also return home @ 100m height
			navigation_data.desired_heading_rad = navigation_heading_rad_fromto(sensor_data.gps.longitude_rad,
	                                                   		         sensor_data.gps.latitude_rad);
            navigation_data.desired_height_above_ground_m = 100.0f;
			break;
        case BLOCK:
            navigation_data.time_block_s = 0;
            navigation_data.current_codeline++;
            break;
        case FLARE_TO_REL:
        case FLARE_TO_ABS:
        {
			navigation_data.desired_pre_bank = 0.0f;
			
			navigation_data.desired_throttle_pct = current_code->b;
			
			float leg_x = (current_code->x - navigation_data.last_waypoint_latitude_rad) * latitude_meter_per_radian;  // lat
  			float leg_y = (current_code->y - navigation_data.last_waypoint_longitude_rad) * longitude_meter_per_radian;  // lon
  			float leg2 = MAX(leg_x * leg_x + leg_y * leg_y, 1.f);
  			float nav_leg_progress = ((sensor_data.gps.latitude_rad - navigation_data.last_waypoint_latitude_rad) * latitude_meter_per_radian * leg_x + 
  			                          (sensor_data.gps.longitude_rad - navigation_data.last_waypoint_longitude_rad) * longitude_meter_per_radian * leg_y) / leg2;
  			float nav_leg_length = sqrtf(leg2);

			  /** distance of carrot (in meter) */
			float carrot = 4.0f * sensor_data.gps.speed_ms;
			
			nav_leg_progress += MAX(carrot / nav_leg_length, 0.f);
			
			/*if (nav_leg_progress >= 1.0f)
			{
				navigation_data.desired_heading_rad = navigation_heading_rad_fromto((float)(sensor_data.gps.longitude_rad - (double)current_code->y),
		                                                                            (float)(sensor_data.gps.latitude_rad - (double)current_code->x));
			}
			else*/
				navigation_data.desired_heading_rad = navigation_heading_rad_fromto(
					(float)(sensor_data.gps.longitude_rad - (double)( navigation_data.last_waypoint_longitude_rad + nav_leg_progress * leg_y / longitude_meter_per_radian)),
		            (float)(sensor_data.gps.latitude_rad - (double)( navigation_data.last_waypoint_latitude_rad + nav_leg_progress * leg_x / latitude_meter_per_radian ) ) );
				                                                         
	        navigation_data.desired_height_above_ground_m = current_code->a;
			
			/*if (waypoint_reached(current_code))
			{
				navigation_data.current_codeline++;
				navigation_data.last_waypoint_latitude_rad = current_code->x;
				navigation_data.last_waypoint_longitude_rad = current_code->y;
			}*/
		    break;	
		}
		default:
			navigation_data.desired_pre_bank = 0.0f;
			navigation_data.current_codeline = 0;
			// also return home @ 100m height
			navigation_data.desired_heading_rad = navigation_heading_rad_fromto(sensor_data.gps.longitude_rad,
	                                                   		         sensor_data.gps.latitude_rad);
	        navigation_data.desired_height_above_ground_m = 100.0f; 
			break;
	
	}	
}


/*!
 *   Are we flying towards or away from the waypoint?
 */
int flying_towards_waypoint(struct NavigationCode *current_code)
{
	float heading_error_rad = navigation_data.desired_heading_rad - sensor_data.gps.heading_rad;
		
	if (heading_error_rad >= DEG2RAD(180.0f))
		heading_error_rad -= DEG2RAD(360.0f);
	else if (heading_error_rad <= DEG2RAD(-180.0f))
		heading_error_rad += DEG2RAD(360.0f);
	
	if (fabs(heading_error_rad) > DEG2RAD(90.0f))
		return 0;
	else
		return 1;	
}

	
int waypoint_reached(struct NavigationCode *current_code)
{
	if (navigation_distance_between_meter(sensor_data.gps.longitude_rad, current_code->y,
			                              sensor_data.gps.latitude_rad, current_code->x) < config.control.waypoint_radius_m)
	{
		if (! flying_towards_waypoint(current_code))  // we are flying away from the waypoint AND we are close enough
			return 1;
		else
			return 0;
	} 
	return 0;
}
	

float get_variable(enum navigation_variable i)
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
			return (float)navigation_data.time_airborne_s;
		case SATELLITES_IN_VIEW:
			return sensor_data.gps.satellites_in_view;
		case HOME_DISTANCE:
			return navigation_distance_between_meter(sensor_data.gps.longitude_rad, navigation_data.home_longitude_rad,
			                                         sensor_data.gps.latitude_rad, navigation_data.home_latitude_rad);
		case PPM_LINK_ALIVE:
			return ppm.connection_alive ? 1.0f : 0.0f;
		case CHANNEL_1:
			return (float)ppm.channel[0];
		case CHANNEL_2:
			return (float)ppm.channel[1];
		case CHANNEL_3:
			return (float)ppm.channel[2];
		case CHANNEL_4:
			return (float)ppm.channel[3];
		case CHANNEL_5:
			return (float)ppm.channel[4];
		case CHANNEL_6:
			return (float)ppm.channel[5];
		case CHANNEL_7:
			return (float)ppm.channel[6];
		case CHANNEL_8:
			return (float)ppm.channel[7];
		case BATT_V:
			return (float)(sensor_data.battery_voltage_10)/10.0f;
        case BLOCK_TIME:
            return (float)navigation_data.time_block_s;
        case ABS_ALTITUDE_ERROR:
        	return fabs(control_state.desired_altitude - sensor_data.pressure_height);
        case ABS_HEADING_ERROR:
        {
	        struct NavigationCode *next_code = & navigation_data.navigation_codes[navigation_data.current_codeline+1];
            float heading_error = navigation_heading_rad_fromto((float)(sensor_data.gps.longitude_rad - (double)(next_code->y)),
	                                                           (float)(sensor_data.gps.latitude_rad - (double)(next_code->x)));
	        heading_error = RAD2DEG(heading_error);
	        if (heading_error > 180.0f)
	        	heading_error -= 360.0f;
	        else if (heading_error < -180.0f)
	        	heading_error += 360.0f;
        	return fabs(heading_error);
	    } 
        case ABS_ALT_AND_HEADING_ERR:
        {
            struct NavigationCode *next_code = & navigation_data.navigation_codes[navigation_data.current_codeline+1];
            float heading_error = navigation_heading_rad_fromto((float)(sensor_data.gps.longitude_rad - (double)(next_code->y)),
	                                                           (float)(sensor_data.gps.latitude_rad - (double)(next_code->x)));
			heading_error = RAD2DEG(heading_error);
	        if (heading_error > 180.0f)
	        	heading_error -= 360.0f;
	        else if (heading_error < -180.0f)
	        	heading_error += 360.0f;
        	return fabs(control_state.desired_altitude - sensor_data.pressure_height) + fabs(heading_error);
        }	
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
	                                                    current_code->x - sensor_data.gps.latitude_rad);  // 0� = top of circle

	float distance_center = 
	 	navigation_distance_between_meter(sensor_data.gps.longitude_rad, current_code->y,
	                                      sensor_data.gps.latitude_rad, current_code->x);
	float next_alpha;
	float rad_ahead = rad_s*carrot;
	
	if (rad_ahead > 0.0f)
		rad_ahead = BIND(rad_ahead, DEG2RAD(10.0f), DEG2RAD(45.0f));   // because of /cos(rad_ahead)
	else
		rad_ahead = BIND(rad_ahead, DEG2RAD(-45.0f), DEG2RAD(-10.0f));   // because of /cos(rad_ahead)
		
		
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

	float next_r = abs_r / cosf(rad_ahead); // CHANGE sqrt(r*r + distance_ahe^ ad*distance_ahead);
			
	// max desired_heading
	float pointlon = current_code->y + sinf(next_alpha) * next_r / longitude_meter_per_radian;
	float pointlat = current_code->x + cosf(next_alpha) * next_r / latitude_meter_per_radian;
	
	
	navigation_data.desired_heading_rad = navigation_heading_rad_fromto(sensor_data.gps.longitude_rad - pointlon,
		                                                                sensor_data.gps.latitude_rad - pointlat);
		                                                     
	if (navigation_data.desired_heading_rad > DEG2RAD(360.0f))
		navigation_data.desired_heading_rad -= DEG2RAD(360.0f);
	else if (navigation_data.desired_heading_rad < 0.0f)
		navigation_data.desired_heading_rad += DEG2RAD(360.0f);
		
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
float navigation_heading_rad_fromto (float diff_long, float diff_lat)
{
	//diff_lat *= cos_latitude;   // Local, flat earth approximation!
	diff_long *= cos_latitude;   // Local, flat earth approximation!
	
	float waypointHeading = atan2(diff_long, -diff_lat);

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
