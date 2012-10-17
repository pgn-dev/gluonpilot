
#include <adc.h>
#include "microcontroller/microcontroller.h"
#include "adc/adc.h"
#include "configuration.h"

// Define Message Buffer Length for ECAN1/ECAN2
#define  MAX_CHNUM	 			24		// Highest Analog input number in Channel Scan
#define  SAMP_BUFF_SIZE	 		8		// Size of the input buffer per analog input
int  NUM_CHS2SCAN=8;		// Number of channels enabled for channel scan

// Number of locations for ADC buffer = 14 (AN0 to AN13) x 8 = 112 words
// Align the buffer to 128 words or 256 bytes. This is needed for peripheral indirect mode
unsigned int BufferA[MAX_CHNUM][SAMP_BUFF_SIZE] __attribute__((space(dma),aligned(256)));
unsigned int BufferB[MAX_CHNUM][SAMP_BUFF_SIZE] __attribute__((space(dma),aligned(256)));

void initDma0(void);
void adc_open_v1o();
void adc_open_v1q();

void adc_open()
{
    if (HARDWARE_VERSION < V01Q)
        adc_open_v1o();
    else
        adc_open_v1q();
}


void adc_open_v1o()
{
    NUM_CHS2SCAN = 8;
	TRISBbits.TRISB0 = 1; // input
	TRISBbits.TRISB1 = 1; // input
	TRISBbits.TRISB2 = 1; // input
	TRISBbits.TRISB3 = 1; // input
	TRISBbits.TRISB4 = 1; // input
	TRISBbits.TRISB5 = 1; // input
	TRISBbits.TRISB6 = 1; // input
	TRISBbits.TRISB7 = 1; // input
	TRISBbits.TRISB8 = 1; // input
	
	AD1CON1bits.FORM   = 0b10;	// Data Output Format: Fractional
	AD1CON1bits.SSRC   = 0b111;	// Sample Clock Source: internal timer (auto-convert)
	AD1CON1bits.ASAM   = 1;		// ADC Sample Control: Sampling begins immediately after conversion
	AD1CON1bits.AD12B  = 0;		// 12-bit ADC operation
	

	AD1CON2bits.CSCNA = 1;		// Scan Input Selections for CH0+ during Sample A bit
	AD1CON2bits.CHPS  = 0;		// Converts CH0
	AD1CON2bits.VCFG  = 0b000;  // use AVDD and AGND

	AD1CON3bits.ADRC = 0;		// ADC Clock is derived from Systems Clock (0; 1=internal clock)
	AD1CON3bits.ADCS = 63;		// ADC Conversion Clock Tad=Tcy*(ADCS+1)= (1/40M)*64 = 1.6us (625Khz)
								// ADC Conversion Time for 10-bit Tc=12*Tab = 19.2us	
	AD1CON3bits.SAMC = 31;  // auto sample time bits

	/*
	Conversion time = sample + conversion
	                = 31*tad + 14*tad (12-bit)
	Conversion time * 7 * 8 = 0.0007s
	
	*/


	AD1CON1bits.ADDMABM = 0; 	// DMA buffers are built in scatter/gather mode
	AD1CON2bits.SMPI    = (NUM_CHS2SCAN-1);	// 4 ADC Channel is scanned
	AD1CON4bits.DMABL   = 3;	// Each buffer contains 8 words

	//AD1CSSH/AD1CSSL: A/D Input Scan Selection Register
	AD1CSSH = 0x0000;			
	AD1CSSL = 0x0000;
	AD1CSSLbits.CSS0 = 1;			// Enable AN0 for channel scan
	AD1CSSLbits.CSS1 = 1;
	AD1CSSLbits.CSS3 = 1;
	AD1CSSLbits.CSS4 = 1;
	AD1CSSLbits.CSS5 = 1;
	AD1CSSLbits.CSS6 = 1;
	AD1CSSLbits.CSS7 = 1;
	AD1CSSLbits.CSS8 = 1;
 
 	//AD1PCFGH/AD1PCFGL: Port Configuration Register
	AD1PCFGL=0xFFFF;
	AD1PCFGH=0xFFFF;
	AD1PCFGLbits.PCFG0 = 0;		// AN0 as Analog Input
	AD1PCFGLbits.PCFG1 = 0;
 	AD1PCFGLbits.PCFG3 = 0;
	AD1PCFGLbits.PCFG4 = 0;
	AD1PCFGLbits.PCFG5 = 0;
	AD1PCFGLbits.PCFG6 = 0;
	AD1PCFGLbits.PCFG7 = 0;
	AD1PCFGLbits.PCFG8 = 0;
	
	IFS0bits.AD1IF   = 0;		// Clear the A/D interrupt flag bit
	IEC0bits.AD1IE   = 0;		// Do Not Enable A/D interrupt 
	
	initDma0();
	
	adc_start();
}


