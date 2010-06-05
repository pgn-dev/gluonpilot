/*!
 *   ConfigurationFrame.c
 *   Manages the User Control for changing gluonpilot configuration.
 *   
 *   ConfigurationFrame <---------> ConfigurationModel <---------> AllConfig
 *                      < ReadModel()        model.ToAllConfig() >
 *                                >                    < model.SetFromAllConfig
 *
 *   @author  Tom Pycke
 */

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
using Configuration;
using System.IO;

namespace Gluonpilot
{
    public partial class ConfigurationFrame : UserControl
    {
        private SerialCommunication _serial;

        private ConfigurationModel _model = new ConfigurationModel();

        public ConfigurationFrame(ConfigurationModel model)
        {
            InitializeComponent();
            _model = model;
        }

        public ConfigurationFrame()
        {
            _model = new ConfigurationModel();
            InitializeComponent();

        }


        public ConfigurationModel Model
        {
            get
            {
                _tbHeight.Focus(); // hack to trigger is_changed on other controls.
                return _model;
            }
            set
            {
                _model = value;
                ReadModel();
            }
        }

        /*!
         *    Reads _model and maps it's value to the Form's components
         */
        private void ReadModel()
        {
            _tbAccXNeutral.Text = _model.NeutralAccX.ToString();
            _tbAccYNeutral.Text = _model.NeutralAccY.ToString();
            _tbAccZNeutral.Text = _model.NeutralAccZ.ToString();
            _tbGyroXNeutral.Text = _model.NeutralGyroX.ToString();
            _tbGyroYNeutral.Text = _model.NeutralGyroY.ToString();
            _tbGyroZNeutral.Text = _model.NeutralGyroZ.ToString();

            _nud_gpsbasic_telemetry.Value = _model.TelemetryGpsBasic;
            _nud_gyroaccraw_telemetry.Value = _model.TelemetryGyroAccRaw;
            _nud_gyroaccproc_telemetry.Value = _model.TelemetryGyroAccProc;
            _nud_ppm_telemetry.Value = _model.TelemetryPpm;
            _nud_pressuretemp_telemetry.Value = _model.TelemetryPressureTemp;

            _tb_initial_baudrate.Text = _model.GpsInitialBaudrate.ToString();
            _tb_operational_baudrate.Text = _model.GpsOperationalBaudrate.ToString();

            _pid_pitch_to_elevator.SetModel(_model.Pitch2ElevatorPidModel);
            _pid_roll_to_aileron.SetModel(_model.Roll2AileronPidModel);
            _pid_heading_to_roll.SetModel(_model.Heading2RollPidModel);

            _cb_reverse_servo1.Checked = _model.ReverseServo1;
            _cb_reverse_servo2.Checked = _model.ReverseServo2;
            _cb_reverse_servo3.Checked = _model.ReverseServo3;
            _cb_reverse_servo4.Checked = _model.ReverseServo4;
            _cb_reverse_servo5.Checked = _model.ReverseServo5;
            _cb_reverse_servo6.Checked = _model.ReverseServo6;

            _rbPpm.Checked = _model.RcTransmitterFromPpm == 1? true : false;
            _rbPwm.Checked = _model.RcTransmitterFromPpm == 0? true : false;

            _nud_control_pitch_max.Value = (int)_model.ControlMaxPitch;
            _nud_control_roll_max.Value = (int)_model.ControlMaxRoll;
            _cbControlMix.SelectedIndex = _model.ControlMixing;

            switch (_model.ChannelRoll)
            {
                case 1: _rbRollCh1.Checked = true; break;
                case 2: _rbRollCh2.Checked = true; break;
                case 3: _rbRollCh3.Checked = true; break;
                case 4: _rbRollCh4.Checked = true; break;
                case 5: _rbRollCh5.Checked = true; break;
                case 6: _rbRollCh6.Checked = true; break;
                case 7: _rbRollCh7.Checked = true; break;
            }
            switch (_model.ChannelPitch)
            {
                case 1: _rbPitchCh1.Checked = true; break;
                case 2: _rbPitchCh2.Checked = true; break;
                case 3: _rbPitchCh3.Checked = true; break;
                case 4: _rbPitchCh4.Checked = true; break;
                case 5: _rbPitchCh5.Checked = true; break;
                case 6: _rbPitchCh6.Checked = true; break;
                case 7: _rbPitchCh7.Checked = true; break;
            }
            switch (_model.ChannelYaw)
            {
                case 1: _rbYawCh1.Checked = true; break;
                case 2: _rbYawCh2.Checked = true; break;
                case 3: _rbYawCh3.Checked = true; break;
                case 4: _rbYawCh4.Checked = true; break;
                case 5: _rbYawCh5.Checked = true; break;
                case 6: _rbYawCh6.Checked = true; break;
                case 7: _rbYawCh7.Checked = true; break;
            }
            switch (_model.ChannelMotor)
            {
                case 1: _rbMotorCh1.Checked = true; break;
                case 2: _rbMotorCh2.Checked = true; break;
                case 3: _rbMotorCh3.Checked = true; break;
                case 4: _rbMotorCh4.Checked = true; break;
                case 5: _rbMotorCh5.Checked = true; break;
                case 6: _rbMotorCh6.Checked = true; break;
                case 7: _rbMotorCh7.Checked = true; break;
            }
            switch (_model.ChannelAp)
            {
                case 1: _rbApCh1.Checked = true; break;
                case 2: _rbApCh2.Checked = true; break;
                case 3: _rbApCh3.Checked = true; break;
                case 4: _rbApCh4.Checked = true; break;
                case 5: _rbApCh5.Checked = true; break;
                case 6: _rbApCh6.Checked = true; break;
                case 7: _rbApCh7.Checked = true; break;
            }

            if (_model.ServoMin != null) // EXTREMELY ANNOYING BUG
            {
                _tb_servo1_min.Text = _model.ServoMin[0].ToString();
                _tb_servo2_min.Text = _model.ServoMin[1].ToString();
                _tb_servo3_min.Text = _model.ServoMin[2].ToString();
                _tb_servo4_min.Text = _model.ServoMin[3].ToString();
                _tb_servo5_min.Text = _model.ServoMin[4].ToString();
                _tb_servo6_min.Text = _model.ServoMin[5].ToString();
                _tb_servo1_max.Text = _model.ServoMax[0].ToString();
                _tb_servo2_max.Text = _model.ServoMax[1].ToString();
                _tb_servo3_max.Text = _model.ServoMax[2].ToString();
                _tb_servo4_max.Text = _model.ServoMax[3].ToString();
                _tb_servo5_max.Text = _model.ServoMax[4].ToString();
                _tb_servo6_max.Text = _model.ServoMax[5].ToString();
                _tb_servo1_neutral.Text = _model.ServoNeutral[0].ToString();
                _tb_servo2_neutral.Text = _model.ServoNeutral[1].ToString();
                _tb_servo3_neutral.Text = _model.ServoNeutral[2].ToString();
                _tb_servo4_neutral.Text = _model.ServoNeutral[3].ToString();
                _tb_servo5_neutral.Text = _model.ServoNeutral[4].ToString();
                _tb_servo6_neutral.Text = _model.ServoNeutral[5].ToString();
            }
        }

