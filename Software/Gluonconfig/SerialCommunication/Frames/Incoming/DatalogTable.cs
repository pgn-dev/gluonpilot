using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class DatalogTable
    {
        public int Index;
        public long Date;
        public long Time;
        public int StartPage;
        public int BootStatus;


        public DatalogTable(int index, long date, long time, int startpage, int bootstatus)
        {
            this.Index = index;
            this.Date = date;
            this.Time = time;
            this.StartPage = startpage;
            this.BootStatus = bootstatus;
        }
    }
}
