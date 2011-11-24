using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class LatLng
    {
        public static double LatitudeMeterPerRadian = 6363057.32484;
        public static double LatitudeMeterPerDegree = 6363057.32484*Math.PI/180.0;

        public double Lat;
        public double Lng;

        public LatLng(double Lat, double Lng)
        {
            this.Lat = Lat;
            this.Lng = Lng;
        }

        public static LatLng ToAbsolute(double lat_home, double lng_home, double lat_m, double lng_m)
        {
            double difflat = lat_m / LatitudeMeterPerDegree;
            double difflng = lng_m / LongitudeMeterPerDegree(lat_home);

            return new LatLng(lat_home + difflat, lng_home + difflng);
        }

        public static LatLng ToRelative(double lat_home, double lng_home, double lat_deg, double lng_deg)
        {
            double difflat = lat_deg - lat_home;
            double difflng = lng_deg - lng_home;
            return new LatLng(difflat * LatitudeMeterPerDegree, difflng * LongitudeMeterPerDegree(lat_home));
        }

        public static double LongitudeMeterPerDegree(double LatDeg)
        {
            return LatitudeMeterPerDegree * Math.Cos(LatDeg / 180.0 * Math.PI);
        }
    }
}
