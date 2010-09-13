using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Kml
{
    public class KmlClassicGenerator
    {
        public static string BuildKml(DataSet loglines)
        {
            string[] flightmodes = new string[5] { "Manual", "Stabilized", "Autopilot", "Loiter", "Return" };

            int flightmode = 0;
            StringBuilder sb = new StringBuilder();

            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                          " <kml xmlns=\"http://earth.google.com/kml/2.0\">\r\n" +
                          " <Document xmlns:xlink=\"http://www.w3/org/1999/xlink\">\r\n" +
                          " <name>Gluonpilot export</name>\r\n");
            for (int j = 0; j <= 180; j++)
            {
                sb.Append(
                          " <Style id=\"dataStyle_" + j + "\">\r\n" +
                          "     <IconStyle><Icon><href>http://maps.google.com/mapfiles/kml/shapes/airports.png</href></Icon><scale>0.5</scale><heading>" + j * 2 + "</heading></IconStyle>\r\n" +
                          " </Style>\r\n");
            }
            sb.Append(
                          " <Style id=\"dataStyle\">\r\n" +
                          "     <IconStyle><Icon><href>http://maps.google.com/mapfiles/kml/shapes/airports.png</href></Icon><scale>0.5</scale></IconStyle>\r\n" +
                          " </Style>\r\n" +
                          " <Style id=\"manualStyle\">\r\n" +
                          "   <LineStyle>" +
                          "   <color>ff00ffff</color>" +
                          "   <width>5</width>" +
                          "   </LineStyle>" +
                          " </Style>\r\n" +
                          " <Style id=\"stabiStyle\">\r\n" +
                          "   <LineStyle>" +
                          "   <color>ffff00ff</color>" +
                          "   <width>5</width>" +
                          "   </LineStyle>" +
                          " </Style>\r\n" +
                          " <Style id=\"autoStyle\">\r\n" +
                          "   <LineStyle>" +
                          "   <color>ff0000ff</color>" +
                          "   <width>5</width>" +
                          "   </LineStyle>" +
                          " </Style>\r\n" +
                          "   <Folder>\r\n" +
                          "   <name>Flight path</name>\r\n");
            string tailpath = "</coordinates></LineString></MultiGeometry></Placemark></Folder>\r\n";
            string tailkml = "</Document></kml>";


            bool firstcall = true;
            int startheight = -11111;
            int exceptions = 0;
            foreach (DataRow dr in loglines.Tables["Data"].Rows)
            {
                try
                {
                    if (firstcall || (dr.Table.Columns.Contains("FlightMode") &&
                        dr["FlightMode"].ToString() != flightmode.ToString() &&
                        int.Parse(dr["FlightMode"].ToString()) >= 0 &&
                        int.Parse(dr["FlightMode"].ToString()) <= flightmodes.Length))
                    {
                        if (dr.Table.Columns.Contains("FlightMode"))
                        {
                            flightmode = int.Parse(dr["FlightMode"].ToString());
                            if (flightmode >= flightmodes.Length)
                                flightmode = 0;
                        }

                        if (!firstcall)
                            sb.Append("</coordinates></LineString></MultiGeometry></Placemark>\r\n");
                        else
                            firstcall = false;
                        sb.Append("<Placemark>" +
                                     "   <name>" + flightmodes[flightmode] + "</name>\r\n");
                        if (flightmode == 0) // manual
                            sb.Append("   <styleUrl>#manualStyle</styleUrl>\r\n");
                        else if (flightmode == 1) // stabilized
                            sb.Append("   <styleUrl>#stabiStyle</styleUrl>\r\n");
                        else
                            sb.Append("   <styleUrl>#autoStyle</styleUrl>\r\n");
                        sb.Append(
                              "   <MultiGeometry>\r\n" +
                              "     <LineString>\r\n" +
                              "       <altitudeMode>relativeToGround</altitudeMode>\r\n" +
                              "       <coordinates>\r\n");
                    }
                    if (startheight == -11111)
                        startheight = (int)double.Parse(dr["HeightBaro"].ToString(), System.Globalization.CultureInfo.InvariantCulture);

                    double lon = Math.Abs(double.Parse(dr["Longitude"].ToString(), System.Globalization.CultureInfo.InvariantCulture));
                    double lat = Math.Abs(double.Parse(dr["Latitude"].ToString(), System.Globalization.CultureInfo.InvariantCulture));
                    if (lon <= 360.0 && lon > 0.001 &&
                        lat <= 360.0 && lat > 0.001)
                        sb.Append(dr["Longitude"] + "," + dr["Latitude"] + "," + (double.Parse(dr["HeightBaro"].ToString(), System.Globalization.CultureInfo.InvariantCulture) - startheight).ToString(System.Globalization.CultureInfo.InvariantCulture) + "\r\n");
                }
                catch (Exception ex)
                {
                    exceptions++;
                }
            }
            sb.Append(tailpath);

            sb.Append("<Folder><name>Datalog</name>\r\n");


            int i = 0;
            double time = 0.0;
            foreach (DataRow dr in loglines.Tables["Data"].Rows)
            {
                time += 0.02;
                if (i++ % 15 == 0)
                {
                    if (dr.Table.Columns.Contains("HeadingGPS") && dr.Table.Columns.Contains("HeightBaro"))
                    {
                        sb.Append("<Placemark><Point><coordinates>\r\n");
                        sb.Append(dr["Longitude"] + "," + dr["Latitude"] + "," + (double.Parse(dr["HeightBaro"].ToString(), System.Globalization.CultureInfo.InvariantCulture) - startheight).ToString(System.Globalization.CultureInfo.InvariantCulture) + "\r\n");

                        sb.Append("</coordinates><altitudeMode>relativeToGround</altitudeMode></Point><styleUrl>#dataStyle_" + int.Parse(dr["HeadingGPS"].ToString()) / 2 + "</styleUrl>\r\n");
                        if (dr.Table.Columns.Contains("Pitch") && dr.Table.Columns.Contains("HeightGPS"))
                        {
                            sb.Append("<description>" + (int)time + " - Roll: " + dr["Roll"].ToString() + ", Desired: " + dr["DesiredRoll"].ToString() + " - Pitch: " + dr["Pitch"].ToString() + ", Desired: " + dr["DesiredPitch"].ToString() + " - Height: " + dr["HeightGPS"].ToString() + "</description>");
                        }
                        else if (dr.Table.Columns.Contains("Time"))
                        {
                            sb.Append("<description>GPS time: " + dr["Time"] + " - elapsed time: " + Math.Round(time, 2) + " - datasample " + i + "</description>");
                        }
                        sb.Append("</Placemark>\r\n");
                    }
                }
            }
            sb.Append("</Folder>\r\n");

            sb.Append(tailkml);

            return sb.ToString();
        }
    }
}
