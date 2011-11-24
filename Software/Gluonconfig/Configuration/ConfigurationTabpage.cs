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
    public partial class ConfigurationTabpage : UserControl
    {
        private SerialCommunication _serial;
        private ConfigurationModel _model = new ConfigurationModel();

        public ConfigurationTabpage(ConfigurationModel model)
        {
            InitializeComponent();
            _model = model;
        }

        public ConfigurationTabpage() :
            this(new ConfigurationModel())
        {
            
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
                _serial.GyroAccRawCommunicationReceived -= ReceiveGyroAccRaw;
                _serial.GyroAccProcCommunicationReceived -= ReceiveGyroAccProc;
                _serial.PressureTempCommunicationReceived -= ReceivePressureTemp;
                _serial.AllConfigCommunicationReceived -= ReceiveAllConfig;
                _serial.RcInputCommunicationReceived -= new SerialCommunication_CSV.ReceiveRcInputCommunicationFrame(ReceiveRcInput);
                _serial.GpsBasicCommunicationReceived -= new SerialCommunication.ReceiveGpsBasicCommunicationFrame(ReceiveGpsBasic);
            }
        }

        public ConfigurationModel GetModel()
        {
            _tbHeight.Focus(); // hack to trigger is_changed on other controls.
            return _model;
        }

        public void SetModel(ConfigurationModel model)
        {
                _model = model;
                ReadModel();
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
            _nud_attitude_telemetry.Value = _model.TelemetryAttitude;
            _nud_control_telemetry.Value = _model.TelemetryControl;

            _tb_initial_baudrate.Text = _model.GpsInitialBaudrate.ToString();
            _tb_operational_baudrate.Text = _model.GpsOperationalBaudrate.ToString();

            _pid_pitch_to_elevator.SetModel(_model.Pitch2ElevatorPidModel);
            _pid_roll_to_aileron.SetModel(_model.Roll2AileronPidModel);
            _pid_heading_to_roll.SetModel(_model.Heading2RollPidModel);
            _pid_altitude_to_pitch.SetModel(_model.Altitude2PitchPidModel);

            _cb_reverse_servo1.Checked = _model.ReverseServo1;
            _cb_reverse_servo2.Checked = _model.ReverseServo2;
            _cb_reverse_servo3.Checked = _model.ReverseServo3;
            _cb_reverse_servo4.Checked = _model.ReverseServo4;
            _cb_reverse_servo5.Checked = _model.ReverseServo5;
            _cb_reverse_servo6.Checked = _model.ReverseServo6;

            _rbPpm.Checked = _model.RcTransmitterFromPpm == 1? true : false;
            _rbPwm.Checked = _model.RcTransmitterFromPpm == 0? true : false;

            _nud_control_pitch_max.Value = (int)_model.ControlMaxPitch;
            _nud_control_pitch_min.Value = (int)_model.ControlMinPitch;
            _nud_control_roll_max.Value = (int)_model.ControlMaxRoll;
            _nud_aileron_diff.Value = _model.ControlAileronDiff;

            tb_speed.SpeedMS = _model.CruisingSpeed;
            _dtb_waypoint_radius.DistanceM = _model.WaypointRadius;
            cb_altitudehold.Checked = _model.StabilizationWithAltitudeHold;

            if (_model.ControlMixing >= 0 && _model.ControlMixing < _cbControlMix.Items.Count)
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

            if (_model.ManualTrim)
                _rb_servos_neutral_configurable.Checked = true;
            else
                _rb_serovs_neutral_auto.Checked = true;

            _nudAutoThrottleMinPct.Value = _model.AutoThrottleMinPct;
            _nudAutoThrottleMaxPct.Value = _model.AutoThrottleMaxPct;
            _nudAutoThrottleCruisePct.Value = _model.AutoThrottleCruisePct;
            _ntbAutoThrottlePGain.DoubleValue = _model.AutoThrottlePGain;
            _cbMotor.SelectedIndex = _model.AutoThrottleEnabled ? 1 : 0;
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
        }


        private void ReceiveAllConfig(AllConfig ac)
        {
            Console.WriteLine("GUI received");
            this.BeginInvoke(new D_ReceiveAllConfig(AllConfig), new object[] { ac });
        }
        private delegate void D_ReceiveAllConfig(AllConfig ac);
        private void AllConfig(AllConfig ac)
        {
            _model = new ConfigurationModel();
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

        private void _llConfigSensors_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gluonpilot.com/wiki/Config_sensors");
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
        
        private void _rbPpm_CheckedChanged(object sender, EventArgs e)
        {
            if (_rbPpm.Checked)
                _model.RcTransmitterFromPpm = 1;
            else
                _model.RcTransmitterFromPpm = 0;
        }

#endregion

#region Telemetry tab page
       
        private void _nud_gyroaccproc_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryGyroAccProc = (int)_nud_gyroaccproc_telemetry.Value;
            if (_model.TelemetryGyroAccProc == 0)
                _lbl_imuprocessed_hz.Text = "disabled";
            else
                _lbl_imuprocessed_hz.Text = (20.0 / (double)_model.TelemetryGyroAccProc).ToString("F") + " Hz";
        }

        private void _nud_ppm_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryPpm = (int)_nud_ppm_telemetry.Value;
            if (_model.TelemetryPpm == 0)
                _lbl_rc_hz.Text = "disabled";
            else
                _lbl_rc_hz.Text = (20.0 / (double)_model.TelemetryPpm).ToString("F") + " Hz";
        }

        private void _nud_pressuretemp_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryPressureTemp = (int)_nud_pressuretemp_telemetry.Value;
            if (_model.TelemetryPressureTemp == 0)
                _lbl_pressure_hz.Text = "disabled";
            else
                _lbl_pressure_hz.Text = (20.0 / (double)_model.TelemetryPressureTemp).ToString("F") + " Hz";
        }

        private void _nud_gpsbasic_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryGpsBasic = (int)_nud_gpsbasic_telemetry.Value;
            if (_model.TelemetryGpsBasic == 0)
                _lbl_basicgps_hz.Text = "disabled";
            else
                _lbl_basicgps_hz.Text = (20.0 / (double)_model.TelemetryGpsBasic).ToString("F") + " Hz";
        }
        
        private void _nud_gyroacc_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryGyroAccRaw = (int)_nud_gyroaccraw_telemetry.Value;
            if (_model.TelemetryGyroAccRaw == 0)
                _lbl_imu_raw_hz.Text = "disabled";
            else
                _lbl_imu_raw_hz.Text = (20.0 / (double)_model.TelemetryGyroAccRaw).ToString("F") + " Hz";
        }

        private void _nud_attitude_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryAttitude = (int)_nud_attitude_telemetry.Value;
            if (_model.TelemetryAttitude == 0)
                _lbl_attitude_hz.Text = "disabled";
            else
                _lbl_attitude_hz.Text = (20.0 / (double)_model.TelemetryAttitude).ToString("F") + " Hz";
        }
        
        private void _nud_control_telemetry_ValueChanged(object sender, EventArgs e)
        {
            _model.TelemetryControl = (int)_nud_control_telemetry.Value;
            if (_model.TelemetryControl == 0)
                _lbl_control_hz.Text = "disabled";
            else
                _lbl_control_hz.Text = (20.0 / (double)_model.TelemetryControl).ToString("F") + " Hz";
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gluonpilot.com/wiki/Config_telemetry");
        }


        private void _btn_telemetry_configuration_Click(object sender, EventArgs e)
        {
            _nud_attitude_telemetry.Value = 4;
            _nud_control_telemetry.Value = 10;
            _nud_gpsbasic_telemetry.Value = 5;
            _nud_gyroaccproc_telemetry.Value = 6;
            _nud_gyroaccraw_telemetry.Value = 5;
            _nud_pressuretemp_telemetry.Value = 5;
            _nud_ppm_telemetry.Value = 4;
            if (_serial != null && _serial.IsOpen)
            {
                _serial.SendWriteTelemetry(
                    (int)_nud_gpsbasic_telemetry.Value,
                    (int)_nud_gyroaccraw_telemetry.Value,
                    (int)_nud_gyroaccproc_telemetry.Value,
                    (int)_nud_ppm_telemetry.Value,
                    (int)_nud_pressuretemp_telemetry.Value,
                    (int)_nud_attitude_telemetry.Value,
                    (int)_nud_control_telemetry.Value);
                _serial.ReadAllConfig();
            }
        }

        private void _btn_telemetry_inflight_Click(object sender, EventArgs e)
        {
            _nud_attitude_telemetry.Value = 7;
            _nud_control_telemetry.Value = 10;
            _nud_gpsbasic_telemetry.Value = 5;
            _nud_gyroaccproc_telemetry.Value = 1; _nud_gyroaccproc_telemetry.Value = 0;
            _nud_gyroaccraw_telemetry.Value = 1; _nud_gyroaccraw_telemetry.Value = 0;
            _nud_pressuretemp_telemetry.Value = 1; _nud_pressuretemp_telemetry.Value = 0;
            _nud_ppm_telemetry.Value = 1; _nud_ppm_telemetry.Value = 0;
            if (_serial != null && _serial.IsOpen)
            {
                _serial.SendWriteTelemetry(
                    (int)_nud_gpsbasic_telemetry.Value,
                    (int)_nud_gyroaccraw_telemetry.Value,
                    (int)_nud_gyroaccproc_telemetry.Value,
                    (int)_nud_ppm_telemetry.Value,
                    (int)_nud_pressuretemp_telemetry.Value,
                    (int)_nud_attitude_telemetry.Value,
                    (int)_nud_control_telemetry.Value);
                _serial.ReadAllConfig();
            }
        }

