using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Communication.Frames.Incoming;
using System.Globalization;
using Configuration.NavigationCommands;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;


namespace Configuration
{
    public partial class NavigationInstructionEdit : Form
    {
        NavigationInstruction ni;

        public NavigationInstruction NavigationInstr
        {
            get { return ni; }

        }

        public NavigationInstructionEdit(NavigationInstruction ni, double homelat, double homelng)
        {
            InitializeComponent();
            this.ni = ni;

            if ((int)ni.opcode > Enum.GetValues(typeof(NavigationInstruction.navigation_command)).Cast<int>().Max())
                ni.opcode = 0;

            /*
                0. EMPTY
                GOTO
                CLIMB
                FROM_TO
                FLY_TO
                5. CIRCLE
                IF()
                UNTIL()
                SERVO_SET(channel, position_us)
                SERVO_TRIGGER(channel, position_us, hold_sec)
                10 BLOCK
                */
            if (ni.opcode == NavigationInstruction.navigation_command.BLOCK)
                _cb_opcode.SelectedIndex = GetIndexFor("BLOCK");
            else if (ni.opcode == NavigationInstruction.navigation_command.FLARE_TO_ABS ||
                     ni.opcode == NavigationInstruction.navigation_command.FLARE_TO_REL)
                _cb_opcode.SelectedIndex = GetIndexFor("FLARE_TO");
            else if (ni.opcode == NavigationInstruction.navigation_command.GLIDE_TO_ABS ||
                     ni.opcode == NavigationInstruction.navigation_command.GLIDE_TO_REL)
                _cb_opcode.SelectedIndex = GetIndexFor("GLIDE_TO");
            else if (ni.opcode == NavigationInstruction.navigation_command.CIRCLE_TO_ABS ||
                     ni.opcode == NavigationInstruction.navigation_command.CIRCLE_TO_REL)
                _cb_opcode.SelectedIndex = GetIndexFor("CIRCLE_TO");
            else if (ni.opcode == NavigationInstruction.navigation_command.CIRCLE_ABS ||
                     ni.opcode == NavigationInstruction.navigation_command.CIRCLE_REL)
                _cb_opcode.SelectedIndex = GetIndexFor("CIRCLE");
            else if (ni.opcode == NavigationInstruction.navigation_command.CLIMB)
                _cb_opcode.SelectedIndex = GetIndexFor("CLIMB");
            else if (ni.opcode == NavigationInstruction.navigation_command.EMPTY)
                _cb_opcode.SelectedIndex = GetIndexFor("EMPTY");
            else if (ni.opcode == NavigationInstruction.navigation_command.FLY_TO_ABS ||
                     ni.opcode == NavigationInstruction.navigation_command.FLY_TO_REL)
                _cb_opcode.SelectedIndex = GetIndexFor("FLY_TO");
            else if (ni.opcode == NavigationInstruction.navigation_command.FROM_TO_ABS ||
                        ni.opcode == NavigationInstruction.navigation_command.FROM_TO_REL)
                _cb_opcode.SelectedIndex = GetIndexFor("FROM_TO");
            else if (ni.opcode == NavigationInstruction.navigation_command.GOTO)
                _cb_opcode.SelectedIndex = GetIndexFor("GOTO");
            else if (ni.opcode == NavigationInstruction.navigation_command.SERVO_SET)
                _cb_opcode.SelectedIndex = GetIndexFor("SERVO_SET");
            else if (ni.opcode == NavigationInstruction.navigation_command.SERVO_TRIGGER)
                _cb_opcode.SelectedIndex = GetIndexFor("SERVO_TRIGGER");
            else if (ni.opcode == NavigationInstruction.navigation_command.IF_EQ ||
                     ni.opcode == NavigationInstruction.navigation_command.IF_NE ||
                     ni.opcode == NavigationInstruction.navigation_command.IF_GR ||
                     ni.opcode == NavigationInstruction.navigation_command.IF_SM)
                _cb_opcode.SelectedIndex = GetIndexFor("IF");
            else if (ni.opcode == NavigationInstruction.navigation_command.UNTIL_EQ ||
                     ni.opcode == NavigationInstruction.navigation_command.UNTIL_NE ||
                     ni.opcode == NavigationInstruction.navigation_command.UNTIL_GR ||
                     ni.opcode == NavigationInstruction.navigation_command.UNTIL_SM)
                _cb_opcode.SelectedIndex = GetIndexFor("UNTIL");
            else if (ni.opcode == NavigationInstruction.navigation_command.SET_LOITER_POSITION)
                _cb_opcode.SelectedIndex = GetIndexFor("SET_LOITER_POSITION");
            else if (ni.opcode == NavigationInstruction.navigation_command.LOITER_CIRCLE)
                _cb_opcode.SelectedIndex = GetIndexFor("LOITER_CIRCLE");
            else if (ni.opcode == NavigationInstruction.navigation_command.SET_BATTERY_ALARM)
                _cb_opcode.SelectedIndex = GetIndexFor("SET_BATTERY_ALARM");
            else
                _cb_opcode.SelectedIndex = GetIndexFor("EMPTY");
            //_cb_opcode.SelectedIndex = (int)ni.opcode;
        }

