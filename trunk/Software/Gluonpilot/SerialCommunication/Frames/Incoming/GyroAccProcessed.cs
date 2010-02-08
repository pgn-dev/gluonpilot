using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class GyroAccProcessed
    {
        private double _acc_x_g;
        private double _acc_y_g;
        private double _acc_z_g;
        private double _gyro_x_dgps;
        private double _gyro_y_dgps;
        private double _gyro_z_dgps;

        public double AccX
        {
            get { return _acc_x_g; }
        }
        public double AccY
        {
            get { return _acc_y_g; }
        }
        public double AccZ
        {
            get { return _acc_z_g; }
        }
        public double GyroX
        {
            get { return _gyro_x_dgps; }
        }
        public double GyroY
        {
            get { return _gyro_y_dgps; }
        }
        public double GyroZ
        {
            get { return _gyro_z_dgps; }
        }



        public GyroAccProcessed(
            double acc_x_g,
            double acc_y_g,
            double acc_z_g,
            double gyro_x_dgps,
            double gyro_y_dgps,
            double gyro_z_dgps)
        {
            _acc_x_g = acc_x_g;
            _acc_y_g = acc_y_g;
            _acc_z_g = acc_z_g;
            _gyro_x_dgps = gyro_x_dgps;
            _gyro_y_dgps = gyro_y_dgps;
            _gyro_z_dgps = gyro_z_dgps;
        }
    }
}
