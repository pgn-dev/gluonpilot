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
    public partial class Climb : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public Climb(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.x = (int)_dtb_height.DistanceM;
            return ni;
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            _dtb_height.DistanceM = ni.x;
            ni.opcode = NavigationInstruction.navigation_command.CLIMB;
        }

        #endregion
    }
}
