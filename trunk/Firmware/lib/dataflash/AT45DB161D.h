#ifndef AT45DB161D_H 
#define AT45DB161D_H 
#include<global.h> 
#include<iom128v.h> 
#include<macros.h> 
#include "at45db161d_commands.h" 

#ifndef SPI 

#define DATAOUT     11 

#define DATAIN      12 

#define SPICLOCK    13 

#define SLAVESELECT 10 

#define RESET        8 

#define WP           7 

/* 
#define DF_CS_inactive digitalWrite(SLAVESELECT,HIGH) 

#define DF_CS_active   digitalWrite(SLAVESELECT,LOW) 
*/ 
/* 
#define DF_CS_inactive    PORTB|=_BV(DDB0) 

#define DF_CS_active      PORTB&=~_BV(DDB0) 
*/ 
#define DF_CS_inactive    PORTB|=1 
#define DF_CS_active      PORTB&=0xfe 

#endif /* SPI */ 

#define READY_BUSY 0x80 

#define COMPARE 0x40 

#define PROTECT 0x02 

#define PAGE_SIZE 0x01 

#define DEVICE_DENSITY 0x2C  

struct ATD45DB161D_ID 
{ 
uint8_t manufacturer;        
uint8_t device[2];           
uint8_t extendedInfoLength;  
}; 

void ATD45DB161D_Init(void); 

uint8_t ReadStatusRegister(void); 

void ReadManufacturerAndDeviceID(struct ATD45DB161D_ID *id); 
                 
void ReadMainMemoryPage(uint16_t page, uint16_t offset); 

void ContinuousArrayRead(uint16_t page, uint16_t offset, uint8_t low); 

void  BufferRead(uint8_t bufferNum, uint16_t offset, uint8_t low); 

void  BufferWrite(uint8_t bufferNum, uint16_t offset); 
                 
void  BufferToPage(uint8_t bufferNum, uint16_t page, uint8_t erase);              

void  PageToBuffer(uint16_t page, uint8_t bufferNum); 

void  PageErase(uint16_t page); 
                 
void  BlockErase(uint16_t block); 

void  SectoreErase(uint8_t sector); 

void  ChipErase(void); 

void  BeginPageWriteThroughBuffer(uint16_t page, uint16_t offset, uint8_t bufferNum); 
                 
void  EndAndWait(void); 

int8_t  ComparePageToBuffer(uint16_t page, uint8_t bufferNum); 

void  DeepPowerDown(void); 

void  ResumeFromDeepPowerDown(void); 


#endif /* AT45DB161D_H */ 