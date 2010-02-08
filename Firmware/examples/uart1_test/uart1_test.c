/*
 *   This simple example shows the uart1 functionality (more
 *   in particular the sending part). It also shows that printf()
 *   by default outputs to uart1. 
 *
 */


#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"

int main()
{
	microcontroller_init();
	
	uart1_open(38400);	

	uart1_puts ("Starting test...\n\r");
	
	microcontroller_delay_ms(1000);
	uart1_puts("One second\n\r");
	microcontroller_delay_ms(1000);
	uart1_puts("Two seconds\n\r");
	microcontroller_delay_ms(1000);
	uart1_puts("Three seconds\n\r");
	microcontroller_delay_ms(1000);
	
	printf("printf() outputs to uart%d by default!\n\r", 1);

	printf("End of test!\n\r");
	while (1)
		;

	return 0;	
}
