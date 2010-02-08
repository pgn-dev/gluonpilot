using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Incoming
{
    class Sensors
    {
        private float _pressure;
        private float _temp_c;
        private float _acc_x_g;
        private float _acc_y_g;
        private float _acc_z_g;
        private float _gyro_x_dgps;
        private float _gyro_y_dgps;
        private float _gyro_z_dgps;

        public float Pressure
        {
            get { return _pressure; }
        }
        public float Temp
        {
            get { return _temp_c; }
        }
        public float AccX
        {
            get { return _acc_x_g; }
        }
        public float AccY
        {
            get { return _acc_y_g; }
        }
        public float AccZ
        {
            get { return _acc_z_g; }
        }
        public float GyroX
        {
            get { return _gyro_x_dgps; }
        }
        public float GyroY
        {
            get { return _gyro_y_dgps; }
        }
        public float GyroZ
        {
            get { return _gyro_z_dgps; }
        }



        public Sensors(
            float pressure,
            float temp_c,
            float acc_x_g,
            float acc_y_g,
            float acc_z_g,
            float gyro_x_dgps,
            float gyro_y_dgps,
            float gyro_z_dgps)
        {
            _pressure = pressure;
            _temp_c = temp_c;
            _acc_x_g = acc_x_g;
            _acc_y_g = acc_y_g;
            _acc_z_g = acc_z_g;
            _gyro_x_dgps = gyro_x_dgps;
            _gyro_y_dgps = gyro_y_dgps;
            _gyro_z_dgps = gyro_z_dgps;
        }
    }
}
