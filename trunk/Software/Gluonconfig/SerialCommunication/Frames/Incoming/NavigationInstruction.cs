using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class NavigationInstruction
    {
        private float X, Y;
        public int a, b;
        public int line;

        public double x { get { return (double)X; } set { X = (float)value; } }
        public double y { get { return (double)Y; } set { Y = (float)value; } }

        public enum navigation_command
        {
            EMPTY = 0,
            CLIMB = 1,
            FROM_TO_REL = 2,   // x, y, height
            FROM_TO_ABS = 3,
            FLY_TO_REL = 4,
            FLY_TO_ABS = 5,    // x, y, height
            GOTO = 6,	   // line number
            CIRCLE_ABS = 7,    // x, y, radius, height <-- should be inside a while  12 B
            CIRCLE_REL = 8,
            IF_EQ = 9,     // x = c goto n
            IF_SM = 10,     // x < c goto n
            IF_GR = 11,     // x > c goto n
            IF_NE = 12,     // x > c goto n
            UNTIL_EQ = 13,
            UNTIL_NE = 14,
            UNTIL_GR = 15,
            UNTIL_SM = 16,
            SERVO_SET = 17,
            SERVO_TRIGGER = 18,
            BLOCK = 19,
            FLARE_TO_ABS = 20,
            FLARE_TO_REL = 21,
            GLIDE_TO_ABS = 22,
            GLIDE_TO_REL = 23,
            SET_LOITER_POSITION = 24,
            LOITER_CIRCLE = 25,
            CIRCLE_TO_ABS = 26,
            CIRCLE_TO_REL = 27,
            SET_BATTERY_ALARM = 28,
            CALL = 29,
            RETURN = 30,
            SERVO_TRIGGER_START = 31,
            SERVO_TRIGGER_STOP = 32,
            SET_FLIGHTPLAN_SWITCH = 33,
            SET_MAXIMUM_RANGE = 34
        };

        public navigation_command opcode;

        public NavigationInstruction()
        {
            this.opcode = navigation_command.EMPTY;
        }

        public NavigationInstruction(int line, navigation_command opcode, double x, double y, int a, int b)
        {
            this.line = line;
            this.opcode = opcode;
            this.X = (float)x;
            this.Y = (float)y;
            this.a = a;
            this.b = b;
        }

        public NavigationInstruction(NavigationInstruction ni)
        {
            this.line = ni.line;
            this.opcode = ni.opcode;
            this.X = ni.X;
            this.Y = ni.Y;
            this.a = ni.a;
            this.b = ni.b;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            NavigationInstruction p = obj as NavigationInstruction;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (X == p.X) && (Y == p.Y) && (a == p.a) && (b == p.b) && (opcode == p.opcode);  // line??
        }

        public static bool operator ==(NavigationInstruction a, NavigationInstruction b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            return a.Equals(b);
        }

        public static bool operator !=(NavigationInstruction a, NavigationInstruction b)
        {
            return !(a == b);
        }

        private double RAD2DEG(double x)
        {
            return x / 3.14159 * 180.0;
        }
        private double DEG2RAD(double x)
        {
            return x / 180.0 * 3.14159;
        }

        public override string ToString()
        {
            string s = "";
            switch (opcode)
            {
            case navigation_command.EMPTY:
                s += "Empty";
                break;
            case navigation_command.CLIMB:
                s += "Climb(" + x + "m)";
                break;
            case navigation_command.SET_LOITER_POSITION:
                s += "SetLoiterPosition()";
                break;
            case navigation_command.LOITER_CIRCLE:
                s += "LoiterCircle(radius: " + a +"m)";
                break;
            case navigation_command.FROM_TO_REL:   // x, y, height
                s += "FromTo[Relative](alt: " + a + "m, lat: " + X.ToString("F0") + "m, lon: " + Y.ToString("F0") + "m)";
                break;
            case navigation_command.FROM_TO_ABS:
                s += "FromTo[Absolute](alt: " + a + "m, lat: " + RAD2DEG(X).ToString("F5") + "°, lon: " + RAD2DEG(y).ToString("F5") + "°)";
                break;
            case navigation_command.FLY_TO_REL:
                s += "FlyTo[Relative](alt: " + a + "m, lat: " + X.ToString("F0") + "m, lon: " + Y.ToString("F0") + "m)";
                break;
            case navigation_command.FLY_TO_ABS:    // x, y, height
                s += "FlyTo[Absolute](alt: " + a + "m, lat: " + RAD2DEG(x).ToString("F5") + "°, lon: " + RAD2DEG(y).ToString("F5") + "°)";
                break;
            case navigation_command.CIRCLE_TO_REL:
                s += "CircleTo[Relative](alt: " + b + "m, lat: " + X.ToString("F0") + "m, lon: " + Y.ToString("F0") + "m)";
                break;
            case navigation_command.CIRCLE_TO_ABS:    // x, y, height
                s += "CircleTo[Absolute](alt: " + b + "m, lat: " + RAD2DEG(x).ToString("F5") + "°, lon: " + RAD2DEG(y).ToString("F5") + "°)";
                break;
            case navigation_command.GOTO:	   // line number
                s += "Goto(" + (a+1) + ")";
                break;
            case navigation_command.RETURN:	   // line number
                s += "Return";
                break;
            case navigation_command.CALL:	   // line number
                s += "Call(" + (a + 1) + ")";
                break;
            case navigation_command.CIRCLE_ABS:    // x, y, radius, height <-- should be inside a while  12 B
                s += "Circle[Absolute](radius: " + a + "m, alt: " + b + "m, lat: " + RAD2DEG(X).ToString("F5") + "°, lon: " + RAD2DEG(y).ToString("F5") + "°)";
                break;
            case navigation_command.CIRCLE_REL:
                s += "Circle[Relative](radius: " + a + "m, alt: " + b + "m, lat: " + X.ToString("F0") + "m, lon: " + y.ToString("F0") + "m)";
                break;
            case navigation_command.UNTIL_SM:
                s += "Until(" + GetVariableText(a) + " < " + X + ")";
                break;
            case navigation_command.UNTIL_GR:
                s += "Until(" + GetVariableText(a) + " > " + X + ")";
                break;
            case navigation_command.UNTIL_NE:
                s += "Until(" + GetVariableText(a) + " <> " + X + ")";
                break;
            case navigation_command.UNTIL_EQ:
                s += "Until(" + GetVariableText(a) + " = " + X + ")";
                break;
            case navigation_command.IF_SM:
                s += "If(" + GetVariableText(a) + " < " + X + ")";
                break;
            case navigation_command.IF_GR:
                s += "If(" + GetVariableText(a) + " > " + X + ")";
                break;
            case navigation_command.IF_NE:
                s += "If(" + GetVariableText(a) + " <> " + X + ")";
                break;
            case navigation_command.IF_EQ:
                s += "If(" + GetVariableText(a) + " = " + X + ")";
                break;
            case navigation_command.SERVO_SET:
                s += "ServoSet(channel: " + (a+1) + ", position: " + b + "us)";
                break;
            case navigation_command.SERVO_TRIGGER:
                s += "ServoTrigger(channel: " + (a + 1) + ", position: " + b + "us, hold: " + X + "s)";
                break;
            case navigation_command.BLOCK:
                s += "Block (" + GetStringArgument() + ")";
                break;
            case navigation_command.FLARE_TO_REL:   // x, y, height
                s += "FlareTo[Relative](alt: " + a + "m, throttle: " + b + "%, lat: " + X.ToString("F0") + "m, lon: " + Y.ToString("F0") + "m)";
                break;
            case navigation_command.FLARE_TO_ABS:
                s += "FlareTo[Absolute](alt: " + a + "m, throttle: " + b + "%, lat: " + RAD2DEG(X).ToString("F5") + "°, lon: " + RAD2DEG(Y).ToString("F5") + "°)";
                break;
            case navigation_command.GLIDE_TO_REL:   // x, y, height
                s += "GlideTo[Relative](alt: " + a + "m, throttle: " + b + "%, lat: " + X.ToString("F0") + "m, lon: " + Y.ToString("F0") + "m)";
                break;
            case navigation_command.GLIDE_TO_ABS:
                s += "GlideTo[Absolute](alt: " + a + "m, throttle: " + b + "%, lat: " + RAD2DEG(X).ToString("F5") + "°, lon: " + RAD2DEG(Y).ToString("F5") + "°)";
                break;
            case navigation_command.SET_BATTERY_ALARM:
                s += "SetBatteryAlarm(Warning < " + X + "V, Panic < " + Y + "V -> " + a + ")";
                break;
            case navigation_command.SET_FLIGHTPLAN_SWITCH:
                s += "SetFlightplanSwitch(Ch " + (a+1) + ", < 1.4ms -> " + b + ", < 1.6ms -> " + (int)x + ", > 1.6ms -> " + (int)y + ")";
                break;
            case navigation_command.SERVO_TRIGGER_STOP:
                s += "ServoTriggerStop()";
                break;
            case navigation_command.SERVO_TRIGGER_START:
                if (Math.Round(y) == 1)
                    s += "ServoTriggerStart(channel: " + (a + 1) + ", CHDK mode)";
                else
                    s += "ServoTriggerStart(channel: " + (a + 1) + ", position: " + b + "us, delay: " + x + "s)";
                break;
            case navigation_command.SET_MAXIMUM_RANGE:
                s += "SetMaximumRange(distance > " + (x) + "m -> " + a + ")";
                break;
            default:
                s += "Unknown/Unsupported (" + (int)opcode + " : " +  X + ", " + Y + ", " + a + ", " + b + ")";
                break;
            }

            return s;
        }

        public string GetStringArgument()
        {
            string s = "";
            s = s + Convert.ToChar((int)(a / 256));
            s = s + Convert.ToChar((int)(a % 256));
            s = s + Convert.ToChar((int)(b / 256));
            s = s + Convert.ToChar((int)(b % 256));
            int c = (int)Math.Round(X);
            int d = (int)Math.Round(Y);
            s = s + Convert.ToChar((int)(c / 256));
            s = s + Convert.ToChar((int)(c % 256));
            s = s + Convert.ToChar((int)(d / 256));
            s = s + Convert.ToChar((int)(d % 256));
            return s.TrimEnd(new char[] { '\n' });
        }

        public void StringToArgument(string s)
        {
            char[] c2 = s.ToCharArray();
            char[] c1 = new char[8] { '\n', '\n', '\n', '\n', '\n', '\n', '\n', '\n' };
            for (int i = 0; i < 8; i++)
                if (i < c2.Length)
                    c1[i] = c2[i];
            a = (int)c1[0];
            a *= 256;
            a += (int)c1[1];
            b = (int)c1[2];
            b *= 256;
            b += (int)c1[3];
            int d;
            d = (int)c1[4];
            d *= 256;
            d += (int)c1[5];
            x = d;
            d = (int)c1[6];
            d *= 256;
            d += (int)c1[7];
            y = d;
        }

        public string GetVariableText(int a)
        {
            if (a == 1)
                return "Height (m)";
            else if (a == 2)
                return "Speed (m/s)";
            else if (a == 3)
                return "Heading (deg)";
            else if (a == 4)
                return "Flight time (s)";
            else if (a == 5)
                return "Satellites in view";
            else if (a == 6)
                return "Home distance (m)";
            else if (a == 7)
                return "PPM link alive";
            else if (a == 8)
                return "Channel 1";
            else if (a == 9)
                return "Channel 2";
            else if (a == 10)
                return "Channel 3";
            else if (a == 11)
                return "Channel 4";
            else if (a == 12)
                return "Channel 5";
            else if (a == 13)
                return "Channel 6";
            else if (a == 14)
                return "Channel 7";
            else if (a == 15)
                return "Channel 8";
            else if (a == 16)
                return "Battery voltage (V)";
            else if (a == 17)
                return "Time in block (s)";
            else if (a == 18)
                return "Absolute altitude error [m]";
            else if (a == 19)
                return "Absolute heading error [°]";
            else if (a == 20)
                return "Absolute altitude && heading error [m*°]";
            else
                return "?";
        }

        public bool HasRelativeCoordinates()
        {
            return opcode == navigation_command.CIRCLE_REL ||
                   opcode == navigation_command.FLARE_TO_REL ||
                   opcode == navigation_command.GLIDE_TO_REL ||
                   opcode == navigation_command.FLY_TO_REL ||
                   opcode == navigation_command.FROM_TO_REL ||
                   opcode == navigation_command.CIRCLE_TO_REL;
        }

        public bool HasAbsoluteCoordinates()
        {
            return opcode == navigation_command.CIRCLE_ABS ||
                   opcode == navigation_command.FLARE_TO_ABS ||
                   opcode == navigation_command.GLIDE_TO_ABS ||
                   opcode == navigation_command.FLY_TO_ABS ||
                   opcode == navigation_command.FROM_TO_ABS ||
                   opcode == navigation_command.CIRCLE_TO_ABS;
        }

        public bool IsWaypoint()
        {
            return opcode == navigation_command.CIRCLE_REL ||
                   opcode == navigation_command.FLARE_TO_REL ||
                   opcode == navigation_command.GLIDE_TO_REL ||
                   opcode == navigation_command.FLY_TO_REL ||
                   opcode == navigation_command.FROM_TO_REL ||
                   opcode == navigation_command.CIRCLE_ABS ||
                   opcode == navigation_command.FLARE_TO_ABS ||
                   opcode == navigation_command.GLIDE_TO_ABS ||
                   opcode == navigation_command.FLY_TO_ABS ||
                   opcode == navigation_command.FROM_TO_ABS ||
                   opcode == navigation_command.CIRCLE_TO_ABS ||
                   opcode == navigation_command.CIRCLE_TO_REL;
        }
    }
}
