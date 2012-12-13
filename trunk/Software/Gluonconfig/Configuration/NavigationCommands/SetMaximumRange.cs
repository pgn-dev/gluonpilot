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
    public partial class SetMaximumRange : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public SetMaximumRange(NavigationInstruction ni)
        {
            InitializeComponent();
            //below = _lblWarning.Text;
            SetNavigationInstruction(ni);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.opcode = NavigationInstruction.navigation_command.SET_MAXIMUM_RANGE;
            ni.x = _dtb_max_distance.DistanceM;
            ni.a = _cbPanicLine.SelectedIndex;
            return new NavigationInstruction(ni);
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            try
            {
                _dtb_max_distance.DistanceM = ni.x;
                _cbPanicLine.SelectedIndex = ni.a;
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

    }
}
