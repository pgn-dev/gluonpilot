/*! 
 *  Implements the CSV communication protocol.
 *
 *  Consists of 2 tasks: telemetry (continuous output) and input (respond to commands).
 *
 *   Commands:
 *   Telemetry: TR, TP, TA, TH, TT, TG
 *   Other: ST, SA, SI, SG, PP, PR, PH, FC, LC, LD, RC
 *
 *  @file     communication_csv.c
 *  @author   Tom Pycke
 *  @date     24-dec-2009
 *  @since    0.1
 */

 
#include <stdio.h>
#include <stdlib.h>
#include <stdarg.h>

#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

#include "microcontroller/microcontroller.h"
#include "uart1_queue/uart1_queue.h"
#include "gps/gps.h"
#include "ppm_in/ppm_in.h"
#include "led/led.h"
#include "servo/servo.h"

#include "task_osd.h"
#include "sensors.h"
#include "communication.h"
#include "configuration.h"
#include "task_datalogger.h"
#include "handler_navigation.h"
#include "handler_alarms.h"

#include "common.h"

// Helper functions
void print_signed_integer(int x, void (*printer)(char[]));
void print_unsigned_integer(unsigned int x, void (*printer)(char[]));
void print_logline(struct LogLine *l);
void print_logline_simulation(struct LogLine *l);

void print_configuration();
void print_navigation();

#define BUFFERSIZE 200
static char  buffer[BUFFERSIZE];

extern unsigned long idle_counter;

#define COMM_BUFFER_LEN 100
char comm_buffer[COMM_BUFFER_LEN];
void comm_send_buffer_with_checksum(int length);

// Only write to output when the uart is available
#define printf_checksum_direct(T,...) \
   if (xSemaphoreTake( xUart1Semaphore, 0 ) == pdTRUE) { \
      comm_send_buffer_with_checksum(snprintf(comm_buffer, COMM_BUFFER_LEN, T, __VA_ARGS__)); \
      xSemaphoreGive( xUart1Semaphore ); \
      }

// Write to output and wait at most 100ms until the semaphore becomes available
#define printf_checksum(T,...) \
   if (xSemaphoreTake( xUart1Semaphore, ( portTickType ) 100 / portTICK_RATE_MS )  == pdTRUE) { \
      comm_send_buffer_with_checksum(snprintf(comm_buffer, COMM_BUFFER_LEN, T, __VA_ARGS__)); \
      xSemaphoreGive( xUart1Semaphore ); \
      }

// Write to output and wait at most 100ms until the semaphore becomes available
#define printf_nochecksum(T,...) \
   if (xSemaphoreTake( xUart1Semaphore, ( portTickType ) 100 / portTICK_RATE_MS )  == pdTRUE) { \
      printf(T, __VA_ARGS__); \
      xSemaphoreGive( xUart1Semaphore ); \
      }

#define printf_nochecksum_direct(T,...) \
   if (xSemaphoreTake( xUart1Semaphore, ( portTickType ) 0 / portTICK_RATE_MS )  == pdTRUE) { \
      printf(T, __VA_ARGS__); \
      xSemaphoreGive( xUart1Semaphore ); \
      }

#define printf_message(T) \
	if (xSemaphoreTake( xUart1Semaphore, ( portTickType ) 100 / portTICK_RATE_MS )  == pdTRUE) { \
      printf(T); \
      xSemaphoreGive( xUart1Semaphore ); \
      }

int check_checksum(char *s);

xSemaphoreHandle xUart1Semaphore;

/*!
 *    This task will send telemetry directly to uart1 at a rate of maximum 
 *    20 times a second.
 *
 *    Used stackspace: 356 / 860 bytes
 */
