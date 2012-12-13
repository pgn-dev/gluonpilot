using System;
using System.Collections.Generic;
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
        public int neutral_pitch;
        public int imu_rotated;

        // control
        public double control_max_roll;
        public double control_max_pitch;
        public double control_min_pitch;
        public int control_aileron_differential;
        public int control_mixing;
        public bool control_stabilization_with_altitude_hold;
        public double control_cruising_speed;
        public double control_waypoint_radius;
        public int control_altitude_mode;

        // gps
        public int gps_initial_baudrate;
        public int gps_operational_baudrate;
        public int gps_enable_waas;

        // channels 1 - 8
        public int channel_roll;
        public int channel_pitch;
        public int channel_yaw;
        public int channel_motor;
        public int channel_ap;
        public int rc_ppm;

        // telemetry
        public int telemetry_gyroaccraw;
        public int telemetry_gyroaccproc;
        public int telemetry_ppm;
        public int telemetry_basicgps;
        public int telemetry_pressuretemp;
        public int telemetry_attitude;
        public int telemetry_control;

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

        public double pid_altitude2pitch_p;
        public double pid_altitude2pitch_i;
        public double pid_altitude2pitch_d;
        public double pid_altitude2pitch_imin;
        public double pid_altitude2pitch_imax;
        public double pid_altitude2pitch_dmin;

        public bool auto_throttle_enabled;
        public int auto_throttle_min_pct;
        public int auto_throttle_max_pct;
        public int auto_throttle_cruise_pct;
        public int auto_throttle_p_gain_10;


        // servo
        public bool[] servo_reverse = new bool[8];

        public int[] servo_min = new int[6];
        public int[] servo_max = new int[6];
        public int[] servo_neutral = new int[6];
        public bool manual_trim;

        // osd
        public int osd_bitmask;
        public int osd_RssiMode;
        public double osd_voltage_low;
        public double osd_voltage_high;
    }
}
