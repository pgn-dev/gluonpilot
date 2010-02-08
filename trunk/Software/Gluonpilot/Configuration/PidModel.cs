using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configuration
{
    [Serializable]
    public class PidModel
    {
        public double P;
        public double I;
        public double D;
        public double IMin;
        public double IMax;
        public double DMin;

        public PidModel(double P, double I, double D, double IMin, double IMax, double DMin)
        {
            this.P = P;
            this.I = I;
            this.D = D;
            this.IMax = IMax;
            this.IMin = IMin;
            this.DMin = DMin;
        }

        public PidModel()
        {
            this.P = 0;
            this.I = 0;
            this.D = 0;
            this.IMax = 9999;
            this.IMin = -9999;
            this.DMin = 0;
        }
    }
}