void communication_telemetry_task( void *parameters )
{
	int c = 0;
	struct TelemetryConfig counters;
		
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime;


	uart1_puts("Telemetry task initializing...");
	vTaskSetApplicationTaskTag( NULL, ( void * ) 6 );
	vSemaphoreCreateBinary(xUart1Semaphore);
	
	counters.stream_PPM = 0;
	counters.stream_GyroAccRaw = 0;
	counters.stream_GyroAccProc = 0;
	counters.stream_PressureTemp = 0;
	counters.stream_GpsBasic = 0;
	counters.stream_Control = 0;
	counters.stream_Attitude = 0;
	
	uart1_puts("done\r\n");
	
	// delay a bit and send navigation and configuration
	vTaskDelay(( ( portTickType ) 1500 / portTICK_RATE_MS ) );  
	//vTaskDelay(( ( portTickType ) 100 / portTICK_RATE_MS ) );  
	print_configuration();
	vTaskDelay( ( ( portTickType ) 100 / portTICK_RATE_MS ) ); 
	print_navigation();

	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil() works correctly. */
	xLastExecutionTime = xTaskGetTickCount();
	
	for( ;; )
	{
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 100 / portTICK_RATE_MS ) );  // 10Hz
		counters.stream_PPM++;
		counters.stream_GyroAccRaw++;
		counters.stream_GyroAccProc++;
		counters.stream_PressureTemp++;
		counters.stream_GpsBasic++;
		counters.stream_Attitude++;
		counters.stream_Control++;
		
		if (c++ % 5 == 0)  // this counter will never be used at 20Hz
			led1_on();
		else
			led1_off();
		
		if (c % 3000 == 0) // reset Xbee every 5 minutes to prevent a lock-up (duty cycle)
		{
			//uart1_puts("\r\nResetting XBEE...\r\n") ;
			vTaskDelay( ( ( portTickType ) 1001 / portTICK_RATE_MS ) ); // guard time wait 1000ms
			uart1_puts("+++");
			vTaskDelay( ( ( portTickType ) 1001 / portTICK_RATE_MS ) ); // guard time wait 1000ms
			uart1_puts("ATFR\r\n") ;
			vTaskDelay( ( ( portTickType ) 10 / portTICK_RATE_MS ) ); // wait 10ms
		}	
		
		if (battery_alarm.alarm_battery_warning == 1)
		{
			printf_message("Warning: Battery low\r\n");
			// clear the flag so it is printed every few seconds
			battery_alarm.alarm_battery_warning = 0;
		}
		else if (battery_alarm.alarm_battery_panic == 1)
		{
			// print this once 
			printf_message("!!! Panic: Battery low !!!\r\n");
			battery_alarm.alarm_battery_panic++; // an ugly hack to make sure it's never printed again
		}
				
		///////////////////////////////////////////////////////////////
		//               GYRO AND ACCELEROMETER RAW                  //
		///////////////////////////////////////////////////////////////
		if (counters.stream_GyroAccRaw == config.telemetry.stream_GyroAccRaw)
		{
			printf_checksum_direct("TR;%u;%u;%u;%u;%u;%u", (sensor_data.acc_x_raw), (sensor_data.acc_y_raw),
			                                    (sensor_data.acc_z_raw), (sensor_data.gyro_x_raw),
			                                    (sensor_data.gyro_y_raw), (sensor_data.gyro_z_raw));
			counters.stream_GyroAccRaw = 0;
		} 
		else if (counters.stream_GyroAccRaw > config.telemetry.stream_GyroAccRaw)
			counters.stream_GyroAccRaw = 0;
		
		///////////////////////////////////////////////////////////////
		//            GYRO AND ACCELEROMETER PROCESSED              //
		///////////////////////////////////////////////////////////////
		if (counters.stream_GyroAccProc == config.telemetry.stream_GyroAccProc)
		{
			printf_checksum_direct("TP;%d;%d;%d;%d;%d;%d", (int)(sensor_data.acc_x*1000), (int)(sensor_data.acc_y*1000),
			                                        (int)(sensor_data.acc_z*1000), (int)(sensor_data.p*1000),
			                                        (int)(sensor_data.q*1000), (int)(sensor_data.r*1000));
		}	
		else if (counters.stream_GyroAccProc > config.telemetry.stream_GyroAccProc)
			counters.stream_GyroAccProc = 0;
		
		///////////////////////////////////////////////////////////////
		//                         ATTITUDE                          //
		///////////////////////////////////////////////////////////////	
		if (counters.stream_Attitude == config.telemetry.stream_Attitude)
		{
            int *t = (int*)&sensor_data.pitch;

			printf_checksum_direct("TA;%d;%d;%d;%x;%x", (int)(sensor_data.roll*1000), (int)(sensor_data.pitch*1000), (int)(sensor_data.yaw*1000), t[1], t[0]);

			if (control_state.simulation_mode)
			{
				printf_checksum_direct("TS;%d;%d;%d", servo_read_us(2), servo_read_us(0), servo_read_us(3));
			}
			counters.stream_Attitude = 0;
		} 
		else if (counters.stream_Attitude > config.telemetry.stream_Attitude)
			counters.stream_Attitude = 0;
			
		///////////////////////////////////////////////////////////////
		//           SCP1000: PRESSURE & TEMPERATURE                 //
		///////////////////////////////////////////////////////////////
		if (counters.stream_PressureTemp == config.telemetry.stream_PressureTemp)
		{
			printf_checksum_direct("TH;%lu;%d", (unsigned long)(sensor_data.pressure), (int)sensor_data.temperature);
			counters.stream_PressureTemp = 0;
		}
		else if (counters.stream_PressureTemp > config.telemetry.stream_PressureTemp)
			counters.stream_PressureTemp = 0;
		
		///////////////////////////////////////////////////////////////
		//                   RC TRANSMITTER INPUT                    //
		///////////////////////////////////////////////////////////////
		if (counters.stream_PPM == config.telemetry.stream_PPM)
		{
			//vTaskGetRunTimeStats( buffer );
			//uart1_puts(buffer);
			printf_checksum_direct("TT;%u;%u;%u;%u;%u;%u;%u;%u", (unsigned int)ppm.channel[0], (unsigned int)ppm.channel[1],
			                                          (unsigned int)ppm.channel[2], (unsigned int)ppm.channel[3],
			                                          (unsigned int)ppm.channel[4], (unsigned int)ppm.channel[5],
			                                          (unsigned int)ppm.channel[6], (unsigned int)ppm.channel[7]);
			counters.stream_PPM = 0;
		}
		else if (counters.stream_PPM > config.telemetry.stream_PPM)
			counters.stream_PPM = 0;
		
		///////////////////////////////////////////////////////////////
		//                        GPS BASIC                          //
		///////////////////////////////////////////////////////////////
		if (counters.stream_GpsBasic == config.telemetry.stream_GpsBasic)
		{
			printf_checksum_direct("TG;%c;%.9f;%.9f;%u;%u;%u;%u", '0' + (unsigned char)sensor_data.gps.status,
			                                            sensor_data.gps.latitude_rad, sensor_data.gps.longitude_rad,
			                                            (unsigned int)(sensor_data.gps.speed_ms*10),
			                                            (unsigned int)(sensor_data.gps.heading_rad*100),
			                                            (unsigned int)(sensor_data.gps.satellites_in_view),
			                                            (unsigned int)(sensor_data.gps.height_m));
			counters.stream_GpsBasic = 0;
		}
		else if (counters.stream_GpsBasic > config.telemetry.stream_GpsBasic)
			counters.stream_GpsBasic = 0;
			
		///////////////////////////////////////////////////////////////
		//                          CONTROL                          //
		///////////////////////////////////////////////////////////////
		//printf("TC;CONTROL_STATUS;LINE;HEIGHT(;CARROTX;CARROTY;CARROTH)");
		if (counters.stream_Control == config.telemetry.stream_Control)
		{
			int sig_quality = 0;
			if (config.control.use_pwm)
            {
	        	if (ppm.connection_alive)
	        		sig_quality = 100;
	        	else
	        		sig_quality = 0;
	        } else // ppm
            	sig_quality = (100-ppm_signal_quality()*4);  // %
            	
			int throttle = (config.control.servo_neutral[3] - (int)servo_read_us(3))/10;
			if (! config.control.reverse_servo4)
				throttle = -throttle;
			if (throttle < 0 || throttle > 100)
				throttle = 0;
			//printf("\r\n %d %d\r\n", config.control.servo_neutral[3], (int)servo_read_us(3));
			
            int altitude;
            if (config.control.altitude_mode == GPS_ABSOLUTE)
                altitude =  sensor_data.gps.height_m;
            else if (config.control.altitude_mode == GPS_RELATIVE)
                altitude = sensor_data.gps.height_m - navigation_data.home_gps_height;
            else //if (config.control.altitude_mode == PRESSURE)
                altitude = (int)(sensor_data.pressure_height - navigation_data.home_pressure_height);
            
			printf_checksum_direct("TC;%d;%d;%d;%u;%d;%d;%d;%d;%d;%d;%u", (int)control_state.flight_mode,
			       gluonscript_data.current_codeline, altitude,
			       sensor_data.battery1_voltage_10,
			       navigation_data.time_airborne_s, navigation_data.time_block_s,
			       sig_quality, throttle, (int)navigation_data.desired_altitude_agl,
                   sensor_data.battery2_voltage_10,(unsigned int)(sensor_data.battery1_mAh/10.0));
			 
			counters.stream_Control = 0;
			//printf_checksum_poll("-- %lu --", idle_counter);
            //idle_counter = 0;
			//printf_checksum_poll("-- %lu --", idle_counter);
		}
		else if (counters.stream_Control > config.telemetry.stream_Control)
			counters.stream_Control = 0;

	}
}


extern xQueueHandle xRxedChars;

#define MAX_TOKEN 10

