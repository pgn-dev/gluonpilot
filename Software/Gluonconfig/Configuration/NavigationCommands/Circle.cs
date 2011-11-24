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
    public partial class Circle : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public Circle(NavigationInstruction ni)
        {
            InitializeComponent();
            if (ni.b == 0) // altitude
                ni.b = (int) GluonCS.Properties.Settings.Default.DefaultAltitudeM;
            if (ni.a == 0) // radius
                ni.a = (int)GluonCS.Properties.Settings.Default.DefaultCircleRadius;
            SetNavigationInstruction(ni);
        }

        private void _cbRelToHome_CheckedChanged(object sender, EventArgs e)
        {
            if (_cbRelToHome.Checked)
                ni.opcode = NavigationInstruction.navigation_command.CIRCLE_REL;
            else
                ni.opcode = NavigationInstruction.navigation_command.CIRCLE_ABS;
            SetNavigationInstruction(ni);
        }

#region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            
            return ((INavigationCommandViewer)tableLayoutPanel.Controls[0]).GetNavigationInstruction();
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            tableLayoutPanel.Controls.Clear();
            if (ni.HasRelativeCoordinates())
                ni.opcode = NavigationInstruction.navigation_command.CIRCLE_REL;
            else
                ni.opcode = NavigationInstruction.navigation_command.CIRCLE_ABS;

            if (ni.opcode == NavigationInstruction.navigation_command.CIRCLE_REL)
            {
                tableLayoutPanel.Controls.Add(new CircleRel(ni));
                _cbRelToHome.Checked = true;
            }
            else
            {
                tableLayoutPanel.Controls.Add(new CircleAbs(ni));
                _cbRelToHome.Checked = false;
            }
            this.Width = tableLayoutPanel.Controls[0].Width;
        }

#endregion

    }
}
