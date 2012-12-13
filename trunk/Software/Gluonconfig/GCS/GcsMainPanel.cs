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
        private LineItem _speedLine;
        private DateTime _beginDateTime;
        private int _timewindow = 180;

        public GcsMainPanel()
        {
            InitializeComponent();
            _btn_ge_server.Enabled = false;
            _btn_goto_ge.Enabled = false;
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

            _speedLine = _zgc_speed.GraphPane.AddCurve("Speed", new PointPairList(), Color.Blue, SymbolType.None);
            _zgc_speed.GraphPane.Title.IsVisible = false;
            _zgc_speed.GraphPane.YAxis.MajorGrid.IsVisible = true;
            _zgc_speed.GraphPane.XAxis.Title.IsVisible = false;
            _zgc_speed.AxisChange();
            _zgc_speed.GraphPane.Legend.IsVisible = false;
            _zgc_speed.GraphPane.IsFontsScaled = false;
            _zgc_speed.GraphPane.YAxis.Title.Text = "Speed [km/h]";
            _zgc_speed.GraphPane.XAxis.IsVisible = false;

            _beginDateTime = DateTime.Now;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

            if (_serial != null)
            {
                //_serial.AttitudeCommunicationReceived -= new SerialCommunication.ReceiveAttitudeCommunicationFrame(serial_AttitudeCommunicationReceived);
                //_serial.PressureTempCommunicationReceived -= new SerialCommunication.ReceivePressureTempCommunicationFrame(serial_PressureTempCommunicationReceived);
                //_serial.GpsBasicCommunicationReceived -= new SerialCommunication.ReceiveGpsBasicCommunicationFrame(serial_GpsBasicCommunicationReceived);
                //_serial.ControlInfoCommunicationReceived -= new SerialCommunication.ReceiveControlInfoCommunicationFrame(serial_ControlInfoCommunicationReceived);
            }
        }


        public void Connect(SerialCommunication serial)
        {
            _serial = serial;
            _btn_ge_server.Enabled = true;
            //_graphControl.SetSerial(serial);
            _serial.AttitudeCommunicationReceived += new SerialCommunication.ReceiveAttitudeCommunicationFrame(serial_AttitudeCommunicationReceived);
            _serial.PressureTempCommunicationReceived += new SerialCommunication.ReceivePressureTempCommunicationFrame(serial_PressureTempCommunicationReceived);
            _serial.GpsBasicCommunicationReceived += new SerialCommunication.ReceiveGpsBasicCommunicationFrame(serial_GpsBasicCommunicationReceived);
            _serial.ControlInfoCommunicationReceived += new SerialCommunication.ReceiveControlInfoCommunicationFrame(serial_ControlInfoCommunicationReceived);
            _graphControl.SetSerial(_serial);
        }


        public void Disconnnect()
        {
            //_serial = null;
            _btn_ge_server.Enabled = false;
            _btn_goto_ge.Enabled = false;
            //_graphControl.Stop();
            _serial.AttitudeCommunicationReceived -= new SerialCommunication.ReceiveAttitudeCommunicationFrame(serial_AttitudeCommunicationReceived);
            _serial.PressureTempCommunicationReceived -= new SerialCommunication.ReceivePressureTempCommunicationFrame(serial_PressureTempCommunicationReceived);
            _serial.GpsBasicCommunicationReceived -= new SerialCommunication.ReceiveGpsBasicCommunicationFrame(serial_GpsBasicCommunicationReceived);
            _serial.ControlInfoCommunicationReceived -= new SerialCommunication.ReceiveControlInfoCommunicationFrame(serial_ControlInfoCommunicationReceived);
        }


        void serial_ControlInfoCommunicationReceived(ControlInfo ci)
        {
            this.BeginInvoke(new D_ControlInfo(ContrlInfo), new object[] { ci });
        }
        private delegate void D_ControlInfo(ControlInfo ci);
        private void ContrlInfo(ControlInfo ci)
        {
            if (ci.FlightMode == ControlInfo.FlightModes.MANUAL)
            {
                _lbl_control_mode.Text = "Manual";
                _lbl_control_mode.BackColor = Color.Red;
            }
            else if (ci.FlightMode == ControlInfo.FlightModes.AUTOPILOT)
            {
                _lbl_control_mode.Text = "Autopilot";
                _lbl_control_mode.BackColor = Color.Green;
            }
            else if (ci.FlightMode == ControlInfo.FlightModes.STABILIZED)
            {
                _lbl_control_mode.Text = "Stabilization";
                _lbl_control_mode.BackColor = Color.Yellow;
            }

            double time = (DateTime.Now - _beginDateTime).TotalSeconds;
            _heightLine.AddPoint(new PointPair(time, ci.Altitude));
            Scale xScale = _zgc_height.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - _timewindow;
            }

            _zgc_height.AxisChange();
            _zgc_height.Invalidate(true);

            _dtb_altitude.DistanceM = ci.Altitude;
            _tb_navigationline.Text = ci.CurrentNavigationLine.ToString();

            _tb_battvoltage.Text = ci.Batt1Voltage.ToString();
        }


        void serial_GpsBasicCommunicationReceived(GpsBasic gpsbasic)
        {
            this.BeginInvoke(new D_UpdateGpsBasic(UpdateGpsBasic), new object[] { gpsbasic });
        }
        private delegate void D_UpdateGpsBasic(GpsBasic gb);
        private void UpdateGpsBasic(GpsBasic gb)
        {
            _stb_speed.SpeedMS = gb.Speed_ms;
            _tb_gps_sattellites.Text = gb.NumberOfSatellites.ToString();

            double time = (DateTime.Now - _beginDateTime).TotalSeconds;
            _speedLine.AddPoint(new PointPair(time, gb.Speed_ms*3.6));
            Scale xScale = _zgc_speed.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - _timewindow;
            }

            _zgc_speed.AxisChange();
            _zgc_speed.Invalidate(true);
        }




        void serial_PressureTempCommunicationReceived(PressureTemp info)
        {   
            this.BeginInvoke(new D_UpdateScp1000(UpdateScp1000), new object[] { info });
        }
        private delegate void D_UpdateScp1000(PressureTemp info);
        private void UpdateScp1000(PressureTemp info)
        {
            
        }

        void serial_AttitudeCommunicationReceived(Communication.Frames.Incoming.Attitude attitude)
        {
            MethodInvoker m = delegate()
            {
                artificialHorizon.pitch_angle = attitude.PitchDeg;
                artificialHorizon.roll_angle = -attitude.RollDeg;
                _lblPitch.Text = "Pitch: " + ((int)attitude.PitchDeg).ToString();
                _lblRoll.Text = "Roll: " + ((int)attitude.RollDeg).ToString();
            };

            try
            {
                BeginInvoke(m);
            }
            catch
            {
            } 
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
