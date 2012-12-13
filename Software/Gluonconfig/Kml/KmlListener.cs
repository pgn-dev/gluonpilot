using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amib.Threading;
using Communication;
using System.Net;
using System.Globalization;


namespace Kml
{
    public class KmlListener
    {
        private SerialCommunication serial_comm;
        private double pitch, roll, yaw;
        private SmartThreadPool _smartThreadPool;

        private double longitude = 3.669214;
        private double latitude = 50.850285;
        private double heading = 0.0;
        private double height = 100.0;
        private double pressure_height_m = 0.0;


        public KmlListener(SerialCommunication serial)
        {
            _smartThreadPool = new SmartThreadPool();
            serial_comm = serial;
            serial.AttitudeCommunicationReceived += new SerialCommunication.ReceiveAttitudeCommunicationFrame(serial_AttitudeCommunicationReceived);
            serial.GpsBasicCommunicationReceived += new SerialCommunication.ReceiveGpsBasicCommunicationFrame(serial_GpsBasicCommunicationReceived);
            serial.PressureTempCommunicationReceived += new SerialCommunication.ReceivePressureTempCommunicationFrame(serial_PressureTempCommunicationReceived);
            serial.ControlInfoCommunicationReceived += new SerialCommunication.ReceiveControlInfoCommunicationFrame(serial_ControlInfoCommunicationReceived);
        }

        void serial_ControlInfoCommunicationReceived(Communication.Frames.Incoming.ControlInfo ci)
        {
            pressure_height_m = ci.Altitude;
        }

        void serial_PressureTempCommunicationReceived(Communication.Frames.Incoming.PressureTemp info)
        {
            /*if (!is_started)
            {
                start_pressure_height_m = info.Height;
                is_started = true;
            }
            pressure_height_m = info.Height - start_pressure_height_m;*/
        }

        void serial_GpsBasicCommunicationReceived(Communication.Frames.Incoming.GpsBasic gpsbasic)
        {
            longitude = gpsbasic.Longitude / 3.14159 * 180.0;
            latitude = gpsbasic.Latitude / 3.14159 * 180.0;
            heading = gpsbasic.Heading_deg;
            height = gpsbasic.Height_m;
        }

        void serial_AttitudeCommunicationReceived(Communication.Frames.Incoming.Attitude attitude)
        {
            roll = (double)attitude.RollDeg;
            pitch = (double)attitude.PitchDeg;
            yaw = -(double)attitude.YawDeg;
        }

        public void Start()
        {
            IWorkItemResult wir =
                        _smartThreadPool.QueueWorkItem(
                            new WorkItemCallback(this.Listen));
        }

        public void Stop()
        {
            _smartThreadPool.Shutdown();
        }

        private object Listen(object o)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/");
            listener.Start();
            //Console.WriteLine("Listening...");
            for(;;)
            {
                HttpListenerContext context = listener.GetContext();

                StringBuilder sb = new StringBuilder();
                sb.Append(
                    "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + 
                    "<kml xmlns=\"http://earth.google.com/kml/2.1\">" +
                    "<Document><Placemark>" + 
                    "<name>Gluonpilot</name>" +
                    "  <LookAt>" +
                    "   <longitude>" + longitude.ToString(CultureInfo.InvariantCulture) + "</longitude>" +
                    "   <latitude>" + latitude.ToString(CultureInfo.InvariantCulture) + "</latitude>" +
                    "   <altitude>50</altitude>" +
                    "  </LookAt>" +
                    "  <Model id=\"model_4\">" +
                    "    <altitudeMode>relativeToGround</altitudeMode>" +
                    "    <Location>" +
                    "      <longitude>" + longitude.ToString(CultureInfo.InvariantCulture) + "</longitude>" +
                    "      <latitude>" + latitude.ToString(CultureInfo.InvariantCulture) + "</latitude>" +
                    "      <altitude>" + (int)pressure_height_m + "</altitude>" +
                    "    </Location>" +
                    "    <Orientation>" +
                    "      <heading>" + (int)heading + "</heading>" +
                    "      <tilt>" + (-pitch).ToString(CultureInfo.InvariantCulture) + "</tilt>" +
                    "      <roll>" + (-roll).ToString(CultureInfo.InvariantCulture) + "</roll>" +
                    "    </Orientation>" +
                    "    <Scale>" +
                    "      <x>6</x>" +
                    "      <y>6</y>" +
                    "      <z>6</z>" +
                    "    </Scale>" +
                    "    <Link>" +
                    "      <href>" + System.Windows.Forms.Application.StartupPath /* C:\\Documents and Settings\\Eigenaar\\Mijn documenten\\MAV\\googleearth */ + "\\PredatorE.dae</href>" +
                    "    </Link>" +
                    "  </Model>" +
                    "</Placemark>" +

                    "<Placemark>" +
                    "    <name>Center earth line</name>" +
                    "    <LineString>" +
                    "      <altitudeMode>relativeToGround</altitudeMode>" +
                    "      <coordinates>" + longitude.ToString(CultureInfo.InvariantCulture) + "," + latitude.ToString(CultureInfo.InvariantCulture) + ",0 " +
                    "" + longitude.ToString(CultureInfo.InvariantCulture) + "," + latitude.ToString(CultureInfo.InvariantCulture) + "," + (int) pressure_height_m + 
                    "      </coordinates>" +
                    "    </LineString>" +
                    "</Placemark>" +
                    "</Document>" +
                    "</kml>"
                );



                byte[] b = Encoding.UTF8.GetBytes(sb.ToString());
                context.Response.ContentLength64 = b.Length;
                context.Response.OutputStream.Write(b, 0, b.Length);
                context.Response.OutputStream.Close();
            }

            return null;
        }
    }
}
