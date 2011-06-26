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
using System.Diagnostics;

namespace Gluonpilot
{
    public partial class GluonConfig : Form
    {
        private DateTime connected;
        private int logging_height;
        private SerialCommunication_CSV _serial;
        
        public GluonConfig()
        {
            InitializeComponent();
            logging_height = splitContainer1.Panel2.Height;
            timer.Start();
            _tc_main.SelectedTab = _tc_main.TabPages["gcs"];
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
                    connected = DateTime.Now;
                    if (_serial != null)
                    {
                        string portname = _serial.PortName;
                        int baudrate = _serial.BaudRate;
                        _serial = new SerialCommunication_CSV();
                        _serial.Open(portname, baudrate);
                    }
                    else
                    {
                        ConnectDialog cd = new ConnectDialog();
                        cd.ShowDialog(this);
                        _serial = new SerialCommunication_CSV();
                        _serial.Open(cd.SelectedPort(), cd.SelectedBaudrate());
                    }

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

        private void _btn_reboot_Click(object sender, EventArgs e)
        {
            _serial.SendReboot();
        }

        private void _btn_firmware_upgrade_Click(object sender, EventArgs e)
        {
            bool connected = _btn_connect.Checked;

            if (MessageBox.Show(this, "Upgrading the firmware may enable features\r\nwhich are not legal in your country.\r\nYou are fully responsibel for your flights.\r\nAre you sure you wish to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;

            if (_serial == null)   // Ask COM port & baudrate if not known yet
            {
                ConnectDialog cd = new ConnectDialog();
                cd.ShowDialog(this);
                _serial = new SerialCommunication_CSV();
                _serial.Open(cd.SelectedPort(), cd.SelectedBaudrate());
            }

            FileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() != DialogResult.OK)
                return;

            if (connected)  // Close the current connection if it's open
                _btn_connect_Click(null, null);

            Process p = System.Diagnostics.Process.Start(Application.StartupPath + "\\ds30loader\\ds30LoaderConsole.exe", " -k=" + _serial.PortName + " -f=\"" + fd.FileName + "\"  -p -d=dsPIC33FJ256MC710 -r=" + _serial.BaudRate + " -q=0a;5a;5a;3b;31;31;32;33;0a -u=115200 -b=1200 -o");
            p.WaitForExit();

            if (connected)  // Reconnect if the state was connected
                _btn_connect_Click(null, null);

            if (p.ExitCode != -1)
                MessageBox.Show("New firmware has been written", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (MessageBox.Show("There has been an error!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    _btn_firmware_upgrade_Click(null, null);
            }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (_serial != null && _serial.IsOpen)
            {
                _tssl_downloadspeed.Text = _serial.ThroughputKbS() + " B/s";
                _tssl_time.Text = (DateTime.Now - connected).Minutes + ":" + (DateTime.Now - connected).Seconds;
            }
        }
    }
}
