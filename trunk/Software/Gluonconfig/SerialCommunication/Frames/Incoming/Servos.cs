using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class Servos
    {
        public int Elevator;
        public int Aileron;
        public int Motor;

        public Servos(int e, int a, int m)
        {
            Elevator = e;
            Aileron = a;
            Motor = m;
        }
    }
}
