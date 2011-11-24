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
    public partial class Goto : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public Goto(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.a = _ntb_linenr.IntValue - 1;
            ni.opcode = NavigationInstruction.navigation_command.GOTO;
            return new NavigationInstruction(ni);
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            _ntb_linenr.IntValue = ni.a + 1;
        }

        #endregion
    }
}
