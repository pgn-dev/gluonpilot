#ifndef HANDLER_MAXIMUM_RANGE_H
#define HANDLER_MAXIMUM_RANGE_H

#include "gluonscript.h"

struct maximum_range
{
    int active;
    float maximum_range;
    int target;
};

ScriptHandlerReturn maximum_range_handle_gluonscriptcommand (struct GluonscriptCode *code);

#endif // HANDLER_MAXIMUM_RANGE_H
