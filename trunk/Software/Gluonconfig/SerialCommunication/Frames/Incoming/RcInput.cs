using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class RcInput
    {
        private int[] _pwm;

        public int GetPwm(int i)
        {
            if (i > _pwm.Length)
                return 1500;
            else
                return _pwm[i-1];
        }

        public RcInput(int[] pwm)
        {
            _pwm = new int[pwm.Length];
            for (int i = 0; i < pwm.Length; i++)
            {
                _pwm[i] = pwm[i];
            }
        }
    }
}