/*!
 *   This task parses and executes all commands coming from the groundstation
 *   or configuration utility. It depends on uart1_queue.c because all data received
 *   on the uart1 is stored in a FreeRTOS queue. When there is data in the queue, the
 *   RTOS executes this function so the data can be parsed.
 *
 *   Measured used stackspace: 388 / 2150 bytes
 *
 *
 */
void communication_input_task( void *parameters )
{
	static int   buffer_position;
	static int   token[MAX_TOKEN+1] = {0,0,0,0,0,0,0,0,0,0,0};
	static int   current_token;
	
	char tmp;

    vTaskSetApplicationTaskTag( NULL, ( void * ) 4 );

	uart1_puts("Console input task initializing...");
	uart1_puts("done\r\n");
	
	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		if( xQueueReceive( xRxedChars, &tmp, portMAX_DELAY  ) )
        {           
	        //vTaskList(&buffer1);
	        //uart1_putc(tmp);
            if (tmp == '\n' || tmp == '\r')
            {
	            buffer[buffer_position] = '\0';
	            //printf("\r\nChecking checksum: %s\r\n", buffer);
	            if (buffer[0] == '$')  // with checksum
	            {
		        	if (check_checksum(buffer))
		        	{
			        	buffer[0] = buffer[1];
			        	buffer[1] = buffer[2];
			        } else
			        	printf_message("Error checksum\r\n");
		        } 
	            //token[current_token + 1] = buffer_position;

                // first handle all navigation
                if (buffer[token[0] + 1] == 'N')
                {
                    ///////////////////////////////////////////////////////////////
                    //                      WRITE NAVIGATION                     //
                    ///////////////////////////////////////////////////////////////
                    if (buffer[token[0]] == 'W')
                    {
                        int i = atoi(&(buffer[token[1]])) - 1;

    #ifdef LIMITED
        //if (i < 2)
            uart1_puts("Not allowed in Limited Edition!\r\n");
    #else
                        if (i < MAX_GLUONSCRIPTCODES)
                        {
                            gluonscript_data.codes[i].opcode = atoi(&(buffer[token[2]]));
                            gluonscript_data.codes[i].x = atof(&(buffer[token[3]]));
                            gluonscript_data.codes[i].y = atof(&(buffer[token[4]]));
                            gluonscript_data.codes[i].a = atoi(&(buffer[token[5]]));
                            gluonscript_data.codes[i].b = atoi(&(buffer[token[6]]));

                            if (navigation_data.relative_positions_calculated)
                                navigation_calculate_relative_position(i);

                            // confirm by sending it back...
                            printf_checksum("ND;%d;%d;%f;%f;%d;%d", i+1, gluonscript_data.codes[i].opcode,
                                            gluonscript_data.codes[i].x, gluonscript_data.codes[i].y,
                                            gluonscript_data.codes[i].a, gluonscript_data.codes[i].b);
                        }
    #endif
                    }
                    ///////////////////////////////////////////////////////////////
                    //                 JUMP TO NAVIGATION LINE                   //
                    ///////////////////////////////////////////////////////////////
                    else if (buffer[token[0]] == 'J')
                    {
                        int number = atoi(&(buffer[token[1]]));
                        gluonscript_goto_from_gcs(number);
                    }
                    ///////////////////////////////////////////////////////////////
                    //                       BURN NAVIGATION                     //
                    ///////////////////////////////////////////////////////////////
                    else if (buffer[token[0]] == 'F')
                    {
                        gluonscript_burn();
                        printf_message("\r\nScript burned to flash\r\n");
                    }
                    ///////////////////////////////////////////////////////////////
                    //                       LOAD NAVIGATION                     //
                    ///////////////////////////////////////////////////////////////
                    else if (buffer[token[0]] == 'L')
                    {
                        gluonscript_load();
                        if (navigation_data.relative_positions_calculated)
                            navigation_calculate_relative_positions();
                    }
                    ///////////////////////////////////////////////////////////////
                    //                       READ NAVIGATION                     //
                    ///////////////////////////////////////////////////////////////
                    else if (buffer[token[0]] == 'R')
                    {
                        print_navigation();
                    }
                }
				///////////////////////////////////////////////////////////////
				//                       SET CONTROL                         //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'C')    // Set control
				{
                    config.control.servo_mix = (buffer[token[1] + 0]) - '0';
                    config.control.max_pitch = atof(&(buffer[token[2]])) / 180.0 * 3.14;
                    config.control.max_roll = atof(&(buffer[token[3]])) / 180.0 * 3.14;
                    config.control.aileron_differential = atoi(&(buffer[token[4]])) / 10;

                    config.control.waypoint_radius_m = atof(&(buffer[token[5]]));
                    config.control.cruising_speed_ms = atof(&(buffer[token[6]]));
                    config.control.stabilization_with_altitude_hold = atoi(&(buffer[token[7]])) == 0? 0 : 1;
                    config.control.min_pitch = DEG2RAD(atof(&(buffer[token[8]])));
                    config.control.altitude_mode = atoi(&(buffer[token[9]]));
                    if (config.control.altitude_mode < 1 || config.control.altitude_mode > 3)
                        config.control.altitude_mode = 1;
				}	
				///////////////////////////////////////////////////////////////
				//                  SET GPS CONFIGURATION                    //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'G')    // Set GPS
				{
					unsigned int x;
					sscanf(&(buffer[token[1]]), "%u", &x);
					config.gps.initial_baudrate = (long)x * 10;
					config.gps.operational_baudrate = 0;
                    if (buffer[token[2]] == '1')
                        config.gps.enable_waas = 1;
                    else
                        config.gps.enable_waas = 0;
				}
				///////////////////////////////////////////////////////////////
				//                 SET PID PITCH 2 ELEVATOR                  //
				///////////////////////////////////////////////////////////////				
				else if (buffer[token[0]] == 'P' && buffer[token[0] + 1] == 'P')    // Set PID
				{
					config.control.pid_pitch2elevator.p_gain = (float)atof(&(buffer[token[1]]));
					config.control.pid_pitch2elevator.i_gain = (float)atof(&(buffer[token[2]]));
					config.control.pid_pitch2elevator.d_gain = (float)atof(&(buffer[token[3]]));
					config.control.pid_pitch2elevator.i_min = (float)atof(&(buffer[token[4]]));
					config.control.pid_pitch2elevator.i_max = (float)atof(&(buffer[token[5]]));
					config.control.pid_pitch2elevator.d_term_min_var = (float)atof(&(buffer[token[6]]));
				}
				///////////////////////////////////////////////////////////////
				//                  SET PID ROLL 2 AILERON                   //
				///////////////////////////////////////////////////////////////				
				else if (buffer[token[0]] == 'P' && buffer[token[0] + 1] == 'R')    // Set PID
				{
					config.control.pid_roll2aileron.p_gain = (float)atof(&(buffer[token[1]]));
					config.control.pid_roll2aileron.i_gain = (float)atof(&(buffer[token[2]]));
					config.control.pid_roll2aileron.d_gain = (float)atof(&(buffer[token[3]]));
					config.control.pid_roll2aileron.i_min = (float)atof(&(buffer[token[4]]));
					config.control.pid_roll2aileron.i_max = (float)atof(&(buffer[token[5]]));
					config.control.pid_roll2aileron.d_term_min_var = (float)atof(&(buffer[token[6]]));
				}
				///////////////////////////////////////////////////////////////
				//                SET PID HEADING 2 ROLL/YAW                 //
				///////////////////////////////////////////////////////////////				
				else if (buffer[token[0]] == 'P' && buffer[token[0] + 1] == 'H')    // Set PID
				{
					config.control.pid_heading2roll.p_gain = (float)atof(&(buffer[token[1]]));
					config.control.pid_heading2roll.i_gain = (float)atof(&(buffer[token[2]]));
					config.control.pid_heading2roll.d_gain = (float)atof(&(buffer[token[3]]));
					config.control.pid_heading2roll.i_min = (float)atof(&(buffer[token[4]]));
					config.control.pid_heading2roll.i_max = (float)atof(&(buffer[token[5]]));
					config.control.pid_heading2roll.d_term_min_var = (float)atof(&(buffer[token[6]]));
				}
				///////////////////////////////////////////////////////////////
				//                  SET PID ALTITUDE 2 PITCH                 //
				///////////////////////////////////////////////////////////////				
				else if (buffer[token[0]] == 'P' && buffer[token[0] + 1] == 'A')    // Set PID
				{
					config.control.pid_altitude2pitch.p_gain = (float)atof(&(buffer[token[1]]));
					config.control.pid_altitude2pitch.i_gain = (float)atof(&(buffer[token[2]]));
					config.control.pid_altitude2pitch.d_gain = (float)atof(&(buffer[token[3]]));
					config.control.pid_altitude2pitch.i_min = (float)atof(&(buffer[token[4]]));
					config.control.pid_altitude2pitch.i_max = (float)atof(&(buffer[token[5]]));
					config.control.pid_altitude2pitch.d_term_min_var = (float)atof(&(buffer[token[6]]));
				}
                ///////////////////////////////////////////////////////////////
				//                  SET AUTOTHROTTLE                 //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'A' && buffer[token[0] + 1] == 'T')    // Set PID
				{
					config.control.auto_throttle_min_pct = atoi(&(buffer[token[1]]));
					config.control.auto_throttle_max_pct = atoi(&(buffer[token[2]]));
					config.control.auto_throttle_cruise_pct = atoi(&(buffer[token[3]]));
					config.control.auto_throttle_p_gain = atoi(&(buffer[token[4]]));
                    config.control.autopilot_auto_throttle = atoi(&(buffer[token[5]])) == 1;
				}
				
				///////////////////////////////////////////////////////////////
				//                      ENABLE SIMULATION                    //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'E') 
				{
					printf_message("Simulation enabled\r\n");
					control_state.simulation_mode = 1;
					sensor_data.gps.satellites_in_view = 9;
					sensor_data.gps.status = ACTIVE;
					sensor_data.gps.date = atol(&(buffer[token[1]]));
					sensor_data.gps.time = atol(&(buffer[token[2]]));
                    navigation_data.home_pressure_height = 0;
                    navigation_data.home_gps_height = 0;
				}
				///////////////////////////////////////////////////////////////
				//                      WRITE SIMULATION                    //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'W') 
				{
					sensor_data.gps.longitude_rad = (float)atof(&(buffer[token[1]]));
					sensor_data.gps.latitude_rad = (float)atof(&(buffer[token[2]]));
					sensor_data.gps.heading_rad	= (float)atof(&(buffer[token[3]]));
					sensor_data.yaw = sensor_data.gps.heading_rad;
					sensor_data.gps.speed_ms = (float)atof(&(buffer[token[4]]));
					sensor_data.pressure_height = (float)atoi(&(buffer[token[5]]));
					sensor_data.roll =  (float)atof(&(buffer[token[6]]));
					sensor_data.pitch =  (float)atof(&(buffer[token[7]]));
                    navigation_data.home_pressure_height = 0;
                    navigation_data.home_gps_height = 0;
					//sensor_data.vertical_speed
					//sensor_data.battery_voltage_10

					//navigation_update();
					//gluonscript_do();
				}
				///////////////////////////////////////////////////////////////
				//                      SET TELEMETRY                        //
				///////////////////////////////////////////////////////////////
                else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'T')    // Set Telemetry
				{
					config.telemetry.stream_GpsBasic = atoi(&(buffer[token[1]]));
					config.telemetry.stream_GyroAccRaw = atoi(&(buffer[token[2]]));
					config.telemetry.stream_GyroAccProc = atoi(&(buffer[token[3]]));
					config.telemetry.stream_PPM = atoi(&(buffer[token[4]]));
					config.telemetry.stream_PressureTemp = atoi(&(buffer[token[5]]));
					config.telemetry.stream_Attitude = atoi(&(buffer[token[6]]));
					config.telemetry.stream_Control = atoi(&(buffer[token[7]]));
				}
				///////////////////////////////////////////////////////////////
				//                    SET ACCELEROMETER                      //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'A')    // Set Accelerometer neutral
				{
					config.sensors.acc_x_neutral = (float)atof(&(buffer[token[1]]));
					config.sensors.acc_y_neutral = (float)atof(&(buffer[token[2]]));
					config.sensors.acc_z_neutral = (float)atof(&(buffer[token[3]]));
				}
				///////////////////////////////////////////////////////////////
				//                        SET GYRO                           //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'Y')    // Set Gyro neutral
				{
					config.sensors.gyro_x_neutral = (float)atof(&(buffer[token[1]]));
					config.sensors.gyro_y_neutral = (float)atof(&(buffer[token[2]]));
					config.sensors.gyro_z_neutral = (float)atof(&(buffer[token[3]]));
				}
                ///////////////////////////////////////////////////////////////
				//                        SET IMU                            //
				///////////////////////////////////////////////////////////////
                else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == '6')    // Set 6DOF settings
				{
					config.sensors.imu_rotated = atoi(&(buffer[token[1]]));
					config.sensors.neutral_pitch = DEG2RAD((float)atof(&(buffer[token[2]])));
				}
				///////////////////////////////////////////////////////////////
				//                     CALIBRATE GYRO                        //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'C' && buffer[token[0] + 1] == 'G')    // Calibrate gyro
				{
					int i;
					long x=0, y=0, z=0;
					for (i = 0; i < 10; i++)
					{
						x += sensor_data.gyro_x_raw;
						y += sensor_data.gyro_y_raw;
						z += sensor_data.gyro_z_raw;
						vTaskDelay(( ( portTickType ) 10 / portTICK_RATE_MS ) );  // delay 10ms
					}	
					config.sensors.gyro_x_neutral = (float)(x / 10);
					config.sensors.gyro_y_neutral = (float)(y / 10);
					config.sensors.gyro_z_neutral = (float)(z / 10);
					
					// reset bias offsets
					sensor_data.p_bias = 0.0;
					sensor_data.q_bias = 0.0;
                    printf("Gyros calibrated\r\n");
				}
				///////////////////////////////////////////////////////////////
				//                    CALIBRATE ACCELERO                     //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'C' && buffer[token[0] + 1] == 'A')    // Calibrate accelerometer
				{
					int i;
					long x=0, y=0, z=0;
					for (i = 0; i < 10; i++)
					{
						x += sensor_data.acc_x_raw;
						y += sensor_data.acc_y_raw;
						z += sensor_data.acc_z_raw;
						vTaskDelay(( ( portTickType ) 10 / portTICK_RATE_MS ) );  // delay 10ms
					}	
					config.sensors.acc_x_neutral = (float)(x / 10);
					config.sensors.acc_y_neutral = (float)(y / 10);
                    if (HARDWARE_VERSION < V01Q)
                        config.sensors.acc_z_neutral = (float)(z / 10) - 6600.0;
                    else
                        config.sensors.acc_z_neutral = (float)(z / 10) - 4096.0;
                    printf("Accelerometers calibrated\r\n");
				}
				///////////////////////////////////////////////////////////////
				//                    SET INPUT CHANNELS                     //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'I')    // Set Input Channel
				{
					config.control.use_pwm = (buffer[token[1] + 0]) == '1';
					config.control.channel_ap = (buffer[token[2] + 0] - 'a');
					config.control.channel_motor = (buffer[token[3] + 0] - 'a');
					config.control.channel_pitch = (buffer[token[4] + 0] - 'a');
					config.control.channel_roll = (buffer[token[5] + 0] - 'a');
					config.control.channel_yaw = (buffer[token[6] + 0] - 'a');
					
				}
				///////////////////////////////////////////////////////////////
				//                    SET SERVO REVERSE                      //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'R')    // Set servo reverse
				{
					unsigned int tmp = atoi(&(buffer[token[1]]));
					config.control.reverse_servo1 = ((tmp & 1) != 0);
					config.control.reverse_servo2 = ((tmp & 2) != 0);
					config.control.reverse_servo3 = ((tmp & 4) != 0);
					config.control.reverse_servo4 = ((tmp & 8) != 0);
					config.control.reverse_servo5 = ((tmp & 16) != 0);
					config.control.reverse_servo6 = ((tmp & 32) != 0);
					config.control.manual_trim = buffer[token[2]] == '1'?1:0;
				}
				///////////////////////////////////////////////////////////////
				//                    SET MIN MAX                            //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'M')    // Set servo reverse
				{
					int nr = atoi(&(buffer[token[1]]));
				    int min = atoi(&(buffer[token[2]]));
				    int neutral = atoi(&(buffer[token[3]]));
				    int max = atoi(&(buffer[token[4]]));
				    config.control.servo_min[nr] = min;
				    config.control.servo_neutral[nr] = neutral;
				    config.control.servo_max[nr] = max;
				}
                ///////////////////////////////////////////////////////////////
				//                    SET OSD                            //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'O')    // Set osd
				{
                    osd_clear();
                    unsigned long bits = (unsigned long)atol(&(buffer[token[1]]));
                    config.osd.show_altitude = ((bits & 1) != 0) ? 1 : 0;
                    config.osd.show_arrow_home = ((bits & 2) != 0) ? 1 : 0;
                    config.osd.show_artificial_horizon = ((bits & 4) != 0) ? 1 : 0;
                    config.osd.show_block_name = ((bits & 8) != 0) ? 1 : 0;
                    config.osd.show_current = ((bits & 16) != 0) ? 1 : 0;
                    config.osd.show_distance_home = ((bits & 32) != 0) ? 1 : 0;
                    config.osd.show_flight_time = (((bits & 64) != 0) ? 1 : 0);
                    config.osd.show_gps_status = ((bits & 128) != 0) ? 1 : 0;
                    config.osd.show_mah = ((bits & 256) != 0) ? 1 : 0;
                    config.osd.show_mode = ((bits & 512) != 0) ? 1 : 0;
                    config.osd.show_rc_link = ((bits & 1024) != 0) ? 1 : 0;
                    config.osd.show_speed = ((bits & 2048) != 0) ? 1 : 0;
                    config.osd.show_vario = ((bits & 4096) != 0) ? 1 : 0;
                    config.osd.show_voltage1 = ((bits & 8192) != 0) ? 1 : 0;
                    config.osd.show_voltage2 = ((bits & 16384) != 0) ? 1 : 0;
                    config.osd.show_block_name = ((bits & 32768) != 0) ? 1 : 0;
                    config.osd.rssi = (enum RssiMode) atoi(&(buffer[token[2]]));
                    config.osd.voltage_low  = (unsigned char) atoi(&(buffer[token[3]]));
                    config.osd.voltage_high  = (unsigned char) atoi(&(buffer[token[4]]));
				}
                ///////////////////////////////////////////////////////////////
				//                      FORMAT DATALOG                       //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'F' && buffer[token[0] + 1] == 'F')
				{
					datalogger_format();
				}
				///////////////////////////////////////////////////////////////
				//                    DATALOG INDEX TABLE                    //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'F' && buffer[token[0] + 1] == 'I')
				{
					int i;
                    //uart1_puts("\n\r");
					for (i = 0; i < MAX_INDEX; i++)
						printf_checksum("DT;%d;%d;%ld;%ld", i, datalogger_index_table[i].page_num, datalogger_index_table[i].date, datalogger_index_table[i].time);
				}
				///////////////////////////////////////////////////////////////
				//                           RESET                           //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'Z' && buffer[token[0] + 1] == 'Z')
				{
					if (atoi(&(buffer[token[1]])) == 1123)  // double check
					{
						printf_message("Reboot command received...\r\n");
						portTickType xLastWakeTime;
     					xLastWakeTime = xTaskGetTickCount();
						vTaskDelayUntil( &xLastWakeTime, ( ( portTickType ) 1000 / portTICK_RATE_MS ) );  // 1s
						asm("reset");
					}
				}
				///////////////////////////////////////////////////////////////
				//                       DATALOG READ                        //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'D' && buffer[token[0] + 1] == 'R')
				{
					int i = atoi(&(buffer[token[1]]));

#ifdef DETAILED_LOG
					printf_message ("\r\nDH;Latitude;Longitude;SpeedGPS;HeadingGPS;HeightGPS;SatellitesGPS;");
					printf_message ("HeightBaro;Pitch;Roll;DesiredPitch;DesiredRoll;DesiredHeading;DesiredHeight;AccXG;AccYG;");
					printf_message ("AccZG;P;Q;R;TempC;FlightMode;NavigationLine\r\n");
#elif RAW_50HZ_LOG
					printf_message ("DH;Latitude;Longitude;Time;SpeedGPS;HeadingGPS;AccX;AccY;AccZ;GyroX;GyroY;GyroZ;HeightBaro;Pitch;Roll;PitchAcc\r\n");//;idg500-vref;FlightMode\r\n");
#else
                    printf_message ("\r\nDH;Date;Time;Latitude;Longitude;SpeedGPS;HeadingGPS;HeightGPS;");
					printf_message ("HeightBaro;Pitch;Roll;Yaw;");
					printf_message ("TempC;FlightMode;NavigationLine;ServoTrigger\r\n");
#endif

					datalogger_disable();

					while (datalogger_print_next_page(i, &print_logline))
						;


					/* ** SIMULATION ** */
					/*ahrs_init();
					while (datalogger_print_next_page(i, &print_logline_simulation))
						;
					*/
					//datalogger_enable();
				}
				///////////////////////////////////////////////////////////////
				//                      WRITE TO FLASH                       //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'F' && buffer[token[0] + 1] == 'C')    // FC write to flash!
				{
					configuration_write();
					printf_message("Configuration burned to flash\r\n");
				}
				///////////////////////////////////////////////////////////////
				//                     LOAD FROM FLASH                       //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'L' && buffer[token[0] + 1] == 'C')    // Load configuration from flash!
				{
					configuration_load();
				}
				///////////////////////////////////////////////////////////////
				//                LOAD DEFAULT CONFIGURATION                 //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'L' && buffer[token[0] + 1] == 'D')    // Load default configuration
				{
					configuration_default();
				}
				///////////////////////////////////////////////////////////////
				//                  READ ALL CONFIGURATION                   //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'R' && buffer[token[0] + 1] == 'C')    // RC;x  Read configuration
				{
					if (buffer[token[1]] == 'A') 
					{
						if (xSemaphoreTake( xUart1Semaphore, ( portTickType ) 100 / portTICK_RATE_MS )  == pdTRUE) { 
							print_configuration();
							xSemaphoreGive( xUart1Semaphore );
						}	
					}	
				}
				else if (current_token > 0)  // && \n or \r
				{
					buffer[buffer_position++] = '\0';
                    buffer[buffer_position] = '\0';
					printf_nochecksum_direct("ERROR received data: %s\r\n", buffer);
				}	

            	buffer_position = 0;
            	current_token = 0;
            	token[0] = 0;
            	token[1] = 0;
            	token[2] = 0;
            	token[3] = 0;
            	token[4] = 0;
            	token[5] = 0;
            	token[6] = 0;
            	token[7] = 0;
            	token[8] = 0;
                token[9] = 0;
            }
            else if ((tmp == ';' || tmp == '*'))
            {
				if  (current_token < MAX_TOKEN && buffer_position < BUFFERSIZE)
				{
	            	buffer[buffer_position++] = tmp;
	            	token[++current_token] = buffer_position;
				} 
				else if (current_token >= MAX_TOKEN) // error!
				{
					buffer_position = 0;
					current_token = 0;
                    printf("\r\n error max token\r\n");
				}
	        } 
	        else if (buffer_position < BUFFERSIZE)
	        	buffer[buffer_position++] = tmp;
        }
	}
}

