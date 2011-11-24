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
    public partial class LoiterCircle : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public LoiterCircle(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.a = (int)_dtb_radius.DistanceM;
            //ni.b = (int)_dtb_height.DistanceM;
            return new NavigationInstruction(ni);
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            _dtb_radius.DistanceM = ni.a;
            //_dtb_height.DistanceM = ni.b;
            ni.opcode = NavigationInstruction.navigation_command.LOITER_CIRCLE;
            _dtb_radius_DistanceChanged(null, EventArgs.Empty);
        }

        #endregion


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
