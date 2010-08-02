using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Communication;
using Communication.Frames.Configuration;
using Communication.Frames.Incoming;

using ZedGraph;


namespace Graph
{
    public partial class GraphControl : UserControl
    {
        private SerialCommunication _serial;
        private DateTime _beginDateTime;
        private int _timewindow = 30;
        private Dictionary<string, LineItem> _lineItems = new Dictionary<string, LineItem>();

        public GraphControl()
        {
            InitializeComponent();
            _zed_graph.GraphPane.IsFontsScaled = false;
        }

          
        public void SetSerial(SerialCommunication serial)
        {
            _serial = serial;
            _serial.PressureTempCommunicationReceived += new SerialCommunication.ReceivePressureTempCommunicationFrame(_serial_PressureTempCommunicationReceived);
            _serial.GyroAccRawCommunicationReceived += new SerialCommunication.ReceiveGyroAccRawCommunicationFrame(_serial_GyroAccRawCommunicationReceived);
            _serial.GyroAccProcCommunicationReceived += new SerialCommunication.ReceiveGyroAccProcCommunicationFrame(_serial_GyroAccProcCommunicationReceived);
            _serial.AttitudeCommunicationReceived += new SerialCommunication.ReceiveAttitudeCommunicationFrame(_serial_AttitudeCommunicationReceived);
        }

        public void Stop()
        {
            if (_serial != null)
            {
                _serial.PressureTempCommunicationReceived -= _serial_PressureTempCommunicationReceived;
                _serial.GyroAccRawCommunicationReceived -= _serial_GyroAccRawCommunicationReceived;
                _serial.GyroAccProcCommunicationReceived -= _serial_GyroAccProcCommunicationReceived;
                _serial.AttitudeCommunicationReceived -= _serial_AttitudeCommunicationReceived;
            }
        }



        private void _serial_GyroAccRawCommunicationReceived(GyroAccRaw ga)
        {
            this.BeginInvoke(new D_UpdateGyroAccRaw(UpdateGyroAccRaw), new object[] { ga });
        }
        private delegate void D_UpdateGyroAccRaw(GyroAccRaw ga);
        private void UpdateGyroAccRaw(GyroAccRaw ga)
        {
            double time = (DateTime.Now - _beginDateTime).TotalSeconds;
            if (_lineItems.ContainsKey("Raw acceleration X"))
                _lineItems["Raw acceleration X"].AddPoint(new PointPair(time, ga.AccXRaw));

            if (_lineItems.ContainsKey("Raw acceleration Y"))
                _lineItems["Raw acceleration Y"].AddPoint(new PointPair(time, ga.AccYRaw));
                
            if (_lineItems.ContainsKey("Raw acceleration Z"))
                _lineItems["Raw acceleration Z"].AddPoint(new PointPair(time, ga.AccZRaw));

            if (_lineItems.ContainsKey("Raw gyro X"))
                _lineItems["Raw gyro X"].AddPoint(new PointPair(time, ga.GyroXRaw));
            if (_lineItems.ContainsKey("Raw gyro Y"))
                _lineItems["Raw gyro Y"].AddPoint(new PointPair(time, ga.GyroYRaw));
            if (_lineItems.ContainsKey("Raw gyro Z"))
                _lineItems["Raw gyro Z"].AddPoint(new PointPair(time, ga.GyroZRaw));


            Scale xScale = _zed_graph.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - _timewindow;
            }
            _zed_graph.AxisChange();
            _zed_graph.Invalidate(true);
        }


