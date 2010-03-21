/*
 *
 *    Dataflash:
 *    [ Page 0 | Page 1 | Page 2 | Page 3 | ... | Page 4096 ]
 *    |  Configuration  | Index+1| Logging .................|
 *     
 *     Logging page=    
 *    [ 2Bytes: index number (0 = unused) | Bytes 3...528: Array of LogLines.... ]
 *
 */
 
 
// Include all FreeRTOS header files
#include "FreeRTOS/FreeRTOS.h"
#include "FreeRTOS/task.h"
#include "FreeRTOS/queue.h"
#include "FreeRTOS/croutine.h"
#include "FreeRTOS/semphr.h"

#include "microcontroller/microcontroller.h"
#include "dataflash/dataflash.h"
#include "datalogger.h"
#include "sensors.h"
#include "control.h"

struct LogIndex datalogger_index_table[MAX_INDEX];

unsigned char buffer[PAGE_SIZE];

int current_page = 3;
int current_index = 0;

int disable_logging = 0; // used when reading out data


xSemaphoreHandle xSpiSemaphore;
void datalogger_read(int page, int size, unsigned char *buffer);
void datalogger_write(int page, int size, unsigned char *buffer);


void datalogger_init()
{
	int i, start_page = 3;
	
	//printf("formatting...");
	//datalogger_format();
	//printf("done");
	datalogger_read(2, sizeof(struct LogIndex) * MAX_INDEX, (unsigned char*)datalogger_index_table);
	
	// find the first empty page
	for (i = 0; i < MAX_INDEX; i++)
	{
		if (datalogger_index_table[i].page_num >= start_page)
		{
			start_page = datalogger_index_table[i].page_num + 1;
			current_index = (i+1)%MAX_INDEX;
		}	
	}

	for (i = start_page; i < MAX_PAGE; i++)
	{
		int *index;
		datalogger_read(i, PAGE_SIZE, buffer);
		//printf("reading page %d: %d %d %d %d\r\n", i, (int)buffer[0], (int)buffer[1], (int)buffer[2], (int)buffer[3]);
		index = (int*) &(buffer[0]);
		if (*index == 0)
		{
			current_page = i;
			*index = (current_index+1); // buffer[0] + [1] contains current_index + 1
			break;
		}			
	}
	
	// update the index
	datalogger_index_table[current_index].page_num = current_page;
	datalogger_index_table[current_index].time = sensor_data.gps.time;
	datalogger_index_table[current_index].date = sensor_data.gps.date;
}


void datalogger_start_session()
{
	datalogger_write(2, sizeof(struct LogIndex) * MAX_INDEX, (unsigned char*)datalogger_index_table);
	//printf("Starting to datalog to page %d, index %d\r\n", current_page, current_index);	
}	


void datalogger_read(int page, int size, unsigned char *buffer)
{
	if (xSemaphoreTake( xSpiSemaphore, ( portTickType ) 10 ))   // Spi1 is shared with SCP1000 and Dataflash
	{
		dataflash_read(page, size, buffer);
		xSemaphoreGive( xSpiSemaphore );
	}	
}	

void datalogger_write(int page, int size, unsigned char *buffer)
{
	if (xSemaphoreTake( xSpiSemaphore, ( portTickType ) 10 ))   // Spi1 is shared with SCP1000 and Dataflash
	{
		dataflash_write(page, size, buffer);
		xSemaphoreGive( xSpiSemaphore );
	}	
}	

	
int current_line = 0;
void datalogger_writeline(struct LogLine *line)
{
	unsigned char *a, *b;
	int i;
	struct LogLine *lines = (struct LogLine*) &(buffer[2]);   // buffer[0-1] set in datalogger_init()
	
	if (2+(current_line+1)*sizeof(struct LogLine) >= PAGE_SIZE)
	{
		datalogger_write(current_page, sizeof(buffer), buffer);
		current_page++;
		if (current_page > MAX_PAGE)
			current_page = 3;
		current_line = 0;
		//printf("write page!\n\r");
	}	
	
	a = (unsigned char*) line;
	b = (unsigned char*) &(lines[current_line++]);
	
	for (i = 0; i < sizeof(struct LogLine); i++)
		b[i] = a[i];
}


int datalogger_print_next_page(int index, void(*printer)(struct LogLine*))
{
	static int last_index = -1;
	static int last_page = -1;
	static struct LogLine *lines = (struct LogLine*) &(buffer[2]);
	
	int *i = (int*) &(buffer[0]);
	int j;
	
	if (index != last_index)
	{
		last_page = datalogger_index_table[index].page_num;
		last_index = index;
	}

	datalogger_read(last_page++, sizeof(buffer), buffer);
	if (last_page > MAX_PAGE)
			last_page = 3;

	if (*i != index+1)
	{
		//printf ("%d != %d\r\n", *i, index+1);
		return 0;
	}	

	for (j = 0; j < (PAGE_SIZE - 2) / sizeof(struct LogLine); j++)
		printer(&lines[j]);
		
	return 1;
}
	

void datalogger_format()
{
	int i;
	for (i = 0; i < PAGE_SIZE; i++)
		buffer[i] = 0;
		
	for (i = 2; i < MAX_PAGE; i++)
	{
		printf("page %d\r\n", i);
		datalogger_write(i, PAGE_SIZE, buffer);
	}	
}	


void datalogger_disable()
{
	disable_logging = 1;
}


void datalogger_enable()
{
	disable_logging = 0;
}


void datalogger_task( void *parameters )
{
	static struct LogLine l;
	
	/* Used to wake the task at the correct frequency. */
	portTickType xLastExecutionTime; 

	uart1_puts("Datalogger task initializing...");

	datalogger_init();
	
	uart1_puts("done\r\n");
	
	/* Initialise xLastExecutionTime so the first call to vTaskDelayUntil()	works correctly. */
	xLastExecutionTime = xTaskGetTickCount();
	
	// wait for GPS	
	while(sensor_data.gps.status != ACTIVE)
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 1000 / portTICK_RATE_MS ) );   // 1Hz
		
	datalogger_start_session();
	
	for( ;; )
	{	
		vTaskDelayUntil( &xLastExecutionTime, ( ( portTickType ) 250 / portTICK_RATE_MS ) );   // 4Hz
		
		if (! disable_logging)
		{
			l.temperature_c = (char)sensor_data.temperature; // -128°C...+128°C
			l.height_m = (int)sensor_data.pressure_height;
			l.gps_latitude_rad = sensor_data.gps.latitude_rad;
			l.gps_longitude_rad = sensor_data.gps.longitude_rad;
			l.gps_height_m = sensor_data.gps.height_m;
			l.gps_heading = sensor_data.gps.heading_rad;
			l.gps_speed_m_s = sensor_data.gps.speed_ms;
			l.acc_x = sensor_data.acc_x_raw;
			l.acc_y = sensor_data.acc_y_raw;
			l.acc_z = sensor_data.acc_z_raw;
			l.gyro_x = sensor_data.gyro_x_raw;
			l.gyro_y = sensor_data.gyro_y_raw;
			l.gyro_z = sensor_data.gyro_z_raw;
			l.pitch = (int)(sensor_data.pitch * (180.0/3.14159));
			l.roll = (int)(sensor_data.roll * (180.0/3.14159));
			l.control_state = control_state.flight_mode;
			
			datalogger_writeline(&l);
		}
	}
}
