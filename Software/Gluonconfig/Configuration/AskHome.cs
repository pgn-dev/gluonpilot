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
                GluonCS.Properties.Settings.Default.HomeLatitude,
                GluonCS.Properties.Settings.Default.HomeLongitude);
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
            GluonCS.Properties.Settings.Default.HomeLatitude = _ce.GetLatitudeRad();
            GluonCS.Properties.Settings.Default.HomeLongitude = _ce.GetLongitudeRad();
            GluonCS.Properties.Settings.Default.Save();
        }
    }
}