void print_navigation()
{
	int i;
	uart1_puts("\n\r");
	for (i = 0; i < MAX_GLUONSCRIPTCODES; i++)
	{
		printf_checksum("ND;%d;%d;%f;%f;%d;%d", i+1, gluonscript_data.codes[i].opcode,
			gluonscript_data.codes[i].x, gluonscript_data.codes[i].y,
			gluonscript_data.codes[i].a, gluonscript_data.codes[i].b);
	}	
}

void print_configuration()
{
	int i;
	uart1_puts("CA;"); // 21
	
	//config.sensors
	print_unsigned_integer((unsigned int)config.sensors.acc_x_neutral, uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)config.sensors.acc_y_neutral, uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)config.sensors.acc_z_neutral, uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)config.sensors.gyro_x_neutral, uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)config.sensors.gyro_y_neutral, uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)config.sensors.gyro_z_neutral, uart1_puts);
	uart1_putc(';');
	
	vTaskDelay(( ( portTickType ) 50 / portTICK_RATE_MS ) );  // 50ms
							
	//config.telemetry
	print_unsigned_integer((unsigned int)config.telemetry.stream_GpsBasic, uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)config.telemetry.stream_PPM, uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)config.telemetry.stream_GyroAccRaw, uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)config.telemetry.stream_GyroAccProc, uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)config.telemetry.stream_PressureTemp, uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)config.telemetry.stream_Attitude, uart1_puts);
	uart1_putc(';');
	
	vTaskDelay(( ( portTickType ) 50 / portTICK_RATE_MS ) );  // 50ms
	
	//config.gps
	print_unsigned_integer((unsigned int)(config.gps.initial_baudrate/10), uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)(config.gps.operational_baudrate/10), uart1_puts);
	uart1_putc(';');
	
	//config.control
	print_unsigned_integer((unsigned int)(config.control.channel_ap), uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)(config.control.channel_motor), uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)(config.control.channel_pitch), uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)(config.control.channel_roll), uart1_puts);
	uart1_putc(';');
	print_unsigned_integer((unsigned int)(config.control.channel_yaw), uart1_puts);
	uart1_putc(';');
	
	vTaskDelay(( ( portTickType ) 50 / portTICK_RATE_MS ) );  // 50ms

	// control.pid
	printf("%f;%f;%f;%f;%f;%f;", config.control.pid_pitch2elevator.p_gain,
	                    config.control.pid_pitch2elevator.d_gain,
	                    config.control.pid_pitch2elevator.i_gain,
	                    config.control.pid_pitch2elevator.i_min,
	                    config.control.pid_pitch2elevator.i_max,
	                    config.control.pid_pitch2elevator.d_term_min_var);
	vTaskDelay(( ( portTickType ) 50 / portTICK_RATE_MS ) );  // 50ms
	printf("%f;%f;%f;%f;%f;%f;", config.control.pid_roll2aileron.p_gain,
	                    config.control.pid_roll2aileron.d_gain,
	                    config.control.pid_roll2aileron.i_gain,
	                    config.control.pid_roll2aileron.i_min,
	                    config.control.pid_roll2aileron.i_max,
	                    config.control.pid_roll2aileron.d_term_min_var);
	vTaskDelay(( ( portTickType ) 50 / portTICK_RATE_MS ) );  // 50ms
	printf("%f;%f;%f;%f;%f;%f;", config.control.pid_heading2roll.p_gain,
	                    config.control.pid_heading2roll.d_gain,
	                    config.control.pid_heading2roll.i_gain,
	                    config.control.pid_heading2roll.i_min,
	                    config.control.pid_heading2roll.i_max,
	                    config.control.pid_heading2roll.d_term_min_var);
	vTaskDelay(( ( portTickType ) 50 / portTICK_RATE_MS ) );  // 50ms
	printf("%f;%f;%f;%f;%f;%f;", config.control.pid_altitude2pitch.p_gain,
	                    config.control.pid_altitude2pitch.d_gain,
	                    config.control.pid_altitude2pitch.i_gain,
	                    config.control.pid_altitude2pitch.i_min,
	                    config.control.pid_altitude2pitch.i_max,
	                    config.control.pid_altitude2pitch.d_term_min_var);
	vTaskDelay(( ( portTickType ) 50 / portTICK_RATE_MS ) );  // 50ms
	// servo_reverse
	print_unsigned_integer(((int)config.control.reverse_servo1) +
	                       ((int)config.control.reverse_servo2<<1) +
	                       ((int)config.control.reverse_servo3<<2) +
	                       ((int)config.control.reverse_servo4<<3) +
	                       ((int)config.control.reverse_servo5<<4) +
	                       ((int)config.control.reverse_servo6<<5), uart1_puts); 
	                     
	// servo max/min/neutral
	for (i = 0; i < 6; i++)
	{
		printf(";%d;%d;%d", config.control.servo_min[i], config.control.servo_max[i], config.control.servo_neutral[i]);
	}	
	vTaskDelay(( ( portTickType ) 50 / portTICK_RATE_MS ) );  // 50ms
	
	printf(";%d", (int)config.control.use_pwm);
	
	printf(";%d;%d;%d;%d;%d", (int)config.control.servo_mix, 
	                     	     (int)(RAD2DEG(config.control.max_pitch)+0.5), 
	                     	     (int)(RAD2DEG(config.control.max_roll)+0.5),
	                   	         (int)(config.control.waypoint_radius_m),
	                 	         (int)(config.control.cruising_speed_ms));
	printf(";%d;%d;%d", (int)(config.control.stabilization_with_altitude_hold), 
	                    config.control.aileron_differential*10, config.telemetry.stream_Control);
	vTaskDelay(( ( portTickType ) 50 / portTICK_RATE_MS ) );  // 50ms
    printf(";%d;%d;%d;%d;%d", (int)config.control.autopilot_auto_throttle, config.control.auto_throttle_min_pct, config.control.auto_throttle_max_pct,
	                    config.control.auto_throttle_cruise_pct, config.control.auto_throttle_p_gain);
	printf(";%d", (int)(RAD2DEG(config.control.min_pitch)-0.5));
	printf(";%d;%d;%u", (int)config.control.manual_trim, (int)config.control.altitude_mode, (unsigned int)config.gps.enable_waas);

    // osd
    unsigned int bitmask = 0;
    bitmask  =  (config.osd.show_altitude? 1 : 0) +
                (config.osd.show_arrow_home? 2 : 0) +
                (config.osd.show_artificial_horizon? 4 : 0) +
                (config.osd.show_block_name? 8 : 0) +
                (config.osd.show_current? 16 : 0) +
                (config.osd.show_distance_home? 32 : 0) +
                (config.osd.show_flight_time? 64 : 0) +
                (config.osd.show_gps_status? 128 : 0) +
                (config.osd.show_mah? 256 : 0) +
                (config.osd.show_mode? 512 : 0) +
                (config.osd.show_rc_link? 1024 : 0) +
                (config.osd.show_speed? 2048 : 0) +
                (config.osd.show_vario? 4096 : 0) +
                (config.osd.show_voltage1? 8192 : 0) +
                (config.osd.show_voltage2? 16384 : 0) +
                (config.osd.show_block_name? 32768 : 0) ;
    printf(";%u;%u;%u;%u", bitmask, (unsigned int)config.osd.rssi, (unsigned int)config.osd.voltage_low, (unsigned int)config.osd.voltage_high);
    printf(";%d;%d", (int)config.sensors.imu_rotated, (int)RAD2DEG(config.sensors.neutral_pitch));
    uart1_puts("\r\n");
}		


