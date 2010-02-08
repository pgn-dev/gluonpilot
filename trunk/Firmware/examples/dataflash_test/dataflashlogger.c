
#include "dataflash.h"

#include "dataflashlogger.h"


unsigned char logbufferwrite[528];
unsigned char logbufferread[528];


unsigned int current_write_page = 1;
unsigned int current_read_page = 1;

#define MAX_PAGES 4096

#define PAGE_HEADER sizeof(struct dataflashlogger_info)

unsigned int logbufferwrite_counter = PAGE_HEADER;
unsigned int logbufferread_counter = 550;


void dataflashlogger_init(long time, long date)
{
	struct dataflashlogger_info *a = (struct dataflashlogger_info*)logbufferwrite;
	a->time = time;
	a->date = date;
	a->page = 0;
	
	logbufferwrite_counter = PAGE_HEADER;
	logbufferread_counter = 550;
	
	//for (current_write_page = 1; current_write_page < MAX_PAGES; current_write_page++)
	{
		/*dataflash_read(current_write_page, 512, logbufferread);
		struct dataflashlogger_info *i = logbufferread;
		if (i->time == 0 && i->date == 0)
			return;*/
	}
}	


void dataflashlogger_format()
{
	int i;
	for (i = 0; i < 512 + PAGE_HEADER; i++)
		logbufferwrite[i] = 0;
		
	for (i = 1; i < MAX_PAGES; i++)
	{
		dataflash_write(i, 512 + PAGE_HEADER, logbufferwrite);
	}	
}	


void dataflashlogger_add(unsigned char *data, int size)
{
	int i;
	
	if (logbufferwrite_counter - PAGE_HEADER + size > 512)
	{
		dataflash_write(current_write_page, 512 + PAGE_HEADER, logbufferwrite);
		printf("Log written to page %d\r\n", current_write_page);
		current_write_page++;
		logbufferwrite_counter = PAGE_HEADER;
		struct dataflashlogger_info *a = (struct dataflashlogger_info*)logbufferwrite;
		a->page++;
	}

	for (i = 0; i < size; i++)
		logbufferwrite[logbufferwrite_counter++] = data[i];
}


void dataflashlogger_read(unsigned char *data, struct dataflashlogger_info *a, int size)
{
	int i;
	
	if (logbufferread_counter > 512+PAGE_HEADER - size)
	{
		dataflash_read(current_read_page, 512+PAGE_HEADER, logbufferread);
		logbufferread_counter = PAGE_HEADER;
		printf ("Reading page %d\r\n", current_read_page);
		char *ab = (char*)a;
		for (i = 0; i < PAGE_HEADER; i++)
			ab[i] = logbufferread[i];
		current_read_page++;
	}
	
	for (i = 0; i < size; i++)
		data[i] = logbufferread[logbufferread_counter++];

}

unsigned char dataflashlogger_equal(struct dataflashlogger_info *a, struct dataflashlogger_info *b)
{
	int i;
	unsigned char *ab = a, *bb = b;
	return a->time == b->time && a->date == b->date;
}
