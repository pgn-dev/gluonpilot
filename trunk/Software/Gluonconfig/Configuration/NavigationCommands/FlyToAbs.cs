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
    public partial class FlyToAbs : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public FlyToAbs(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni = new NavigationInstruction(ni);
            ni.a = (int)_dtb_height.DistanceM;
            ni.x = _ce.GetLatitudeRad();
            ni.y = _ce.GetLongitudeRad();
            return ni;
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            _dtb_height.DistanceM = ni.a;
            _ce.SetCoordinateRad(ni.x, ni.y);
        }

        #endregion
    }
}