        /*!
         *    Use serial as SerialCommunication and register our methods at the events
         */
        public void Connect(SerialCommunication serial)
        {
            _serial = serial;
            _serial.GyroAccRawCommunicationReceived += new SerialCommunication_CSV.ReceiveGyroAccRawCommunicationFrame(ReceiveGyroAccRaw);
            _serial.GyroAccProcCommunicationReceived += new SerialCommunication_CSV.ReceiveGyroAccProcCommunicationFrame(ReceiveGyroAccProc);
            _serial.PressureTempCommunicationReceived += new SerialCommunication_CSV.ReceivePressureTempCommunicationFrame(ReceivePressureTemp);
            _serial.AllConfigCommunicationReceived += new SerialCommunication_CSV.ReceiveAllConfigCommunicationFrame(ReceiveAllConfig);
            _serial.RcInputCommunicationReceived += new SerialCommunication_CSV.ReceiveRcInputCommunicationFrame(ReceiveRcInput);
            _serial.GpsBasicCommunicationReceived += new SerialCommunication.ReceiveGpsBasicCommunicationFrame(ReceiveGpsBasic);
            _serial.DatalogTableCommunicationReceived += new SerialCommunication.ReceiveDatalogTableCommunicationFrame(ReceiveDatalogTable);
            _serial.DatalogLineCommunicationReceived += new SerialCommunication.ReceiveDatalogLineCommunicationFrame(ReceiveDatalogLine);
        }


        private void ReceiveAllConfig(AllConfig ac)
        {
            this.BeginInvoke(new D_ReceiveAllConfig(AllConfig), new object[] { ac });
        }
        private delegate void D_ReceiveAllConfig(AllConfig ac);
        private void AllConfig(AllConfig ac)
        {
            _model.SetFromAllConfig(ac);
            ReadModel();
        }


#region Sensors tab page

        private void ReceiveGyroAccRaw(GyroAccRaw ga)
        {
            this.BeginInvoke(new D_UpdateGyroAccRaw(UpdateGyroAccRaw), new object[] { ga });
        }
        private delegate void D_UpdateGyroAccRaw(GyroAccRaw ga);
        private void UpdateGyroAccRaw(GyroAccRaw ga)
        {
            _tbAccXRaw.Text = ga.AccXRaw.ToString();
            _tbAccYRaw.Text = ga.AccYRaw.ToString();
            _tbAccZRaw.Text = ga.AccZRaw.ToString();

            _tbGyroXRaw.Text = ga.GyroXRaw.ToString();
            _tbGyroYRaw.Text = ga.GyroYRaw.ToString();
            _tbGyroZRaw.Text = ga.GyroZRaw.ToString();
        }

