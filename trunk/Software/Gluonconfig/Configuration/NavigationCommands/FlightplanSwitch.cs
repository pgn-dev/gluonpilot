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
    public partial class FlightplanSwitch : UserControl, INavigationCommandViewer
    {
        public FlightplanSwitch()
        {
            InitializeComponent();
        }


        private NavigationInstruction ni;
        private string below;

        public FlightplanSwitch(NavigationInstruction ni)
        {
            InitializeComponent();
            //below = _lblWarning.Text;
            SetNavigationInstruction(ni);


        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.opcode = NavigationInstruction.navigation_command.SET_FLIGHTPLAN_SWITCH;

            ni.a = _cbChannel.SelectedIndex;
            ni.b = (int)_nud_14.Value;
            ni.x = (double)_nud_15.Value;
            ni.y = (double)_nud_16.Value;

            return new NavigationInstruction(ni);
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            try
            {
                ni.opcode = NavigationInstruction.navigation_command.SET_FLIGHTPLAN_SWITCH;
                _cbChannel.SelectedIndex = ni.a;
                _nud_14.Value = Math.Min(Math.Max(ni.b, _nud_14.Minimum), _nud_14.Maximum);
                _nud_15.Value = Math.Min(Math.Max((int)ni.x, _nud_15.Minimum), _nud_14.Maximum);
                _nud_16.Value = Math.Min(Math.Max((int)ni.y, _nud_16.Minimum), _nud_14.Maximum);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
