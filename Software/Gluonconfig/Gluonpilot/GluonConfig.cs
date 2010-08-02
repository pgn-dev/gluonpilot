using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Communication;
using System.IO;
using System.Xml.Serialization;

namespace Gluonpilot
{
    public partial class GluonConfig : Form
    {
        private int logging_height;
        private SerialCommunication_CSV _serial;
        
        public GluonConfig()
        {
            InitializeComponent();
            logging_height = splitContainer1.Panel2.Height;
        }


        private void _btn_showlogging_Click(object sender, EventArgs e)
        {
            _btn_showlogging.Checked = !_btn_showlogging.Checked;

            if (_btn_showlogging.Checked)
            {
                this.Height += logging_height + splitContainer1.SplitterWidth;
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                this.Height -= logging_height + splitContainer1.SplitterWidth;
                splitContainer1.Panel2Collapsed = true;
            }
            splitContainer1.Refresh();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!splitContainer1.Panel2Collapsed)
                logging_height = splitContainer1.Panel2.Height;
        }

        private void GluonConfig_Resize(object sender, EventArgs e)
        {

        }

        private void _btn_connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_btn_connect.Checked)
                {
                    _serial.Close();
                    configurationControl.Disconnect();
                    datalogging.Disconnect();
                    navigationListView1.Disconnect();
                    _gcsMainPanel.Disconnnect();

                    _btn_connect.Checked = false;

                }
                else
                {
                    ConnectDialog cd = new ConnectDialog();
                    cd.ShowDialog(this);

                    _serial = new SerialCommunication_CSV();
                    _serial.Open(cd.SelectedPort(), cd.SelectedBaudrate());

                    configurationControl.Connect(_serial);
                    datalogging.Connect(_serial);
                    navigationListView1.Connect(_serial);
                    _gcsMainPanel.Connect(_serial);

                    _btn_connect.Checked = true;

                    _serial.CommunicationReceived += new SerialCommunication_CSV.ReceiveCommunication(ReceiveCommunication);
                    _serial.NonParsedCommunicationReceived += new SerialCommunication.ReceiveNonParsedCommunication(ReceiveNonParsedCommunication);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private delegate void UpdateTextBox(string line);
        private void ReceiveCommunication(string line)
        {
            if (!this._cb_hide_parsed.Checked)
                this.BeginInvoke(new UpdateTextBox(UpdateText), new object[] { line });
        }
        private void ReceiveNonParsedCommunication(string line)
        {
            if (this._cb_hide_parsed.Checked)
                this.BeginInvoke(new UpdateTextBox(UpdateText), new object[] { line });
        }
        private void UpdateText(string line)
        {
            if (_cb_print_timestamp.Checked)
                _tb_logging.AppendText("[" + DateTime.Now.ToString("hh:mm:ss.ff") + "]  ");
            _tb_logging.AppendText(line + "\r\n");
            _tb_logging.ScrollToCaret();
        }
        
    }
}