        private void ReceiveGyroAccProc(GyroAccProcessed ga)
        {
            this.BeginInvoke(new D_UpdateGyroAccProc(UpdateGyroAccProc), new object[] { ga });
        }
        private delegate void D_UpdateGyroAccProc(GyroAccProcessed ga);
        private void UpdateGyroAccProc(GyroAccProcessed ga)
        {
            _tbAccX.Text = ga.AccX.ToString();
            _tbAccY.Text = ga.AccY.ToString();
            _tbAccZ.Text = ga.AccZ.ToString();

            _tbSumAcc.Text = Math.Sqrt(ga.AccX * ga.AccX + ga.AccY * ga.AccY + ga.AccZ * ga.AccZ).ToString();

            _tbGyroX.Text = ga.GyroX.ToString();
            _tbGyroY.Text = ga.GyroY.ToString();
            _tbGyroZ.Text = ga.GyroZ.ToString();
        }


        private void ReceivePressureTemp(PressureTemp pt)
        {
            string s = "";
            this.BeginInvoke(new D_UpdateScp1000(UpdateScp1000), new object[] { pt });
        }
        private delegate void D_UpdateScp1000(PressureTemp pt);
        private void UpdateScp1000(PressureTemp pt)
        {
            _tbPressure.Text = pt.Pressure.ToString();
            _tbTemperature.Text = pt.Temperature.ToString();
            _tbHeight.Text = pt.Height.ToString();
        }

        private void _btn_sensors_current_to_neutral_Click(object sender, EventArgs e)
        {
            _tbAccXNeutral.Text = _tbAccXRaw.Text;
            _tbAccYNeutral.Text = _tbAccYRaw.Text;
            _tbAccZNeutral.Text = (double.Parse(_tbAccZRaw.Text, System.Globalization.CultureInfo.InvariantCulture) - 6600.0).ToString();
        }

        private void _btn_use_current_gyro_Click(object sender, EventArgs e)
        {
            _tbGyroXNeutral.Text = _tbGyroXRaw.Text;
            _tbGyroYNeutral.Text = _tbGyroYRaw.Text;
            _tbGyroZNeutral.Text = _tbGyroZRaw.Text;
        }

        private void _tbGyroNeutral_TextChanged(object sender, EventArgs e)
        {
            if (sender == _tbGyroXNeutral)
                _model.NeutralGyroX = _tbGyroXNeutral.IntValue;
            if (sender == _tbGyroYNeutral)
                _model.NeutralGyroY = _tbGyroYNeutral.IntValue;
            if (sender == _tbGyroZNeutral)
                _model.NeutralGyroZ = _tbGyroZNeutral.IntValue;
        }

        private void _tbAccNeutral_TextChanged(object sender, EventArgs e)
        {
            if (sender == _tbAccXNeutral)
                _model.NeutralAccX = _tbAccXNeutral.IntValue;
            if (sender == _tbAccYNeutral)
                _model.NeutralAccY = _tbAccYNeutral.IntValue;
            if (sender == _tbAccZNeutral)
                _model.NeutralAccZ = _tbAccZNeutral.IntValue;
        }

#endregion

#region RcInput tab page

        private void ReceiveRcInput(RcInput rc)
        {
            this.BeginInvoke(new D_ReceiveRcInput(RcTransmitterInput), new object[] { rc });
        }
        private delegate void D_ReceiveRcInput(RcInput rc);
        private void RcTransmitterInput(RcInput rc)
        {
            _pbCh1.Value = InBetween(rc.GetPwm(1), 500, 2200);
            _lblCh1Ms.Text = rc.GetPwm(1).ToString();
            _pbCh2.Value = InBetween(rc.GetPwm(2), 500, 2200);
            _lblCh2Ms.Text = rc.GetPwm(2).ToString();
            _pbCh3.Value = InBetween(rc.GetPwm(3), 500, 2200);
            _lblCh3Ms.Text = rc.GetPwm(3).ToString();
            _pbCh4.Value = InBetween(rc.GetPwm(4), 500, 2200);
            _lblCh4Ms.Text = rc.GetPwm(4).ToString();
            _pbCh5.Value = InBetween(rc.GetPwm(5), 500, 2200);
            _lblCh5Ms.Text = rc.GetPwm(5).ToString();
            _pbCh6.Value = InBetween(rc.GetPwm(6), 500, 2200);
            _lblCh6Ms.Text = rc.GetPwm(6).ToString();
            _pbCh7.Value = InBetween(rc.GetPwm(7), 500, 2200);
            _lblCh7Ms.Text = rc.GetPwm(7).ToString();
        }

