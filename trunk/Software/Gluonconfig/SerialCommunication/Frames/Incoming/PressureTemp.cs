using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class PressureTemp
    {
        double _temperature;
        double _pressure;

        public double Pressure
        {
            get { return _pressure; }
        }
        public double Temperature
        {
            get { return _temperature; }
        }
        public double Height
        {
            get { /*return 44330.0 * (1.0 - Math.Pow(_pressure / 101325.0, 0.19));*/
                return -Math.Log(_pressure / 101000) * (273 + _temperature) * 287.05 / 9.81;

            }
        }

        public PressureTemp(double temp, double pressure)
        {
            this._pressure = pressure;
            this._temperature = temp;
        }
    }
}
