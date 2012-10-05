/*!
 *    @title  Flash access library for gluonpilot
 *
 *    @author (c) Tom Pycke
 *    @date    23-nov-2008
 */

//#define MAX_PAGE 4095 
extern int MAX_PAGE;
extern int PAGE_SIZE;

extern int START_LOG_PAGE;
extern int LOG_INDEX_PAGE;
extern int CONFIGURATION_PAGE;
extern int NAVIGATION_PAGE;


struct Dataflash {
        void (*open) ();
        void (*read) (int page, int size, unsigned char *buffer);
        void (*write) (int page, int size, unsigned char *buffer);
        int (*read_Mbit) ();
} ;

extern struct Dataflash dataflash;

/*!
 *    Opens the connection with the hardware.
 */
void dataflash_open();