        private int InBetween(int x, int a, int b)
        {
            if (x < a)
                return a;
            if (x > b)
                return b;
            return x;
        }

        private void RollChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (_rbRollCh1.Checked)
                _model.ChannelRoll = 1;
            else if (_rbRollCh2.Checked)
                _model.ChannelRoll = 2;
            else if (_rbRollCh3.Checked)
                _model.ChannelRoll = 3;
            else if (_rbRollCh4.Checked)
                _model.ChannelRoll = 4;
            else if (_rbRollCh5.Checked)
                _model.ChannelRoll = 5;
            else if (_rbRollCh6.Checked)
                _model.ChannelRoll = 6;
            else if (_rbRollCh7.Checked)
                _model.ChannelRoll = 7;
        }

        private void PitchChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (_rbPitchCh1.Checked)
                _model.ChannelPitch = 1;
            else if (_rbPitchCh2.Checked)
                _model.ChannelPitch = 2;
            else if (_rbPitchCh3.Checked)
                _model.ChannelPitch = 3;
            else if (_rbPitchCh4.Checked)
                _model.ChannelPitch = 4;
            else if (_rbPitchCh5.Checked)
                _model.ChannelPitch = 5;
            else if (_rbPitchCh6.Checked)
                _model.ChannelPitch = 6;
            else if (_rbPitchCh7.Checked)
                _model.ChannelPitch = 7;
        }

        private void YawChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (_rbYawCh1.Checked)
                _model.ChannelYaw = 1;
            else if (_rbYawCh2.Checked)
                _model.ChannelYaw = 2;
            else if (_rbYawCh3.Checked)
                _model.ChannelYaw = 3;
            else if (_rbYawCh4.Checked)
                _model.ChannelYaw = 4;
            else if (_rbYawCh5.Checked)
                _model.ChannelYaw = 5;
            else if (_rbYawCh6.Checked)
                _model.ChannelYaw = 6;
            else if (_rbYawCh7.Checked)
                _model.ChannelYaw = 7;
        }

        private void MotorChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (_rbMotorCh1.Checked)
                _model.ChannelMotor = 1;
            else if (_rbMotorCh2.Checked)
                _model.ChannelMotor = 2;
            else if (_rbMotorCh3.Checked)
                _model.ChannelMotor = 3;
            else if (_rbMotorCh4.Checked)
                _model.ChannelMotor = 4;
            else if (_rbMotorCh5.Checked)
                _model.ChannelMotor = 5;
            else if (_rbMotorCh6.Checked)
                _model.ChannelMotor = 6;
            else if (_rbMotorCh7.Checked)
                _model.ChannelMotor = 7;
        }

        private void ApChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (_rbApCh1.Checked)
                _model.ChannelAp = 1;
            else if (_rbApCh2.Checked)
                _model.ChannelAp = 2;
            else if (_rbApCh3.Checked)
                _model.ChannelAp = 3;
            else if (_rbApCh4.Checked)
                _model.ChannelAp = 4;
            else if (_rbApCh5.Checked)
                _model.ChannelAp = 5;
            else if (_rbApCh6.Checked)
                _model.ChannelAp = 6;
            else if (_rbApCh7.Checked)
                _model.ChannelAp = 7;
        }
#endregion

#region Telemetry tab page
       
        private void _nud_gyroaccproc_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryGyroAccProc = (int)_nud_gyroaccproc_telemetry.Value;
        }

        private void _nud_ppm_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryPpm = (int)_nud_ppm_telemetry.Value;
        }

        private void _nud_pressuretemp_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryPressureTemp = (int)_nud_pressuretemp_telemetry.Value;
        }

        private void _nud_gpsbasic_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryGpsBasic = (int)_nud_gpsbasic_telemetry.Value;
        }
        
        private void _nud_gyroacc_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryGyroAccRaw = (int)_nud_gyroaccraw_telemetry.Value;
        }
#endregion

#region Servos tab page

        private void _cb_reverse_servo_Click(object sender, EventArgs e)
        {
            if (sender == _cb_reverse_servo1)
                _model.ReverseServo1 = _cb_reverse_servo1.Checked;
            if (sender == _cb_reverse_servo2)
                _model.ReverseServo2 = _cb_reverse_servo2.Checked;
            if (sender == _cb_reverse_servo3)
                _model.ReverseServo3 = _cb_reverse_servo3.Checked;
            if (sender == _cb_reverse_servo4)
                _model.ReverseServo4 = _cb_reverse_servo4.Checked;
            if (sender == _cb_reverse_servo5)
                _model.ReverseServo5 = _cb_reverse_servo5.Checked;
            if (sender == _cb_reverse_servo6)
                _model.ReverseServo6 = _cb_reverse_servo6.Checked;
        }