void adc_open_v1q()
{
    NUM_CHS2SCAN = 4;
    TRISBbits.TRISB3 = 1; // OPTIONAL
	TRISBbits.TRISB8 = 1; // input
    TRISBbits.TRISB9 = 1; // input

    TRISAbits.TRISA7 = 1; // input

	AD1CON1bits.FORM   = 0b10;	// Data Output Format: Fractional
	AD1CON1bits.SSRC   = 0b111;	// Sample Clock Source: internal timer (auto-convert)
	AD1CON1bits.ASAM   = 1;		// ADC Sample Control: Sampling begins immediately after conversion
	AD1CON1bits.AD12B  = 0;		// 12-bit ADC operation


	AD1CON2bits.CSCNA = 1;		// Scan Input Selections for CH0+ during Sample A bit
	AD1CON2bits.CHPS  = 0;		// Converts CH0
	AD1CON2bits.VCFG  = 0b000;  // use AVDD and AGND

	AD1CON3bits.ADRC = 0;		// ADC Clock is derived from Systems Clock (0; 1=internal clock)
	AD1CON3bits.ADCS = 63;		// ADC Conversion Clock Tad=Tcy*(ADCS+1)= (1/40M)*64 = 1.6us (625Khz)
								// ADC Conversion Time for 10-bit Tc=12*Tab = 19.2us
	AD1CON3bits.SAMC = 31;  // auto sample time bits

	/*
	Conversion time = sample + conversion
	                = 31*tad + 14*tad (12-bit)
	Conversion time * 7 * 8 = 0.0007s
	*/


	AD1CON1bits.ADDMABM = 0; 	// DMA buffers are built in scatter/gather mode
	AD1CON2bits.SMPI    = (NUM_CHS2SCAN-1);	// 4 ADC Channel is scanned
	AD1CON4bits.DMABL   = 3;	// Each buffer contains 8 words

	//AD1CSSH/AD1CSSL: A/D Input Scan Selection Register
	AD1CSSH = 0x0000;
	AD1CSSL = 0x0000;
	AD1CSSLbits.CSS3 = 1;			// Enable AN0 for channel scan
	AD1CSSLbits.CSS8 = 1;
	AD1CSSLbits.CSS9 = 1;
    AD1CSSHbits.CSS23 = 1; // RA7

 	//AD1PCFGH/AD1PCFGL: Port Configuration Register
	AD1PCFGL=0xFFFF;
	AD1PCFGH=0xFFFF;
	AD1PCFGLbits.PCFG3 = 0;		// AN0 as Analog Input
	AD1PCFGLbits.PCFG8 = 0;
 	AD1PCFGLbits.PCFG9 = 0;
	AD1PCFGHbits.PCFG23 = 0;

    IFS0bits.AD1IF   = 0;		// Clear the A/D interrupt flag bit
	IEC0bits.AD1IE   = 0;		// Do Not Enable A/D interrupt

	initDma0();

	adc_start();
}

void adc_start()
{
	AD1CON1bits.ADON = 1;		// Turn on the A/D converter
}

void adc_stop()
{
	AD1CON1bits.ADON = 0;		// Turn off the A/D converter	
}		

void initDma0(void)
{
	DMA0CONbits.AMODE = 2;			// Configure DMA for Peripheral indirect mode
	DMA0CONbits.MODE  = 2;			// Configure DMA for Continuous Ping-Pong mode
	DMA0PAD=(int)&ADC1BUF0;
	DMA0CNT = (SAMP_BUFF_SIZE*NUM_CHS2SCAN)-1;					
	DMA0REQ = 13;					// Select ADC1 as DMA Request source

	DMA0STA = __builtin_dmaoffset(BufferA);		
	DMA0STB = __builtin_dmaoffset(BufferB);

	IFS0bits.DMA0IF = 0;			//Clear the DMA interrupt flag bit
    IEC0bits.DMA0IE = 1;			//Set the DMA interrupt enable bit

	DMA0CONbits.CHEN=1;				// Enable DMA
}


unsigned int ProcessADCSamples(unsigned int * AdcBuffer)
{
	return (unsigned int) (((long)(AdcBuffer[0]/4 + AdcBuffer[1]/4 + AdcBuffer[2]/4 + AdcBuffer[3]/4) + (long)(AdcBuffer[4]/4 + AdcBuffer[5]/4 + AdcBuffer[6]/4 + AdcBuffer[7]/4)) / 2);
}


unsigned int DmaBuffer = 0;

void __attribute__((interrupt, no_auto_psv)) _DMA0Interrupt(void)
{
	if(DmaBuffer == 0)
	{
	/*	ProcessADCSamples(&BufferA[0][0]); 
		ProcessADCSamples(&BufferA[1][0]);
		//ProcessADCSamples(&BufferA[2][0]);    // we don't use AN2
		ProcessADCSamples(&BufferA[3][0]);
		ProcessADCSamples(&BufferA[4][0]);
		ProcessADCSamples(&BufferA[5][0]);
		ProcessADCSamples(&BufferA[6][0]);
		ProcessADCSamples(&BufferA[7][0]);
	*/
        //printf("\r\n%u %u %u %u\r\n", BufferA[23][0], BufferA[23][1], BufferA[23][2], BufferA[23][3]);
	}
	else
	{
	/*	ProcessADCSamples(&BufferB[0][0]);
		ProcessADCSamples(&BufferB[1][0]);
		//ProcessADCSamples(&BufferB[2][0]);    // we don't use AN2
		ProcessADCSamples(&BufferB[3][0]);
		ProcessADCSamples(&BufferB[4][0]);
		ProcessADCSamples(&BufferB[5][0]);
		ProcessADCSamples(&BufferB[6][0]);
		ProcessADCSamples(&BufferB[7][0]);
	*/
		adc_stop();
		// we converted everything * 8 * 2buffers every 8ms
	
	}

	DmaBuffer ^= 1;

	IFS0bits.DMA0IF = 0;		// Clear the DMA0 Interrupt Flag
}


unsigned int adc_get_channel(int i)
{
    //printf("\r\n%u %u %u %u\r\n", BufferA[i][0], BufferB[i][7], BufferA[i][6], BufferB[i][7]);
	return ProcessADCSamples(&BufferA[i][0]) / 2 + ProcessADCSamples(&BufferB[i][0]) / 2; 
}
