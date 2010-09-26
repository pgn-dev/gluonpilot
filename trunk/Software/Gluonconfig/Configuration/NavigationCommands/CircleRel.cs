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
    public partial class CircleRel : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public CircleRel(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        public NavigationInstruction GetNavigationInstruction()
        {
            double x, y;
            int a, b;
            y = distanceTextBoxEast.DistanceM;
            x = distanceTextBoxNorth.DistanceM;
            a = (int) distanceTextBoxRadius.DistanceM;
            b = (int) distanceTextBoxHeight.DistanceM;
            return new NavigationInstruction(
                ni.line, NavigationInstruction.navigation_command.CIRCLE_REL,
                x, y, a, b);
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            distanceTextBoxNorth.DistanceM = ni.x;
            distanceTextBoxEast.DistanceM = ni.y;
            distanceTextBoxRadius.DistanceM = ni.a;
            distanceTextBoxHeight.DistanceM = ni.b;
        }
    }
}
