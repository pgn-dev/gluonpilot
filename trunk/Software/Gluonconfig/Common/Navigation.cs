using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Navigation
    {
        public static KeyValuePair<double, double> AbsoluteToRelativeLatLng(double lat_home, double lng_home, double lat, double lng)
        {
            double latitude_meter_per_degree = 6363057.32484 / 180.0 * Math.PI;
            double longitude_meter_per_degree = latitude_meter_per_degree * Math.Cos(lat_home / 180.0 * Math.PI);
            double difflat = lat - lat_home;
            double difflng = lng - lng_home;

            double newlat = difflat * latitude_meter_per_degree;
            double newlng = difflng * longitude_meter_per_degree;

            return new KeyValuePair<double, double>(newlat, newlng);
        }
    }
}