/*!
 *     This function sends LogLine l to the communication channel.
 */ 
void print_logline(struct LogLine *l)
{
#ifdef DETAILED_LOG
	// Normal logging
	printf_nochecksum ("DD;%f;%f;", RAD2DEG(l->gps_latitude_rad), RAD2DEG(l->gps_longitude_rad));
	printf_nochecksum ("%f;%d;%d;%d;", ((float)l->gps_speed_m_s)/100.0, l->gps_heading, l->gps_height_m, (int)l->gps_satellites);
	printf_nochecksum ("%d;%d;", l->height_m, l->pitch);
	printf_nochecksum ("%d;", l->roll);
	printf_nochecksum ("%d;%d;%d;%d;", l->desired_pitch, l->desired_roll, l->desired_heading, l->desired_height);
	printf_nochecksum ("%f;", l->acc_x_g);
	printf_nochecksum ("%f;%f;", l->acc_y_g, l->acc_z_g);
	printf_nochecksum ("%d;%d;%d;", l->p, l->q, l->r);
	printf_nochecksum ("%d;%d;%d\r\n", (int)l->temperature_c, (int)l->control_state, l->navigation_code_line+1);

#elif RAW_50HZ_LOG
	// Raw sensor logging @ 50Hz
	printf ("DD;%f;%f;", l->gps_latitude_rad*(180.0/3.14159), l->gps_longitude_rad*(180.0/3.14159));
	printf ("%lu;%f;%d;", l->gps_time, ((float)l->gps_speed_m_s_10) / 10.0, ((int)l->gps_heading_2)*2);

	printf ("%u;%u;%u;", l->acc_x, l->acc_y, l->acc_z);
	printf ("%u;%u;%u;", l->gyro_x, l->gyro_y, l->gyro_z);

//	printf ("%f;%d;%d;%d;%u;%d\r\n", ((float)l->height_m_5) / 5.0, l->pitch, l->roll, l->pitch_acc, l->idg500_vref, l->control_state);
	printf ("%f;%d;%d;%d\r\n", ((float)l->height_m_5) / 5.0, l->pitch, l->roll, l->pitch_acc);
#else
	// Normal logging
	//printf_nochecksum ("DD;%lu;%lu;%.9f;%.9f;", l->date, l->time, RAD2DEG(l->gps_latitude_rad), RAD2DEG(l->gps_longitude_rad));
	//printf_nochecksum ("%f;%d;%d;", ((float)l->gps_speed_m_s)/3.0, l->gps_heading, l->gps_height_m);
	//printf_nochecksum ("%d;%d;%d;%d;", l->height_m, l->pitch, l->roll, l->yaw);
	//printf_nochecksum ("%d;%d;%d;%u\r\n", (int)l->temperature_c, (int)l->control_state, l->navigation_code_line+1, l->servo_trigger);

    printf_checksum("DD;%lu;%lu;%.6f;%.6f;%.1f;%d;%d;%d;%d;%d;%d;%d;%d;%d;%u",
                            l->date, l->time, RAD2DEG(l->gps_latitude_rad), RAD2DEG(l->gps_longitude_rad),
                            ((float)l->gps_speed_m_s)/3.0, l->gps_heading, l->gps_height_m,
                            l->height_m, l->pitch, l->roll, l->yaw,
                            (int)l->temperature_c, (int)l->control_state, l->navigation_code_line+1, l->servo_trigger);
#endif
}	