        private void _serial_GyroAccProcCommunicationReceived(GyroAccProcessed ga)
        {
            this.BeginInvoke(new D_UpdateGyroAccProc(UpdateGyroAccProc), new object[] { ga });
        }
        private delegate void D_UpdateGyroAccProc(GyroAccProcessed ga);
        private void UpdateGyroAccProc(GyroAccProcessed ga)
        {
            double time = (DateTime.Now - _beginDateTime).TotalSeconds;

            if (_lineItems.ContainsKey("Rotationrate X"))
                _lineItems["Rotationrate X"].AddPoint(new PointPair(time, ga.GyroX));
            if (_lineItems.ContainsKey("Rotationrate Y"))
                _lineItems["Rotationrate Y"].AddPoint(new PointPair(time, ga.GyroY));
            if (_lineItems.ContainsKey("Rotationrate Z"))
                _lineItems["Rotationrate Z"].AddPoint(new PointPair(time, ga.GyroZ));
            if (_lineItems.ContainsKey("Acceleration X"))
                _lineItems["Acceleration X"].AddPoint(new PointPair(time, ga.AccX));
            if (_lineItems.ContainsKey("Acceleration Y"))
                _lineItems["Acceleration Y"].AddPoint(new PointPair(time, ga.AccY));
            if (_lineItems.ContainsKey("Acceleration Z"))
                _lineItems["Acceleration Z"].AddPoint(new PointPair(time, ga.AccZ));

            Scale xScale = _zed_graph.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - _timewindow;
            }
            _zed_graph.AxisChange();
            _zed_graph.Invalidate(true);
        }


        void _serial_AttitudeCommunicationReceived(Attitude attitude)
        {
            this.BeginInvoke(new D_UpdateAttitude(UpdateAttitude), new object[] { attitude });
        }
        private delegate void D_UpdateAttitude(Attitude attitude);
        private void UpdateAttitude(Attitude attitude)
        {
            double time = (DateTime.Now - _beginDateTime).TotalSeconds;
            if (_lineItems.ContainsKey("Roll"))
                _lineItems["Roll"].AddPoint(new PointPair(time, attitude.RollDeg));

            if (_lineItems.ContainsKey("Pitch"))
                _lineItems["Pitch"].AddPoint(new PointPair(time, attitude.PitchDeg));
            if (_lineItems.ContainsKey("Roll acc"))
                _lineItems["Roll acc"].AddPoint(new PointPair(time, attitude.RollAccDeg));
            if (_lineItems.ContainsKey("Pitch acc"))
                _lineItems["Pitch acc"].AddPoint(new PointPair(time, attitude.PitchAccDeg));
            if (_lineItems.ContainsKey("Yaw"))
                _lineItems["Yaw"].AddPoint(new PointPair(time, attitude.YawDeg));


            Scale xScale = _zed_graph.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - _timewindow;
            }
            _zed_graph.AxisChange();
            _zed_graph.Invalidate(true);
        }






        private void _serial_PressureTempCommunicationReceived(PressureTemp info)
        {
            
            this.BeginInvoke(new D_UpdateScp1000(UpdateScp1000), new object[] { info });
        }
        private delegate void D_UpdateScp1000(PressureTemp pt);
        private void UpdateScp1000(PressureTemp pt)
        {
            double time = (DateTime.Now - _beginDateTime).TotalSeconds;
            if (_lineItems.ContainsKey("Height (SCP1000)"))
                _lineItems["Height (SCP1000)"].AddPoint(new PointPair(time, pt.Height));

            if (_lineItems.ContainsKey("Temperature (SCP1000)"))
                _lineItems["Temperature (SCP1000)"].AddPoint(new PointPair(time, pt.Temperature));


            Scale xScale = _zed_graph.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - _timewindow;
            }
            _zed_graph.AxisChange();
            _zed_graph.Invalidate(true);
        }

        private void Graph_Load(object sender, EventArgs e)
        {
            _beginDateTime = DateTime.Now;

            GraphPane myPane = _zed_graph.GraphPane;

            // Set the Titles

            myPane.Title.IsVisible = false;//.Text = "Gluonpilot variables";
            myPane.YAxis.Title.IsVisible = false; //.Text = "Time";

            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.Title.IsVisible = false;

            _zed_graph.AxisChange();
        }

        private void _nud_timewindow_ValueChanged(object sender, EventArgs e)
        {
            _timewindow = (int)_nud_timewindow.Value;
        }

        private Color RandomColor()
        {
            Random r = new Random();
            return System.Drawing.Color.FromArgb(
                r.Next(256),
                r.Next(256),
                r.Next(256)
            );
        }

        private void btn_collapse_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        }

        private void _clb_variables_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Object o in _clb_variables.Items)
            {
                if (_clb_variables.CheckedItems.Contains(o))
                {
                    if (!_lineItems.ContainsKey(o.ToString()))
                    {
                        // checked and not in graph list -> add
                        _lineItems.Add(o.ToString(), _zed_graph.GraphPane.AddCurve(o.ToString(),
                            new PointPairList(), RandomColor(), SymbolType.None));
                    }
                }
                else
                {
                    if (_lineItems.ContainsKey(o.ToString()))
                    {
                        // not checheckt but in graph list -> delete
                        _zed_graph.GraphPane.CurveList.Remove(_lineItems[o.ToString()]);
                        _lineItems.Remove(o.ToString());
                    }
                }
            }
        }

        private void _zed_graph_Resize(object sender, EventArgs e)
        {

        }
    }
}
