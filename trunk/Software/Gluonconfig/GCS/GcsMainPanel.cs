using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kml;
using Communication;

namespace GCS
{
    public partial class GcsMainPanel : UserControl
    {
        private Kml.KmlListener kml_list;

        private SerialCommunication _serial;


        public GcsMainPanel()
        {
            InitializeComponent();
            Disconnnect();
        }

        public void Connect(SerialCommunication serial)
        {
            _serial = serial;
            _btn_ge_server.Enabled = true;
            _graphControl.SetSerial(serial);
        }

        public void Disconnnect()
        {
            _serial = null;
            _btn_ge_server.Enabled = false;
            _btn_goto_ge.Enabled = false;
            _graphControl.Stop();
        }

        private void _btn_ge_server_Click(object sender, EventArgs e)
        {
            if (_btn_ge_server.Checked == false)
            {
                try
                {
                    kml_list = new Kml.KmlListener(_serial);
                    kml_list.Start();
                    _btn_goto_ge.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
                _btn_ge_server.Checked = true;
            }
            else
            {
                kml_list.Stop();
                _btn_ge_server.Checked = false;
                _btn_goto_ge.Enabled = false;
            }
            
        }

        private void _btn_goto_ge_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\networklink.kml");
        }
    }
}
