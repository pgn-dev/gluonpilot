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
    public partial class GluonConfigEasy : UserControl
    {
        private SerialCommunication serial;
        private Communication.Frames.Configuration.AllConfig config = null;
        private Communication.Frames.Incoming.ControlInfo ci = null;

        private bool updating_gui = false;

        public GluonConfigEasy()
        {
            InitializeComponent();
        }

        public void Connect(SerialCommunication serial)
        {
            this.serial = serial;
            serial.RcInputCommunicationReceived += new SerialCommunication.ReceiveRcInputCommunicationFrame(serial_RcInputCommunicationReceived);
            serial.AllConfigCommunicationReceived += new SerialCommunication.ReceiveAllConfigCommunicationFrame(serial_AllConfigCommunicationReceived);
            serial.AttitudeCommunicationReceived += new SerialCommunication.ReceiveAttitudeCommunicationFrame(serial_AttitudeCommunicationReceived);
            serial.ControlInfoCommunicationReceived += new SerialCommunication.ReceiveControlInfoCommunicationFrame(serial_ControlInfoCommunicationReceived);
        }

        void serial_ControlInfoCommunicationReceived(Communication.Frames.Incoming.ControlInfo ci)
        {
            this.ci = ci;
        }

        public void Disconnect()
        {
            serial.RcInputCommunicationReceived -= serial_RcInputCommunicationReceived;
            serial.AllConfigCommunicationReceived -= serial_AllConfigCommunicationReceived;
            serial.AttitudeCommunicationReceived -= serial_AttitudeCommunicationReceived;
            serial.ControlInfoCommunicationReceived -= serial_ControlInfoCommunicationReceived;
        }

        void serial_AttitudeCommunicationReceived(Communication.Frames.Incoming.Attitude attitude)
        {
            //throw new NotImplementedException();
        }

        void serial_AllConfigCommunicationReceived(Communication.Frames.Configuration.AllConfig config)
        {
            updating_gui = true;

            this.config = config;

            MethodInvoker m = delegate()
            {
                _cbInvert1.Checked = config.servo_reverse[0];
                _cbInvert2.Checked = config.servo_reverse[1];
                _cbInvert3.Checked = config.servo_reverse[2];
                _cbInvert4.Checked = config.servo_reverse[3];

                _lblInterpretation1.Text = "";
                _lblInterpretation2.Text = "";
                _lblInterpretation3.Text = "";
                _lblInterpretation4.Text = "";

                ComboBox[] cbn = { _cbInputFunction1, _cbInputFunction2, _cbInputFunction3, _cbInputFunction4 };
                cbn[0].SelectedIndex = 4;
                cbn[1].SelectedIndex = 4;
                cbn[2].SelectedIndex = 4;
                cbn[3].SelectedIndex = 4;

                if (config.channel_ap <= cbn.Length && config.channel_ap > 0)
                    cbn[config.channel_ap - 1].SelectedIndex = 3;
                if (config.channel_motor <= cbn.Length && config.channel_motor > 0)
                    cbn[config.channel_motor - 1].SelectedIndex = 2;
                if (config.channel_pitch <= cbn.Length && config.channel_pitch > 0)
                    cbn[config.channel_pitch - 1].SelectedIndex = 0;
                if (config.channel_roll <= cbn.Length && config.channel_roll > 0)
                    cbn[config.channel_roll - 1].SelectedIndex = 1;
                //if (config.channel_yaw <= cbn.Length && config.channel_yaw > 0)
                //    cbn[config.channel_yaw - 1].SelectedIndex = 2;

                if (config.control_mixing < _cbMixing.Items.Count)
                    _cbMixing.SelectedIndex = Math.Max(0, Math.Min(10, config.control_mixing));

                _hsPitchSensitivity.Value = Math.Max(0, (int)(config.pid_pitch2elevator_p * 10.0));
                _hsRollSensitivity.Value = Math.Max(0, (int)(config.pid_roll2aileron_p * 10.0));

                _cbAutothrottle.Checked = config.auto_throttle_enabled;
                _hsbCruiseThrottle.Value = Math.Max(0, config.auto_throttle_cruise_pct);
                _hsbCruiseThrottle_Scroll(null, new ScrollEventArgs(ScrollEventType.EndScroll, _hsbCruiseThrottle.Value));

                updating_gui = false;
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

        private void UpdateLabel(Label l, string text)
        {
            if (l.Text != text)
                l.Text = text;
        }

        void serial_RcInputCommunicationReceived(Communication.Frames.Incoming.RcInput rcinput)
        {
            if (config != null)
            {
                MethodInvoker m = delegate()
                {
                    ProgressBar[] pbs = { _pbChannel1, _pbChannel2, _pbChannel3, _pbChannel4 };
                    for (int i = 0; i < pbs.Length; i++)
                    {
                        if (rcinput.GetPwm(i + 1) > 900 && rcinput.GetPwm(i + 1) < 2100)
                            pbs[i].Value = rcinput.GetPwm(i + 1) - 900;
                        else
                            pbs[i].Value = 0;
                    }

                    Label[] channel_labels = { _lblInterpretation1, _lblInterpretation2, _lblInterpretation3, _lblInterpretation4, };
                    //_lblInterpretation1.Text = "";
                    //_lblInterpretation2.Text = "";
                    //_lblInterpretation3.Text = "";
                    //_lblInterpretation4.Text = "";


                    if (config.channel_ap > 0 && config.channel_ap <= channel_labels.Length)
                    {
                        if (rcinput.GetPwm(config.channel_ap) > 1750)
                            UpdateLabel(channel_labels[config.channel_ap - 1], "Manual mode");
                        else if (rcinput.GetPwm(config.channel_ap) > 1350)
                            UpdateLabel(channel_labels[config.channel_ap - 1], "Stabilized mode");
                        else if (rcinput.GetPwm(config.channel_ap) > 900)
                            UpdateLabel(channel_labels[config.channel_ap - 1], "Autopilot mode");
                        else
                            UpdateLabel(channel_labels[config.channel_ap - 1], "Undetermined");
                    }

                    if (config.channel_motor > 0 && config.channel_motor <= channel_labels.Length)
                    {
                        if (rcinput.GetPwm(config.channel_motor) > 1500)
                            UpdateLabel(channel_labels[config.channel_motor - 1], "Throttle high (" + (ci == null ? "?)" : ci.Throttle.ToString() + "%)"));
                        else if (rcinput.GetPwm(config.channel_motor) > 920)
                            UpdateLabel(channel_labels[config.channel_motor - 1], "Throttle low (" + (ci == null ? "?)" : ci.Throttle.ToString() + "%)"));
                        else if (rcinput.GetPwm(config.channel_motor) > 800)
                            UpdateLabel(channel_labels[config.channel_motor - 1], "Throttle in autopilot failsafe");
                        else
                            UpdateLabel(channel_labels[config.channel_motor - 1], "Undetermined");
                    }

                    if (config.channel_pitch > 0 && config.channel_pitch <= channel_labels.Length)
                    {
                        if (rcinput.GetPwm(config.channel_pitch) > 1750)
                            UpdateLabel(channel_labels[config.channel_pitch - 1], "Pitching UP");
                        else if (rcinput.GetPwm(config.channel_pitch) > 1250)
                            UpdateLabel(channel_labels[config.channel_pitch - 1], "Pithing +- neutral");
                        else if (rcinput.GetPwm(config.channel_pitch) > 900)
                            UpdateLabel(channel_labels[config.channel_pitch - 1], "Pithing DOWN");
                        else
                            UpdateLabel(channel_labels[config.channel_pitch - 1], "Undetermined");
                    }

                    if (config.channel_roll > 0 && config.channel_roll <= channel_labels.Length)
                    {
                        if (rcinput.GetPwm(config.channel_roll) > 1750)
                            UpdateLabel(channel_labels[config.channel_roll - 1], "Rolling RIGHT");
                        else if (rcinput.GetPwm(config.channel_roll) > 1250)
                            UpdateLabel(channel_labels[config.channel_roll - 1], "Roll +- neutral");
                        else if (rcinput.GetPwm(config.channel_roll) > 900)
                            UpdateLabel(channel_labels[config.channel_roll - 1], "Rolling LEFT");
                        else
                            UpdateLabel(channel_labels[config.channel_roll - 1], "Undetermined");
                    }
                };

                try
                {
                    BeginInvoke(m);
                }
                catch
                {
                }
            }
        }

        private void _lblFunctionOut2_Click(object sender, EventArgs e)
        {

        }

        private void _btnReloadFactorySettings_Click(object sender, EventArgs e)
        {
            if (PSTaskDialog.cTaskDialog.ShowTaskDialogBox("Are you sure?", "Are you sure you wish to reload the factory settings?", "Reloading the factory settings will overwrite all your current settings. Please don't forget to re-configure the module step by step.\n\nWhen the factory settings have been reset:",
                "", "", "", "Go to the calibration routine|Skip the calibration routine", "", PSTaskDialog.eTaskDialogButtons.YesNoCancel, PSTaskDialog.eSysIcons.Question, PSTaskDialog.eSysIcons.Question) == DialogResult.Yes)
            {
                serial.SendLoadConfigurationDefault();
                if (PSTaskDialog.cTaskDialog.RadioButtonResult == 0)
                    _btnCaliSensors_Click(this, null);
            }
        }

        private void _btnCaliSensors_Click(object sender, EventArgs e)
        {
            PSTaskDialog.cTaskDialog.PlaySystemSounds = false;
            if (PSTaskDialog.cTaskDialog.ShowTaskDialogBox(
                "Sensor calibration",
                "This step will calibrate your sensors.", "Make sure to do this accurately as it will greatly affect the performance of the stabilized and autopilot flight.\n\nIt is recommended to check the correct angle in the artificial horizon before every flight. If it's not accurate enough, repeat this procedure.",
                "The step will calibrate both accelerometers and gyroscopes. The accelerometers determine the accelerations && gravity vector. The gyroscopes are able to sense the rate of turn. Both are needed for a stabilized and autopilot flight!",
                "", "", "Calibrate accelerometers && gyroscopes|Only calibrate gyroscopes|Only calibrate accelerometers", "", PSTaskDialog.eTaskDialogButtons.OKCancel, PSTaskDialog.eSysIcons.Information, PSTaskDialog.eSysIcons.Information) == DialogResult.OK)
            {
                int rb = PSTaskDialog.cTaskDialog.RadioButtonResult;
                if (rb == 0 || rb == 1)
                {
                    if (PSTaskDialog.cTaskDialog.ShowTaskDialogBox(
                        "Gyroscope calibration",
                        "We will now calibrate the gyroscopes",
                        "Gyroscopes sense the rate of turn. Please make sure the module is not moving and turning!\nPress OK when ready.",
                        "This step will determine the \"zero-rate\" output of the sensors. This value can change after shock, environmental changes, temperature or just time. During flight it also slightly changes, but the calculations compensate for this.",
                        "", "", "", "", PSTaskDialog.eTaskDialogButtons.OKCancel, PSTaskDialog.eSysIcons.Information, PSTaskDialog.eSysIcons.Information) == DialogResult.OK)
                    {
                        serial.SendCalibrateGyros();
                    }
                }
                if (rb == 0 || rb == 2)
                {
                    if (PSTaskDialog.cTaskDialog.ShowTaskDialogBox(
                        "Accelerometer calibration",
                        "We will now calibrate the accelerometers",
                        "Accelerometers sense the earth's gravity vector. Please make sure the module is perfectly parallel with the ground!\nPress OK when ready.",
                        "This step will determine the gravity vector sensed by the accelerometers. It is much more stable than the gyroscopes' output over time.",
                        "", "", "", "", PSTaskDialog.eTaskDialogButtons.OKCancel, PSTaskDialog.eSysIcons.Information, PSTaskDialog.eSysIcons.Information) == DialogResult.OK)
                    {
                        serial.SendCalibrateAcceleros();
                    }
                }
            }
            PSTaskDialog.cTaskDialog.PlaySystemSounds = true;
        }

        private void _hsbCruiseThrottle_Scroll(object sender, ScrollEventArgs e)
        {
            if (!updating_gui)
            {
                serial.SendAutoThrottleConfig(config.auto_throttle_min_pct, config.auto_throttle_max_pct, _hsbCruiseThrottle.Value, config.auto_throttle_p_gain_10, _cbAutothrottle.Checked);
            }
            _lblCruiseThrottle.Text = "(" + _hsbCruiseThrottle.Value.ToString() + "%)";
        }

        private void _cbAutothrottle_CheckedChanged(object sender, EventArgs e)
        {
            _hsbCruiseThrottle_Scroll(sender, null);
        }

        private void _hsRollSensitivity_ValueChanged(object sender, EventArgs e)
        {
            _lblRollSensitivity.Text = "(" + (((double)_hsRollSensitivity.Value) / 10.0).ToString("F1") + ")";
            if (!updating_gui)
            {
                serial.SendPidRoll2Aileron((((double)_hsRollSensitivity.Value) / 10.0),
                    config.pid_roll2aileron_i, config.pid_roll2aileron_d, config.pid_roll2aileron_imin, config.pid_roll2aileron_imax, config.pid_roll2aileron_dmin);
            }
        }

        private void _hsPitchSensitivity_ValueChanged(object sender, EventArgs e)
        {
            _lblPitchSensitivity.Text = "(" + (((double)_hsPitchSensitivity.Value) / 10.0).ToString("F1") + ")";
            if (!updating_gui)
            {
                serial.SendPidPitch2Elevator((((double)_hsPitchSensitivity.Value) / 10.0),
                    config.pid_pitch2elevator_i, config.pid_pitch2elevator_d, config.pid_pitch2elevator_imin, config.pid_pitch2elevator_imax, config.pid_pitch2elevator_dmin);
            }
        }

        private void _btnReadConfig_Click(object sender, EventArgs e)
        {
            ReadConfiguration rc = new ReadConfiguration(serial);
            rc.ShowDialog();
        }

        private void _cbInvert_CheckedChanged(object sender, EventArgs e)
        {
            serial.SendServoReverse(_cbInvert1.Checked, _cbInvert2.Checked, _cbInvert3.Checked, _cbInvert4.Checked, config.servo_reverse[4], config.servo_reverse[5], config.manual_trim);
        }

        private void _cbMixing_SelectedIndexChanged(object sender, EventArgs e)
        {
            int mixing = 0;
            //serial.Send
            if (_cbMixing.SelectedItem.ToString() == "Aileron")
            {
                _lblFunctionOut1.Text = "Aileron 1";
                _lblFunctionOut2.Text = "Aileron 2";
                _lblFunctionOut3.Text = "Elevator";
                _lblFunctionOut4.Text = "Motor";
                mixing = 0;
            }
            if (_cbMixing.SelectedItem.ToString() == "Delta+")
            {
                _lblFunctionOut1.Text = "Elevon 1";
                _lblFunctionOut2.Text = "Elevon 2";
                _lblFunctionOut3.Text = "";
                _lblFunctionOut4.Text = "Motor";
                mixing = 1;
            }
            if (_cbMixing.SelectedItem.ToString() == "Delta-")
            {
                _lblFunctionOut1.Text = "Elevon 1";
                _lblFunctionOut2.Text = "Elevon 2";
                _lblFunctionOut3.Text = "";
                _lblFunctionOut4.Text = "Motor";
                mixing = 2;
            }
            if (_cbMixing.SelectedItem.ToString() == "Aileron-flaperons on yaw")
            {
                _lblFunctionOut1.Text = "Aileron 1";
                _lblFunctionOut2.Text = "Aileron 2";
                _lblFunctionOut3.Text = "Elevator";
                _lblFunctionOut4.Text = "Motor";
                mixing = 4;
            }
            if (config != null && !updating_gui)
                serial.SendControlSettings(mixing, config.control_max_pitch, config.control_min_pitch, config.control_max_roll, config.control_aileron_differential, config.control_waypoint_radius, config.control_cruising_speed, config.control_stabilization_with_altitude_hold, config.control_altitude_mode);
            //else
            //    MessageBox.Show("No configuration has been received from the gluonpilot module, please retry", "Error");
        }

        private void _cbInputFunction_SelectionChanged(object sender, EventArgs e)
        {
            if (!updating_gui)
            {
                // Pitch Roll Yaw Motor Mode (none)
                int channel_ap = 3, channel_motor = 2, channel_pitch = 0, channel_roll = 1;//, channel_yaw = 2;
                int[] channels = { config.channel_pitch, config.channel_roll, config.channel_yaw, config.channel_motor, config.channel_ap };
                if (_cbInputFunction1.SelectedIndex >= 0 && _cbInputFunction1.SelectedIndex < channels.Length)
                    channels[_cbInputFunction1.SelectedIndex] = 1;
                if (_cbInputFunction2.SelectedIndex >= 0 && _cbInputFunction2.SelectedIndex < channels.Length)
                    channels[_cbInputFunction2.SelectedIndex] = 2;
                if (_cbInputFunction3.SelectedIndex >= 0 && _cbInputFunction3.SelectedIndex < channels.Length)
                    channels[_cbInputFunction3.SelectedIndex] = 3;
                if (_cbInputFunction4.SelectedIndex >= 0 && _cbInputFunction4.SelectedIndex < channels.Length)
                    channels[_cbInputFunction4.SelectedIndex] = 4;


                config.channel_ap = channels[channel_ap];
                config.channel_motor = channels[channel_motor];
                config.channel_pitch = channels[channel_pitch];
                config.channel_roll = channels[channel_roll];
                //config.channel_yaw = channels[channel_yaw];
                serial.SendConfigChannels(config.rc_ppm, channels[channel_ap], channels[channel_motor], channels[channel_pitch], channels[channel_roll], config.channel_yaw);
                //serial_AllConfigCommunicationReceived(config);
            }
        }

        private void _lblInterpretation3_Click(object sender, EventArgs e)
        {

        }

        private void _btnBurn_Click(object sender, EventArgs e)
        {
            serial.SendFlashConfiguration();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
