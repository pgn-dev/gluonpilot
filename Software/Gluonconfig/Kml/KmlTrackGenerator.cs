using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Data;
using System.Globalization;

namespace Kml
{
   public class KmlTrackGenerator
   {
       private static Dictionary<string, int> GenerateColumnLookup(DataTable t)
       {
           Dictionary<string, int> d = new Dictionary<string, int>();
           foreach (DataColumn c in t.Columns)
           {
               d.Add(c.ColumnName, c.Ordinal);
           }

           return d;
       }

       public static string BuildKml(DataSet s, DateTime timestamp)
       {
           int stepsize = 4;

           if (s == null || s.Tables.Count == 0)
           {
               throw new Exception("No data available");
           }

           StringBuilder sb = new StringBuilder();

           Dictionary<string, int> columns = GenerateColumnLookup(s.Tables[0]);

           WriteKmlHeader(sb);
           sb.Append("<name>Gluonpilot export</name>");
           WriteTrackSchema(sb);
           WriteTrackStyle(sb);

           // generate <when>
           StringBuilder sb_when = new StringBuilder();
           DateTime dt = new DateTime(timestamp.Ticks);
           for (int i = 0; i < s.Tables[0].Rows.Count; i += stepsize)
           {
               sb_when.Append(
                   "<when>" +
                       dt.ToString("yyyy-MM-ddTHH:mm:ssZ") + 
                   "</when>\r\n");
               dt = dt.AddMilliseconds(1000);
           }

           // generate <gx:coord>
           double first_height = double.NegativeInfinity;
           StringBuilder sb_coord = new StringBuilder();
           for (int i = 0; i < s.Tables[0].Rows.Count; i += stepsize)
           {
               if (first_height == double.NegativeInfinity)
                   first_height = double.Parse(s.Tables[0].Rows[i].ItemArray[columns["HeightBaro"]].ToString(), CultureInfo.InvariantCulture);

               sb_coord.Append(
                   "<gx:coord>" +
                    s.Tables[0].Rows[i].ItemArray[columns["Longitude"]].ToString() + " " +
                    s.Tables[0].Rows[i].ItemArray[columns["Latitude"]].ToString() + " " +
                    (double.Parse(s.Tables[0].Rows[i].ItemArray[columns["HeightBaro"]].ToString(), CultureInfo.InvariantCulture) - first_height) + 
                   "</gx:coord>\r\n");
           }

           // generate <gx:angles>
           StringBuilder sb_angles = new StringBuilder();
           for (int i = 0; i < s.Tables[0].Rows.Count; i += stepsize)
           {
               sb_angles.Append(
                   "<gx:angles>" +
                   s.Tables[0].Rows[i].ItemArray[columns["HeadingGPS"]].ToString() + " " +
                   s.Tables[0].Rows[i].ItemArray[columns["Pitch"]].ToString() + " " +
                   s.Tables[0].Rows[i].ItemArray[columns["Roll"]].ToString() +
                   "</gx:angles>\r\n");
           }

           StringBuilder sb_sat = new StringBuilder();
           StringBuilder sb_temp = new StringBuilder();
           try
           {
               // generate <satellites>

               for (int i = 0; i < s.Tables[0].Rows.Count; i += stepsize)
               {
                   sb_sat.Append(
                       "<gx:value>" +
                        s.Tables[0].Rows[i].ItemArray[columns["SatellitesGPS"]].ToString() +
                       "</gx:value>\r\n");
               }

               // generate <temp>

               for (int i = 0; i < s.Tables[0].Rows.Count; i += stepsize)
               {
                   sb_temp.Append(
                       "<gx:value>" +
                        s.Tables[0].Rows[i].ItemArray[columns["TempC"]].ToString() +
                       "</gx:value>\r\n");
               }
           }
           catch (Exception ex)
           {
           }

           // generate <speed>
           StringBuilder sb_speed = new StringBuilder();
           for (int i = 0; i < s.Tables[0].Rows.Count; i += stepsize)
           {
               sb_speed.Append(
                   "<gx:value>" +
                    (double.Parse(s.Tables[0].Rows[i].ItemArray[columns["SpeedGPS"]].ToString(), CultureInfo.InvariantCulture) * 3.6).ToString(CultureInfo.InvariantCulture) +
                   "</gx:value>\r\n");
           }
           // generate <desiredheading>
           //StringBuilder sb_desiredheading = new StringBuilder();
           //for (int i = 0; i < s.Tables[0].Rows.Count; i += stepsize)
           //{
           //    sb_desiredheading.Append(
           //        "<gx:value>" +
           //         s.Tables[0].Rows[i].ItemArray[columns["DesiredHeading"]].ToString() +
           //        "</gx:value>\r\n");
           //}

           // generate <heading>
           StringBuilder sb_heading = new StringBuilder();
           for (int i = 0; i < s.Tables[0].Rows.Count; i += stepsize)
           {
               sb_heading.Append(
                   "<gx:value>" +
                    s.Tables[0].Rows[i].ItemArray[columns["HeadingGPS"]].ToString() +
                   "</gx:value>\r\n");
           }


           // generate <desiredroll>
           //StringBuilder sb_desiredroll = new StringBuilder();
           //for (int i = 0; i < s.Tables[0].Rows.Count; i += stepsize)
           //{
           //    sb_desiredroll.Append(
           //        "<gx:value>" +
           //         s.Tables[0].Rows[i].ItemArray[columns["DesiredRoll"]].ToString() +
           //        "</gx:value>\r\n");
           //}
           // generate <roll>
           StringBuilder sb_roll = new StringBuilder();
           for (int i = 0; i < s.Tables[0].Rows.Count; i += stepsize)
           {
               sb_roll.Append(
                   "<gx:value>" +
                    s.Tables[0].Rows[i].ItemArray[columns["Roll"]].ToString() +
                   "</gx:value>\r\n");
           }

           // Putting it all together...
           sb.Append("<Placemark><styleUrl>#gp_manual</styleUrl>\r\n" +
                     "  <name>Name</name>\r\n" +
               //"  <styleUrl>#msn_airports</styleUrl>
               //<gx:balloonVisibility>1</gx:balloonVisibility>
                     "<gx:MultiTrack>\r\n" +
                     "<gx:Track><altitudeMode>relativeToGround</altitudeMode>\r\n" +
                       sb_when + "\r\n" + sb_coord + "\r\n" + sb_angles + "\r\n" +
                     "<ExtendedData>" +
                     "<SchemaData schemaUrl=\"#schema\">" +
                     "   <gx:SimpleArrayData name=\"gps_satellites\">" +
                             sb_sat.ToString() +
                     "   </gx:SimpleArrayData>" +
                     "   <gx:SimpleArrayData name=\"temperature\">" +
                             sb_temp.ToString() +
                     "   </gx:SimpleArrayData>" +
                     "   <gx:SimpleArrayData name=\"speed\">" +
                             sb_speed.ToString() +
                     "   </gx:SimpleArrayData>" +
                     "   <gx:SimpleArrayData name=\"desiredheading\">" +
                             //sb_desiredheading.ToString() +
                     "   </gx:SimpleArrayData>" +
                     "   <gx:SimpleArrayData name=\"heading\">" +
                             sb_heading.ToString() +
                     "   </gx:SimpleArrayData>" +
                     "   <gx:SimpleArrayData name=\"desiredroll\">" +
                             //sb_desiredroll.ToString() +
                     "   </gx:SimpleArrayData>" +
                     "   <gx:SimpleArrayData name=\"roll\">" +
                             sb_roll.ToString() +
                     "   </gx:SimpleArrayData>" +
                     "</SchemaData></ExtendedData>" +
                     "</gx:Track></gx:MultiTrack>\r\n</Placemark>\r\n");


           WriteKmlFooter(sb);

           return sb.ToString();
       }

