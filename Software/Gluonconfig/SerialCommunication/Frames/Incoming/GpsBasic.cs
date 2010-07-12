using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.Frames.Incoming
{
    public class GpsBasic
    {
        private double lon;
        private double lat;
        private double height_m;
        private double heading_deg;
        private double speed_ms;
        private int numberOfSatellites;
        private int status;

        public double Longitude
        {
            get {
                return lon;
            }
        }
        public double Latitude
        {
            get
            {
                return lat;
            }
        }
        public double Height_m
        {
            get
            {
                return height_m;
            }
        }
        public double Heading_deg
        {
            get
            {
                return heading_deg;
            }
        }
        public double Speed_ms
        {
            get
            {
                return speed_ms;
            }
        }
        public int NumberOfSatellites
        {
            get
            {
                return numberOfSatellites;
            }
        }
        public int Status
        {
            get
            {
                return status;
            }
        }

        public GpsBasic(double lat, double lon, double height_m, double heading_rad, double speed_ms,
                        int num_of_satellites, int status)
        {
            this.lat = lat;
            this.lon = lon;
            this.height_m = height_m;
            this.heading_deg = heading_rad/3.14159*180.0;
            this.speed_ms = speed_ms;
            this.numberOfSatellites = num_of_satellites;
            this.status = status;
        }
    }
}
