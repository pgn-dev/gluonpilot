/*! 
 *  @file     microcontroller.h
 *  @brief    To be included by every file that would use the microcontroller's hardware
 *  @detailed This takes case of the initialization of the microcontroller (PLL, XT). Also
 *            contains delay functions and traps.
 *  @author   Tom Pycke
 *  @date     23-nov-2008
 *  @since    0.1
 */

#include <p33FJ256MC710.h>
 
#define FOSC 4000000        // We're using a 4MHz Crystal
#define PLL  80/4           // PLL configuration
#define FCY  PLL*FOSC/2     // Resulting Fcy for a dsPic33

void microcontroller_init();

void microcontroller_delay_us(unsigned long us);
void microcontroller_delay_ms(unsigned long ms);
int microcontroller_after_reboot();
void microcontroller_reset_type();


#define INTERRUPT_PROTECT(x) {              \
    char saved_ipl;                         \
                                            \
    SET_AND_SAVE_CPU_IPL(saved_ipl,7);      \
    x;                                      \
    RESTORE_CPU_IPL(saved_ipl); } (void) 0;

