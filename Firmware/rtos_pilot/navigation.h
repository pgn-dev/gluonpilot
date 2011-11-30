#ifndef NAVIGATION_H
#define NAVIGATION_H

#include "gluonscript.h"

void navigation_init();
//void navigation_update();
float navigation_heading_rad_fromto (float diff_long, float diff_lat); // used in OSD-code
float navigation_distance_between_meter(float long1, float long2, float lat1, float lat2);
void navigation_calculate_relative_position(int i);
void navigation_calculate_relative_positions();


/*!
 *  The different waypoint types.
 */
/*enum waypoint_type { 
	RADIANS,              //!< Represents a standard waypoint (lon, lat) in radians 
	RELATIVE_HOME_METERS, //!< The (lon, lat) values are meters relative to the home position
	RELATIVE_LAST_METERS, //!< The (lon, lat) values are meters relative to the previous waypoint
	REWIND,               //!< Go back to the first waypoint
	FROM_TO               //!< Fly on a straight line from the previous waypoint to the next one
};*/


/*!
 *   Holds all the navigation information.
 */
struct NavigationData
{
	double home_longitude_rad;     //!< Home position, radians.
	double home_latitude_rad;      //!< Home position, radians.
	float home_gps_height;        //!< Height of home.
	float home_pressure_height;
	
	float home_distance;          //<! Use for OSD
	float home_heading;           //<! Use for OSD
	
	float last_waypoint_latitude_rad;
	float last_waypoint_longitude_rad;
	float last_waypoint_altitude_agl;
	
	float loiter_waypoint_latitude_rad;
	float loiter_waypoint_longitude_rad;
	float loiter_waypoint_altitude_agl;
	
	unsigned int relative_positions_calculated : 1;  
	unsigned int airborne : 1;                //!< Is 0 when the plane didn't take off yet.	
	float wind_heading;           //!< Wind comes from...
	unsigned int wind_heading_set : 1;
	
	int desired_throttle_pct; // -1 = auto
	
	float desired_heading_rad;    //!< Last calculated desired heading. In radians. Zero is north.
	float desired_altitude_agl;
	float desired_pre_bank;
	
	//float height_error;       
	//float distance_next_waypoint; //!< Distance to next waypoint in meter.
	
	unsigned int time_airborne_s;
    unsigned int time_block_s;
};


volatile extern struct NavigationData navigation_data;

ScriptHandlerReturn navigation_handle_gluonscriptcommand (struct GluonscriptCode *code);

#endif // NAVIGATION_H
