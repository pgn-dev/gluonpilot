
#include "ppm_in/ppm_in.h"
#include "gluonscript.h"
#include "handler_maximum_range.h"
#include "handler_navigation.h"
#include "sensors.h"

struct maximum_range maximum_range = { .active = 0 };

static int i = 0;

ScriptHandlerReturn maximum_range_handle_gluonscriptcommand (struct GluonscriptCode *code)
{
    if (i++ % 5 == 1 && maximum_range.active)   // save some uC cycles; i++ % 2 == 1 to make sure it has a startup delay (and has a good PWM/PPM reception)
    {
        if (navigation_distance_between_meter(sensor_data.gps.longitude_rad, navigation_data.home_longitude_rad,
                                              sensor_data.gps.latitude_rad, navigation_data.home_latitude_rad) > maximum_range.maximum_range)
        {
            uart1_puts("\r\nRange limit!\r\n");
            gluonscript_data.current_codeline = maximum_range.target - 1;  // is incremented on HANDLED_FINISHED
            return HANDLED_FINISHED;
        }
    }

    if (code->opcode == SET_MAXIMUM_RANGE)
    {
        maximum_range.active = 1;
        maximum_range.maximum_range = code->x;
        maximum_range.target = code->a -1;
        return HANDLED_FINISHED;
    }
    return NOT_HANDLED;
}