#ifdef RAW_50HZ_LOG
void print_logline_simulation(struct LogLine *l)
{
	static int i = 0;
	static float last_height;
	
	sensor_data.acc_x_raw = l->acc_x;
	sensor_data.acc_y_raw = l->acc_y;
	sensor_data.acc_z_raw = l->acc_z;
	sensor_data.gyro_x_raw = l->gyro_x;
	sensor_data.gyro_y_raw = l->gyro_y;
	sensor_data.gyro_z_raw = l->gyro_z;
	sensor_data.gps.speed_ms = (float)l->gps_speed_m_s_10/10.0;
	sensor_data.pressure_height = ((float)l->height_m_5) / 5.0;
	
	i++;
	if (last_height != sensor_data.pressure_height)
	{
		float dt = (float)i * 0.02;
		i = 0;
		sensor_data.vertical_speed = sensor_data.vertical_speed * 0.5 + (sensor_data.pressure_height - last_height)/dt * 0.5;
		last_height = sensor_data.pressure_height;
		sensor_data.vertical_speed = 0.0;
	}	
	
	sensor_data.acc_x = ((float)(sensor_data.acc_x_raw) - (float)config.sensors.acc_x_neutral) / (-6600.0*-1.0);
	sensor_data.acc_y = ((float)(sensor_data.acc_y_raw) - (float)config.sensors.acc_y_neutral) / (-6600.0*-1.0);
	sensor_data.acc_z = ((float)(sensor_data.acc_z_raw) - (float)config.sensors.acc_z_neutral) / (-6600.0);
			
	// scale to rad/sec
	sensor_data.p = ((float)(sensor_data.gyro_x_raw)-config.sensors.gyro_x_neutral) * (-0.02518315*3.14159/180.0 * -1.0);  // 0.02518315f
	sensor_data.q = ((float)(sensor_data.gyro_y_raw)-config.sensors.gyro_y_neutral) * (-0.02538315*3.14159/180.0 * -1.0);
	sensor_data.r = ((float)(sensor_data.gyro_z_raw)-config.sensors.gyro_z_neutral) * (0.0062286*3.14159/180.0);  //(2^16-1 - (2^5-1)) / 3.3 * 0.0125*(22)/(22+12)
	
	ahrs_filter();
	
	l->pitch = (sensor_data.pitch/3.14*180.0);
	l->roll = (sensor_data.roll/3.14*180.0);
	print_logline(l);
	//printf ("%f;%f;%f\r\n", sensor_data.pitch, sensor_data.roll, sensor_data.pitch_acc );
}	
#endif


