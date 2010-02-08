using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Communication;

namespace Graph
{
    public partial class GraphForm : Form
    {
        private SerialCommunication _serial;

        public GraphForm(SerialCommunication serial)
        {
            _serial = serial;

            InitializeComponent();

            graph1.SetSerial(serial);
        }

        private void GraphForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            graph1.Stop();
        }
    }
}
