using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class DatalogLine
    {
        public string[] Line;
        public string[] Header;

        public DatalogLine(string[] line, string[] header)
        {
            Line = line;
            Header = header;
        }
    }
}
