#ifndef NAVIGATION_H
#define NAVIGATION_H


#define MAX_NAVIGATIONCODES 37

void navigation_burn();
void navigation_load();
void navigation_update();

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


enum navigation_command {
	EMPTYCMD=0,
	CLIMB=1,
	FROM_TO_REL=2,   // x, y, height
	FROM_TO_ABS=3,
	FLY_TO_REL=4,
	FLY_TO_ABS=5,    // x, y, height
	GOTO=6,	   // line number
	CIRCLE_ABS=7,    // x, y, radius, height <-- should be inside a while  12 B
	CIRCLE_REL=8,
	IF_EQ=9,     // x = c goto n
	IF_SM=10,     // x < c goto n
	IF_GR=11,     // x > c goto n
	IF_NE=12,     // x > c goto n
	WHILE_EQ=13,
	WHILE_NE=14,
	WHILE_GR=15,
	WHILE_SM=16
};



struct NavigationCode
{
	unsigned char opcode;
	float x;  // latitude
	float y;  // longitude
	int a;    //
	int b;    //
};


/*!
 *   Holds all the navigation information.
 */
struct NavigationData
{
	float home_longitude_rad;     //!< Home position, radians.
	float home_latitude_rad;      //!< Home position, radians.
	float home_gps_height;        //!< Height of home.
	float home_pressure_height;
	
	char airborne;                //!< Is 0 when the plane didn't take off yet.	
	float wind_heading;           //!< Wind comes from...
	
	float desired_heading_rad;    //!< Last calculated desired heading. In radians. Zero is north.
	float desired_height_m;
	float desired_pre_bank;
	
	//float height_error;       
	//float distance_next_waypoint; //!< Distance to next waypoint in meter.
	
	//! Stores the list of waypoints.
	struct NavigationCode navigation_codes[MAX_NAVIGATIONCODES];
	int current_codeline;       //!< Index in the waypoint array pointing to the current waypoint.
	int last_code;
};

extern struct NavigationData navigation_data;

#endif // NAVIGATION_H