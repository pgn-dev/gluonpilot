using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Communication.Frames.Incoming;

namespace Configuration.NavigationCommands
{
    public partial class NavigationCommandEditor : Form
    {
        NavigationInstruction ni;

        public NavigationCommandEditor(NavigationInstruction ni)
        {
            InitializeComponent();
            this.ni = ni;
            _cb_opcode.SelectedIndex = (int)ni.opcode;
        }

        private void _cb_opcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel.Controls.Clear();
            ni.opcode = (NavigationInstruction.navigation_command)_cb_opcode.SelectedIndex;

            if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.CIRCLE_REL)
                flowLayoutPanel.Controls.Add(new NavigationCommands.CircleRel(ni));
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FLY_TO_REL)
                flowLayoutPanel.Controls.Add(new NavigationCommands.FlyToRel(ni));
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.CIRCLE_ABS)
                flowLayoutPanel.Controls.Add(new NavigationCommands.CircleAbs(ni));
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FLY_TO_ABS)
                flowLayoutPanel.Controls.Add(new NavigationCommands.FlyToAbs(ni));
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FROM_TO_REL)
                flowLayoutPanel.Controls.Add(new NavigationCommands.FromToRel(ni));
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.GOTO)
                flowLayoutPanel.Controls.Add(new NavigationCommands.Goto(ni));
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FROM_TO_ABS)
                flowLayoutPanel.Controls.Add(new NavigationCommands.FromToAbs(ni));
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.CLIMB)
                flowLayoutPanel.Controls.Add(new NavigationCommands.Climb(ni));
        }

        private void _btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _btn_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public NavigationInstruction GetNavigationInstruction()
        {
            if (flowLayoutPanel.Controls.Count == 1)
                return ((INavigationCommandViewer)flowLayoutPanel.Controls[0]).GetNavigationInstruction();
            else
                return ni;
        }
    }
}
