#ifndef NAVIGATION_H
#define NAVIGATION_H


#define MAX_NAVIGATIONCODES 36

void navigation_burn();
void navigation_load();
void navigation_update();
double navigation_heading_rad_fromto (double diff_long, double diff_lat); // used in OSD-code
float navigation_distance_between_meter(float long1, float long2, float lat1, float lat2);

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

enum navigation_variable {
	HEIGHT = 1,
	SPEED_MS = 2,
	HEADING_DEG = 3,
	FLIGHT_TIME_S = 4,
	SATELLITES_IN_VIEW = 5,
	HOME_DISTANCE = 6,
	PPM_LINK_ALIVE = 7,
	CHANNEL_1 = 8,
	CHANNEL_2 = 9,
	CHANNEL_3 = 10,
	CHANNEL_4 = 11,
	CHANNEL_5 = 12,
	CHANNEL_6 = 13,
	CHANNEL_7 = 14,
	CHANNEL_8 = 15,
	BATT_V = 16
};	

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
	UNTIL_EQ=13,
	UNTIL_NE=14,
	UNTIL_GR=15,
	UNTIL_SM=16
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
	
	float home_distance;          //<! Use for OSD
	float home_heading;           //<! Use for OSD
	
	float last_waypoint_latitude_rad;
	float last_waypoint_longitude_rad;
	
	char airborne;                //!< Is 0 when the plane didn't take off yet.	
	float wind_heading;           //!< Wind comes from...
	
	float desired_heading_rad;    //!< Last calculated desired heading. In radians. Zero is north.
	float desired_height_above_ground_m;
	float desired_pre_bank;
	
	//float height_error;       
	//float distance_next_waypoint; //!< Distance to next waypoint in meter.
	
	//! Stores the list of waypoints.
	struct NavigationCode navigation_codes[MAX_NAVIGATIONCODES];
	int current_codeline;       //!< Index in the waypoint array pointing to the current waypoint.
	int last_code;
	
	unsigned int time_airborne_s;
};

extern struct NavigationData navigation_data;

#endif // NAVIGATION_H
