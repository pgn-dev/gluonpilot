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


/*!
 *    Opens the connection with the hardware.
 */
void dataflash_open();

/*!
 *    Reads a page from the flash chip
 *    @param page   The page number.
 *    @param size   Number of bytes to read into buffer
 *    @param buffer Pointer to a buffer of at least "size" bytes. This will store the result.
 */
void dataflash_read(int page, int size, unsigned char *buffer);

void dataflash_read_config(int size, unsigned char *configbuffer);

/*!
 *    Writes a page to the flash chip
 *    @param page   The page address
 *    @param size   Number of bytes to read into buffer
 *    @param buffer Pointer to a buffer of at least "size" bytes. 
 */
void dataflash_write(int page, int size, unsigned char *buffer);

/*!
 *    Reads the AT45xxxx chip type and returns the nr. of megabits
 */
int dataflash_read_Mbit();

