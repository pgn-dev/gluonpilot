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

#include "sensors.h"
#include "communication.h"
#include "configuration.h"
#include "datalogger.h"


// Helper functions
void print_signed_integer(int x, void (*printer)(char[]));
void print_unsigned_integer(unsigned int x, void (*printer)(char[]));
void print_logline(struct LogLine *l);
void print_logline_simulation(struct LogLine *l);

#define BUFFERSIZE 200
static char  buffer[BUFFERSIZE];



/*!
 *    This task will send telemetry directly to uart1 at a rate of maximum 
 *    20 times a second.
 */
void communication_telemetry_task( void *parameters )
{
	int i, c = 0;
	struct TelemetryConfig counters;
		
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Telemetry task initializing...");
	
	counters.stream_PPM = 0;
	counters.stream_GyroAccRaw = 0;
	counters.stream_GyroAccProc = 0;
	counters.stream_PressureTemp = 0;
	counters.stream_GpsBasic = 0;
	
	led_init();
	
	uart1_puts("done\r\n");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil() works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 50 / portTICK_RATE_MS ) );  // 20Hz
		counters.stream_PPM++;
		counters.stream_GyroAccRaw++;
		counters.stream_GyroAccProc++;
		counters.stream_PressureTemp++;
		counters.stream_GpsBasic++;
		
		if (c++ % 10 == 0)  // this counter will never be used at 20Hz
			led1_on();
		else
			led1_off();
				
		///////////////////////////////////////////////////////////////
		//               GYRO AND ACCELEROMETER RAW                  //
		///////////////////////////////////////////////////////////////
		if (counters.stream_GyroAccRaw == config.telemetry.stream_GyroAccRaw)
		{
			uart1_puts("TR;");
			print_unsigned_integer((int)(sensor_data.acc_x_raw), &uart1_puts);
			uart1_putc(';');
			print_unsigned_integer((int)(sensor_data.acc_y_raw), &uart1_puts);
			uart1_putc(';');
			print_unsigned_integer((int)(sensor_data.acc_z_raw), &uart1_puts);
			uart1_putc(';');
			print_unsigned_integer((int)(sensor_data.gyro_x_raw), &uart1_puts);
			uart1_putc(';');
			print_unsigned_integer((int)(sensor_data.gyro_y_raw), &uart1_puts);
			uart1_putc(';');
			print_unsigned_integer((int)(sensor_data.gyro_z_raw), &uart1_puts);
			//printf(";%f", sensor_data.temperature);
			uart1_puts("\r\n");
			counters.stream_GyroAccRaw = 0;
		} 
		else if (counters.stream_GyroAccRaw > config.telemetry.stream_GyroAccRaw)
			counters.stream_GyroAccRaw = 0;
		
		///////////////////////////////////////////////////////////////
		//            GYRO AND ACCELEROMETER PROCESSED              //
		///////////////////////////////////////////////////////////////
		if (counters.stream_GyroAccProc == config.telemetry.stream_GyroAccProc)
		{
			uart1_puts("TP;");
			print_signed_integer((int)(sensor_data.acc_x*1000), &uart1_puts);
			uart1_putc(';');
			print_signed_integer((int)(sensor_data.acc_y*1000), &uart1_puts);
			uart1_putc(';');
			print_signed_integer((int)(sensor_data.acc_z*1000), &uart1_puts);
			uart1_putc(';');
			print_signed_integer((int)(sensor_data.p*1000), &uart1_puts);
			uart1_putc(';');
			print_signed_integer((int)(sensor_data.q*1000), &uart1_puts);
			uart1_putc(';');
			print_signed_integer((int)(sensor_data.r*1000), &uart1_puts);
			uart1_puts("\r\n");
			
			uart1_puts("TA;");
			print_signed_integer((int)(sensor_data.roll*1000), &uart1_puts);
			uart1_putc(';');
			print_signed_integer((int)(sensor_data.pitch*1000), &uart1_puts);
			uart1_putc(';');
			print_signed_integer((int)(sensor_data.roll_acc*1000), &uart1_puts);
			uart1_putc(';');
			print_signed_integer((int)(sensor_data.pitch_acc*1000), &uart1_puts);
			uart1_putc(';');
			print_signed_integer((int)(sensor_data.yaw*1000), &uart1_puts);
			uart1_puts("\r\n");
			counters.stream_GyroAccProc = 0;
		} 
		else if (counters.stream_GyroAccProc > config.telemetry.stream_GyroAccProc)
			counters.stream_GyroAccProc = 0;
			
		///////////////////////////////////////////////////////////////
		//           SCP1000: PRESSURE & TEMPERATURE                 //
		///////////////////////////////////////////////////////////////
		if (counters.stream_PressureTemp == config.telemetry.stream_PressureTemp)
		{
			uart1_puts("TH;");
			printf("%.0f", (sensor_data.pressure));
			//PrintUnsignedInteger((unsigned int)(sensor_data.pressure/10), &uart1_puts);
			uart1_putc(';');
			print_signed_integer((int)sensor_data.temperature, &uart1_puts);
			uart1_puts("\r\n");
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

			uart1_puts("TT");
			for (i = 0; i < 8; i++)
			{
				uart1_putc(';');
				print_unsigned_integer((unsigned int)ppm.channel[i], &uart1_puts);
			}			
			uart1_puts("\r\n");
			counters.stream_PPM = 0;
		}
		else if (counters.stream_PPM > config.telemetry.stream_PPM)
			counters.stream_PPM = 0;
		
		///////////////////////////////////////////////////////////////
		//                        GPS BASIC                          //
		///////////////////////////////////////////////////////////////
		if (counters.stream_GpsBasic == config.telemetry.stream_GpsBasic)
		{
			uart1_puts("TG");
			uart1_putc(';');
			uart1_putc('0' + (unsigned char)sensor_data.gps.status);
			uart1_putc(';');
			printf ("%f;%f", sensor_data.gps.latitude_rad, sensor_data.gps.longitude_rad);
			//PrintUnsignedLong((unsigned long)(sensor_data.gps.latitude_rad*1000000), &uart1_puts);
			//uart1_putc(';');
			//PrintUnsignedLong((unsigned long)(sensor_data.gps.longitude_rad*1000000), &uart1_puts);
			uart1_putc(';');
			print_unsigned_integer((unsigned int)(sensor_data.gps.speed_ms*10), &uart1_puts);
			uart1_putc(';');
			print_unsigned_integer((unsigned int)(sensor_data.gps.heading_rad*100), &uart1_puts);
			uart1_putc(';');
			print_unsigned_integer((unsigned int)(sensor_data.gps.satellites_in_view), &uart1_puts);
			uart1_putc(';');
			print_unsigned_integer((unsigned int)(sensor_data.gps.height_m), &uart1_puts);

			uart1_puts("\r\n");
			counters.stream_GpsBasic = 0;
		}
		else if (counters.stream_GpsBasic > config.telemetry.stream_GpsBasic)
			counters.stream_GpsBasic = 0;
	}
}