        private int GetIndexFor(string name)
        {
            for (int i = 0; i < _cb_opcode.Items.Count; i++)
            {
                if (name == _cb_opcode.Items[i].ToString())
                    return i;
            }
            return 0; // EMPTY
        }

        private void _btn_cancel_Click(object sender, EventArgs e)
        {
            //this.ni = copy_ni;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void _btn_ok_Click(object sender, EventArgs e)
        {
            ni = ((INavigationCommandViewer)tableLayoutPanel.Controls[0]).GetNavigationInstruction();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void _cb_opcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_gb_edit.Enabled = true;

            //ni.opcode = (NavigationInstruction.navigation_command)_cb_opcode.SelectedIndex;

            // already a navigationcommand on the panel? delete it so we can add a new one.
            if (tableLayoutPanel.Controls.Count > 0)
                tableLayoutPanel.Controls.RemoveAt(tableLayoutPanel.Controls.Count - 1);

            string directoryname = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Documentation";
            if (GluonCS.Properties.Settings.Default.Language == "Deutsch")
                directoryname = directoryname + "\\de";
            else
                directoryname = directoryname + "\\en";

            // Add the correct usercontrol
            Control c;
            if (_cb_opcode.Text == "CIRCLE")
            {
                webBrowser.Navigate(directoryname + "\\circle.html");
                c = new NavigationCommands.Circle(ni);
            }
            else if (_cb_opcode.Text.StartsWith("GOTO"))
            {
                webBrowser.Navigate(directoryname + "\\goto.html");
                c = new NavigationCommands.Goto(ni);
            }
            else if (_cb_opcode.Text.StartsWith("CLIMB"))
            {
                webBrowser.Navigate(directoryname + "\\climb.html");
                c = new NavigationCommands.Climb(ni);
            }
            else if (_cb_opcode.Text.StartsWith("SERVO_SET"))
                c = new NavigationCommands.ServoSet(ni);
            else if (_cb_opcode.Text.StartsWith("SERVO_TRIGGER"))
                c = new NavigationCommands.ServoTrigger(ni);
            else if (_cb_opcode.Text.StartsWith("FLY_TO"))
            {
                webBrowser.Navigate(directoryname + "\\flyto.html");
                c = new NavigationCommands.FlyTo(ni);
            }
            else if (_cb_opcode.Text.StartsWith("FROM_TO"))
            {
                webBrowser.Navigate(directoryname + "\\fromto.html");
                c = new NavigationCommands.FromTo(ni);
            }
            else if (_cb_opcode.Text.StartsWith("FLARE_TO"))
            {
                webBrowser.Navigate(directoryname + "\\flareto.html");
                c = new NavigationCommands.FlareTo(ni);
            }
            else if (_cb_opcode.Text.StartsWith("GLIDE_TO"))
            {
                webBrowser.Navigate(directoryname + "\\glideto.html");
                c = new NavigationCommands.GlideTo(ni);
            }
            else if (_cb_opcode.Text.StartsWith("CIRCLE_TO"))
            {
                webBrowser.Navigate(directoryname + "\\circleto.html");
                c = new NavigationCommands.CircleTo(ni);
            }
            else if (_cb_opcode.Text.StartsWith("IF"))
            {
                webBrowser.Navigate(directoryname + "\\if.html");
                c = new NavigationCommands.If(ni);
            }
            else if (_cb_opcode.Text.StartsWith("UNTIL"))
            {
                webBrowser.Navigate(directoryname + "\\until.html");
                c = new NavigationCommands.Until(ni);
            }
            else if (_cb_opcode.Text.StartsWith("BLOCK"))
            {
                c = new NavigationCommands.Block(ni);
                webBrowser.Navigate(directoryname + "\\block.html");
            }
            else if (_cb_opcode.Text.StartsWith("SET_BATTERY_ALARM"))
            {
                c = new NavigationCommands.BattAlarm(ni);
                webBrowser.Navigate(directoryname + "\\setbatteryalarm.html");
            }
            else if (_cb_opcode.Text.StartsWith("EMPTY"))
            {
                c = new NavigationCommands.Empty(ni);
                webBrowser.Navigate(directoryname + "\\emptycmd.html");
            }
            else if (_cb_opcode.Text.StartsWith("SET_LOITER_POSITION"))
            {
                c = new NavigationCommands.SetLoiterPosition(ni);
                webBrowser.Navigate(directoryname + "\\setloiterposition.html");
            }
            else if (_cb_opcode.Text.StartsWith("LOITER_CIRCLE"))
            {
                c = new NavigationCommands.LoiterCircle(ni);
                webBrowser.Navigate(directoryname + "\\loitercircle.html");
            }
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FLY_TO_REL)
                c = new NavigationCommands.FlyToRel(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.CIRCLE_ABS)
                c = new NavigationCommands.CircleAbs(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FLY_TO_ABS)
                c = new NavigationCommands.FlyToAbs(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FROM_TO_REL)
                c = new NavigationCommands.FromToRel(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FROM_TO_ABS)
                c = new NavigationCommands.FromToAbs(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.UNTIL_GR)
                c = new NavigationCommands.UntilGr(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.UNTIL_SM)
                c = new NavigationCommands.UntilSm(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.UNTIL_EQ)
                c = new NavigationCommands.UntilEq(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.UNTIL_NE)
                c = new NavigationCommands.UntilNe(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.IF_GR)
                c = new NavigationCommands.IfGr(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.IF_SM)
                c = new NavigationCommands.IfSm(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.IF_EQ)
                c = new NavigationCommands.IfEq(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.IF_NE)
                c = new NavigationCommands.IfNe(ni);
            else //if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.EMPTY)
            {
                webBrowser.Navigate(directoryname + "\\emptycmd.html");
                c = new NavigationCommands.Empty(ni);
            }

            // add our edit-control
            tableLayoutPanel.Controls.Add(c);
            tableLayoutPanel.SetCellPosition(c, new TableLayoutPanelCellPosition(0, 1));

            c.Anchor = AnchorStyles.None;
            //groupBox1.Height = tableLayoutPanel.Height + 30;
            //groupBox1.Width = tableLayoutPanel.Width + 20;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void _gbParameters_Resize(object sender, EventArgs e)
        {
            // Do some lay-outin'
            webBrowser.Left = _gbParameters.Left + _gbParameters.Width + 20;
            webBrowser.Width = this.Width - webBrowser.Left - 20;
        }

        private void NavigationInstructionEdit_Resize(object sender, EventArgs e)
        {
            _gbParameters_Resize(null, EventArgs.Empty);
        }
    }
}