       private static void WriteTrackStyle(StringBuilder sb)
       {
           sb.Append(
               "<Style id=\"gp_manual\">\r\n" +
               "   <IconStyle>\r\n" +
               "   <Icon>\r\n" +
               "<href>http://maps.google.com/mapfiles/kml/shapes/airports.png</href>\r\n" +
               "   </Icon>\r\n" +
               "   <hotSpot x=\"0.5\" y=\"0\" xunits=\"fraction\" yunits=\"fraction\"/>\r\n" +
               "   </IconStyle>\r\n" +
               "       <LineStyle>\r\n" +
               "      <color>b32d47ff</color>\r\n" +
               "      <width>5</width>\r\n" +
               "   </LineStyle>\r\n" +
               "</Style>\r\n");
       }

       private static void WriteTrackSchema(StringBuilder sb)
       {
           sb.Append("<Schema id=\"schema\">\r\n" +
                     "<gx:SimpleArrayField name=\"gps_satellites\" type=\"int\">\r\n" +
                     "   <displayName># GPS Sat</displayName>\r\n" +
                     "</gx:SimpleArrayField>\r\n" +
                     "<gx:SimpleArrayField name=\"temperature\" type=\"int\">\r\n" +
                     "   <displayName>Temp (°C)</displayName>\r\n" +
                     "</gx:SimpleArrayField>\r\n" +
                     "<gx:SimpleArrayField name=\"speed\" type=\"float\">\r\n" +
                     "   <displayName>GPS Speed (km/h)</displayName>\r\n" +
                     "</gx:SimpleArrayField>\r\n" +
                     "<gx:SimpleArrayField name=\"desiredheading\" type=\"int\">\r\n" +
                     "   <displayName>Desired heading</displayName>\r\n" +
                     "</gx:SimpleArrayField>\r\n" +
                     "<gx:SimpleArrayField name=\"heading\" type=\"int\">\r\n" +
                     "   <displayName>Heading</displayName>\r\n" +
                     "</gx:SimpleArrayField>\r\n" +
                     "<gx:SimpleArrayField name=\"desiredroll\" type=\"int\">\r\n" +
                     "   <displayName>Desired roll</displayName>\r\n" +
                     "</gx:SimpleArrayField>\r\n" +
                     "<gx:SimpleArrayField name=\"roll\" type=\"int\">\r\n" +
                     "   <displayName>Roll</displayName>\r\n" +
                     "</gx:SimpleArrayField>\r\n" +
                     "</Schema>\r\n");
       }

       private static void WriteKmlHeader(StringBuilder sb)
       {
           sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                     "<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\">\r\n" +
                     "<Document>\r\n");
       }

       private static void WriteKmlFooter(StringBuilder sb)
       {
           sb.Append("</Document>\r\n</kml>");
       }
   }
}