#endregion

#region GPS tab page

        private void ReceiveGpsBasic(GpsBasic gb)
        {
            string s = "";
            this.BeginInvoke(new D_UpdateGpsBasic(UpdateGpsBasic), new object[] { gb });
        }
        private delegate void D_UpdateGpsBasic(GpsBasic gb);
        private void UpdateGpsBasic(GpsBasic gb)
        {
            if (gb.Status == 1)
                _rb_gps_status_active.Checked = true;
            else if (gb.Status == 0)
                _rb_gps_status_void.Checked = true;
            else
                _rb_gps_notfound.Checked = true;


            _tb_gps_numsat.Text = gb.NumberOfSatellites.ToString();
            _tb_gps_latitude.Text = gb.Latitude.ToString();
            _tb_gps_longitude.Text = gb.Longitude.ToString();
            _tb_gps_height.Text = gb.Height_m.ToString();
            _tb_gps_speed.Text = gb.Speed_ms.ToString();
            _tb_gps_heading.Text = gb.Heading_deg.ToString();
        }

        private void _tb_operational_baudrate_TextChanged(object sender, EventArgs e)
        {
            _model.GpsOperationalBaudrate = _tb_operational_baudrate.IntValue;
        }

        private void _tb_initial_baudrate_TextChanged(object sender, EventArgs e)
        {
            _model.GpsInitialBaudrate = _tb_initial_baudrate.IntValue;
        }

        private void _llGoogleMaps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://maps.google.com/maps?ll=" + 
                (double.Parse(_tb_gps_latitude.Text) / 3.14159 * 180.0).ToString(System.Globalization.CultureInfo.InvariantCulture) + 
                "," +
                (double.Parse(_tb_gps_longitude.Text) / 3.14159 * 180.0).ToString(System.Globalization.CultureInfo.InvariantCulture) + "&spn=0.005025,0.008500&t=h");
        }

#endregion