#endregion

#region Servos tab page

        private void _nud_servo_TextChanged(object sender, EventArgs e)
        {
            if (sender == _tb_servo1_min)
                int.TryParse(_tb_servo1_min.Text, out _model.ServoMin[0]);
            if (sender == _tb_servo2_min)
                int.TryParse(_tb_servo2_min.Text, out _model.ServoMin[1]);
            if (sender == _tb_servo3_min)
                int.TryParse(_tb_servo3_min.Text, out _model.ServoMin[2]);
            if (sender == _tb_servo4_min)
                int.TryParse(_tb_servo4_min.Text, out _model.ServoMin[3]);
            if (sender == _tb_servo5_min)
                int.TryParse(_tb_servo5_min.Text, out _model.ServoMin[4]);
            if (sender == _tb_servo6_min)
                int.TryParse(_tb_servo6_min.Text, out _model.ServoMin[5]);

            if (sender == _tb_servo1_max)
                int.TryParse(_tb_servo1_max.Text, out _model.ServoMax[0]);
            if (sender == _tb_servo2_max)
                int.TryParse(_tb_servo2_max.Text, out _model.ServoMax[1]);
            if (sender == _tb_servo3_max)
                int.TryParse(_tb_servo3_max.Text, out _model.ServoMax[2]);
            if (sender == _tb_servo4_max)
                int.TryParse(_tb_servo4_max.Text, out _model.ServoMax[3]);
            if (sender == _tb_servo5_max)
                int.TryParse(_tb_servo5_max.Text, out _model.ServoMax[4]);
            if (sender == _tb_servo6_max)
                int.TryParse(_tb_servo6_max.Text, out _model.ServoMax[5]);

            if (sender == _tb_servo1_neutral)
                int.TryParse(_tb_servo1_neutral.Text, out _model.ServoNeutral[0]);
            if (sender == _tb_servo2_neutral)
                int.TryParse(_tb_servo2_neutral.Text, out _model.ServoNeutral[1]);
            if (sender == _tb_servo3_neutral)
                int.TryParse(_tb_servo3_neutral.Text, out _model.ServoNeutral[2]);
            if (sender == _tb_servo4_neutral)
                int.TryParse(_tb_servo4_neutral.Text, out _model.ServoNeutral[3]);
            if (sender == _tb_servo5_neutral)
                int.TryParse(_tb_servo5_neutral.Text, out _model.ServoNeutral[4]);
            if (sender == _tb_servo6_neutral)
                int.TryParse(_tb_servo6_neutral.Text, out _model.ServoNeutral[5]);
        }


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

        private void _llServos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gluonpilot.com/wiki/Config_Servos");
        }

     
        private void _rb_serovs_neutral_auto_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in _panelServos.Controls)
            {
                TextBox tb = c as TextBox;
                if (tb != null)
                    tb.ReadOnly = true;
            }
            _model.ManualTrim = false;
        } 
  
        private void _rb_servos_neutral_configurable_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in _panelServos.Controls)
            {
                TextBox tb = c as TextBox;
                if (tb != null)
                    tb.ReadOnly = false;
            }
            _model.ManualTrim = true;
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


        private void _llConfigGps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gluonpilot.com/wiki/Config_Gps");
        }

