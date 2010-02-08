using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class GyroAccRaw
    {
        private double _acc_x_raw;
        private double _acc_y_raw;
        private double _acc_z_raw;
        private double _gyro_x_raw;
        private double _gyro_y_raw;
        private double _gyro_z_raw;

        public double AccXRaw
        {
            get { return _acc_x_raw; }
        }
        public double AccYRaw
        {
            get { return _acc_y_raw; }
        }
        public double AccZRaw
        {
            get { return _acc_z_raw; }
        }
        public double GyroXRaw
        {
            get { return _gyro_x_raw; }
        }
        public double GyroYRaw
        {
            get { return _gyro_y_raw; }
        }
        public double GyroZRaw
        {
            get { return _gyro_z_raw; }
        }



        public GyroAccRaw(
            double acc_x_raw,
            double acc_y_raw,
            double acc_z_raw,
            double gyro_x_raw,
            double gyro_y_raw,
            double gyro_z_raw)
        {
            _acc_x_raw = acc_x_raw;
            _acc_y_raw = acc_y_raw;
            _acc_z_raw = acc_z_raw;
            _gyro_x_raw = gyro_x_raw;
            _gyro_y_raw = gyro_y_raw;
            _gyro_z_raw = gyro_z_raw;
        }
    }
}
