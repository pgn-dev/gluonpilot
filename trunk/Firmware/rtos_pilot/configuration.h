#ifndef CONFIGURATION_H
#define CONFIGURATION_H


#include "sensors.h"
#include "communication.h"
#include "control.h"
#include "gps/gps.h"

struct Configuration
{
	struct SensorConfig sensors;
	struct TelemetryConfig telemetry;
	struct GpsConfig gps;
	struct ControlConfig control;
};	

extern struct Configuration config;

void configuration_default();

void configuration_load();

void configuration_write();

#endif // CONFIGURATION_H