extern xQueueHandle xRxedChars;


/*!
 *   This task parses and executes all commands coming from the groundstation
 *   or configuration utility. It depends on uart1_queue.c because all data received
 *   on the uart1 is stored in a FreeRTOS queue. When there is data in the queue, the
 *   RTOS executes this function so the data can be parsed.
 */
void communication_input_task( void *parameters )
{
	static int   buffer_position;
	static int   token[7] = {0,0,0,0,0,0,0};
	static int   current_token;
	
	char tmp;

	uart1_puts("Console input task initializing...");
	uart1_puts("done\r\n");
	
	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		if( xQueueReceive( xRxedChars, &tmp, portMAX_DELAY  ) )
        {           
	        //vTaskList(&buffer1);
	        //uart1_putc(tmp);
            if ((tmp == '\n' || tmp == '\r'))
            {
	            buffer[buffer_position] = '\0';
	            //token[current_token + 1] = buffer_position;

				///////////////////////////////////////////////////////////////
				//                      SET TELEMETRY                        //
				///////////////////////////////////////////////////////////////
				if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'T')    // Set Telemetry
				{
					config.telemetry.stream_GpsBasic = atoi(&(buffer[token[1]]));
					config.telemetry.stream_GyroAccRaw = atoi(&(buffer[token[2]]));
					config.telemetry.stream_GyroAccProc = atoi(&(buffer[token[3]]));
					config.telemetry.stream_PPM = atoi(&(buffer[token[4]]));
					config.telemetry.stream_PressureTemp = atoi(&(buffer[token[5]]));
				}
				///////////////////////////////////////////////////////////////
				//                    SET ACCELEROMETER                      //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'A')    // Set Accelerometer neutral
				{
					//sscanf(&(buffer[token[1]]), "%u", &x);
					config.sensors.acc_x_neutral = (float)atof(&(buffer[token[1]]));
					//sscanf(&(buffer[token[2]]), "%u", &x);
					config.sensors.acc_y_neutral = (float)atof(&(buffer[token[2]]));
					//sscanf(&(buffer[token[3]]), "%u", &x);
					config.sensors.acc_z_neutral = (float)atof(&(buffer[token[3]]));
				}
				///////////////////////////////////////////////////////////////
				//                        SET GYRO                           //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'Y')    // Set Gyro neutral
				{
					//sscanf(&(buffer[token[1]]), "%u", &x);
					config.sensors.gyro_x_neutral = (float)atof(&(buffer[token[1]]));
					//sscanf(&(buffer[token[2]]), "%u", &x);
					config.sensors.gyro_y_neutral = (float)atof(&(buffer[token[2]]));
					//sscanf(&(buffer[token[3]]), "%u", &x);
					config.sensors.gyro_z_neutral = (float)atof(&(buffer[token[3]]));
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
				}
				///////////////////////////////////////////////////////////////
				//                       SET CONTROL                         //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'C')    // Set control
				{
					config.control.servo_mix = (buffer[token[1] + 0]) - '0';
					config.control.max_pitch = atof(&(buffer[token[2]])) / 180.0 * 3.14;
					config.control.max_roll = atof(&(buffer[token[3]])) / 180.0 * 3.14;
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
					for (i = 0; i < MAX_INDEX; i++)
						printf("DT;%d;%d;%ld;%ld\r\n", i, datalogger_index_table[i].page_num, datalogger_index_table[i].date, datalogger_index_table[i].time);
				}	
				///////////////////////////////////////////////////////////////
				//                       DATALOG READ                        //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'D' && buffer[token[0] + 1] == 'R')    
				{
					int i = atoi(&(buffer[token[1]]));
					
#ifndef RAW_50HZ_LOG
					printf ("DH;Latitude;Longitude;SpeedGPS;HeadingGPS;HeightGPS;");
					printf ("HeightBaro;Pitch;PitchAcc;Roll;RollAcc;AccX;AccXG;AccY;AccYG;AccZ;");
					printf ("AccZG;GyroX;GyroY;GyroZ;P;Q;R;TempC;FlightMode\r\n");
#else
					printf ("DH;Latitude;Longitude;Time;SpeedGPS;HeadingGPS;AccX;AccY;AccZ;GyroX;GyroY;GyroZ;HeightBaro;Pitch;Roll;PitchAcc\r\n");
#endif

					datalogger_disable();
					
					while (datalogger_print_next_page(i, &print_logline))
						;
					/* ** SIMULATION ** */
					//ahrs_init();
					//while (datalogger_print_next_page(i, &print_logline_simulation))
					//	;
					
					//datalogger_enable();
				}	
				///////////////////////////////////////////////////////////////
				//                      WRITE TO FLASH                       //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'F' && buffer[token[0] + 1] == 'C')    // FC write to flash!
				{
					configuration_write();
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
						int i;
						uart1_puts("\n\rCA;");
						
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

						// control.pid
						printf("%f;%f;%f;%f;%f;%f;", config.control.pid_pitch2elevator.p_gain,
						                    config.control.pid_pitch2elevator.d_gain,
						                    config.control.pid_pitch2elevator.i_gain,
						                    config.control.pid_pitch2elevator.i_min,
						                    config.control.pid_pitch2elevator.i_max,
						                    config.control.pid_pitch2elevator.d_term_min_var);
						printf("%f;%f;%f;%f;%f;%f;", config.control.pid_roll2aileron.p_gain,
						                    config.control.pid_roll2aileron.d_gain,
						                    config.control.pid_roll2aileron.i_gain,
						                    config.control.pid_roll2aileron.i_min,
						                    config.control.pid_roll2aileron.i_max,
						                    config.control.pid_roll2aileron.d_term_min_var);
						printf("%f;%f;%f;%f;%f;%f", config.control.pid_heading2roll.p_gain,
						                    config.control.pid_heading2roll.d_gain,
						                    config.control.pid_heading2roll.i_gain,
						                    config.control.pid_heading2roll.i_min,
						                    config.control.pid_heading2roll.i_max,
						                    config.control.pid_heading2roll.d_term_min_var);

						// servo_reverse
						uart1_putc(';');
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
						
						printf(";%d", (int)config.control.use_pwm);
						
						printf(";%d;%d;%d", (int)config.control.servo_mix, (int)(config.control.max_pitch/3.14*180.0), (int)(config.control.max_roll/3.14*180.0));
						uart1_puts("\r\n");
					}	
					
					else if (current_token > 0)
						uart1_puts("\r\nERROR\r\n");
				}
				else if (current_token > 0)
					uart1_puts("\r\nERROR\r\n");

            	buffer_position = 0;
            	current_token = 0;
            	token[0] = 0;
            	token[1] = 0;
            	token[2] = 0;
            	token[3] = 0;
            	token[4] = 0;
            	token[5] = 0;
            	token[6] = 0;
            }
            else if (tmp == ';' && current_token < 6 && buffer_position < BUFFERSIZE)
            {
	            buffer[buffer_position++] = tmp;
	            token[++current_token] = buffer_position;
	            
	        } 
	        else if (buffer_position < BUFFERSIZE)
	        	buffer[buffer_position++] = tmp;
        }
	}
}


