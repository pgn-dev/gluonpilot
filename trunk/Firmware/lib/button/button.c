/*!
 *   
 *
 */


#include "microcontroller/microcontroller.h"


/*!
 *   Initialize the button
 */
void button_init()
{
	TRISE = TRISE | 0b10000000;
}

/*!
 *   Returns 1 if the button is pressed down. Otherwise the return value is 0.
 */
int button_down()
{
	return (PORTE & 0b10000000) == 0;
}


/*!
 *   Returns 1 if the button is not pressed down. Otherwise the return value is 0.
 */
int button_up()
{
	return (PORTE & 0b10000000) != 0;
}
