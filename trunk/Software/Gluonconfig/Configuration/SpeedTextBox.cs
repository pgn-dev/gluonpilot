using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Configuration
{
    public partial class SpeedTextBox : UserControl
    {
        private double current_speed_ms;

        public event EventHandler SpeedChanged;

        public SpeedTextBox()
        {
            InitializeComponent();

            if (Properties.Settings.Default.SpeedUnit == "m/s")
                cb_unit.SelectedIndex = 0;
            else if (Properties.Settings.Default.SpeedUnit == "km/h")
                cb_unit.SelectedIndex = 1;
            else
                cb_unit.SelectedIndex = 2;

            Properties.Settings.Default.Save();
        }

        [BrowsableAttribute(true)]
        public double SpeedMS
        {
            get
            {
                if (cb_unit.SelectedIndex == 0) // m/s
                    return tb_speed.DoubleValue;
                else if (cb_unit.SelectedIndex == 1) // km/h
                    return tb_speed.DoubleValue / 3.6;
                else // mph
                    return tb_speed.DoubleValue / (3.6 * 0.621371192);
            }
            set
            {
                current_speed_ms = value;
                cb_unit_SelectedIndexChanged(null, null);
            }
        }
        

        private void cb_unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_unit.SelectedIndex == 0) // m/s
            {
                Properties.Settings.Default.SpeedUnit = "m/s";
                tb_speed.Text = current_speed_ms.ToString(CultureInfo.InvariantCulture);
            }
            else if (cb_unit.SelectedIndex == 1) // km/h
            {
                Properties.Settings.Default.SpeedUnit = "km/h";
                tb_speed.Text = (current_speed_ms * 3.6).ToString(CultureInfo.InvariantCulture);
            }
            else // mph
            {
                Properties.Settings.Default.SpeedUnit = "mph";
                tb_speed.Text = (current_speed_ms * (3.6 * 0.621371192)).ToString(CultureInfo.InvariantCulture);
            }

            Properties.Settings.Default.Save();
        }

        private void cb_unit_DropDown(object sender, EventArgs e)
        {
            current_speed_ms = SpeedMS;
        }

        private void tb_speed_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void tb_speed_TextChanged(object sender, EventArgs e)
        {
            if (SpeedChanged != null)
                SpeedChanged(e, null);
        }
    }
}
