using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Configuration
{
    public partial class AskHome : Form
    {
        public AskHome()
        {
            InitializeComponent();
            _ce.SetCoordinateRad(
                Properties.Settings.Default.home_latitude,
                Properties.Settings.Default.home_longitude);
        }

        public double GetLatitudeRad()
        {
            return _ce.GetLatitudeRad();
        }

        public double GetLongitudeRad()
        {
            return _ce.GetLongitudeRad();
        }

        private void _btn_ok_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.home_latitude = _ce.GetLatitudeRad();
            Properties.Settings.Default.home_longitude = _ce.GetLongitudeRad();
            Properties.Settings.Default.Save();
        }
    }
}
