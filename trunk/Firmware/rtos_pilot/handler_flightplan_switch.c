
#include "ppm_in/ppm_in.h"
#include "gluonscript.h"
#include "handler_flightplan_switch.h"

struct flightplan_switch flightplan_switch = { .active = 0, .current_state = -1 };

static int last_switch_state = -1;
static int i = 0;

ScriptHandlerReturn flightplan_switch_handle_gluonscriptcommand (struct GluonscriptCode *code)
{
    enum FlightplanStates this_state;

    if (flightplan_switch.active && i++ % 2 == 1)   // save some uC cycles; i++ % 2 == 1 to make sure it has a startup delay (and has a good PWM/PPM reception)
    {
        int channel_value = ppm.channel[flightplan_switch.channel];
        if (channel_value < 1400)
            this_state = BELOW_1400;
        else if (channel_value < 1600)
            this_state = BELOW_1600;
        else
            this_state = ABOVE_1600;

        if (last_switch_state == -1)
        {
            last_switch_state = this_state;
            flightplan_switch.current_state = this_state;
        }

        if (this_state != flightplan_switch.current_state && this_state == last_switch_state)
        {
            //printf("\r\nVal %d -> State %d->%d -> Line %d \r\n", channel_value, flightplan_switch.current_state, this_state, gluonscript_data.current_codeline+2); // not + 1 -> ++ follows after HANDLED_FINISHED
            printf("\r\nFlightplan switch: new block selected\r\n");
            gluonscript_data.current_codeline = flightplan_switch.target[this_state] - 1;  // is incremented on HANDLED_FINISHED
            flightplan_switch.current_state = this_state;
            last_switch_state = this_state;
            return HANDLED_FINISHED;
        }
        else
        {
            last_switch_state = this_state;
        }
    }

    if (code->opcode == SET_FLIGHTPLAN_SWITCH)
    {
        flightplan_switch.active = 1;
        flightplan_switch.channel = code->a;
        flightplan_switch.target[BELOW_1400] = code->b - 1;
        flightplan_switch.target[BELOW_1600] = (int)code->x - 1;
        flightplan_switch.target[ABOVE_1600] = (int)code->y - 1;
        return HANDLED_FINISHED;
    }
    return NOT_HANDLED;
}