#region Datalog
        private DataSet _ds_logLines;

        void ReceiveDatalogTable(DatalogTable table)
        {
            this.BeginInvoke(new D_ReceiveDatalogTable(DatalogTable), new object[] { table });
        }
        private delegate void D_ReceiveDatalogTable(DatalogTable table);
        private void DatalogTable(DatalogTable table)
        {
            while (_lv_datalogtable.Items.Count <= table.Index)
                _lv_datalogtable.Items.Add("");
            _lv_datalogtable.Items[table.Index] = new ListViewItem();
            while (_lv_datalogtable.Items[table.Index].SubItems.Count <= 4)
                _lv_datalogtable.Items[table.Index].SubItems.Add("");
            _lv_datalogtable.Items[table.Index].SubItems[0] = new ListViewItem.ListViewSubItem(_lv_datalogtable.Items[table.Index], table.Index.ToString());
            _lv_datalogtable.Items[table.Index].SubItems[1] = new ListViewItem.ListViewSubItem(_lv_datalogtable.Items[table.Index], table.StartPage.ToString());
            string date = table.Date / 10000 + "." + (table.Date / 100) % 100 + "." + table.Date % 100;
            string time = table.Time / 10000 + ":" + (table.Time / 100) % 100 + ":" + table.Time % 100;
            _lv_datalogtable.Items[table.Index].SubItems[2] = new ListViewItem.ListViewSubItem(_lv_datalogtable.Items[table.Index], date);
            _lv_datalogtable.Items[table.Index].SubItems[3] = new ListViewItem.ListViewSubItem(_lv_datalogtable.Items[table.Index], time);
        }

        void ReceiveDatalogLine(DatalogLine line)
        {
            this.BeginInvoke(new D_ReceiveDatalogLine(DatalogLine), new object[] { line });
        }
        private delegate void D_ReceiveDatalogLine(DatalogLine line);
        private void DatalogLine(DatalogLine line)
        {
            // first call: init dataset & headers
            if (_ds_logLines == null)
            {
                _ds_logLines = new DataSet();
                _ds_logLines.Tables.Add("Data");

                foreach (string s in line.Header)
                    _ds_logLines.Tables["Data"].Columns.Add(s);

                _dgv_datalog.SelectionMode = DataGridViewSelectionMode.CellSelect;
                _dgv_datalog.DataSource = _ds_logLines;
                _dgv_datalog.DataMember = "Data";
                foreach (DataGridViewColumn dgvc in _dgv_datalog.Columns)
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                _dgv_datalog.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            }
            // add row
            DataRow dr = _ds_logLines.Tables["Data"].NewRow();
            dr.ItemArray = new String[line.Line.Length];
            for (int i = 0; i < line.Line.Length; i++)
                dr[i] = line.Line[i];
            _ds_logLines.Tables["Data"].Rows.Add(dr);

            _pb_datalog.Value = (_pb_datalog.Value + 1) % 50;
         
        }

     private void _btn_dataflash_format_Click(object sender, EventArgs e)
        {
            _serial.SendDatalogFormat();
        }

        private void _btn_datalog_readtable_Click(object sender, EventArgs e)
        {
            _serial.SendDatalogTableRequest();
        }

        private void _btn_readdatalog_Click(object sender, EventArgs e)
        {
            _ds_logLines = null;
            if (_lv_datalogtable.SelectedIndices.Count != 1)
                MessageBox.Show("Please select 1 row from the index table.");
            else
                _serial.SendDatalogTableRead(_lv_datalogtable.SelectedIndices[0]);
        }
        
        private void _button_datalog_kml_Click(object sender, EventArgs e)
        {
            string[] flightmodes = new string[5] { "Manual", "Stabilized", "Autopilot", "Loiter", "Return" };
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "kml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filename = sfd.FileName.Replace(".kml", "");
                int flightmode = 0;

                Stream s = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(s);

                sw.WriteLine( "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                              " <kml xmlns=\"http://earth.google.com/kml/2.0\">" +
                              " <Document xmlns:xlink=\"http://www.w3/org/1999/xlink\">" +
                              " <name>Gluonpilot export</name>");
                for (int j = 0; j <= 180; j++)
                {
                    sw.WriteLine(
                              " <Style id=\"dataStyle_" + j + "\">" +
                              "     <IconStyle><Icon><href>http://maps.google.com/mapfiles/kml/shapes/airports.png</href></Icon><scale>0.5</scale><heading>" + j*2 + "</heading></IconStyle>" +
                              " </Style>");
                }
                sw.WriteLine(
                              " <Style id=\"dataStyle\">" +
                              "     <IconStyle><Icon><href>http://maps.google.com/mapfiles/kml/shapes/airports.png</href></Icon><scale>0.5</scale></IconStyle>" +
                              " </Style>" +
                              " <Style id=\"manualStyle\">" +
                              "   <LineStyle>" +
                              "   <color>ff00ffff</color>" +
                              "   <width>5</width>" +
                              "   </LineStyle>" +
                              " </Style>" +
                              " <Style id=\"stabiStyle\">" +
                              "   <LineStyle>" +
                              "   <color>ffff00ff</color>" +
                              "   <width>5</width>" +
                              "   </LineStyle>" +
                              " </Style>" +
                              " <Style id=\"autoStyle\">" +
                              "   <LineStyle>" +
                              "   <color>ff0000ff</color>" +
                              "   <width>5</width>" +
                              "   </LineStyle>" +
                              " </Style>" +
                              "   <Folder>" +
                              "   <name>Flight path</name>") ;
                string tailpath = "</coordinates></LineString></MultiGeometry></Placemark></Folder>";
                string tailkml = "</Document></kml>";


                bool firstcall = true;
                int startheight = -11111;
                int exceptions = 0;
                foreach (DataRow dr in _ds_logLines.Tables["Data"].Rows)
                {
                    try
                    {
                        if (firstcall || (dr.Table.Columns.Contains("FlightMode") &&
                            dr["FlightMode"].ToString() != flightmode.ToString() &&
                            int.Parse(dr["FlightMode"].ToString()) >= 0 &&
                            int.Parse(dr["FlightMode"].ToString()) <= flightmodes.Length))
                        {
                            if (dr.Table.Columns.Contains("FlightMode"))
                            {
                                flightmode = int.Parse(dr["FlightMode"].ToString());
                                if (flightmode >= flightmodes.Length)
                                    flightmode = 0;
                            }

                            if (!firstcall)
                                sw.WriteLine("</coordinates></LineString></MultiGeometry></Placemark>");
                            else
                                firstcall = false;
                            sw.WriteLine("<Placemark>" +
                                         "   <name>" + flightmodes[flightmode] + "</name>");
                            if (flightmode == 0) // manual
                                sw.WriteLine("   <styleUrl>#manualStyle</styleUrl>");
                            else if (flightmode == 1) // stabilized
                                sw.WriteLine("   <styleUrl>#stabiStyle</styleUrl>");
                            else
                                sw.WriteLine("   <styleUrl>#autoStyle</styleUrl>");
                            sw.WriteLine(
                                  "   <MultiGeometry>" +
                                  "     <LineString>" +
                                  "       <altitudeMode>relativeToGround</altitudeMode>" +
                                  "       <coordinates>");
                        }
                        if (startheight == -11111)
                            startheight = (int)double.Parse(dr["HeightBaro"].ToString(), System.Globalization.CultureInfo.InvariantCulture);

                        if (Math.Abs(double.Parse(dr["Longitude"].ToString(), System.Globalization.CultureInfo.InvariantCulture)) <= 360.0 &&
                            Math.Abs(double.Parse(dr["Latitude"].ToString(), System.Globalization.CultureInfo.InvariantCulture)) <= 360.0)
                            sw.WriteLine("{0},{1},{2}", dr["Longitude"], dr["Latitude"], (double.Parse(dr["HeightBaro"].ToString(), System.Globalization.CultureInfo.InvariantCulture) - startheight).ToString(System.Globalization.CultureInfo.InvariantCulture));
                    }
                    catch (Exception ex)
                    {
                        exceptions++;
                    }
                }
                sw.WriteLine(tailpath);

                sw.WriteLine("<Folder><name>Datalog</name>");

                
                int i = 0;
                double time = 0.0;
                foreach (DataRow dr in _ds_logLines.Tables["Data"].Rows)
                {
                    time += 0.02;
                    if (i++ % 15 == 0)
                    {
                        if (dr.Table.Columns.Contains("HeadingGPS") && dr.Table.Columns.Contains("HeightBaro"))
                        {
                            sw.WriteLine("<Placemark><Point><coordinates>");
                            sw.WriteLine("{0},{1},{2}", dr["Longitude"], dr["Latitude"], double.Parse(dr["HeightBaro"].ToString(), System.Globalization.CultureInfo.InvariantCulture) - startheight);

                            sw.WriteLine("</coordinates><altitudeMode>relativeToGround</altitudeMode></Point><styleUrl>#dataStyle_" + int.Parse(dr["HeadingGPS"].ToString()) / 2 + "</styleUrl>");
                            if (dr.Table.Columns.Contains("Pitch") && dr.Table.Columns.Contains("HeightGPS"))
                            {
                                sw.WriteLine("<description>" + (int)time + " - Roll: " + dr["Roll"].ToString() + ", Desired: " + dr["DesiredRoll"].ToString() + " - Pitch: " + dr["Pitch"].ToString() + ", Desired: " + dr["DesiredPitch"].ToString() + " - Height: " + dr["HeightGPS"].ToString() + "</description>");
                            } 
                            else if (dr.Table.Columns.Contains("Time"))
                            {
                                    sw.WriteLine("<description>GPS time: " + dr["Time"] + " - elapsed time: " + Math.Round(time, 2) + " - datasample " + i + "</description>");
                            }
                            sw.WriteLine("</Placemark>");
                        }
                    }
                }
                sw.WriteLine("</Folder>");
                
                sw.WriteLine(tailkml);
                sw.Close();

                if (exceptions > 0)
                    MessageBox.Show("Export ended with " + exceptions + " exceptions");

            }
        }

        // Save datalog to XML file
        private void _btn_datalog_to_xml_Click(object sender, EventArgs e)
        {
            Stream s;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                s = sfd.OpenFile();
                _ds_logLines.WriteXml(s);
                s.Close();
            }
        }

        // Load datalog from XML file
        private void _btn_datalog_load_xml_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (_ds_logLines != null)
            {
                _ds_logLines.Tables.Clear();
            }
                
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (_ds_logLines == null)
                    _ds_logLines = new DataSet();
                Stream s = ofd.OpenFile();
                _ds_logLines.ReadXml(s);
                _dgv_datalog.SelectionMode = DataGridViewSelectionMode.CellSelect;
                _dgv_datalog.DataSource = _ds_logLines;
                _dgv_datalog.DataMember = "Data";

                foreach (DataGridViewColumn dgvc in _dgv_datalog.Columns)
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                _dgv_datalog.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                s.Close();
            }
        }
        
        private void _btn_datalog_copy_Click(object sender, EventArgs e)
        {
            _dgv_datalog.SelectAll();
            DataObject dataObj = _dgv_datalog.GetClipboardContent();
            Clipboard.SetDataObject(dataObj, true);
            _dgv_datalog.ClearSelection();
        }

        private void _btn_datalog_to_matlab_Click(object sender, EventArgs e)
        {
            DataColumnCollection dc = _ds_logLines.Tables["Data"].Columns;
            if (!dc.Contains("AccX") || !dc.Contains("GyroX") || !dc.Contains("SpeedGPS") ||
                !dc.Contains("HeightBaro") || !dc.Contains("HeadingGPS") || !dc.Contains("Latitude"))
            {
                MessageBox.Show("Not all needed data available: Acc[XYZ], Gyro[XYZ], Latitude, Longitude, SpeedGPS, HeadingGPS, HeightBaro");
                return;
            }
            string prefix = Microsoft.VisualBasic.Interaction.InputBox("Please enter the prefix for the files:", "Export to *.dat", "", 10, 20);
            SaveFileDialog sfd = new SaveFileDialog();
            System.Windows.Forms.FolderBrowserDialog f = new FolderBrowserDialog();
            DialogResult r = f.ShowDialog();
            if (r == DialogResult.OK)
            {
                StreamWriter f_acc_x = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_raw_acc_x.dat");
                StreamWriter f_acc_y = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_raw_acc_y.dat");
                StreamWriter f_acc_z = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_raw_acc_z.dat");
                StreamWriter f_gyro_x = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_raw_gyro_x.dat");
                StreamWriter f_gyro_y = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_raw_gyro_y.dat");
                StreamWriter f_gyro_z = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_raw_gyro_z.dat");
                StreamWriter f_speed_m_s = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_speed_m_s.dat");
                StreamWriter f_time_s = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_time_s.dat");
                StreamWriter f_longitude_deg = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_longitude_deg.dat");
                StreamWriter f_latitude_deg = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_latitude_deg.dat");
                StreamWriter f_heading_deg = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_heading_deg.dat");
                StreamWriter f_height_m = new StreamWriter(f.SelectedPath + "\\" +
                                                        prefix + "_height_m.dat");


                foreach (DataRow dr in _ds_logLines.Tables["Data"].Rows)
                {
                    f_acc_x.WriteLine(dr["AccX"]);
                    f_acc_y.WriteLine(dr["AccY"]);
                    f_acc_z.WriteLine(dr["AccZ"]);
                    f_gyro_x.WriteLine(dr["GyroX"]);
                    f_gyro_y.WriteLine(dr["GyroY"]);
                    f_gyro_z.WriteLine(dr["GyroZ"]);

                    f_speed_m_s.WriteLine(dr["SpeedGPS"]);
                    f_height_m.WriteLine(dr["HeightBaro"]);
                    f_heading_deg.WriteLine(dr["HeadingGPS"]);
                    f_latitude_deg.WriteLine(dr["Latitude"]);
                    f_longitude_deg.WriteLine(dr["Longitude"]);
                    if (dr.Table.Columns.Contains("Time"))
                        f_time_s.WriteLine(dr["Time"]);
                }
                f_acc_x.Close();
                f_acc_y.Close();
                f_acc_z.Close();
                f_gyro_x.Close();
                f_gyro_y.Close();
                f_gyro_z.Close();

                f_speed_m_s.Close();
                f_height_m.Close();
                f_heading_deg.Close();
                f_latitude_deg.Close();
                f_longitude_deg.Close();
                f_time_s.Close();
            }
        }