/*!
 *  Sends a signed integer as a string to a function.
 *  This function converts integer x to a series of characters and sends
 *  them one by one to function printer. Faster than printf, obviously.
 *  @param x The signed integer
 *  @param printer The function that the signed integer will be send to as a
 *                 string. It must have one argument: a pointer to an array 
 *                 of chars.
 */
void print_signed_integer(int x, void (*printer)(char[]))
{
	static char tmpChar[2] = "0";
	unsigned int radix,
                 i, converted;
	char emptyChar = ' ';
	
	if (x < 0)
	{
		printer("-");
		x*=-1;
	}
	else if (x == 0)
	{
		printer("0");
		return;	
	}
	converted = (unsigned int) x;
	// auto-align on 16bit size (655.. = 5 positions)
	for (radix=10000, i=0; i < 5; radix/=10, i++)
	{
		if (converted > radix-1) {
			tmpChar[0] =  '0' + (converted/radix);
			printer(tmpChar);
			converted = converted % radix;
			emptyChar = '0';
		} else if (emptyChar == '0') {
			printer("0");
		}
	}
}




/*!
 *  Sends an unsigned integer as a string to a function.
 *  This function converts integer x to a series of characters and sends
 *  them one by one to function printer. Faster than printf, obviously.
 *  @param x The unsigned integer
 *  @param printer The function that the unsigned integer will be send to as a
 *                 string. It must have one argument: a pointer to an array 
 *                 of chars.
 */
