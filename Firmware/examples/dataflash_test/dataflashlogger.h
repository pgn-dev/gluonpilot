
struct dataflashlogger_info
{
	long time;
	long date;
	unsigned int  page;
};

void dataflashlogger_add(unsigned char *data, int size);

void dataflashlogger_read(unsigned char *data, struct dataflashlogger_info *a, int size);

unsigned char dataflashlogger_equal(struct dataflashlogger_info *a, struct dataflashlogger_info *b);

void dataflashlogger_format();

void dataflashlogger_init(long time, long date);
