using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class Attitude
    {
        double _pitch_deg;
        double _roll_deg;
        double _yaw_deg;
        double _pitch_acc_deg;
        double _roll_acc_deg;

        public double PitchAccDeg
        {
            get
            {
                return _pitch_acc_deg;
            }
        }
        public double RollAccDeg
        {
            get
            {
                return _roll_acc_deg;
            }
        }
        public double PitchDeg
        {
            get { 
                return _pitch_deg; 
            }
        }
        public double RollDeg
        {
            get
            {
                return _roll_deg;
            }
        }
        public double YawDeg
        {
            get
            {
                return _yaw_deg;
            }
        }

        public Attitude(double roll, double pitch, double roll_acc, double pitch_acc, double yaw)
        {
            _pitch_deg = pitch;
            _roll_deg = roll;
            _pitch_acc_deg = pitch_acc;
            _roll_acc_deg = roll_acc;
            _yaw_deg = yaw;
        }
    }
}