/*!
 *     This function sends LogLine l to the communication channel.
 */ 
void print_logline(struct LogLine *l)
{
#ifndef RAW_50HZ_LOG
	// Normal logging
	printf ("DD;%f;%f;", l->gps_latitude_rad*(180.0/3.14159), l->gps_longitude_rad*(180.0/3.14159));
	printf ("%d;%d;%d;", l->gps_speed_m_s, l->gps_heading, l->gps_height_m);
	printf ("%d;%d;%d;", l->height_m, l->pitch, l->pitch_acc);
	printf ("%d;%d;", l->roll, l->roll_acc);
	printf ("%u;%f;%u;", l->acc_x, l->acc_x_g, l->acc_y);
	printf ("%f;%u;%f;", l->acc_y_g, l->acc_z, l->acc_z_g);
	printf ("%u;%u;%u;", l->gyro_x, l->gyro_y, l->gyro_z);
	printf ("%d;%d;%d;", l->p, l->q, l->r);
	printf ("%d;%d\r\n", (int)l->temperature_c, l->control_state);
#else
	// Raw sensor logging @ 50Hz
	printf ("DD;%f;%f;", l->gps_latitude_rad*(180.0/3.14159), l->gps_longitude_rad*(180.0/3.14159));
	printf ("%lu;%f;%d;", l->gps_time, ((float)l->gps_speed_m_s_10) / 10.0, ((int)l->gps_heading_2)*2);

	printf ("%u;%u;%u;", l->acc_x, l->acc_y, l->acc_z);
	printf ("%u;%u;%u;", l->gyro_x, l->gyro_y, l->gyro_z);

	printf ("%f;%d;%d;%d\r\n", ((float)l->height_m_5) / 5.0, l->pitch, l->roll, l->pitch_acc);
#endif
}	


