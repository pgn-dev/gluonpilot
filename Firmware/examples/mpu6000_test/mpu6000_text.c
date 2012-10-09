
#include <stdio.h>
#include "microcontroller/microcontroller.h"
#include "uart1/uart1.h"
#include "mpu6000/mpu6000.h"

void main()
{
    int i;

    microcontroller_init();
    uart1_open(57600L);

    printf("MPU-6000 test\r\n");

    printf("init...\r\n");

    mpu6000_init();

    printf("done.\r\n");

    for (i = 0; i < 100; i++)
    {
        mpu6000_update_sensor_readings();
        printf("%f\r\n", (float) mpu6000_raw_sensor_readings.temp / 340.0 + 36.54);
        microcontroller_delay_ms(200);
    }

    printf("done.\r\n");

    while (uart1_getc() != 'Z')
        ;
    microcontroller_delay_ms(1000);
    asm("reset");
}
