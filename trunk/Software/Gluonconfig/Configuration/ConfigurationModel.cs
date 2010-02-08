using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configuration;

namespace Gluonpilot
{
    [Serializable]
    public class ConfigurationModel
    {
        public PidModel Roll2AileronPidModel = new PidModel();
        public PidModel Pitch2ElevatorPidModel = new PidModel();
        public PidModel Heading2RollPidModel = new PidModel();
    
        public bool ReverseServo1;
        public bool ReverseServo2;
        public bool ReverseServo3;
        public bool ReverseServo4;
        public bool ReverseServo5;
        public bool ReverseServo6;

        public int GpsInitialBaudrate;
        public int GpsOperationalBaudrate;

        public int TelemetryGyroAccRaw;
        public int TelemetryGyroAccProc;
        public int TelemetryPpm;
        public int TelemetryGpsBasic;
        public int TelemetryPressureTemp;

        public int NeutralAccX;
        public int NeutralAccY;
        public int NeutralAccZ;
        public int NeutralGyroX;
        public int NeutralGyroY;
        public int NeutralGyroZ;

        public int ChannelRoll;
        public int ChannelPitch;
        public int ChannelYaw;
        public int ChannelMotor;
        public int ChannelAp;
    }
}