#ifdef RAW_50HZ_LOG
void print_logline_simulation(struct LogLine *l)
{
	static int i = 0;
	static double last_height;
	
	sensor_data.acc_x_raw = l->acc_x;
	sensor_data.acc_y_raw = l->acc_y;
	sensor_data.acc_z_raw = l->acc_z;
	sensor_data.gyro_x_raw = l->gyro_x;
	sensor_data.gyro_y_raw = l->gyro_y;
	sensor_data.gyro_z_raw = l->gyro_z;
	sensor_data.gps.speed_ms = (float)l->gps_speed_m_s_10/10.0;
	sensor_data.pressure_height = ((float)l->height_m_5) / 5.0;
	
	if (i++ % 5 == 0)
	{
		sensor_data.vertical_speed = sensor_data.vertical_speed * 0.5 + (sensor_data.pressure_height - last_height)/0.1 * 0.5;
		last_height = sensor_data.pressure_height;
	}	
	
	sensor_data.acc_x = ((double)(sensor_data.acc_x_raw) - (double)config.sensors.acc_x_neutral) / (-6600.0*-1.0);
	sensor_data.acc_y = ((double)(sensor_data.acc_y_raw) - (double)config.sensors.acc_y_neutral) / (-6600.0*-1.0);
	sensor_data.acc_z = ((double)(sensor_data.acc_z_raw) - (double)config.sensors.acc_z_neutral) / (-6600.0);
			
	// scale to rad/sec
	sensor_data.p = ((double)(sensor_data.gyro_x_raw)-config.sensors.gyro_x_neutral) * (-0.02518315*3.14159/180.0 * -1.0);  // 0.02518315f
	sensor_data.q = ((double)(sensor_data.gyro_y_raw)-config.sensors.gyro_y_neutral) * (-0.02538315*3.14159/180.0 * -1.0);
	sensor_data.r = ((double)(sensor_data.gyro_z_raw)-config.sensors.gyro_z_neutral) * (0.0062286*3.14159/180.0);  //(2^16-1 - (2^5-1)) / 3.3 * 0.0125*(22)/(22+12)
	
	ahrs_filter();
	
	
	printf ("%f;%f;%f\r\n", sensor_data.pitch, sensor_data.roll, sensor_data.pitch_acc );
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

