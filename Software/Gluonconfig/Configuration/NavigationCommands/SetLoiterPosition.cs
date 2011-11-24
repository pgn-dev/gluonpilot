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
    public partial class SetLoiterPosition : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public SetLoiterPosition(NavigationInstruction ni)
        {
            InitializeComponent();
            this.ni = ni;
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.opcode = NavigationInstruction.navigation_command.SET_LOITER_POSITION;
            return ni;
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
        }

        #endregion
    }
}
