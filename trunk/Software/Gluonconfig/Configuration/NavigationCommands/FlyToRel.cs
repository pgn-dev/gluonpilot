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
    public partial class FlyToRel : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public FlyToRel(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        public NavigationInstruction GetNavigationInstruction()
        {
            double x, y;
            int a;
            y = distanceTextBoxEast.DistanceM;
            x = distanceTextBoxNorth.DistanceM;
            a = (int)distanceTextBoxHeight.DistanceM;
            return new NavigationInstruction(
                ni.line, NavigationInstruction.navigation_command.FLY_TO_REL,
                x, y, a, 0);
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            distanceTextBoxNorth.DistanceM = ni.x;
            distanceTextBoxEast.DistanceM = ni.y;
            distanceTextBoxHeight.DistanceM = ni.a;
        }
    }
}
