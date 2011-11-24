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
    public partial class ServoSet : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public ServoSet(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.a = (int)_nud_channel.Value - 1;
            ni.b = (int)_nud_us.Value;
            ni.opcode = NavigationInstruction.navigation_command.SERVO_SET;
            return ni;
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            _nud_channel.Value = Math.Min(7, ni.a) + 1;
            _nud_us.Value = Math.Min(2500, Math.Max(500, ni.b));
        }

        #endregion
    }
}
