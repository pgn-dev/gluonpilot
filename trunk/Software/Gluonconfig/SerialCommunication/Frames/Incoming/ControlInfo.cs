using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class ControlInfo
    {
        public enum FlightModes { 
            MANUAL = 0, STABILIZED = 1, AUTOPILOT = 2, LOITER = 3, RETURN = 4 
        };

        public FlightModes FlightMode;
        public double HeightAboveStartGround;
        public int CurrentNavigationLine;
        public double BattVoltage;
    }
}
