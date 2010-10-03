using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class NavigationInstruction
    {
        public double x, y;
        public int a, b;
        public int line;

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
            UNTIL_SM = 16
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
            this.x = x;
            this.y = y;
            this.a = a;
            this.b = b;
        }

        public NavigationInstruction(NavigationInstruction ni)
        {
            this.line = ni.line;
            this.opcode = ni.opcode;
            this.x = ni.x;
            this.y = ni.y;
            this.a = ni.a;
            this.b = ni.b;
        }

        private double RAD2DEG(double x)
        {
            return x / 3.14159 * 180.0;
        }
        private double DEG2RAD(double x)
        {
            return x / 180.0 * 3.14159;
        }

        public string ToString()
        {
            string s = "";
            switch (opcode)
            {
            case navigation_command.EMPTY:
                s += "EMPTY";
                break;
            case navigation_command.CLIMB:
                s += "CLIMB(" + x + "m)";
                break;

            case navigation_command.FROM_TO_REL:   // x, y, height
                s += "FROM_TO_RELATIVE(lat: " + x + "m, lon: " + y + "m, height: " + a + "m)";
                break;
            case navigation_command.FROM_TO_ABS:
                s += "FROM_TO_ABSOLUTE(lat: " + RAD2DEG(x).ToString("F5") + "°, lon: " + RAD2DEG(y).ToString("F5") + "°, height: " + a + "m)";
                break;
            case navigation_command.FLY_TO_REL:
                s += "FLY_TO_RELATIVE(lat: " + x + "m, lon: " + y + "m, height: " + a + "m)";
                break;
            case navigation_command.FLY_TO_ABS:    // x, y, height
                s += "FLY_TO_ABSOLUTE(lat: " + RAD2DEG(x).ToString("F5") + "°, lon: " + RAD2DEG(y).ToString("F5") + "°, height: " + a + "m)";
                break;
            case navigation_command.GOTO:	   // line number
                s += "GOTO(" + (a+1) + ")";
                break;
            case navigation_command.CIRCLE_ABS:    // x, y, radius, height <-- should be inside a while  12 B
                s += "CIRCLE_ABSOLUTE(lat: " + RAD2DEG(x).ToString("F5") + "°, lon: " + RAD2DEG(y).ToString("F5") + "°, radius: " + a + "m, height: " + b + "m)";
                break;
            case navigation_command.CIRCLE_REL:
                s += "CIRCLE_RELATIVE(lat: " + x + "m, lon: " + y + "m, radius: " + a + "m, height: " + b + " m)";
                break;
            case navigation_command.UNTIL_SM:
                s += "UNTIL(" + GetVariableText(a) + " < " + x + ")";
                break;
            case navigation_command.UNTIL_GR:
                s += "UNTIL(" + GetVariableText(a) + " > " + x + ")";
                break;
            default:
                s += "UNKNOWN/UNSUPPORTED (" + (int)opcode + " : " +  x + ", " + y + ", " + a + ", " + b + ")";
                break;
            }

            return s;
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

            else
                return "?";
        }
    }
}
