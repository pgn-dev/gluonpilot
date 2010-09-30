using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Communication.Frames.Incoming;
using System.Globalization;

namespace Kml
{
    public class KmlNavigation
    {
        public static String BuildKml(List<NavigationInstruction> list, double lat_home_rad, double lon_home_rad)
        {
            StringBuilder placemarks = new StringBuilder();
            StringBuilder path = new StringBuilder();


            double latitude_meter_per_radian = 6363057.32484;
            double longitude_meter_per_radian = latitude_meter_per_radian * Math.Cos(lat_home_rad);

            path.Append(
               (lon_home_rad / 3.14159 * 180.0).ToString(System.Globalization.CultureInfo.InvariantCulture) +
               "," + (lat_home_rad / 3.14159 * 180.0).ToString(System.Globalization.CultureInfo.InvariantCulture) + ",0");

            placemarks.Append("<Placemark><name>Home</name><styleUrl>#homePlacemark</styleUrl>\n\r" +
                    "<Point><altitudeMode>relativeToGround</altitudeMode>\n\r<coordinates>" +
                    path.ToString() + "</coordinates>\n\r</Point></Placemark>");

            foreach (NavigationInstruction ni in list)
            {
                if (ni.opcode == NavigationInstruction.navigation_command.FLY_TO_ABS ||
                    ni.opcode == NavigationInstruction.navigation_command.FROM_TO_ABS ||
                    ni.opcode == NavigationInstruction.navigation_command.CIRCLE_ABS)
                {
                    int altitude = ni.opcode == NavigationInstruction.navigation_command.CIRCLE_ABS ? ni.b : ni.a;
                    string coord = String.Format(CultureInfo.InvariantCulture, "{0},{1},{2}", Rad2Deg(ni.y), Rad2Deg(ni.x), altitude);

                    placemarks.Append("\n\r<Placemark><name>" + ni.line + "</name><styleUrl>#squarePlacemark</styleUrl>\n\r" +
                                      "<description>" + ni.ToString() + "</description>\n\r" +
                                      "<Point><extrude>1</extrude><altitudeMode>relativeToGround</altitudeMode>\n\r<coordinates>" +
                                      coord + "</coordinates>\n\r</Point></Placemark>");

                    // add circle coordinates if needed
                    if (ni.opcode == NavigationInstruction.navigation_command.CIRCLE_ABS)
                        path.Append("\r\n" + coord + "\r\n" + BuildCircleCoordinates(ni.x, ni.y, ni.b, altitude));
                    else
                        path.Append("\r\n" + coord);
                }
                else if (ni.opcode == NavigationInstruction.navigation_command.FLY_TO_REL ||
                         ni.opcode == NavigationInstruction.navigation_command.FROM_TO_REL ||
                         ni.opcode == NavigationInstruction.navigation_command.CIRCLE_REL)
                {
                    int altitude = ni.opcode == NavigationInstruction.navigation_command.CIRCLE_REL ? ni.b : ni.a;
                    double lat_rad = ni.x / latitude_meter_per_radian + lat_home_rad;
                    double lon_rad = ni.y / longitude_meter_per_radian + lon_home_rad;
                    string coord = String.Format(CultureInfo.InvariantCulture, "\r\n{0},{1},{2}", Rad2Deg(lon_rad), Rad2Deg(lat_rad), altitude);

                    placemarks.Append("\n\r<Placemark><name>" + ni.line + "</name><styleUrl>#squarePlacemark</styleUrl>\n\r" +
                                      "<description>" + ni.ToString() + "</description>\n\r" +
                                      "<Point><extrude>1</extrude><altitudeMode>relativeToGround</altitudeMode>\n\r<coordinates>" +
                                      coord + "</coordinates>\n\r</Point></Placemark>\n\r");

                    if (ni.opcode == NavigationInstruction.navigation_command.CIRCLE_REL)
                        path.Append("\r\n" + coord + "\r\n" + BuildCircleCoordinates(lat_rad, lon_rad, ni.a, altitude));
                    else
                        path.Append("\r\n" + coord);
                }
            }

            StringBuilder kml = new StringBuilder();
            kml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><kml xmlns=\"http://www.opengis.net/kml/2.2\"><Document><name>Navigation</name>");

            kml.Append("<Style id=\"homePlacemark\"><IconStyle>" +
                "<Icon><href>http://maps.google.com/mapfiles/kml/shapes/ranger_station.png</href></Icon>" +
                "</IconStyle></Style>");
            kml.Append("<Style id=\"squarePlacemark\"><IconStyle>" +
                "<Icon><href>http://maps.google.com/mapfiles/kml/shapes/placemark_square.png</href></Icon>" +
                "</IconStyle></Style>");
            kml.Append("<Style id=\"roundPlacemark\"><IconStyle>" +
                "<Icon><href>http://maps.google.com/mapfiles/kml/shapes/placemark_circle.png</href></Icon>" +
                "</IconStyle></Style>");
            kml.Append("<Style id=\"path\"><LineStyle><color>ffff0000</color><width>3</width></LineStyle></Style>");


            kml.Append("<Folder><name>Waypoints</name>");
            kml.Append(placemarks);
            kml.Append("</Folder>");
            kml.Append("<Placemark><styleUrl>#path</styleUrl><name>Flightpath</name><LineString><altitudeMode>relativeToGround</altitudeMode>" +
                       "<coordinates>" + path.ToString() + "</coordinates></LineString></Placemark>");
            kml.Append("</Document></kml>");

            return kml.ToString();
        }

        private static string BuildCircleCoordinates(double lat_rad, double lon_rad, double r, double altitude)
        {
            double latitude_meter_per_rad = 6363057.32484;
            double cos_latitude = Math.Cos(lat_rad);
            double longitude_meter_per_rad = latitude_meter_per_rad * cos_latitude;

            StringBuilder sb = new StringBuilder();
            for (double i = 0.0; i <= Math.PI * 2.00001; i += Math.PI * 2.0 / 30.0)
            {
                double clon = lon_rad + Math.Cos(i) * r / longitude_meter_per_rad;
                double clat = lat_rad + Math.Sin(i) * r / latitude_meter_per_rad;
                sb.Append(String.Format(CultureInfo.InvariantCulture, "{0},{1},{2}\r\n", Rad2Deg(clon), Rad2Deg(clat),altitude));
            }
            return sb.ToString();
        }

        private static double Rad2Deg(double x)
        {
            return x / Math.PI * 180.0;
        }

        private static double Deg2Rad(double x)
        {
            return x / 180.0 * Math.PI;
        }
    }
}
