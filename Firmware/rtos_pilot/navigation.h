#ifndef NAVIGATION_H
#define NAVIGATION_H


void navigation_update();

/*!
 *  The different waypoint types.
 */
enum waypoint_type { 
	RADIANS,              //!< Represents a standard waypoint (lon, lat) in radians 
	RELATIVE_HOME_METERS, //!< The (lon, lat) values are meters relative to the home position
	RELATIVE_LAST_METERS, //!< The (lon, lat) values are meters relative to the previous waypoint
	REWIND,               //!< Go back to the first waypoint
	FROM_TO               //!< FLight on a straight line from the previous waypoint to the next one
};


/*!
 *   Holds all the navigation information.
 */
struct NavigationData
{
	float home_longitude_rad;     //!< Home position, radians.
	float home_latitude_rad;      //!< Home position, radians.

	float home_gps_height;            //!< Height of home.
	float home_pressure_height;
	
	float desired_heading_rad;    //!< Last calculated desired heading. In radians. Zero is north.
	//float heading_error_rad;      //!< Last calculated error between current heading and actual heading.
	float height_error;       
	float distance_next_waypoint; //!< Distance to next waypoint in meter.
	char airborne;                //!< Is 0 when the plane didn't take off yet.
	
	//! Stores the list of waypoints.
//	struct waypoint waypoint[MAX_WAYPOINTS];
	int current_waypoint;       //!< Index in the waypoint array pointing to the current waypoint.
	int last_waypoint;
};

extern struct NavigationData navigation_data;

#endif // NAVIGATION_H