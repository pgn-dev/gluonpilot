using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class Attitude
    {
        double _pitch_deg_sec;
        double _roll_deg_sec;
        double _yaw_deg_sec;
        double _pitch_acc_deg_sec;
        double _roll_acc_deg_sec;

        public double PitchAccDegSec
        {
            get
            {
                return _pitch_acc_deg_sec;
            }
        }
        public double RollAccDegSec
        {
            get
            {
                return _roll_acc_deg_sec;
            }
        }
        public double PitchDegSec
        {
            get { 
                return _pitch_deg_sec; 
            }
        }
        public double RollDegSec
        {
            get
            {
                return _roll_deg_sec;
            }
        }
        public double YawDegSec
        {
            get
            {
                return _yaw_deg_sec;
            }
        }

        public Attitude(double roll, double pitch, double roll_acc, double pitch_acc, double yaw)
        {
            _pitch_deg_sec = pitch;
            _roll_deg_sec = roll;
            _pitch_acc_deg_sec = pitch_acc;
            _roll_acc_deg_sec = roll_acc;
            _yaw_deg_sec = yaw;
        }
    }
}
