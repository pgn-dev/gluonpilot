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
    public partial class CoordinateEdit : UserControl
    {
        double[] lat = new double[3];
        double[] lon = new double[3];

        public CoordinateEdit()
        {
            InitializeComponent();
        }

        public double GetLatitudeRad()
        {
            double l = (Math.Abs(lat[0]) + lat[1] / 60 + lat[2] / 3600) / 180.0 * 3.14159;
            if (lat[0] < 0)
                l = -l;
            return l;
        }

        public double GetLongitudeRad()
        {
            ParseTextbox(_tb_lon, lon);
            ParseTextbox(_tb_lat, lat);
            double l = (Math.Abs(lon[0]) + lon[1] / 60 + lon[2] / 3600) / 180.0 * 3.14159;
            if (lon[0] < 0)
                l = -l;
            return l;
        }

        public void SetCoordinateRad(double lat, double lon)
        {
            this.lat[0] = lat * 180.0 / 3.14159;
            this.lat[1] = 0; this.lat[2] = 0;
            this.lon[0] = lon * 180.0 / 3.14159;
            this.lon[1] = 0; this.lon[2] = 0;

            _tb_lat.Text = this.lat[0].ToString(CultureInfo.InvariantCulture);
            _tb_lon.Text = this.lon[0].ToString(CultureInfo.InvariantCulture);
            _tb_lon_Leave(null, null);
            _tb_lat_Leave(null, null);
        }



        private void _tb_lat_Leave(object sender, EventArgs e)
        {
            if (_tb_lat.Text.Contains(','))
            {
                string[] p = _tb_lat.Text.Split(',');
                if (p.Length == 2)
                {
                    _tb_lat.Focus();
                    _tb_lat.Text = p[0];
                    _tb_lon.Focus();
                    _tb_lon.Text = p[1];
                    _tb_lat.Focus();
                }
            }
            ParseTextbox(_tb_lat, lat);
        }

        private void _tb_lon_Leave(object sender, EventArgs e)
        {
            ParseTextbox(_tb_lon, lon);
        }

        private void ParseTextbox(TextBox tb, double[] dest)
        {
            tb.ForeColor = Color.Black;

            dest[0] = 0;
            dest[1] = 0;
            dest[2] = 0;

            string[] parts = tb.Text.Split(new char[] { '°', '\'', '\"' });

            if (parts.Length == 1 || parts.Length == 2) // only °
            {
                if (!double.TryParse(parts[0].Replace(" ", ""), NumberStyles.Float, CultureInfo.InvariantCulture, out dest[0]))
                    tb.ForeColor = Color.Red;

                tb.Text = dest[0].ToString(CultureInfo.InvariantCulture) + "°";
            }
            else if (parts.Length == 3) // only ° and '
            {
                if (!double.TryParse(parts[0].Replace(" ", ""), NumberStyles.Float, CultureInfo.InvariantCulture, out dest[0]) ||
                    !double.TryParse(parts[1].Replace(" ", ""), NumberStyles.Float, CultureInfo.InvariantCulture, out dest[1]))
                    tb.ForeColor = Color.Red;


                tb.Text = dest[0].ToString(CultureInfo.InvariantCulture) + "° " + dest[1].ToString(CultureInfo.InvariantCulture) + "' " + dest[2].ToString(CultureInfo.InvariantCulture) + "\"";
            }
            else
            {
                if (!double.TryParse(parts[0].Replace(" ", ""), NumberStyles.Float, CultureInfo.InvariantCulture, out dest[0]) ||
                    !double.TryParse(parts[1].Replace(" ", ""), NumberStyles.Float, CultureInfo.InvariantCulture, out dest[1]) ||
                    !double.TryParse(parts[2].Replace(" ", ""), NumberStyles.Float, CultureInfo.InvariantCulture, out dest[2]))
                    tb.ForeColor = Color.Red;

                tb.Text = dest[0].ToString(CultureInfo.InvariantCulture) + "° " + dest[1].ToString(CultureInfo.InvariantCulture) + "' " + dest[2].ToString(CultureInfo.InvariantCulture) + "\"";
            }

        }

    }
}
