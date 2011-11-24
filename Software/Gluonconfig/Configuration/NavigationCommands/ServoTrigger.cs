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
    public partial class ServoTrigger : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;

        public ServoTrigger(NavigationInstruction ni)
        {
            InitializeComponent();
            SetNavigationInstruction(ni);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.a = (int)_nud_channel.Value - 1;
            ni.b = (int)_nud_us.Value;
            ni.x = ((double)_nud_position_hold.Value) / 1000.0;
            ni.opcode = NavigationInstruction.navigation_command.SERVO_TRIGGER;
            return ni;
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            _nud_channel.Value = Math.Min(7, ni.a) + 1;
            _nud_us.Value = Math.Min(2500, Math.Max(500, ni.b));
            _nud_position_hold.Value = (int)(Math.Max(0.001, Math.Min(3, ni.x)) * 1000.0);
        }

        #endregion
    }
}
