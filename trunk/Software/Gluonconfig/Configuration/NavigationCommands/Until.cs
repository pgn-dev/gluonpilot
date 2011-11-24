using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Communication.Frames.Incoming;

namespace Configuration.NavigationCommands
{
    public partial class Until : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public Until(NavigationInstruction ni)
        {
            InitializeComponent();
            foreach (string s in Global.Variables)
                _cb_variables.Items.Add(s);
            SetNavigationInstruction(ni);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.a = _cb_variables.SelectedIndex + 1;
            ni.x = _ntb.DoubleValue;
            if (_cbOperator.Text == "=")
                ni.opcode = NavigationInstruction.navigation_command.UNTIL_EQ;
            else if (_cbOperator.Text == "<>")
                ni.opcode = NavigationInstruction.navigation_command.UNTIL_NE;
            else if (_cbOperator.Text == "<")
                ni.opcode = NavigationInstruction.navigation_command.UNTIL_SM;
            else if (_cbOperator.Text == ">")
                ni.opcode = NavigationInstruction.navigation_command.UNTIL_GR;
            return ni;
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            if (ni.a <= _cb_variables.Items.Count)
                _cb_variables.SelectedIndex = ni.a - 1;
            _ntb.DoubleValue = ni.x;

            if (ni.opcode == NavigationInstruction.navigation_command.UNTIL_EQ)
                _cbOperator.Text = "=";
            else if (ni.opcode == NavigationInstruction.navigation_command.UNTIL_NE)
                _cbOperator.Text = "<>";
            else if (ni.opcode == NavigationInstruction.navigation_command.UNTIL_GR)
                _cbOperator.Text = ">";
            else if (ni.opcode == NavigationInstruction.navigation_command.UNTIL_SM)
                _cbOperator.Text = "<";
        }

        #endregion
    }
}
