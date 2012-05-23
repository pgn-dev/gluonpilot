using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Communication;
using Communication.Frames;
using System.Threading;


namespace Gluonpilot
{
    public partial class EasyConfig : Form
    {
        private SerialCommunication serial;
        Communication.Frames.Configuration.AllConfig config = null;
        Communication.Frames.Configuration.AllConfig config_original = null;
        private Artificial3DHorizon.AI3D aI3D = null;
        private bool guiUpdateBusy = false;
        private Communication.Frames.Incoming.ControlInfo ci = null;

        public EasyConfig()
        {
            InitializeComponent();
        }


        public EasyConfig(SerialCommunication serial) : this()
        {
            this.serial = serial;
            serial.AllConfigCommunicationReceived += new SerialCommunication.ReceiveAllConfigCommunicationFrame(serial_AllConfigCommunicationReceived);
            serial.AttitudeCommunicationReceived += new SerialCommunication.ReceiveAttitudeCommunicationFrame(serial_AttitudeCommunicationReceived);
            serial.RcInputCommunicationReceived += new SerialCommunication.ReceiveRcInputCommunicationFrame(serial_RcInputCommunicationReceived);
            serial.ControlInfoCommunicationReceived += new SerialCommunication.ReceiveControlInfoCommunicationFrame(serial_ControlInfoCommunicationReceived);
        }

        void serial_ControlInfoCommunicationReceived(Communication.Frames.Incoming.ControlInfo ci)
        {
            this.ci = ci;
        }