#endregion

#region Control tab page
        private void _cbControlMix_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.ControlMixing = _cbControlMix.SelectedIndex;
            if (_cbControlMix.SelectedIndex == 0)
                _lblControlMixInfo.Text = "1: Aileron1, 2: Aileron2, 3: Up/Down, 2, 4: Motor, 6: roll stabi";
            if (_cbControlMix.SelectedIndex == 1)
                _lblControlMixInfo.Text = "1: Elevon1, 2: Elevon2, 4: Motor";
            if (_cbControlMix.SelectedIndex == 2)
                _lblControlMixInfo.Text = "1: Elevon1, 2: Elevon2, 4: Motor";
            if (_cbControlMix.SelectedIndex == 3)
                _lblControlMixInfo.Text = "See quadrocopter page";
            if (_cbControlMix.SelectedIndex == 4)
                _lblControlMixInfo.Text = "1: Aileron1, 2: Aileron2, 3: Up/Down, 2, 4: Motor, 6: roll stabi";
        }

        private void _pid_pitch_to_elevator_IsChanged(object sender, EventArgs e)
        {
            _model.Pitch2ElevatorPidModel = _pid_pitch_to_elevator.GetModel();
        }

        private void _pid_roll_to_aileron_IsChanged(object sender, EventArgs e)
        {
            _model.Roll2AileronPidModel = _pid_roll_to_aileron.GetModel();
        }

        private void _pid_heading_to_roll_IsChanged(object sender, EventArgs e)
        {
            _model.Heading2RollPidModel = _pid_heading_to_roll.GetModel();
        }

        private void _pid_altitude_to_pitch_IsChanged(object sender, EventArgs e)
        {
            _model.Altitude2PitchPidModel = _pid_altitude_to_pitch.GetModel();
        }

        private void _nud_control_roll_max_ValueChanged(object sender, EventArgs e)
        {
            _model.ControlMaxRoll = Convert.ToDouble(_nud_control_roll_max.Value);
            CalculateMinimumRadius(sender, null);
        }

        private void _nud_control_pitch_min_ValueChanged(object sender, EventArgs e)
        {
            _model.ControlMinPitch = Convert.ToDouble(_nud_control_pitch_min.Value);
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

            //_model.CruisingSpeed = tb_speed.SpeedMS;
        }

        private void cb_altitudehold_CheckedChanged(object sender, EventArgs e)
        {
            _model.StabilizationWithAltitudeHold = cb_altitudehold.Checked;
        }

        private void _dtb_waypoint_radius_DistanceChanged(object sender, EventArgs e)
        {
            _model.WaypointRadius = _dtb_waypoint_radius.DistanceM;
        }

        private void tb_speed_SpeedChanged(object sender, EventArgs e)
        {
            _model.CruisingSpeed = tb_speed.SpeedMS;
        }
        
        private void _nud_aileron_diff_ValueChanged(object sender, EventArgs e)
        {
            _model.ControlAileronDiff = (int)_nud_aileron_diff.Value;
        }


        private void _nudAutoThrottleMinPct_ValueChanged(object sender, EventArgs e)
        {
            _model.AutoThrottleMinPct = (int)_nudAutoThrottleMinPct.Value;
        }

        private void _nudAutoThrottleCruisePct_ValueChanged(object sender, EventArgs e)
        {
            _model.AutoThrottleCruisePct = (int)_nudAutoThrottleCruisePct.Value;
        }

        private void _nudAutoThrottleMaxPct_ValueChanged(object sender, EventArgs e)
        {
            _model.AutoThrottleMaxPct = (int)_nudAutoThrottleMaxPct.Value;
        }

        private void _cbMotor_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.AutoThrottleEnabled = _cbMotor.SelectedIndex == 1;
        }

        private void _ntbAutoThrottlePGain_TextChanged(object sender, EventArgs e)
        {
            _model.AutoThrottlePGain = _ntbAutoThrottlePGain.DoubleValue;
        }

#endregion



        private void label63_Click(object sender, EventArgs e)
        {

        }






    }
}
