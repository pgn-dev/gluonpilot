#ifndef TELEMETRY_H
#define TELEMETRY_H


/*!
 *   Configuration parameters
 */
struct TelemetryConfig
{
	unsigned char stream_PPM;
	unsigned char stream_GyroAccRaw;
	unsigned char stream_GyroAccProc;
	unsigned char stream_PressureTemp;
	unsigned char stream_GpsBasic;
};

/*!
 *    FreeRTOS task that sends telemetry to uart1
 */
void vTelemetryTask( void *pvParameters );

void vConsoleInputTask( void *pvParameters );



#endif // TELEMETRY_H
