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
            a = (int) _dtb_radius.DistanceM;
            b = (int) _dtb_altitude.DistanceM;
            return new NavigationInstruction(
                ni.line, NavigationInstruction.navigation_command.CIRCLE_REL,
                x, y, a, b);
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            distanceTextBoxNorth.DistanceM = ni.x;
            distanceTextBoxEast.DistanceM = ni.y;
            _dtb_radius.DistanceM = ni.a;
            _dtb_altitude.DistanceM = ni.b;
            ni.opcode = NavigationInstruction.navigation_command.CIRCLE_REL;

            _dtb_radius_DistanceChanged(null, EventArgs.Empty);
        }

        private void _dtb_radius_DistanceChanged(object sender, EventArgs e)
        {
            if (_dtb_radius.DistanceM < 50)
                _dtb_radius.Color = Color.Red;
            else if (_dtb_radius.DistanceM < 70)
                _dtb_radius.Color = Color.Yellow;
            else
                _dtb_radius.Color = Color.White;

        }
    }
}
