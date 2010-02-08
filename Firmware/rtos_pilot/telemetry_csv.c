
#include <stdio.h>

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
#include "telemetry.h"
#include "configuration.h"


// Helper functions
void PrintSignedInteger(int x, void (*printer)(char[]));
void PrintUnsignedInteger(unsigned int x, void (*printer)(char[]));


/*!
 *    This task will send a line directly to uart1 20 times a second.
 */
void vTelemetryTask( void *pvParameters )
{
	int i;
	struct TelemetryConfig counters;
		
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Telemetry task initializing...");
	
	counters.stream_PPM = 0;
	counters.stream_GyroAccRaw = 0;
	counters.stream_GyroAccProc = 0;
	counters.stream_PressureTemp = 0;
	counters.stream_GpsBasic = 0;
	//counters.stream_GpsBasic = 0;
	
	led_init();
	
	uart1_puts("done\n\r");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil() works correctly. */
	xLastExecutionTime = xTaskGetTickCount();

	for( ;; )
	{
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 50 / portTICK_RATE_MS ) );
		counters.stream_PPM++;
		counters.stream_GyroAccRaw++;
		counters.stream_GyroAccProc++;
		counters.stream_PressureTemp++;
		counters.stream_GpsBasic++;
		
		if (counters.stream_GpsBasic % 2 == 0)  // this counter will never be used at 20Hz
			led1_toggle();
				
		///////////////////////////////////////////////////////////////
		//               GYRO AND ACCELEROMETER RAW                  //
		///////////////////////////////////////////////////////////////
		if (counters.stream_GyroAccRaw == config.telemetry.stream_GyroAccRaw)
		{
			uart1_puts("TR;");
			PrintUnsignedInteger((int)(sensor_data.acc_x_raw), &uart1_puts);
			uart1_putc(';');
			PrintUnsignedInteger((int)(sensor_data.acc_y_raw), &uart1_puts);
			uart1_putc(';');
			PrintUnsignedInteger((int)(sensor_data.acc_z_raw), &uart1_puts);
			uart1_putc(';');
			PrintUnsignedInteger((int)(sensor_data.gyro_x_raw), &uart1_puts);
			uart1_putc(';');
			PrintUnsignedInteger((int)(sensor_data.gyro_y_raw), &uart1_puts);
			uart1_putc(';');
			PrintUnsignedInteger((int)(sensor_data.gyro_z_raw), &uart1_puts);
			uart1_puts("\n\r");
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
			PrintSignedInteger((int)(sensor_data.acc_x*1000), &uart1_puts);
			uart1_putc(';');
			PrintSignedInteger((int)(sensor_data.acc_y*1000), &uart1_puts);
			uart1_putc(';');
			PrintSignedInteger((int)(sensor_data.acc_z*1000), &uart1_puts);
			uart1_putc(';');
			PrintSignedInteger((int)(sensor_data.p*1000), &uart1_puts);
			uart1_putc(';');
			PrintSignedInteger((int)(sensor_data.q*1000), &uart1_puts);
			uart1_putc(';');
			PrintSignedInteger((int)(sensor_data.r*1000), &uart1_puts);
			uart1_puts("\n\r");
			
			uart1_puts("TA;");
			PrintSignedInteger((int)(sensor_data.roll*1000), &uart1_puts);
			uart1_putc(';');
			PrintSignedInteger((int)(sensor_data.pitch*1000), &uart1_puts);
			uart1_putc(';');
			PrintSignedInteger((int)(sensor_data.roll_acc*1000), &uart1_puts);
			uart1_putc(';');
			PrintSignedInteger((int)(sensor_data.pitch_acc*1000), &uart1_puts);
			uart1_putc(';');
			PrintSignedInteger((int)(sensor_data.yaw*1000), &uart1_puts);
			uart1_puts("\n\r");
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
			PrintSignedInteger((int)sensor_data.temperature, &uart1_puts);
			uart1_puts("\n\r");
			counters.stream_PressureTemp = 0;
		}
		else if (counters.stream_PressureTemp > config.telemetry.stream_PressureTemp)
			counters.stream_PressureTemp = 0;
		
		///////////////////////////////////////////////////////////////
		//                   RC TRANSMITTER INPUT                    //
		///////////////////////////////////////////////////////////////
		if (counters.stream_PPM == config.telemetry.stream_PPM)
		{
			uart1_puts("TT");
			for (i = 0; i < 8; i++)
			{
				uart1_putc(';');
				PrintUnsignedInteger((unsigned int)ppm.channel[i], &uart1_puts);
			}			
			uart1_puts("\n\r");
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
			PrintUnsignedInteger((unsigned int)(sensor_data.gps.speed_ms*10), &uart1_puts);
			uart1_putc(';');
			PrintUnsignedInteger((unsigned int)(sensor_data.gps.heading_rad*100), &uart1_puts);
			uart1_putc(';');
			PrintUnsignedInteger((unsigned int)(sensor_data.gps.satellites_in_view), &uart1_puts);
			

			uart1_puts("\n\r");
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
void vConsoleInputTask( void *pvParameters )
{
	char  buffer[30];
	//char  buffer1[200];
	int   buffer_position;
	int   token[7] = {0,0,0,0,0,0, 0};
	int   current_token;
	
	char tmp;

	uart1_puts("Console input task initializing...");
	uart1_puts("done\n\r");
	
	for( ;; )
	{
		/* Wait until it is time for the next cycle. */
		if( xQueueReceive( xRxedChars, &tmp, portMAX_DELAY  ) )
        {           
	        //vTaskList(&buffer1);
	        //uart1_puts(buffer1);
            if (tmp == '\n' || tmp == '\r')
            {
	            buffer[buffer_position] = '\0';
	            token[current_token + 1] = buffer_position;

				///////////////////////////////////////////////////////////////
				//                      SET TELEMETRY                        //
				///////////////////////////////////////////////////////////////
				if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'T')    // Set Telemetry
				{
					config.telemetry.stream_GpsBasic = (buffer[token[1] + 0] - 'a');
					config.telemetry.stream_GyroAccRaw = (buffer[token[2] + 0] - 'a');
					config.telemetry.stream_GyroAccProc = (buffer[token[3] + 0] - 'a');
					config.telemetry.stream_PPM = (buffer[token[4] + 0] - 'a');
					config.telemetry.stream_PressureTemp = (buffer[token[5] + 0] - 'a');
				}
				///////////////////////////////////////////////////////////////
				//                    SET ACCELEROMETER                      //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'A')    // Set Accelerometer neutral
				{
					unsigned int x;
					sscanf(&(buffer[token[1]]), "%u", &x);
					config.sensors.acc_x_neutral = (float)x;
					sscanf(&(buffer[token[2]]), "%u", &x);
					config.sensors.acc_y_neutral = (float)x;
					sscanf(&(buffer[token[3]]), "%u", &x);
					config.sensors.acc_z_neutral = (float)x;
				}
				///////////////////////////////////////////////////////////////
				//                    SET INPUT CHANNELS                     //
				///////////////////////////////////////////////////////////////
				else if (buffer[token[0]] == 'S' && buffer[token[0] + 1] == 'I')    // Set Input Channel
				{
					config.control.channel_ap = (buffer[token[1] + 0] - 'a');
					config.control.channel_motor = (buffer[token[2] + 0] - 'a');
					config.control.channel_pitch = (buffer[token[3] + 0] - 'a');
					config.control.channel_roll = (buffer[token[4] + 0] - 'a');
					config.control.channel_yaw = (buffer[token[5] + 0] - 'a');
					
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
						uart1_puts("\n\rCA;");
						
						//config.sensors
						PrintUnsignedInteger((unsigned int)config.sensors.acc_x_neutral, uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)config.sensors.acc_y_neutral, uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)config.sensors.acc_z_neutral, uart1_puts);
						uart1_putc(';');
						
						//config.telemetry
						PrintUnsignedInteger((unsigned int)config.telemetry.stream_GpsBasic, uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)config.telemetry.stream_PPM, uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)config.telemetry.stream_GyroAccRaw, uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)config.telemetry.stream_GyroAccProc, uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)config.telemetry.stream_PressureTemp, uart1_puts);
						uart1_putc(';');
						
						//config.gps
						PrintUnsignedInteger((unsigned int)(config.gps.initial_baudrate/10), uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)(config.gps.operational_baudrate/10), uart1_puts);
						uart1_putc(';');
						
						//config.control
						PrintUnsignedInteger((unsigned int)(config.control.channel_ap), uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)(config.control.channel_motor), uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)(config.control.channel_pitch), uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)(config.control.channel_roll), uart1_puts);
						uart1_putc(';');
						PrintUnsignedInteger((unsigned int)(config.control.channel_yaw), uart1_puts);

						uart1_puts("\n\r");
					}	
					
					else if (current_token > 0)
						uart1_puts("\n\rERROR\n\r");
				}
				else if (current_token > 0)
					uart1_puts("\n\rERROR\n\r");

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
            else if (tmp == ';' && current_token < 6 && buffer_position < 30)
            {
	            buffer[buffer_position++] = tmp;
	            token[++current_token] = buffer_position;
	            
	        } 
	        else if (buffer_position < 30)
	        	buffer[buffer_position++] = tmp;
        }
	}
}




/*!
 *  Sends a signed integer as a string to a function.
 *  This function converts integer x to a series of characters and sends
 *  them one by one to function printer. Faster than printf, obviously.
 *  @param x The signed integer
 *  @param printer The function that the signed integer will be send to as a
 *                 string. It must have one argument: a pointer to an array 
 *                 of chars.
 */
void PrintSignedInteger(int x, void (*printer)(char[]))
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
void PrintUnsignedInteger(unsigned int x, void (*printer)(char[]))
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

