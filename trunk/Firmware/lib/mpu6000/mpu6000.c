
#include "mpu6000/mpu6000.h"
#include "microcontroller/microcontroller.h"

#define CS PORTGbits.RG9
#define MISO PORTGbits.RG8
#define MOSI PORTGbits.RG7
#define SCK PORTGbits.RG6

unsigned char spi_comm_bitbang(unsigned char outgoing_byte);
void spi_cs_disable();
void spi_cs_enable();
void spi_write_reg(unsigned char addr, unsigned char data);
unsigned int spiGet16(void);
unsigned char spi_read_reg(unsigned char addr);

struct mpu6000_raw_sensors mpu6000_raw_sensor_readings;

void mpu6000_init()
{
    TRISGbits.TRISG9 = 0; // cs
    spi_cs_disable();
    TRISGbits.TRISG6 = 0; // sck
    TRISGbits.TRISG7 = 0; // MOSI
    TRISGbits.TRISG8 = 1; // MISO


    //spi_write_reg(MPUREG_PWR_MGMT_1, BIT_H_RESET);
    //microcontroller_delay_us(10);
    //spi_write_reg(MPUREG_PWR_MGMT_1, BIT_H_RESET);

    //microcontroller_delay_us(1200);

    spi_write_reg(MPUREG_AUX_VDDIO, 0); // Auxiliary I2C Supply Selection, set to zero for MPU-6000
    microcontroller_delay_us(100);
    // Wake up device and select GyroZ clock (better performance)
    spi_write_reg(MPUREG_PWR_MGMT_1, MPU_CLK_SEL_PLLGYROZ);
    microcontroller_delay_us(100);
    // Disable I2C bus (recommended on datasheet)
    spi_write_reg(MPUREG_USER_CTRL, BIT_I2C_IF_DIS);
    microcontroller_delay_us(100);
    spi_write_reg(MPUREG_SMPLRT_DIV, 0x04);     // Sample rate = 200Hz    Fsample= 1Khz/(4+1) = 200Hz
    microcontroller_delay_us(100);
    //spi_write_reg(MPUREG_SMPLRT_DIV, 0x09);     // Sample rate = 100Hz    Fsample= 1Khz/(9+1) = 100Hz
   // microcontroller_delay_us(100);
    spi_write_reg(MPUREG_CONFIG, BITS_DLPF_CFG_42HZ);  // BITS_DLPF_CFG_20HZ BITS_DLPF_CFG_42HZ BITS_DLPF_CFG_98HZ
    microcontroller_delay_us(100);
//  spi_write_reg(MPUREG_GYRO_CONFIG, BITS_FS_2000DPS);  // Gyro scale 2000º/s
    spi_write_reg(MPUREG_GYRO_CONFIG, BITS_FS_1000DPS);  // Gyro scale 1000º/s
    microcontroller_delay_us(100);
//  spi_write_reg(MPUREG_ACCEL_CONFIG, BITS_FS_2G);           // Accel scele 2g (g=8192)
    spi_write_reg(MPUREG_ACCEL_CONFIG, BITS_FS_8G);           // Accel scele 8g
    microcontroller_delay_us(100);

    //spi_write_reg(0x38, BIT_MOT_EN);

}

int mpu6000_is_moving()
{
    return (int)spi_read_reg(0x3A);// & BIT_MOT_INT;
}

// very rough measurement: takes 0,0001s (0,1ms)
void mpu6000_update_sensor_readings()
{
    // We start a SPI multibyte read of sensors
    spi_cs_enable();
    spi_comm_bitbang(MPUREG_ACCEL_XOUT_H | 0x80);
    mpu6000_raw_sensor_readings.acc_x = spiGet16();
    mpu6000_raw_sensor_readings.acc_y = spiGet16();
    mpu6000_raw_sensor_readings.acc_z = spiGet16();
    mpu6000_raw_sensor_readings.temp = spiGet16();
    mpu6000_raw_sensor_readings.gyro_x = spiGet16();
    mpu6000_raw_sensor_readings.gyro_y = spiGet16();
    mpu6000_raw_sensor_readings.gyro_z = spiGet16();
    spi_cs_disable();
}

unsigned int spiGet16(void)
{
       return ((int)spi_comm_bitbang(0) << 8) | ((int)spi_comm_bitbang(0) & 0xFF);
}

void spi_cs_disable()
{
    CS = 1;
}

void spi_cs_enable()
{
    CS = 0;
}

void spi_write_reg(unsigned char addr, unsigned char data)
{
    spi_cs_disable();
    spi_cs_enable();

    spi_comm_bitbang(addr);
    spi_comm_bitbang(data);

    spi_cs_disable();
}

unsigned char spi_read_reg(unsigned char addr)
{
    spi_cs_disable();
    spi_cs_enable();

    spi_comm_bitbang(addr | 0x80);
    unsigned char data = spi_comm_bitbang(0x00);

    spi_cs_disable();

    return data;
}

unsigned char spi_comm_bitbang(unsigned char outgoing_byte)
{
    unsigned char incoming_byte = 0, x;
    
    for(x = 0 ; x < 8 ; x++)
    {
        //asm("nop");asm("nop");asm("nop");asm("nop");
        SCK = 0; //Toggle the SPI clock
        MOSI = outgoing_byte >> 7; //Put bit on SPI data bus , SDO
        outgoing_byte <<= 1; //Rotate byte 1 to the left
        //asm("nop");asm("nop");asm("nop");asm("nop");
        SCK = 1;
        incoming_byte <<= 1; //Rotate byte 1 to the left
        incoming_byte |= MISO & 0x01; //Read bit on SPI data bus, SDI
    }
    
    return(incoming_byte);
}
