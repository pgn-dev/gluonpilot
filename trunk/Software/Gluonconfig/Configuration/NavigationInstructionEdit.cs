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


namespace Configuration
{
    public partial class NavigationInstructionEdit : Form
    {
        NavigationInstruction ni;
        NavigationInstruction copy_ni;

        public NavigationInstructionEdit(NavigationInstruction ni)
        {
            InitializeComponent();
            this.ni = ni;
            copy_ni = new NavigationInstruction(ni);

            if ((int)ni.opcode > 25)
                ni.opcode = 0;
            _cb_opcode.SelectedIndex = (int)ni.opcode;

            _tb_a.Text = ni.a.ToString();
            _tb_b.Text = ni.b.ToString();
            _tb_x.Text = ni.x.ToString(CultureInfo.InvariantCulture);
            _tb_y.Text = ni.y.ToString(CultureInfo.InvariantCulture);

            if (ni.opcode == NavigationInstruction.navigation_command.FROM_TO_ABS ||
                ni.opcode == NavigationInstruction.navigation_command.FLY_TO_ABS ||
                ni.opcode == NavigationInstruction.navigation_command.CIRCLE_ABS)
            {
                _tb_x.Text = RAD2DEG(ni.x).ToString("F5", CultureInfo.InvariantCulture);
                _tb_y.Text = RAD2DEG(ni.y).ToString("F5", CultureInfo.InvariantCulture);
            }

            // index normally starts at 0
            if (ni.opcode == NavigationInstruction.navigation_command.GOTO)
                _tb_a.Text = (ni.a + 1).ToString();
        }

        private double RAD2DEG(double x)
        {
            return x / 3.14159 * 180.0;
        }
        private double DEG2RAD(double x)
        {
            return x / 180.0 * 3.14159;
        }


        private void _btn_cancel_Click(object sender, EventArgs e)
        {
            //this.ni = copy_ni;
            this.Close();
        }

        private void _btn_ok_Click(object sender, EventArgs e)
        {
            ni.a = int.Parse(_tb_a.Text);
            ni.b = int.Parse(_tb_b.Text);
            ni.x = double.Parse(_tb_x.Text, CultureInfo.InvariantCulture);
            ni.y = double.Parse(_tb_y.Text, CultureInfo.InvariantCulture);
            ni.opcode = (NavigationInstruction.navigation_command)_cb_opcode.SelectedIndex;


            if (ni.opcode == NavigationInstruction.navigation_command.FROM_TO_ABS ||
                ni.opcode == NavigationInstruction.navigation_command.FLY_TO_ABS ||
                ni.opcode == NavigationInstruction.navigation_command.CIRCLE_ABS)
            {
                ni.x = DEG2RAD(ni.x);
                ni.y = DEG2RAD(ni.y);
            }

            // index normally starts at 0
            if (ni.opcode == NavigationInstruction.navigation_command.GOTO)
                ni.a -= 1;

            this.Close();
        }

        private void _cb_opcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _tb_x.Enabled = true;
            _tb_y.Enabled = true;
            _tb_a.Enabled = true;
            _tb_b.Enabled = true;

            switch ((NavigationInstruction.navigation_command)_cb_opcode.SelectedIndex)
            {
                case NavigationInstruction.navigation_command.CIRCLE_ABS:
                    _lbl_x.Text = "Latitude (°)";
                    _lbl_y.Text = "Longitude (°)";
                    _lbl_a.Text = "Radius (m)";
                    _lbl_b.Text = "Height (m)";
                    break;
                case NavigationInstruction.navigation_command.CIRCLE_REL:
                    _lbl_x.Text = "Latitude (m)";
                    _lbl_y.Text = "Longitude (m)";
                    _lbl_a.Text = "Radius (m)";
                    _lbl_b.Text = "Height (m)";
                    break;
                case NavigationInstruction.navigation_command.CLIMB:
                    _lbl_x.Text = "Height (m)";
                    _lbl_y.Text = "";
                    _lbl_a.Text = "";
                    _lbl_b.Text = "";
                    _tb_y.Enabled = false;
                    _tb_a.Enabled = false;
                    _tb_b.Enabled = false;
                    break;
                case NavigationInstruction.navigation_command.FLY_TO_ABS:
                    _lbl_x.Text = "Latitude (°)";
                    _lbl_y.Text = "Longitude (°)";
                    _lbl_a.Text = "Height (m)";
                    _tb_b.Enabled = false;
                    _lbl_b.Text = "";
                    break;
                case NavigationInstruction.navigation_command.FLY_TO_REL:
                    _lbl_x.Text = "Latitude (m)";
                    _lbl_y.Text = "Longitude (m)";
                    _lbl_a.Text = "Height (m)";
                    _tb_b.Enabled = false;
                    _lbl_b.Text = "";
                    break;
                case NavigationInstruction.navigation_command.GOTO:
                    _tb_x.Enabled = false;
                    _tb_y.Enabled = false;
                    _tb_b.Enabled = false;
                    _lbl_x.Text = "";
                    _lbl_y.Text = "";
                    _lbl_a.Text = "Line #";
                    _lbl_b.Text = "";
                    break;
                default:
                    break;
            }


        }
    }
}
