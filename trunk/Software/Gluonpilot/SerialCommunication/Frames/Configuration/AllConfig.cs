using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Configuration
{
    public class AllConfig
    {
        // sensors
        public int acc_x_neutral;
        public int acc_y_neutral;
        public int acc_z_neutral;
        public int gyro_x_neutral;
        public int gyro_y_neutral;
        public int gyro_z_neutral;

        // gps
        public int gps_initial_baudrate;
        public int gps_operational_baudrate;

        // channels 1 - 8
        public int channel_roll;
        public int channel_pitch;
        public int channel_yaw;
        public int channel_motor;
        public int channel_ap;

        // telemetry
        public int telemetry_gyroaccraw;
        public int telemetry_gyroaccproc;
        public int telemetry_ppm;
        public int telemetry_basicgps;
        public int telemetry_pressuretemp;

        // pid
        public double pid_pitch2elevator_p;
        public double pid_pitch2elevator_i;
        public double pid_pitch2elevator_d;
        public double pid_pitch2elevator_imin;
        public double pid_pitch2elevator_imax;
        public double pid_pitch2elevator_dmin;

        public double pid_roll2aileron_p;
        public double pid_roll2aileron_i;
        public double pid_roll2aileron_d;
        public double pid_roll2aileron_imin;
        public double pid_roll2aileron_imax;
        public double pid_roll2aileron_dmin;

        public double pid_heading2roll_p;
        public double pid_heading2roll_i;
        public double pid_heading2roll_d;
        public double pid_heading2roll_imin;
        public double pid_heading2roll_imax;
        public double pid_heading2roll_dmin;

        // servo
        public bool[] servo_reverse = new bool[8];
    }
}
