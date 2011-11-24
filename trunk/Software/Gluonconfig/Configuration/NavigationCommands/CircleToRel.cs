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
    public partial class CircleToRel : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public CircleToRel(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        public NavigationInstruction GetNavigationInstruction()
        {
            double x, y;
            int b;
            y = distanceTextBoxEast.DistanceM;
            x = distanceTextBoxNorth.DistanceM;
            b = (int)distanceTextBoxHeight.DistanceM;
            return new NavigationInstruction(
                ni.line, NavigationInstruction.navigation_command.CIRCLE_TO_REL,
                x, y, 0, b);
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            distanceTextBoxNorth.DistanceM = ni.x;
            distanceTextBoxEast.DistanceM = ni.y;
            distanceTextBoxHeight.DistanceM = ni.b;
        }
    }
}
