/*!
 *   SerialCommunication_CSV.c
 *   Implements the CSV variant of the gluonpilot communication protocol.
 *   
 *   @author  Tom Pycke
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;

using Amib.Threading;
using Communication.Frames.Configuration;
using Communication.Frames.Incoming;
using System.Threading;

namespace Communication
{
    public class SerialCommunication_CSV : SerialCommunication
    {
        private SmartThreadPool _smartThreadPool = new SmartThreadPool();

        // General: all lines received will be broadcasted by this event
        public override event ReceiveCommunication CommunicationReceived;
        // Gyro & Acc
        public override event ReceiveGyroAccRawCommunicationFrame GyroAccRawCommunicationReceived;
        public override event ReceiveGyroAccProcCommunicationFrame GyroAccProcCommunicationReceived;
        // Pressure & Temperature
        public override event ReceivePressureTempCommunicationFrame PressureTempCommunicationReceived;
        // Configuration
        public override event ReceiveAllConfigCommunicationFrame AllConfigCommunicationReceived;
        // RC transmitter
        public override event ReceiveRcInputCommunicationFrame RcInputCommunicationReceived;
        // Gps Basic
        public override event ReceiveGpsBasicCommunicationFrame GpsBasicCommunicationReceived;
        // Attitude
        public override event ReceiveAttitudeCommunicationFrame AttitudeCommunicationReceived;


        public SerialCommunication_CSV()
        {
            _serialPort = new SerialPort();
        }

        /*!
         *    Opens the COM port and creates a thread that will 
         *    process all incoming serial data
         */
        public void Open(string portName, int baudrate)
        {
            _serialPort.PortName = portName;
            _serialPort.BaudRate = baudrate;
            _serialPort.Open();

            IWorkItemResult wir =
                        _smartThreadPool.QueueWorkItem(
                            new WorkItemCallback(this.ReceiveThreadedData), _serialPort);
        }

        public void Close()
        {
            _smartThreadPool.Shutdown(false, 300);
            _serialPort.Close();
        }

        /*!
         *    This function (that executes in a separate thread) is an
         *    infinite loop that receives all lines from the serial port
         *    and parses and executes them.
         */
        private object ReceiveThreadedData(object state)
        {
            _serialPort.ReadTimeout = 200;
            while (_serialPort.IsOpen)
            {
                try
                {
                    string line = _serialPort.ReadLine();
                    string[] lines = line.Split(';');
                    Console.WriteLine(line + "\n\r");
                    // TR: Gyro & Acc raw
                    if (lines[0].EndsWith("TR") && lines.Length >= 6)
                    {
                        double acc_x_raw = double.Parse(lines[1]);
                        double acc_y_raw = double.Parse(lines[2]);
                        double acc_z_raw = double.Parse(lines[3]);
                        double gyro_x = double.Parse(lines[4]);
                        double gyro_y = double.Parse(lines[5]);
                        double gyro_z = double.Parse(lines[6]);
                        GyroAccRaw ga = new GyroAccRaw(acc_x_raw, acc_y_raw, acc_z_raw, gyro_x, gyro_y, gyro_z);
                        GyroAccRawCommunicationReceived(ga);
                    }

                    // TP: Processed gyro & acc
                    else if (lines[0].EndsWith("TP") && lines.Length >= 6)
                    {
                        double acc_x = double.Parse(lines[1]) / 1000.0;
                        double acc_y = double.Parse(lines[2]) / 1000.0;
                        double acc_z = double.Parse(lines[3]) / 1000.0;
                        double gyro_x = double.Parse(lines[4]) / 1000.0 * 180.0 / 3.14;
                        double gyro_y = double.Parse(lines[5]) / 1000.0 * 180.0 / 3.14;
                        double gyro_z = double.Parse(lines[6]) / 1000.0 * 180.0 / 3.14;
                        GyroAccProcessed ga = new GyroAccProcessed(acc_x, acc_y, acc_z, gyro_x, gyro_y, gyro_z);
                        GyroAccProcCommunicationReceived(ga);
                    }

                    // TH: Pressure & Temp
                    else if (lines[0].EndsWith("TH") && lines.Length >= 2)
                    {
                        float pressure = float.Parse(lines[1]);
                        float temp = float.Parse(lines[2]);
                        PressureTemp pt = new PressureTemp(temp, pressure);
                        PressureTempCommunicationReceived(pt);
                    }

                    // CA: All configuration
                    else if (lines[0].EndsWith("CA") && lines.Length >= 2)
                    {
                        AllConfig ac = new AllConfig();
                        ac.acc_x_neutral = int.Parse(lines[1]);
                        ac.acc_y_neutral = int.Parse(lines[2]);
                        ac.acc_z_neutral = int.Parse(lines[3]);
                        ac.gyro_x_neutral = int.Parse(lines[4]);
                        ac.gyro_y_neutral = int.Parse(lines[5]);
                        ac.gyro_z_neutral = int.Parse(lines[6]);

                        ac.telemetry_basicgps = int.Parse(lines[7]);
                        ac.telemetry_ppm = int.Parse(lines[8]);
                        ac.telemetry_gyroaccraw = int.Parse(lines[9]);
                        ac.telemetry_gyroaccproc = int.Parse(lines[10]);
                        ac.telemetry_pressuretemp = int.Parse(lines[11]);

                        ac.gps_initial_baudrate = int.Parse(lines[12]) * 10;
                        ac.gps_operational_baudrate = int.Parse(lines[13]) * 10;

                        ac.channel_ap = int.Parse(lines[14]) + 1;
                        ac.channel_motor = int.Parse(lines[15]) + 1;
                        ac.channel_pitch = int.Parse(lines[16]) + 1;
                        ac.channel_roll = int.Parse(lines[17]) + 1;
                        ac.channel_yaw = int.Parse(lines[18]) + 1;

                        ac.pid_pitch2elevator_p = double.Parse(lines[19], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_pitch2elevator_d = double.Parse(lines[20], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_pitch2elevator_i = double.Parse(lines[21], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_pitch2elevator_imin = double.Parse(lines[22], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_pitch2elevator_imax = double.Parse(lines[23], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_pitch2elevator_dmin = double.Parse(lines[24], System.Globalization.CultureInfo.InvariantCulture);

                        ac.pid_roll2aileron_p = double.Parse(lines[25], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_roll2aileron_d = double.Parse(lines[26], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_roll2aileron_i = double.Parse(lines[27], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_roll2aileron_imin = double.Parse(lines[28], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_roll2aileron_imax = double.Parse(lines[29], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_roll2aileron_dmin = double.Parse(lines[30], System.Globalization.CultureInfo.InvariantCulture);

                        ac.pid_heading2roll_p = double.Parse(lines[31], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_heading2roll_d = double.Parse(lines[32], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_heading2roll_i = double.Parse(lines[33], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_heading2roll_imin = double.Parse(lines[34], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_heading2roll_imax = double.Parse(lines[35], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_heading2roll_dmin = double.Parse(lines[36], System.Globalization.CultureInfo.InvariantCulture);

                        int r = int.Parse(lines[37]);
                        byte r2 = (byte)r;
                        ac.servo_reverse[0] = (r & 1) != 0;
                        ac.servo_reverse[1] = (r & 2) != 0;
                        ac.servo_reverse[2] = (r & 4) != 0;
                        ac.servo_reverse[3] = (r & 8) != 0;
                        ac.servo_reverse[4] = (r & 16) != 0;
                        ac.servo_reverse[5] = (r & 32) != 0;

                        for (int i = 0; i < 6; i++)
                        {
                            ac.servo_min[i] = int.Parse(lines[38 + i * 3], System.Globalization.CultureInfo.InvariantCulture);
                            ac.servo_max[i] = int.Parse(lines[39 + i * 3], System.Globalization.CultureInfo.InvariantCulture);
                            ac.servo_neutral[i] = int.Parse(lines[40 + i * 3], System.Globalization.CultureInfo.InvariantCulture);
                        }
                        // 58

                        AllConfigCommunicationReceived(ac);
                    }

                    // TT: RC transmitter
                    else if (lines[0].EndsWith("TT") && lines.Length >= 7)
                    {
                        RcInput rc = new RcInput(
                            new int[] { 
                                int.Parse(lines[1]),
                                int.Parse(lines[2]),
                                int.Parse(lines[3]), 
                                int.Parse(lines[4]), 
                                int.Parse(lines[5]),
                                int.Parse(lines[6]),
                                int.Parse(lines[7])});
                        RcInputCommunicationReceived(rc);
                    }
                    // TG: GPS basic
                    else if (lines[0].EndsWith("TG") && lines.Length >= 7)
                    {
                        GpsBasic gb = new GpsBasic(
                            double.Parse(lines[2], System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(lines[3], System.Globalization.CultureInfo.InvariantCulture),
                            0,
                            double.Parse(lines[5])/100,
                            double.Parse(lines[4])/10,
                            int.Parse(lines[6]),
                            int.Parse(lines[1])
                            );
                        GpsBasicCommunicationReceived(gb);
                    }
                    // TA: Attitude
                    else if (lines[0].EndsWith("TA") && lines.Length >= 3)
                    {
                        Attitude att = new Attitude(
                            double.Parse(lines[1], System.Globalization.CultureInfo.InvariantCulture) / 1000.0 / 3.14 * 180.0,
                            double.Parse(lines[2], System.Globalization.CultureInfo.InvariantCulture) / 1000.0 / 3.14 * 180.0,
                            double.Parse(lines[3], System.Globalization.CultureInfo.InvariantCulture) / 1000.0 / 3.14 * 180.0,
                            double.Parse(lines[4], System.Globalization.CultureInfo.InvariantCulture) / 1000.0 / 3.14 * 180.0,
                            double.Parse(lines[5], System.Globalization.CultureInfo.InvariantCulture) / 1000.0 / 3.14 * 180.0
                            );
                        if (AttitudeCommunicationReceived != null)
                            AttitudeCommunicationReceived(att);
                    }
                    CommunicationReceived(line);
                }
                catch (TimeoutException toe)
                {
                }
                catch (IOException ioe)
                {
                    // happens when thread is shut down
                }
                catch (Exception e)
                {

                }
            }

            return null;
        }

        /*!
         *    Sends the complete configuration set AllConfig to the gluonpilot.
         */
        public override void Send(AllConfig ac)
        {
            // telemetry
            _serialPort.WriteLine("\nST;" +
                ac.telemetry_basicgps.ToString() + ";" +
                ac.telemetry_gyroaccraw.ToString() + ";" +
                ac.telemetry_gyroaccproc.ToString() + ";" + 
                ac.telemetry_ppm.ToString() + ";" +
                ac.telemetry_pressuretemp.ToString() + "\n");
            Console.WriteLine("\nST;" +
                ac.telemetry_basicgps.ToString() + ";" +
                ac.telemetry_gyroaccraw.ToString() + ";" +
                ac.telemetry_gyroaccproc.ToString() + ";" +
                ac.telemetry_ppm.ToString() + ";" +
                ac.telemetry_pressuretemp.ToString() + "\n");

            Thread.Sleep(200);
            
            // neutral acc
            _serialPort.WriteLine("\nSA;" +
                ac.acc_x_neutral.ToString() + ";" +
                ac.acc_y_neutral.ToString() + ";" +
                ac.acc_z_neutral.ToString() + "\n");
            Console.WriteLine("\nSA;" +
                ac.acc_x_neutral.ToString() + ";" +
                ac.acc_y_neutral.ToString() + ";" +
                ac.acc_z_neutral.ToString() + "\n");
            Thread.Sleep(200);

            // neutral gyro
            _serialPort.WriteLine("\nSY;" +
                ac.gyro_x_neutral.ToString() + ";" +
                ac.gyro_y_neutral.ToString() + ";" +
                ac.gyro_z_neutral.ToString() + "\n");
            Console.WriteLine("\nSY;" +
                ac.gyro_x_neutral.ToString() + ";" +
                ac.gyro_y_neutral.ToString() + ";" +
                ac.gyro_z_neutral.ToString() + "\n");
            Thread.Sleep(200);
            
            // channel config
            _serialPort.WriteLine("\nSI;" + 
                Char.ConvertFromUtf32(97 + ac.channel_ap - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_motor - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_pitch - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_roll - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_yaw - 1) + "\n");
            Console.WriteLine("\nSI;" +
                Char.ConvertFromUtf32(97 + ac.channel_ap - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_motor - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_pitch - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_roll - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_yaw - 1) + "\n");

            Thread.Sleep(200);
            // gps config
            _serialPort.WriteLine("\nSG;" + (ac.gps_initial_baudrate / 10).ToString() + "\n");
            Console.WriteLine("\nSG;" + (ac.gps_initial_baudrate / 10).ToString() + "\n");

            Thread.Sleep(200);

            _serialPort.WriteLine("\nPP;" +
                ac.pid_pitch2elevator_p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_pitch2elevator_i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_pitch2elevator_d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_pitch2elevator_imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_pitch2elevator_imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_pitch2elevator_dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\n");

            Thread.Sleep(200);

            _serialPort.WriteLine("\nPR;" +
                ac.pid_roll2aileron_p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_roll2aileron_i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_roll2aileron_d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_roll2aileron_imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_roll2aileron_imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_roll2aileron_dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\n");
            
            Thread.Sleep(200);

            _serialPort.WriteLine("\nPH;" +
                ac.pid_heading2roll_p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\n\n");
            Console.WriteLine("\nPH;" +
                ac.pid_heading2roll_p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\n");

            Thread.Sleep(200);

            int s = 0;
            if (ac.servo_reverse[0])
                s += 1;
            if (ac.servo_reverse[1])
                s += 2;
            if (ac.servo_reverse[2])
                s += 4;
            if (ac.servo_reverse[3])
                s += 8;
            if (ac.servo_reverse[4])
                s += 16;
            if (ac.servo_reverse[5])
                s += 32;
            _serialPort.WriteLine("\nSR;" + s.ToString() + "\n");
        }

        public override void ReadAllConfig()
        {
            _serialPort.WriteLine("\nRC;A\n");
        }

        public override void SendFlashConfiguration()
        {
            _serialPort.WriteLine("\nFC;\n");
        }

        public override void SendLoadConfigurationFromFlash()
        {
            _serialPort.WriteLine("\nLC;\n");
        }

        public override void SendLoadConfigurationDefault()
        {
            _serialPort.WriteLine("\nLD;\n");
        }
    }
}
