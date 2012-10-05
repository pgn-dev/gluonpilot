
#include <math.h>
#include <stdio.h>

// Include all FreeRTOS header files
#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

#include "servo/servo.h"
#include "ppm_in/ppm_in.h"
#include "dataflash/dataflash.h"

#include "common.h"
#include "handler_trigger.h"
#include "handler_alarms.h"
#include "handler_navigation.h"
#include "handler_flightplan_switch.h"
#include "task_sensors_analog.h"
#include "task_control.h"
#include "configuration.h"
#include "gluonscript.h"


/*volatile */struct GluonscriptData gluonscript_data = {.current_codeline = 0, .last_code = 0, .tick = 0 };

void gluonscript_init()
{
	gluonscript_data.current_codeline = 0;
	gluonscript_data.last_code = 0;
	gluonscript_data.tick = 0;
	gluonscript_load();
	navigation_init();
}	


#define STACK_DEPTH 3
int stack[STACK_DEPTH] = { 0, 0, 0 }; // lets start with a 2-level stack
int stack_pointer = -1;

void push_codeline()
{
	if (stack_pointer < STACK_DEPTH-1)
	{
		stack_pointer++;
		stack[stack_pointer] = gluonscript_data.current_codeline;
	}
	else // stack full
	{
		stack_pointer = 0;
		stack[stack_pointer] = gluonscript_data.current_codeline;
	}
}

void pop_codeline()
{
	if (stack_pointer > 0)
	{
		gluonscript_data.current_codeline = stack[stack_pointer];
		stack_pointer--;
	}
	else if (stack_pointer == 0)
	{
		gluonscript_data.current_codeline = stack[stack_pointer];
		stack_pointer = STACK_DEPTH-1;
	}
}

inline int stack_empty()
{
	return stack_pointer <= -1;
}


