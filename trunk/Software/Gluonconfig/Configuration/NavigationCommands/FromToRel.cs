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
    public partial class FromToRel : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public FromToRel(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.x = _dtb_north.DistanceM;
            ni.y = _dtb_east.DistanceM;
            ni.a = (int)_dtb_height.DistanceM;
            return ni;
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            _dtb_north.DistanceM = ni.x;
            _dtb_east.DistanceM = ni.y;
            _dtb_height.DistanceM = ni.a;
        }

        #endregion
    }
}
