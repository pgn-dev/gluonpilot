using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Communication;

namespace Configuration
{
    public partial class OsdConfig : UserControl
    {
        private SerialCommunication _serial;
        private bool is_updating = false;
        private int last_bitmask = -1;

        public OsdConfig()
        {
            InitializeComponent();
        }

        public OsdConfig(SerialCommunication serial)
            :this()
        {
            Connect(serial);
        }

        private bool connected = false;
        public void Connect(SerialCommunication serial)
        {
            _serial = serial;
            _serial.AllConfigCommunicationReceived += new SerialCommunication.ReceiveAllConfigCommunicationFrame(_serial_AllConfigCommunicationReceived);
            connected = true;
        }

        public void Disconnect()
        {
            if (connected)
                _serial.AllConfigCommunicationReceived += new SerialCommunication.ReceiveAllConfigCommunicationFrame(_serial_AllConfigCommunicationReceived);
            _serial = null;
            connected = false;
        }


        private void UncheckAll()
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                    checkedListBox.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void Check(string s)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                if (checkedListBox.Items[i].ToString().Contains(s))
                {
                    checkedListBox.SetItemCheckState(i, CheckState.Checked);
                }
            }
        }

        
        void _serial_AllConfigCommunicationReceived(Communication.Frames.Configuration.AllConfig config)
        {
            MethodInvoker m = delegate()
                {
                    is_updating = true;

                    UncheckAll();
                    if ((config.osd_bitmask & 4) != 0)
                        Check("horizon");
                    if ((config.osd_bitmask & 8192) != 0)
                        Check("Voltage battery 1");
                    if ((config.osd_bitmask & 16384) != 0)
                        Check("Voltage battery 2");
                    if ((config.osd_bitmask & 16) != 0)
                        Check("Current battery 1");
                    if ((config.osd_bitmask & 256) != 0)
                        Check("mAh");
                    if ((config.osd_bitmask & 512) != 0)
                        Check("Autopilot mode");
                    if ((config.osd_bitmask & 128) != 0)
                        Check("GPS");
                    if ((config.osd_bitmask & 2) != 0)
                        Check("Home arrow");
                    if ((config.osd_bitmask & 32) != 0)
                        Check("Distance");
                    if ((config.osd_bitmask & 1024) != 0)
                        Check("RC-receiver");
                    if ((config.osd_bitmask & 32) != 0)
                        Check("Distance to home");
                    if ((config.osd_bitmask & 4096) != 0)
                        Check("Vario");
                    if ((config.osd_bitmask & 64) != 0)
                        Check("time");
                    if ((config.osd_bitmask & 2048) != 0)
                        Check("Speed");
                    if ((config.osd_bitmask & 1) != 0)
                        Check("Altitude");
                    if ((config.osd_bitmask & 32768) != 0)
                        Check("block name");

                    last_bitmask = config.osd_bitmask;

                    try
                    {
                        _nud_rssi_low.Value = (decimal)config.osd_voltage_low;
                        _nud_rssi_high.Value = (decimal)config.osd_voltage_high;
                    }
                    catch (Exception ex)
                    {
                    }

                    if (config.osd_RssiMode >= 0 && config.osd_RssiMode < _cbRssi.Items.Count) 
                        _cbRssi.SelectedIndex = config.osd_RssiMode;

                    is_updating = false;
                };

            try
            {
                BeginInvoke(m);
            }
            catch
            {
            } 
        }

        private int BuildBitmask()
        {
            int bitmask = 0;
            foreach (object o in checkedListBox.CheckedItems)
            {
                if (o.ToString() == "Artificial horizon")
                    bitmask += 4;
                else if (o.ToString() == "Voltage battery 1")
                    bitmask += 8192;
                else if (o.ToString() == "Voltage battery 2")
                    bitmask += 16384;
                else if (o.ToString() == "Current battery 1")
                    bitmask += 16;
                else if (o.ToString() == "mAh battery 1")
                    bitmask += 256;
                else if (o.ToString() == "Autopilot mode")
                    bitmask += 512;
                else if (o.ToString().Contains("GPS"))
                    bitmask += 128;
                else if (o.ToString().Contains("Home arrow"))
                    bitmask += 2;
                else if (o.ToString().Contains("Distance"))
                    bitmask += 32;
                else if (o.ToString().Contains("RC-receiver"))
                    bitmask += 1024;
                else if (o.ToString().Contains("Vario"))
                    bitmask += 4096;
                else if (o.ToString().Contains("time"))
                    bitmask += 64;
                else if (o.ToString().Contains("Speed"))
                    bitmask += 2048;
                else if (o.ToString().Contains("Altitude"))
                    bitmask += 1;
                else if (o.ToString().Contains("block name"))
                    bitmask += 32768;
            }
            return bitmask;
        }

        private void updateOsd()
        {
            if (is_updating)
                return;

            if (_btnLive.Checked)
            {

                int bitmask = BuildBitmask();

                if (last_bitmask != bitmask)
                    _serial.SendOsdConfiguration(bitmask, _cbRssi.SelectedIndex, (double)_nud_rssi_low.Value, (double)_nud_rssi_high.Value);

                last_bitmask = bitmask;
            }
        }


        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
        }

        private void checkedListBox_Click(object sender, EventArgs e)
        {
           
        }

        private void checkedListBox_MouseUp(object sender, MouseEventArgs e)
        {
             updateOsd();
        }

        private void _btnLive_Click(object sender, EventArgs e)
        {
            _btnLive.Checked = !_btnLive.Checked;
        }

        private void _btnWriteOsdSettings_Click(object sender, EventArgs e)
        {
            int bitmask = BuildBitmask();
            _serial.SendOsdConfiguration(bitmask, _cbRssi.SelectedIndex, (double)_nud_rssi_low.Value, (double)_nud_rssi_high.Value);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            _serial.SendFlashConfiguration();
        }

        private void _btnReadSettings_Click(object sender, EventArgs e)
        {
            ReadConfiguration rc = new ReadConfiguration(_serial);
            rc.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void _cbRssi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cbRssi.SelectedIndex == 1)
                _panelVoltage.Enabled = true;
            else
                _panelVoltage.Enabled = false;

            updateOsd();
        }

        private void _nud_rssi_low_ValueChanged(object sender, EventArgs e)
        {
            updateOsd();
        }

        private void _nud_rssi_high_ValueChanged(object sender, EventArgs e)
        {
            updateOsd();
        } 
    }
}
