using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class ControlInfo
    {
        public enum FlightModes { 
            MANUAL = 0, STABILIZED = 1, AUTOPILOT = 2, LOITER = 3, RETURN = 4 
        };

        public FlightModes FlightMode = FlightModes.AUTOPILOT;
        public double Altitude = 0;
        public double TargetAltitude = 0;
        public int CurrentNavigationLine = 1;
        public double Batt1Voltage = 0;
        public double Batt2Voltage = 0;
        public double Batt_mAh = 0;
        public int FlightTime = 0;
        public int BlockTime = 0;
        public int Throttle = 0;
        public int RcLink = 0;
    }
}
