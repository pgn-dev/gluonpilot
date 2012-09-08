#ifndef GLUONSCRIPT_H
#define GLUONSCRIPT_H


#define MAX_GLUONSCRIPTCODES 72
#define GLUONSCRIPT_HZ 5

enum gluonscript_handler_return
{
	NOT_HANDLED = 0,
	HANDLED_FINISHED = 1,
	HANDLED_UNFINISHED = 2
};	

typedef enum gluonscript_handler_return ScriptHandlerReturn;

enum gluonscript_variable {
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
	BATT_V = 16,
	BLOCK_TIME = 17,
	ABS_ALTITUDE_ERROR = 18,
	ABS_HEADING_ERROR = 19,
	ABS_ALT_AND_HEADING_ERR = 20
};



enum gluonscript_command {
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
	UNTIL_SM=16,
	SERVO_SET=17,
	SERVO_TRIGGER=18,
	BLOCK=19,
	FLARE_TO_ABS=20,
	FLARE_TO_REL=21,
	GLIDE_TO_ABS=22,
	GLIDE_TO_REL=23,
	SET_LOITER_POSITION = 24,
    LOITER_CIRCLE = 25,
	CIRCLE_TO_ABS = 26,
    CIRCLE_TO_REL = 27,
    SET_BATTERY_ALARM = 28,
	CALL = 29,
	RETURN = 30,
    SERVO_START_TRIGGER = 31,
    SERVO_STOP_TRIGGER = 32,
    SET_FLIGHTPLAN_SWITCH = 33
};



struct GluonscriptCode
{
	unsigned char opcode;
	float x;  // latitude
	float y;  // longitude
	int a;    //
	int b;    //
};

struct GluonscriptData
{
	//! Stores the list of waypoints.
	struct GluonscriptCode codes[MAX_GLUONSCRIPTCODES];
	int current_codeline;       //!< Index in the waypoint array pointing to the current waypoint.
	int last_code;
	unsigned int tick;
};	

extern volatile struct GluonscriptData gluonscript_data;

void gluonscript_do();
float gluonscript_get_variable(enum gluonscript_variable i);
struct GluonscriptCode * gluonscript_next_waypoint_code(int current_codeline);
void gluonscript_burn();	
void gluonscript_load();
void gluonscript_init();

void gluonscript_goto_from_gcs(int line_number);

#endif //GLUONSCRIPT_H
