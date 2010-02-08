
#include "../uart1/uart1.h"
#include "../microcontroller.h"


int main()
{
	microcontroller_init();
	
	uart1_open(38400L);
	
	
}