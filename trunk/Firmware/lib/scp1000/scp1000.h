
//! Initializes and configures the scp1000 sensor
void scp1000_init();

//! Returns the pressure in Pa
float scp1000_get_pressure();

//! Returns the height in meters
float scp1000_get_height();

//! Returns the temperature in °C
float scp1000_get_temperature();

//! Returns the status byte of the sensor
unsigned int scp1000_get_status();

int scp1000_dataready();

//! Converts the pressure in Pa to height in meter
float scp1000_pressure_to_height(float pressure, float temperature);