#endregion

        private void _cbControlMix_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.ControlMixing = _cbControlMix.SelectedIndex;
            _lblControlMixInfo.Text = "1: Up/Down, 2: Aileron1, 3: Aileron2, 4: Motor";
        }

        private void _pid_pitch_to_elevator_IsChanged(object sender, EventArgs e)
        {
            _model.Pitch2ElevatorPidModel = _pid_pitch_to_elevator.Model;
        }

        private void _pid_roll_to_aileron_IsChanged(object sender, EventArgs e)
        {
            _model.Roll2AileronPidModel = _pid_roll_to_aileron.Model;
        }

        private void _pid_heading_to_roll_IsChanged(object sender, EventArgs e)
        {
            _model.Heading2RollPidModel = _pid_heading_to_roll.Model;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gluonpilot.com/wiki/Config_telemetry");
        }

        private void _llConfigSensors_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gluonpilot.com/wiki/Config_sensors");
        }

        private void _llConfigGps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gluonpilot.com/wiki/Config_Gps");
        }

        private void _rbPpm_CheckedChanged(object sender, EventArgs e)
        {
            if (_rbPpm.Checked)
                _model.RcTransmitterFromPpm = 1;
            else
                _model.RcTransmitterFromPpm = 0;
        }

        private void _llServos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gluonpilot.com/wiki/Config_Servos");
        }

        private void _nud_control_roll_max_ValueChanged(object sender, EventArgs e)
        {
            _model.ControlMaxRoll = Convert.ToDouble(_nud_control_roll_max.Value);
            CalculateMinimumRadius(sender, null);
        }

        private void _nud_control_pitch_max_ValueChanged(object sender, EventArgs e)
        {
            _model.ControlMaxPitch = Convert.ToDouble(_nud_control_pitch_max.Value);
        }

        private void CalculateMinimumRadius(object sender, KeyPressEventArgs e)
        {
            // atan(v^2  / (G * r)) = roll
            // tan(roll) = v^2 / (G * r)
            tb_min_circle_radius.DistanceM =
                tb_speed.SpeedMS * tb_speed.SpeedMS / Math.Tan((double)_nud_control_roll_max.Value / 180.0 * 3.14) / 9.81;
        }






       

    }
}
