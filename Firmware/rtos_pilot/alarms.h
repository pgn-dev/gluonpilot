#ifndef ALARMS_H
#define ALARMS_H

#include "navigation.h"

struct BatteryAlarm {
	float panic_v;
	float warning_v;
	int panic_line;
	
	int alarm_battery_panic;
	int alarm_battery_warning;
};

extern struct BatteryAlarm battery_alarm;

ScriptHandlerReturn alarms_handle_gluonscriptcommand (struct GluonscriptCode *code);


#endif
