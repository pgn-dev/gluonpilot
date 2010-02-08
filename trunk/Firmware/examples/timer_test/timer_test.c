/*
 *   This simple example illustrates the usage of the timer lib.
 *   Basically, it returns the number of seconds between 
 *   every timer_dt() call.
 *
 */
#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "timer/timer.h"


int main()
{
	microcontroller_init();
	uart1_open(38400L);
	
	printf("Timer test\n\r");
	
	timer_init();
	
	while (1)
	{
		float t1, t10, t100;
		timer_dt();
		t1 = timer_dt();
		uart1_puts(".\n\r");
		timer_dt();
		microcontroller_delay_ms(10);
		t10 = timer_dt();
		uart1_puts("10\n\r");
		uart1_puts(".\n\r");
		timer_dt();
		microcontroller_delay_ms(100);
		t100 = timer_dt();
		uart1_puts("100\n\r");
		printf("[0] = %f\n\r", t1);
		printf("[10] = %f\n\r", t10);
		printf("[100] = %f\n\r", t100);
	}	
	
	return 0;	
}
