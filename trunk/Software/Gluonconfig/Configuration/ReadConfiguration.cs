using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Communication;

namespace Configuration
{
    public partial class ReadConfiguration : Form
    {
        private int secondswaiting = 0;
        private int attempt = 0;
        private SerialCommunication serial;

        public ReadConfiguration(SerialCommunication serial)
        {
            InitializeComponent();
            this.serial = serial; 
        }

        void serial_AllConfigCommunicationReceived(Communication.Frames.Configuration.AllConfig config)
        {
            MethodInvoker m = delegate()
            {
                timer.Stop();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            };
            
            try
            {
                BeginInvoke(m);
            }
            catch
            {
                ;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (secondswaiting % 4 == 0 && serial != null && serial.IsOpen)
            {
                serial.ReadAllConfig();
                attempt++;
                _lblProgress.Text = "Reading configuration (attempt " + attempt + ")";
                secondswaiting = 1;
            }

            if (_pb.Value == 100)
            {
                secondswaiting++;
                _pb.Value = 0;
            }
            _pb.Value = _pb.Value + 10;
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            timer.Stop();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void ReadConfiguration_Shown(object sender, EventArgs e)
        {

            if (serial == null || !serial.IsOpen)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            else
            {

                serial.AllConfigCommunicationReceived += new SerialCommunication.ReceiveAllConfigCommunicationFrame(serial_AllConfigCommunicationReceived);
                timer.Start();
            }
        }
    }
}
