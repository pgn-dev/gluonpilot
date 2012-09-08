#ifndef HANDLER_FLIGHTPLAN_SWITCH_H
#define HANDLER_FLIGHTPLAN_SWITCH_H

#include "handler_flightplan_switch.h"
#include "gluonscript.h"

enum FlightplanStates { BELOW_1400 = 0, BELOW_1600 = 1, ABOVE_1600 = 2};

struct flightplan_switch
{
    int active;
    int channel;
    int target[3];

    enum FlightplanStates current_state;
};

ScriptHandlerReturn flightplan_switch_handle_gluonscriptcommand (struct GluonscriptCode *code);

#endif // HANDLER_FLIGHTPLAN_SWITCH_H
