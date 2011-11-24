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
    public partial class BattAlarm : UserControl, INavigationCommandViewer
    {
        private NavigationInstruction ni;
        private string below;

        public BattAlarm(NavigationInstruction ni)
        {
            InitializeComponent();
            //below = _lblWarning.Text;
            SetNavigationInstruction(ni);
            _ntbWarning_TextChanged(this, EventArgs.Empty);
            _ntbPanic_TextChanged(this, EventArgs.Empty);
        }

        #region INavigationCommandViewer Members

        public NavigationInstruction GetNavigationInstruction()
        {
            ni.opcode = NavigationInstruction.navigation_command.SET_BATTERY_ALARM;
            ni.x = _ntbWarning.DoubleValue;
            ni.y = _ntbPanic.DoubleValue;
            ni.a = _cbPanicLine.SelectedIndex;
            return new NavigationInstruction(ni);
        }

        public void SetNavigationInstruction(NavigationInstruction ni)
        {
            this.ni = ni;
            try
            {
                _ntbPanic.DoubleValue = ni.y;
                _ntbWarning.DoubleValue = ni.x;
                _cbPanicLine.SelectedIndex = ni.a;
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        private void _ntbWarning_TextChanged(object sender, EventArgs e)
        {
            //_lblWarning.Text = below + " " + _ntbWarning.DoubleValue;
            //_lblVerWarning.Text = _ntbWarning.DoubleValue + "V\n|\n|\n|\n|\n|";
        }

        private void _ntbPanic_KeyDown(object sender, KeyEventArgs e)
        {
            //_lblPanic.Text = below + " " + _ntbWarning.DoubleValue;
        }

        private void _ntbPanic_TextChanged(object sender, EventArgs e)
        {
            //_lblPanic.Text = below + " " + _ntbPanic.DoubleValue;
            //_lblVerPanic.Text = _ntbPanic.DoubleValue + "V\n|\n|\n|\n|\n|";
        }
    }
}
