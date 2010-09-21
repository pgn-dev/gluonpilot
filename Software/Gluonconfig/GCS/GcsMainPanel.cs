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
using Communication.Frames.Incoming;

using ZedGraph;

namespace GCS
{
    public partial class GcsMainPanel : UserControl
    {
        private Kml.KmlListener kml_list;

        private SerialCommunication _serial;
        private LineItem _heightLine;
        private DateTime _beginDateTime;
        private int _timewindow = 180;

        public GcsMainPanel()
        {
            InitializeComponent();
            Disconnnect();
            artificialHorizon.BackColor = toolStripContainer1.ContentPanel.BackColor;
            _heightLine = _zgc_height.GraphPane.AddCurve("Height", new PointPairList(), Color.Blue, SymbolType.None);
            _zgc_height.GraphPane.Title.IsVisible = false;
            _zgc_height.GraphPane.YAxis.MajorGrid.IsVisible = true;
            _zgc_height.GraphPane.XAxis.Title.IsVisible = false;
            _zgc_height.AxisChange();
            _zgc_height.GraphPane.Legend.IsVisible = false;
            _zgc_height.GraphPane.IsFontsScaled = false;
            _zgc_height.GraphPane.YAxis.Title.Text = "Height [m]";
            _zgc_height.GraphPane.XAxis.IsVisible = false;
            _beginDateTime = DateTime.Now;
        }

        public void Connect(SerialCommunication serial)
        {
            _serial = serial;
            _btn_ge_server.Enabled = true;
            //_graphControl.SetSerial(serial);
            serial.AttitudeCommunicationReceived += new SerialCommunication.ReceiveAttitudeCommunicationFrame(serial_AttitudeCommunicationReceived);
            serial.PressureTempCommunicationReceived += new SerialCommunication.ReceivePressureTempCommunicationFrame(serial_PressureTempCommunicationReceived);
        }

        void serial_PressureTempCommunicationReceived(PressureTemp info)
        {   
            this.BeginInvoke(new D_UpdateScp1000(UpdateScp1000), new object[] { info });
        }
        private delegate void D_UpdateScp1000(PressureTemp info);
        private void UpdateScp1000(PressureTemp info)
        {
            double time = (DateTime.Now - _beginDateTime).TotalSeconds;
            _heightLine.AddPoint(new PointPair(time, info.Height));
            Scale xScale = _zgc_height.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - _timewindow;
            }

            _zgc_height.AxisChange();
            _zgc_height.Invalidate(true);
        }

        void serial_AttitudeCommunicationReceived(Communication.Frames.Incoming.Attitude attitude)
        {
            artificialHorizon.pitch_angle = attitude.PitchDeg;
            artificialHorizon.roll_angle = attitude.RollDeg;
        }

        public void Disconnnect()
        {
            _serial = null;
            _btn_ge_server.Enabled = false;
            _btn_goto_ge.Enabled = false;
            //_graphControl.Stop();
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
            Process.Start(Application.StartupPath + "\\networklink.kml");
        }
    }
}