void gluonscript_do()  // executed when a new GPS line has arrived (5Hz)
{
	struct GluonscriptCode *current_code = & gluonscript_data.codes[gluonscript_data.current_codeline];
	ScriptHandlerReturn handlers_result = 0;
	
	gluonscript_data.tick++;
	
	// call all handlers, returns UNHANDLED 0, HANDLED_FINISHED 1 or HANDLED_UNFINISHED 2
    handlers_result |= flightplan_switch_handle_gluonscriptcommand(current_code);
	handlers_result |= alarms_handle_gluonscriptcommand(current_code);
 	handlers_result |= trigger_handle_gluonscriptcommand(current_code);
 	handlers_result |= navigation_handle_gluonscriptcommand(current_code);
 	
 	if (handlers_result & HANDLED_FINISHED)  // one of the handlers already handled it completely
 	{
		gluonscript_data.current_codeline++;
	}
	else
	{
		switch(current_code->opcode)
		{
			case CALL:
				push_codeline();
				if (current_code->a < 0)
					gluonscript_data.current_codeline = gluonscript_data.current_codeline + current_code->a;
				else
					gluonscript_data.current_codeline = current_code->a;
				break;
			case RETURN:
				pop_codeline();
				gluonscript_data.current_codeline++;
				break;
			case GOTO:
				if (current_code->a < 0)
					gluonscript_data.current_codeline = gluonscript_data.current_codeline + current_code->a;
				else
					gluonscript_data.current_codeline = current_code->a;
				break;
			case UNTIL_GR:
				if (gluonscript_get_variable(current_code->a) > current_code->x)
					gluonscript_data.current_codeline++;
				else
					gluonscript_data.current_codeline--;
				break;
			case UNTIL_SM:
				if (gluonscript_get_variable(current_code->a) < current_code->x)
					gluonscript_data.current_codeline++;
				else
					gluonscript_data.current_codeline--;
				break;
			case UNTIL_EQ:
				if (fabs(gluonscript_get_variable(current_code->a) - current_code->x) < 1e-6f)
					gluonscript_data.current_codeline++;
				else
					gluonscript_data.current_codeline--;
				break;
			case UNTIL_NE:
				if (fabs(gluonscript_get_variable(current_code->a) - current_code->x) > 1e-6f)
					gluonscript_data.current_codeline++;
				else
					gluonscript_data.current_codeline--;
				break;
			case IF_GR:
				if (gluonscript_get_variable(current_code->a) > current_code->x)
					gluonscript_data.current_codeline++;
				else
					gluonscript_data.current_codeline += 2;
				break;
			case IF_SM:
				if (gluonscript_get_variable(current_code->a) < current_code->x)
					gluonscript_data.current_codeline++;
				else
					gluonscript_data.current_codeline += 2;
				break;
			case IF_EQ:
				if (fabs(gluonscript_get_variable(current_code->a) - current_code->x) < 1e-6f)
					gluonscript_data.current_codeline++;
				else
					gluonscript_data.current_codeline += 2;
				break;
			case IF_NE:
				if (fabs(gluonscript_get_variable(current_code->a) - current_code->x) > 1e-6f)
					gluonscript_data.current_codeline++;
				else
					gluonscript_data.current_codeline += 2;
				break;
			case SERVO_SET:

                if (current_code->b == 0)
                {
                    //OC6CONbits.OCM = 0;//0b010;
                    //TRISDbits.TRISD5 = 0;
                    //PORTDbits.RD5 = 0;
                    servo_set_logical_0(current_code->a);
                }
                else if (current_code->b > 2499)
                {
                    //OC6CONbits.OCM = 0;//0b001;
                    //TRISDbits.TRISD5 = 0;
                    //PORTDbits.RD5 = 1;
                    servo_set_logical_1(current_code->a);
                } 
                else
                {
                    servo_set_us(current_code->a, current_code->b);  // a = channel(0..7), b = microseconds (1000...2000)
                }
                
                gluonscript_data.current_codeline++;
				break;
	        case BLOCK:
	            navigation_data.time_block_s = 0;
	            gluonscript_data.current_codeline++;
	            break;
			case EMPTYCMD: // should not happen!!!
				navigation_data.desired_pre_bank = 0.0f;
				navigation_data.desired_throttle_pct = -1;
				gluonscript_data.current_codeline = 0;
				// also return home @ 100m height
				navigation_data.desired_heading_rad = navigation_heading_rad_fromto(sensor_data.gps.longitude_rad,
		                                                   		         sensor_data.gps.latitude_rad);
	            navigation_data.desired_altitude_agl = 98.0f;
				break;
			default:
				if (handlers_result == NOT_HANDLED)
				{
					navigation_data.desired_pre_bank = 0.0f;
					gluonscript_data.current_codeline = 0;
					// also return home @ 100m height
					navigation_data.desired_heading_rad = navigation_heading_rad_fromto(sensor_data.gps.longitude_rad,
			                                                   		         sensor_data.gps.latitude_rad);
			        navigation_data.desired_altitude_agl = 99.0f;
			 	}       
				break;
		}
	}	
}

void gluonscript_goto_from_gcs(int line_number)
{
	gluonscript_data.current_codeline--;  // because RETURN does pop+1
	push_codeline(); // in case this is a function. In case it's not: our circular buffer will handle it.
	if (line_number >= 0 && line_number < MAX_GLUONSCRIPTCODES)
		gluonscript_data.current_codeline = line_number;
}

