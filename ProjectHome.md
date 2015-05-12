# Gluonpilot #

Visit the [weblog](http://gluonpilot.com)! More documentation on the [website and wiki](http://gluonpilot.com).
Interested? The module can be found in the [shop](http://gluonpilot.com/wiki/Shop).

First release: libraries & example projects.

Interested in the module: contact me (project owner on your right)!

Overview:

## Intro ##
This project contains the libraries and sample autopilot systems for the gluonpilot module.

## The module ##
  * 6DOF IMU (3 gyros and 3 accelerometers)
  * 16-bit pressure sensor for height
  * Can control 6 servos (PWM outputs)
  * RC-transmitter input through PPM or PWM (4 channels)
  * A push button, for more ease of use.
  * 2 LEDs
  * 4MB flash for datalogging and configuration
  * 256kB flash for code
  * works with EB-85 GPS and other (4-pin) GPS modules
  * connects to your PC using the standard [FTDI USB data cable](http://apple.clickandbuild.com/cnb/shop/ftdichip?productID=49&op=catalogue-product_info-null&prodCategoryID=47)
  * SPI, I2C, UART headers available for communication with other devices

| ![http://tom.pycke.be/images/73t.jpg](http://tom.pycke.be/images/73t.jpg) | ![http://tom.pycke.be/images/74t.jpg](http://tom.pycke.be/images/74t.jpg) |
|:--------------------------------------------------------------------------|:--------------------------------------------------------------------------|

Additional requirements:
  * GPS module (EB-85 or [Locosys](http://www.sparkfun.com/commerce/product_info.php?products_id=8975) are tested & supported).
  * Serial connection to your PC for debugging: [FTDI cable](http://apple.clickandbuild.com/cnb/shop/ftdichip?productID=49&op=catalogue-product_info-null&prodCategoryID=47) is supported, or your own TTL-USB/RS232 cable.


## The code ##
This project page is about the code. All hardware functionality can be accessed by using the libraries that are already developed.
Next to that, there are more advanced examples available:
  * A very basic autopilot
  * A more advanced autopilot using a real time operating system (FreeRTOS).

In the future we think of adding support for quadrocopters.

For more information, or if you are interested in purchasing the module or joining the development, please contact Tom (see project owner on the right).