void print_unsigned_integer(unsigned int x, void (*printer)(char[]))
{
	static char tmpChar[2] = "0";
	unsigned int radix,
                 i, converted;
	char emptyChar = ' ';
	
	if (x == 0)
	{
		printer("0");
		return;	
	}
	converted = (unsigned int) x;
	// auto-align on 16bit size (655.. = 5 positions)
	for (radix=10000, i=0; i < 5; radix/=10, i++)
	{
		if (converted > radix-1) {
			tmpChar[0] =  '0' + (converted/radix);
			printer(tmpChar);
			converted = converted % radix;
			emptyChar = '0';
		} else if (emptyChar == '0') {
			printer("0");
		}
	}
}



char hex[] = {'0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f'};
void comm_send_buffer_with_checksum(int length)
{
	char checksum = 0;
	int j;
	uart1_putc('$');
	for (j=0;j < length && j < COMM_BUFFER_LEN; j++)
	{
		char c = comm_buffer[j];
		uart1_putc(c);
		checksum ^= c;
	}
	uart1_putc('*');
	//0123456789ABCDEF
	uart1_putc(hex[checksum/16]);
	uart1_putc(hex[checksum%16]);
	uart1_putc('\r');
	uart1_putc('\n');
}

int check_checksum(char *s)
{
	int i = 1;
	char checksum = 0;
	
	while (s[i] != '*' && i < BUFFERSIZE-2)
	{
		checksum ^= s[i];
		i++;
	}
	//if (! (s[i+1] == hex[checksum/16] && s[i+2] == hex[checksum%16]))
	//	printf("\r\n %c %c <> %c %c\r\n", s[i+1], s[i+2], hex[checksum/16], hex[checksum%16]);
//	s[i]='*';
//printf("\r\n %s  %d <> %c%c\r\n", s,checksum, s[i+1], s[i+2]);
	return s[i+1] == hex[checksum/16] && s[i+2] == hex[checksum%16];
}	
	

/*void comm_printf(const char *fmt, ...)
{
    va_list args;
    va_start(args, fmt);
    int i;

    //return printf(fmt, args);

	comm_send_buffer_with_checksum(sprintf(comm_buffer, fmt, args));
}*/