float gluonscript_get_variable(enum gluonscript_variable i)
{
	switch (i)
	{
		case HEIGHT:
            if (config.control.altitude_mode == GPS_ABSOLUTE)
                return sensor_data.gps.height_m;
            else if (config.control.altitude_mode == GPS_RELATIVE)
                return sensor_data.gps.height_m - navigation_data.home_gps_height;
            else //if (config.control.altitude_mode == PRESSURE)
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
        	return fabs(control_state.desired_altitude - gluonscript_get_variable(HEIGHT));
        case ABS_HEADING_ERROR:
        {
	        struct GluonscriptCode *next_code = & gluonscript_data.codes[gluonscript_data.current_codeline+1];
	        if (next_code->opcode != FROM_TO_ABS && next_code->opcode != FLY_TO_ABS && next_code->opcode != CIRCLE_ABS && 
                next_code->opcode != FLARE_TO_ABS && next_code->opcode != GLIDE_TO_ABS && next_code->opcode != CIRCLE_TO_ABS)  // was || next_code->opcode != CIRCLE_TO_ABS
            {
                next_code = & gluonscript_data.codes[gluonscript_data.current_codeline+2];
	            if (next_code->opcode != FROM_TO_ABS && next_code->opcode != FLY_TO_ABS && next_code->opcode != CIRCLE_ABS && 
	                next_code->opcode != FLARE_TO_ABS && next_code->opcode != GLIDE_TO_ABS && next_code->opcode != CIRCLE_TO_ABS)
	            {
	                next_code = & gluonscript_data.codes[gluonscript_data.current_codeline+3];
	            	if (next_code->opcode != FROM_TO_ABS && next_code->opcode != FLY_TO_ABS && next_code->opcode != CIRCLE_ABS && 
	                	next_code->opcode != FLARE_TO_ABS && next_code->opcode != GLIDE_TO_ABS && next_code->opcode != CIRCLE_TO_ABS)
	               		printf("\r\nBad ABS_HEADING_ERR position\r\n");
	            }   		
			}
			
            float heading_error = navigation_heading_rad_fromto((float)(sensor_data.gps.longitude_rad - (double)(next_code->y)),
	                                                           (float)(sensor_data.gps.latitude_rad - (double)(next_code->x)));
	        heading_error = RAD2DEG(heading_error - sensor_data.gps.heading_rad);
	        if (heading_error > 180.0f)
	        	heading_error -= 360.0f;
	        else if (heading_error < -180.0f)
	        	heading_error += 360.0f;
        	return fabs(heading_error);
	    } 
        case ABS_ALT_AND_HEADING_ERR:
        {
            /*struct GluonscriptCode *next = gluonscript_next_waypoint_code(gluonscript_data.current_codeline);
			                
            float heading_error = navigation_heading_rad_fromto((float)(sensor_data.gps.longitude_rad - (double)(next->y)),
	                                                           (float)(sensor_data.gps.latitude_rad - (double)(next->x)));
			heading_error = RAD2DEG(heading_error - sensor_data.gps.heading_rad);
			//printf("\r\n%d\r\n", (int)heading_error);
	        if (heading_error > 180.0f)
	        	heading_error -= 360.0f;
	        else if (heading_error < -180.0f)
	        	heading_error += 360.0f;*/
        	return gluonscript_get_variable(ABS_ALTITUDE_ERROR) + gluonscript_get_variable(ABS_HEADING_ERROR); //fabs(heading_error);
        }	
        default:
			return 0.0;
	}	
}	


struct GluonscriptCode * gluonscript_next_waypoint_code(int current_codeline)
{
	struct GluonscriptCode *next = & (gluonscript_data.codes[current_codeline+1]);
	
	if (next->opcode != FROM_TO_ABS && next->opcode != FLY_TO_ABS && next->opcode != CIRCLE_ABS && 
        next->opcode != FLARE_TO_ABS && next->opcode != GLIDE_TO_ABS && next->opcode != CIRCLE_TO_ABS)
	{
		if (next->opcode == GOTO)
		{
			if (next->a >= 0)
				current_codeline = next->a - 2;
			else
				current_codeline = (current_codeline + 1) + next->a - 2;
		}		
		
		next = & gluonscript_data.codes[current_codeline+2];
		if (next->opcode != FROM_TO_ABS && next->opcode != FLY_TO_ABS && next->opcode != CIRCLE_ABS && 
            next->opcode != FLARE_TO_ABS && next->opcode != GLIDE_TO_ABS && next->opcode != CIRCLE_TO_ABS)
		{
			if (next->opcode == GOTO)
			{
				if (next->a >= 0)
					current_codeline = next->a - 3;
				else
					current_codeline = (current_codeline + 1) + next->a - 3;
			}
			next = & gluonscript_data.codes[current_codeline+3];
			if (next->opcode != FROM_TO_ABS && next->opcode != FLY_TO_ABS && next->opcode != CIRCLE_ABS && 
			    next->opcode != FLARE_TO_ABS && next->opcode != GLIDE_TO_ABS)
				printf("\r\nNext code not found!!\r\n");
		}   		
	}
	return next;
}


void gluonscript_burn()
{
	dataflash.write(NAVIGATION_PAGE, sizeof(gluonscript_data.codes), (unsigned char*) & (gluonscript_data.codes));
}

	
void gluonscript_load()
{
	dataflash.read(NAVIGATION_PAGE, sizeof(gluonscript_data.codes), (unsigned char*) & (gluonscript_data.codes));
}	