        void serial_RcInputCommunicationReceived(Communication.Frames.Incoming.RcInput rcinput)
        {
            if (config != null)
            {
                MethodInvoker m = delegate()
                {
                    ProgressBar[] pbs = { _pbChannel1, _pbChannel2, _pbChannel3, _pbChannel4, _pbChannel5, _pbChannel6 };
                    for (int i = 0; i < 6; i++)
                    {
                        if (rcinput.GetPwm(i + 1) > 900 && rcinput.GetPwm(i + 1) < 2100)
                            pbs[i].Value = rcinput.GetPwm(i + 1) - 900;
                        else
                            pbs[i].Value = 0;
                    }

                    Label[] channel_labels = {_lblInterpretation1, _lblInterpretation2, _lblInterpretation3, _lblInterpretation4, _lblInterpretation5, _lblInterpretation6 };
                    _lblInterpretation1.Text = "";
                    _lblInterpretation2.Text = "";
                    _lblInterpretation3.Text = "";
                    _lblInterpretation4.Text = "";
                    _lblInterpretation5.Text = "";
                    _lblInterpretation6.Text = "";

                    if (config.channel_ap > 0 && config.channel_ap <= channel_labels.Length)
                    {
                        if (rcinput.GetPwm(config.channel_ap) > 1750)
                            channel_labels[config.channel_ap - 1].Text = "Manual mode";
                        else if (rcinput.GetPwm(config.channel_ap) > 1350)
                            channel_labels[config.channel_ap - 1].Text = "Stabilized mode";
                        else if (rcinput.GetPwm(config.channel_ap) > 900)
                            channel_labels[config.channel_ap - 1].Text = "Autopilot mode";
                        else
                            channel_labels[config.channel_ap - 1].Text = "Undetermined";
                    }

                    if (config.channel_motor > 0 && config.channel_motor <= channel_labels.Length)
                    {
                        if (rcinput.GetPwm(config.channel_motor) > 1500)
                            channel_labels[config.channel_motor - 1].Text = "Throttle high (" + (ci == null ? "?)" : ci.Throttle.ToString() + "%)");
                        else if (rcinput.GetPwm(config.channel_motor) > 920)
                            channel_labels[config.channel_motor - 1].Text = "Throttle low (" + (ci == null ? "?)" : ci.Throttle.ToString() + "%)");
                        else if (rcinput.GetPwm(config.channel_motor) > 800)
                            channel_labels[config.channel_motor - 1].Text = "Throttle in autopilot failsafe";
                        else
                            channel_labels[config.channel_motor - 1].Text = "Undetermined";
                    }

                    if (config.channel_pitch > 0 && config.channel_pitch <= channel_labels.Length)
                    {
                        if (rcinput.GetPwm(config.channel_pitch) > 1750)
                            channel_labels[config.channel_pitch - 1].Text = "Pitching UP";
                        else if (rcinput.GetPwm(config.channel_pitch) > 1250)
                            channel_labels[config.channel_pitch - 1].Text = "Pithing +- neutral";
                        else if (rcinput.GetPwm(config.channel_pitch) > 900)
                            channel_labels[config.channel_pitch - 1].Text = "Pithing DOWN";
                        else
                            channel_labels[config.channel_pitch - 1].Text = "Undetermined";
                    }

                    if (config.channel_roll > 0 && config.channel_roll <= channel_labels.Length)
                    {
                        if (rcinput.GetPwm(config.channel_roll) > 1750)
                            channel_labels[config.channel_roll - 1].Text = "Rolling RIGHT";
                        else if (rcinput.GetPwm(config.channel_roll) > 1250)
                            channel_labels[config.channel_roll - 1].Text = "Roll +- neutral";
                        else if (rcinput.GetPwm(config.channel_roll) > 900)
                            channel_labels[config.channel_roll - 1].Text = "Rolling LEFT";
                        else
                            channel_labels[config.channel_roll - 1].Text = "Undetermined";
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

        void serial_AttitudeCommunicationReceived(Communication.Frames.Incoming.Attitude attitude)
        {
            if (aI3D != null)
            {
                aI3D.Roll = attitude.RollDeg / 180.0 * Math.PI;
                aI3D.Pitch = attitude.PitchDeg / 180.0 * Math.PI;
            }
            artificialHorizon1.pitch_angle = attitude.PitchDeg;
            artificialHorizon1.roll_angle = -attitude.RollDeg;
        }


        void serial_AllConfigCommunicationReceived(Communication.Frames.Configuration.AllConfig config)
        {
            if (this.config_original == null)
                config_original = config;

            guiUpdateBusy = true;
            _tmrGuiUpdateBusy.Enabled = true;
            _tmrGuiUpdateBusy.Start();

            this.config = config;

            MethodInvoker m = delegate()
            {
                _cbInvert1.Checked = config.servo_reverse[0];
                _cbInvert2.Checked = config.servo_reverse[1];
                _cbInvert3.Checked = config.servo_reverse[2];
                _cbInvert4.Checked = config.servo_reverse[3];
                _cbInvert5.Checked = config.servo_reverse[4];
                _cbInvert6.Checked = config.servo_reverse[5];

                ComboBox[] cbn = { _cbInputFunction1, _cbInputFunction2, _cbInputFunction3, _cbInputFunction4, _cbInputFunction5, _cbInputFunction6 };
                cbn[0].SelectedIndex = 5;
                cbn[1].SelectedIndex = 5;
                cbn[2].SelectedIndex = 5;
                cbn[3].SelectedIndex = 5;
                cbn[4].SelectedIndex = 5;
                cbn[5].SelectedIndex = 5;

                if (config.channel_ap <= 6 && config.channel_ap > 0)
                    cbn[config.channel_ap - 1].SelectedIndex = 4;
                if (config.channel_motor <= 6 && config.channel_motor > 0)
                    cbn[config.channel_motor - 1].SelectedIndex = 3;
                if (config.channel_pitch <= 6 && config.channel_pitch > 0)
                    cbn[config.channel_pitch - 1].SelectedIndex = 0;
                if (config.channel_roll <= 6 && config.channel_roll > 0)
                    cbn[config.channel_roll - 1].SelectedIndex = 1;
                if (config.channel_yaw <= 6 && config.channel_yaw > 0)
                    cbn[config.channel_yaw - 1].SelectedIndex = 2;

                if (config.control_mixing < _cbMixing.Items.Count)
                    _cbMixing.SelectedIndex = config.control_mixing;

                _hsPitchSensitivity.Value = Math.Max(0, (int)(config.pid_pitch2elevator_p * 10.0));
                _hsRollSensitivity.Value = Math.Max(0, (int)(config.pid_roll2aileron_p * 10.0));
                //_hsRollSensitivity_ValueChanged(null, ScrollEventArgs.Empty);
                //_hsPitchSensitivity_ValueChanged(null, ScrollEventArgs.Empty);

                _cbAutothrottle.Checked = config.auto_throttle_enabled;
                _hsbCruiseThrottle.Value = Math.Max(0, config.auto_throttle_cruise_pct);
                _hsbCruiseThrottle_Scroll(null, new ScrollEventArgs(ScrollEventType.EndScroll, _hsbCruiseThrottle.Value));
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

        private void _btnRead_Click(object sender, EventArgs e)
        {
            serial.ReadAllConfig();
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
                _lblFunctionOut5.Text = "Yaw";
                _lblFunctionOut6.Text = "Camera roll";
                mixing = 0;
            }
            if (_cbMixing.SelectedItem.ToString() == "Delta+")
            {
                _lblFunctionOut1.Text = "Elevon 1";
                _lblFunctionOut2.Text = "Elevon 2";
                _lblFunctionOut3.Text = "";
                _lblFunctionOut4.Text = "Motor";
                _lblFunctionOut5.Text = "";
                _lblFunctionOut6.Text = "";
                mixing = 1;
            }
            if (_cbMixing.SelectedItem.ToString() == "Delta-")
            {
                _lblFunctionOut1.Text = "Elevon 1";
                _lblFunctionOut2.Text = "Elevon 2";
                _lblFunctionOut3.Text = "";
                _lblFunctionOut4.Text = "Motor";
                _lblFunctionOut5.Text = "";
                _lblFunctionOut6.Text = "";
                mixing = 2;
            }
            if (_cbMixing.SelectedItem.ToString() == "Aileron-flaperons on yaw")
            {
                _lblFunctionOut1.Text = "Aileron 1";
                _lblFunctionOut2.Text = "Aileron 2";
                _lblFunctionOut3.Text = "Elevator";
                _lblFunctionOut4.Text = "Motor";
                _lblFunctionOut5.Text = "NO YAW";
                _lblFunctionOut6.Text = "Camera roll";
                mixing = 4;
            }
            if (config != null)
                serial.SendControlSettings(mixing, config.control_max_pitch, config.control_min_pitch, config.control_max_roll, config.control_aileron_differential, config.control_waypoint_radius, config.control_cruising_speed, config.control_stabilization_with_altitude_hold, config.control_altitude_mode);
            else
                MessageBox.Show("No configuration has been received from the gluonpilot module, please retry", "Error");
        }

        private void _btnCalibrateGyroscopes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please make sure the module is not moving", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            serial.SendCalibrateGyros();
        }

        private void _btnCalibrateAccelerometers_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please make sure the module parallel to the ground", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            serial.SendCalibrateAcceleros();
        }

        private void EasyConfig_Load(object sender, EventArgs e)
        {
            aI3D = new Artificial3DHorizon.AI3D("Models\\Funjet\\funjet.x");
            aI3D.Dock = DockStyle.Fill;
            _pl3d.Controls.Add(aI3D);
            Control c = this;
            while (c.BackColor == Color.Transparent)
                c = c.Parent;
            aI3D.BackColor = c.BackColor;
        }


        private void _cbInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (!guiUpdateBusy)
                serial.SendServoReverse(_cbInvert1.Checked, _cbInvert2.Checked, _cbInvert3.Checked, _cbInvert4.Checked, _cbInvert5.Checked, _cbInvert6.Checked, config_original.manual_trim);
        }

        private void _tmrGuiUpdateBusy_Tick(object sender, EventArgs e)
        {
            guiUpdateBusy = false;
        }

        private void _cbInputFunction_SelectionChanged(object sender, EventArgs e)
        {
            if (!guiUpdateBusy)
            {
                // Pitch Roll Yaw Motor Mode (none)
                int channel_ap = 4, channel_motor = 3, channel_pitch = 0, channel_roll = 1, channel_yaw = 2;
                int[] channels = { channel_pitch, channel_roll, channel_yaw, channel_motor, channel_ap };
                if (_cbInputFunction1.SelectedIndex >= 0 && _cbInputFunction1.SelectedIndex < channels.Length)
                    channels[_cbInputFunction1.SelectedIndex] = 1;
                if (_cbInputFunction2.SelectedIndex >= 0 && _cbInputFunction2.SelectedIndex < channels.Length)
                    channels[_cbInputFunction2.SelectedIndex] = 2;
                if (_cbInputFunction3.SelectedIndex >= 0 && _cbInputFunction3.SelectedIndex < channels.Length)
                    channels[_cbInputFunction3.SelectedIndex] = 3;
                if (_cbInputFunction4.SelectedIndex >= 0 && _cbInputFunction4.SelectedIndex < channels.Length)
                    channels[_cbInputFunction4.SelectedIndex] = 4;
                if (_cbInputFunction5.SelectedIndex >= 0 && _cbInputFunction5.SelectedIndex < channels.Length)
                    channels[_cbInputFunction5.SelectedIndex] = 5;
                if (_cbInputFunction6.SelectedIndex >= 0 && _cbInputFunction6.SelectedIndex < channels.Length)
                    channels[_cbInputFunction6.SelectedIndex] = 6;
                config.channel_ap = channels[channel_ap];
                config.channel_motor = channels[channel_motor];
                config.channel_pitch = channels[channel_pitch];
                config.channel_roll = channels[channel_roll];
                config.channel_yaw = channels[channel_yaw];
                serial.SendConfigChannels(config.rc_ppm, channels[channel_ap], channels[channel_motor], channels[channel_pitch], channels[channel_roll], channels[channel_yaw]);
                serial_AllConfigCommunicationReceived(config);
            }
        }

        private void EasyConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            serial.AllConfigCommunicationReceived -= new SerialCommunication.ReceiveAllConfigCommunicationFrame(serial_AllConfigCommunicationReceived);
            serial.AttitudeCommunicationReceived -= new SerialCommunication.ReceiveAttitudeCommunicationFrame(serial_AttitudeCommunicationReceived);
            serial.RcInputCommunicationReceived -= new SerialCommunication.ReceiveRcInputCommunicationFrame(serial_RcInputCommunicationReceived);
        }

        private void _hsRollSensitivity_ValueChanged(object sender, EventArgs e)
        {
            _lblRollSensitivity.Text = "(" + (((double)_hsRollSensitivity.Value) / 10.0).ToString("F1") + ")";
            if (!guiUpdateBusy)
            {
                serial.SendPidRoll2Aileron((((double)_hsRollSensitivity.Value) / 10.0),
                    config.pid_roll2aileron_i, config.pid_roll2aileron_d, config.pid_roll2aileron_imin, config.pid_roll2aileron_imax, config.pid_roll2aileron_dmin);
            }
        }

        private void _hsPitchSensitivity_ValueChanged(object sender, EventArgs e)
        {
            _lblPitchSensitivity.Text = "(" + (((double)_hsPitchSensitivity.Value) / 10.0).ToString("F1") + ")";
            if (!guiUpdateBusy)
            {
                serial.SendPidPitch2Elevator((((double)_hsPitchSensitivity.Value) / 10.0),
                    config.pid_pitch2elevator_i, config.pid_pitch2elevator_d, config.pid_pitch2elevator_imin, config.pid_pitch2elevator_imax, config.pid_pitch2elevator_dmin);
            }
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Resetting original settings", "Please be patient", MessageBoxButtons.
            serial.Send(config_original);
            this.Close();
        }

        private void _btnSaveAndClose_Click(object sender, EventArgs e)
        {
            serial.SendFlashConfiguration();
            this.Close();
        }

        private void _hsbCruiseThrottle_Scroll(object sender, ScrollEventArgs e)
        {
            if (!guiUpdateBusy)
            {
                serial.SendAutoThrottleConfig(config.auto_throttle_min_pct, config.auto_throttle_max_pct, _hsbCruiseThrottle.Value, config.auto_throttle_p_gain_10, _cbAutothrottle.Checked);
            }
            _lblCruiseThrottle.Text = "(" + _hsbCruiseThrottle.Value.ToString() + "%)";
        }

        private void _cbAutothrottle_CheckedChanged(object sender, EventArgs e)
        {
            _hsbCruiseThrottle_Scroll(sender, null);
        }

        private void _btnReloadFactorySettings_Click(object sender, EventArgs e)
        {
            serial.SendLoadConfigurationDefault();
            Thread.Sleep(200);
            serial.ReadAllConfig();
        }
    }
}
