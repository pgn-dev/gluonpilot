

void bmp085_init();

long bmp085_read_temp(void);

long bmp085_read_pressure(void);

void bmp085_convert_temp(long raw, int *temp);

void bmp085_convert_pressure(long up, long* pressure);

void bmp085_start_convert_pressure();

void bmp085_start_convert_temp();
