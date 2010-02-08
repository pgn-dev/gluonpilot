
#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "dataflash/dataflash.h"


int main()
{
	microcontroller_init();
	
	uart1_open(115200l);
	microcontroller_delay_ms(100);
	printf ("starting...");
	microcontroller_delay_ms(100);
	dataflash_open();

	
	microcontroller_delay_ms(100);
	
	unsigned char buffer1[512] = "This data should be properly stored inside the dataflash memory, and fetched back in the same proper condition :-)";
	unsigned char buffer2[512];

	int i;
	microcontroller_delay_ms(100);
	
	printf("\n\rstart write");

	for (i = 0; i < 1000; i++)
		dataflash_write(i, 512, buffer1);
	printf("\n\rdone");
	microcontroller_delay_ms(40);  // Wait 40 ms! (typ. 17)



	microcontroller_delay_ms(80);  // Wait 40 ms! (typ. 17)
	printf("\n\rstart read");

	for (i = 0; i < 1000; i++)
		dataflash_read(0, 512, buffer2);

	printf("\n\rdone\n\r");	
	printf ("\n\r[%s]\n\r", buffer2);
	microcontroller_delay_ms(10);
		
	while (1);
}	


