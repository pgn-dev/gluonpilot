/*!
 *   SerialCommunication_CSV.c
 *   Implements the CSV variant of the gluonpilot communication protocol.
 *   
 *   @author  Tom Pycke
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;

using Amib.Threading;
using Communication.Frames.Configuration;
using Communication.Frames.Incoming;
using System.Threading;
using System.Globalization;

namespace Communication
{
    public class SerialCommunication_CSV : SerialCommunication
    {
        private string filename;
        public override string LogToFilename
        {
            set
            {
                try
                {
                    filename = value;
                    logfile = new System.IO.StreamWriter(filename);
                }
                catch (Exception e)
                {
                    logfile = null;
                }
            }
            get
            {
                if (logfile == null)
                    return "";
                else
                    return filename;
            }
        }
        private System.IO.StreamWriter logfile;

        private SmartThreadPool _smartThreadPool;
        private int bytes_read = 0;
        private double download_speed_kb_s;
        private DateTime last_throughput_calculation;
        private int FramesReceived = 0;
        private bool CommunicationAlive = false;
        private DateTime LastValidFrame;
        //private Timer CheckCommunicationReceived = new Timer();

        // General: all lines received will be broadcasted by this event
        public override event ReceiveCommunication CommunicationReceived;
        public override event ReceiveNonParsedCommunication NonParsedCommunicationReceived;
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
        // Datalog
        public override event ReceiveDatalogTableCommunicationFrame DatalogTableCommunicationReceived;
        public override event ReceiveDatalogLineCommunicationFrame DatalogLineCommunicationReceived;
        // Navigation
        public override event ReceiveNavigationInstructionCommunicationFrame NavigationInstructionCommunicationReceived;
        // ControlInfo
        public override event ReceiveControlInfoCommunicationFrame ControlInfoCommunicationReceived;
        public override event ReceiveServosCommunicationFrame ServosCommunicationReceived;
        // Communication status
        public override event LostCommunication CommunicationLost;
        public override event EstablishedCommunication CommunicationEstablished;

        public override double SecondsConnectionLost()
        {
            return (DateTime.Now - LastValidFrame).TotalSeconds;
        }

        private string[] DatalogHeader;

        public SerialCommunication_CSV()
        {
            LastValidFrame = DateTime.Now;
            _serialPort = new SerialPort();
            last_throughput_calculation = DateTime.Now;

            _smartThreadPool = new SmartThreadPool();
            _smartThreadPool.Name = "ReceiveThreadedData";

            IWorkItemResult wir =
                        _smartThreadPool.QueueWorkItem(
                            new WorkItemCallback(this.ReceiveThreadedData), _serialPort);
        }

        ~SerialCommunication_CSV()
        {
            if (_smartThreadPool != null)
                _smartThreadPool.Shutdown(false, 100);
            if (logfile != null)
                logfile.Close();

            _serialPort.Dispose();
        }

        public double ThroughputKbS()
        {
            double s;

            lock (this)
            {
                s = bytes_read;
                bytes_read = 0;
            }

            if ((double)(DateTime.Now - last_throughput_calculation).Seconds > 1e-6)
            {
                s /= (double)(DateTime.Now - last_throughput_calculation).Seconds;
                download_speed_kb_s = s;
            } 
            last_throughput_calculation = DateTime.Now;
            return download_speed_kb_s;
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
        }

        public override void Close()
        {
            if (_serialPort != null)
                _serialPort.Close();

        }

        /*!
         *    This function (that executes in a separate thread) is an
         *    infinite loop that receives all lines from the serial port
         *    and parses and executes them.
         */
        private object ReceiveThreadedData(object state)
        {
            _serialPort.ReadTimeout = 1000;
            bool recognised_frame = true;
            string line = string.Empty;

            while (true)//_serialPort.IsOpen)
            {
                try
                {
                    // A bit extra logic to set communication lost after 1 second of no data 
                    while (/*_serialPort == null || !_serialPort.IsOpen ||*/ _serialPort.BytesToRead < 3)
                    {
                        if (CommunicationAlive && SecondsConnectionLost() > 1.0)
                        {
                            if (CommunicationLost != null && SecondsConnectionLost() >= 5.0)
                            {
                                CommunicationAlive = false;
                                CommunicationLost();
                            }
                        }
                        Thread.Sleep(50);
                        //Console.WriteLine("Waiting for communication...");
                    }

                    line = _serialPort.ReadLine().Replace("\r", "").Replace("\n", "");

                    if (line.StartsWith("$")) // line with checksum
                    {
                        string[] frame = line.Substring(1, line.Length-1).Split('*');
                        //line = frame[0];
                        if (calculateChecksum(frame[0]) == Int32.Parse(frame[1], System.Globalization.NumberStyles.HexNumber))
                            line = frame[0];
                        else
                            throw new Exception("Checksum error");
                    }

                    if (logfile != null)
                        logfile.WriteLine("[" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture) + "] " + line);

                    lock (this)
                    {
                        bytes_read += line.Length + 1;
                    }

                    line = line.Replace("\r", "");

                    string[] lines = line.Split(';');
                    //Console.WriteLine(line + "\n\r");
                    // TR: Gyro & Acc raw
                    recognised_frame = true;
                    if (lines[0].EndsWith("TR") && lines.Length >= 6)
                    {
                        double acc_x_raw = double.Parse(lines[1]);
                        double acc_y_raw = double.Parse(lines[2]);
                        double acc_z_raw = double.Parse(lines[3]);
                        double gyro_x = double.Parse(lines[4]);
                        double gyro_y = double.Parse(lines[5]);
                        double gyro_z = double.Parse(lines[6]);
                        GyroAccRaw ga = new GyroAccRaw(acc_x_raw, acc_y_raw, acc_z_raw, gyro_x, gyro_y, gyro_z);
                        if (GyroAccRawCommunicationReceived != null)
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
                        if (GyroAccProcCommunicationReceived != null)
                            GyroAccProcCommunicationReceived(ga);
                    }

                    // TH: Pressure & Temp
                    else if (lines[0].EndsWith("TH") && lines.Length >= 2)
                    {
                        float pressure = float.Parse(lines[1]);
                        float temp = float.Parse(lines[2]);
                        PressureTemp pt = new PressureTemp(temp, pressure);
                        if (PressureTempCommunicationReceived != null)
                            PressureTempCommunicationReceived(pt);
                    }

                    // CA: All configuration
                    else if (lines[0].EndsWith("CA") && lines.Length >= 2)
                    {
                        Console.WriteLine("receiving...");
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
                        ac.telemetry_attitude = int.Parse(lines[12]);

                        ac.gps_initial_baudrate = int.Parse(lines[13]) * 10;
                        ac.gps_operational_baudrate = int.Parse(lines[14]) * 10;

                        ac.channel_ap = int.Parse(lines[15]) + 1;
                        ac.channel_motor = int.Parse(lines[16]) + 1;
                        ac.channel_pitch = int.Parse(lines[17]) + 1;
                        ac.channel_roll = int.Parse(lines[18]) + 1;
                        ac.channel_yaw = int.Parse(lines[19]) + 1;

                        ac.pid_pitch2elevator_p = double.Parse(lines[20], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_pitch2elevator_d = double.Parse(lines[21], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_pitch2elevator_i = double.Parse(lines[22], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_pitch2elevator_imin = double.Parse(lines[23], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_pitch2elevator_imax = double.Parse(lines[24], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_pitch2elevator_dmin = double.Parse(lines[25], System.Globalization.CultureInfo.InvariantCulture);

                        ac.pid_roll2aileron_p = double.Parse(lines[26], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_roll2aileron_d = double.Parse(lines[27], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_roll2aileron_i = double.Parse(lines[28], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_roll2aileron_imin = double.Parse(lines[29], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_roll2aileron_imax = double.Parse(lines[30], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_roll2aileron_dmin = double.Parse(lines[31], System.Globalization.CultureInfo.InvariantCulture);

                        ac.pid_heading2roll_p = double.Parse(lines[32], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_heading2roll_d = double.Parse(lines[33], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_heading2roll_i = double.Parse(lines[34], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_heading2roll_imin = double.Parse(lines[35], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_heading2roll_imax = double.Parse(lines[36], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_heading2roll_dmin = double.Parse(lines[37], System.Globalization.CultureInfo.InvariantCulture);

                        ac.pid_altitude2pitch_p = double.Parse(lines[38], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_altitude2pitch_d = double.Parse(lines[39], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_altitude2pitch_i = double.Parse(lines[40], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_altitude2pitch_imin = double.Parse(lines[41], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_altitude2pitch_imax = double.Parse(lines[42], System.Globalization.CultureInfo.InvariantCulture);
                        ac.pid_altitude2pitch_dmin = double.Parse(lines[43], System.Globalization.CultureInfo.InvariantCulture);

                        int r = int.Parse(lines[44]);
                        byte r2 = (byte)r;
                        ac.servo_reverse[0] = (r & 1) != 0;
                        ac.servo_reverse[1] = (r & 2) != 0;
                        ac.servo_reverse[2] = (r & 4) != 0;
                        ac.servo_reverse[3] = (r & 8) != 0;
                        ac.servo_reverse[4] = (r & 16) != 0;
                        ac.servo_reverse[5] = (r & 32) != 0;

                        for (int i = 0; i < 6; i++)
                        {
                            ac.servo_min[i] = int.Parse(lines[45 + i * 3], System.Globalization.CultureInfo.InvariantCulture);
                            ac.servo_max[i] = int.Parse(lines[46 + i * 3], System.Globalization.CultureInfo.InvariantCulture);
                            ac.servo_neutral[i] = int.Parse(lines[47 + i * 3], System.Globalization.CultureInfo.InvariantCulture);
                        }

                        ac.rc_ppm = 1 - int.Parse(lines[63]);

                        ac.control_mixing = int.Parse(lines[64]);
                        ac.control_max_pitch = int.Parse(lines[65]);
                        ac.control_max_roll = int.Parse(lines[66]);

                        // for backwards compatibility
                        if (lines.Length > 67)
                            ac.control_waypoint_radius = int.Parse(lines[67]);
                        if (lines.Length > 68)
                            ac.control_cruising_speed = int.Parse(lines[68]);
                        if (lines.Length > 69)
                            ac.control_stabilization_with_altitude_hold = int.Parse(lines[69]) == 0 ? false : true;
                        if (lines.Length > 70)
                            ac.control_aileron_differential = int.Parse(lines[70]);
                        if (lines.Length > 71)
                            ac.telemetry_control = int.Parse(lines[71]);
                        if (lines.Length > 72)
                        {
                            ac.auto_throttle_enabled = int.Parse(lines[72]) == 1;
                            ac.auto_throttle_min_pct = int.Parse(lines[73]);
                            ac.auto_throttle_max_pct = int.Parse(lines[74]);
                            ac.auto_throttle_cruise_pct = int.Parse(lines[75]);
                            ac.auto_throttle_p_gain_10 = int.Parse(lines[76]);
                        }
                        if (lines.Length > 77)
                            ac.control_min_pitch = int.Parse(lines[77]);
                        if (lines.Length > 78)
                            ac.manual_trim = int.Parse(lines[78]) == 0 ? false : true;
                        else
                            Console.WriteLine("FOUT");

                        Console.WriteLine("receive: " + lines[78]);
                        if (AllConfigCommunicationReceived != null)
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
                        if (RcInputCommunicationReceived != null)
                            RcInputCommunicationReceived(rc);
                    }
                    // TG: GPS basic
                    else if (lines[0].EndsWith("TG") && lines.Length >= 7)
                    {
                        //Console.WriteLine(line);
                        GpsBasic gb = new GpsBasic(
                            double.Parse(lines[2], System.Globalization.CultureInfo.InvariantCulture),
                            double.Parse(lines[3], System.Globalization.CultureInfo.InvariantCulture),
                            int.Parse(lines[7]),
                            double.Parse(lines[5]) / 100,
                            double.Parse(lines[4]) / 10,
                            int.Parse(lines[6]),
                            int.Parse(lines[1])
                            );
                        if (GpsBasicCommunicationReceived != null)
                            GpsBasicCommunicationReceived(gb);
                    }
                    // TA: Attitude
                    else if (lines[0].EndsWith("TA") && lines.Length >= 3)
                    {
                        Attitude att = new Attitude(
                            double.Parse(lines[1], CultureInfo.InvariantCulture) / 1000.0 / 3.14 * 180.0,
                            double.Parse(lines[2], CultureInfo.InvariantCulture) / 1000.0 / 3.14 * 180.0,
                            /*double.Parse(lines[3], CultureInfo.InvariantCulture) / 1000.0 / 3.14 * 180.0,
                            double.Parse(lines[4], CultureInfo.InvariantCulture) / 1000.0 / 3.14 * 180.0,*/0,0,
                            double.Parse(lines[3], CultureInfo.InvariantCulture) / 1000.0 / 3.14 * 180.0
                            );
                        if (AttitudeCommunicationReceived != null)
                            AttitudeCommunicationReceived(att);
                    }
                    // DT: Datalog table
                    else if (lines[0].EndsWith("DT") && lines.Length >= 4)
                    {
                        DatalogTable dt = new DatalogTable(
                            int.Parse(lines[1]),
                            int.Parse(lines[3]),
                            int.Parse(lines[4]),
                            int.Parse(lines[2]),
                            0);
                        if (DatalogTableCommunicationReceived != null)
                            DatalogTableCommunicationReceived(dt);
                    }
                    // DH: Datalog header
                    else if (lines[0].EndsWith("DH") && lines.Length >= 4)
                    {
                        DatalogHeader = new string[lines.Length - 1];

                        for (int i = 1; i < lines.Length; i++)
                            DatalogHeader[i - 1] = lines[i];

                    }
                    // DD: Datalog data
                    else if (lines[0].EndsWith("DD") && lines.Length >= 4)
                    {
                        string[] logline = new string[DatalogHeader.Length];
                        for (int i = 0; i < logline.Length; i++)
                            logline[i] = lines[i + 1];
                        DatalogLine dl = new DatalogLine(
                            logline, DatalogHeader);
                        if (DatalogLineCommunicationReceived != null)
                            DatalogLineCommunicationReceived(dl);
                    }
                    // ND: Navigation data (Navigation instruction)
                    else if (lines[0].EndsWith("ND") && lines.Length >= 6)
                    {
                        Console.WriteLine(line);
                        lines[1] = lines[1].Replace("nan", "0");
                        lines[2] = lines[2].Replace("nan", "0");
                        lines[3] = lines[3].Replace("nan", "0");
                        lines[4] = lines[4].Replace("nan", "0");
                        lines[5] = lines[5].Replace("nan", "0");
                        lines[6] = lines[6].Replace("nan", "0");

                        NavigationInstruction ni = 
                            new NavigationInstruction(
                                int.Parse(lines[1]),
                                (NavigationInstruction.navigation_command)int.Parse(lines[2]),
                                double.Parse(lines[3], CultureInfo.InvariantCulture),
                                double.Parse(lines[4], CultureInfo.InvariantCulture),
                                int.Parse(lines[5]),
                                int.Parse(lines[6]) );
                        if (NavigationInstructionCommunicationReceived != null)
                            NavigationInstructionCommunicationReceived(ni);
                    }
                    // TS: Servos (simulation)
                    else if (lines[0].EndsWith("TS") && lines.Length >= 3)
                    {
                        Console.WriteLine(line);

                        Servos s = 
                            new Servos(
                                int.Parse(lines[1]),
                                int.Parse(lines[2]),
                                int.Parse(lines[3]));
                        if (ServosCommunicationReceived != null)
                            ServosCommunicationReceived(s);
                    }
                    // Control
                    else if (lines[0].EndsWith("TC") && lines.Length >= 3)
                    {
                        ControlInfo ci =
                            new ControlInfo();
                        ci.FlightMode = (ControlInfo.FlightModes)int.Parse(lines[1]);
                        ci.CurrentNavigationLine = int.Parse(lines[2]);
                        ci.HeightAboveStartGround = int.Parse(lines[3]);
                        if (lines.Length >= 5)
                        {
                            ci.BattVoltage = double.Parse(lines[4]) / 10.0;
                            if (lines.Length >= 6)
                            {
                                ci.FlightTime = int.Parse(lines[5]);
                                ci.BlockTime = int.Parse(lines[6]);
                                ci.RcLink = int.Parse(lines[7]);
                                ci.Throttle = int.Parse(lines[8]);
                            }
                        }
                        if (ControlInfoCommunicationReceived != null)
                            ControlInfoCommunicationReceived(ci);
                    }
                    else
                    {
                        recognised_frame = false;
                        Console.WriteLine(line);
                        if (NonParsedCommunicationReceived != null)
                            NonParsedCommunicationReceived(line);
                    }
                }
                catch (TimeoutException toe)
                {
                    if (CommunicationAlive)
                    {
                        if (CommunicationLost != null && SecondsConnectionLost() >= 5.0)
                        {
                            CommunicationLost();
                            CommunicationAlive = false;
                        }
                    }
                }
                catch (IOException ioe)
                {
                    // happens when thread is shut down
                }
                catch (Exception e)
                {
                    ;
                }

                try
                {
                    if (recognised_frame)
                    {
                        //Console.WriteLine(line);
                        LastValidFrame = DateTime.Now;
                        FramesReceived++;
                        if (!CommunicationAlive)
                        {
                            CommunicationAlive = true;
                            if (CommunicationEstablished != null)
                                CommunicationEstablished();
                        }
                    }
                    if (CommunicationReceived != null)
                        CommunicationReceived(line);
                }
                catch (Exception e)
                {
                    ;
                }
            }

            return null;
        }


        public override void SendTelemetry(int basicgps, int gyroaccraw, int gyroaccproc, int ppm, int pressuretemp, int attitude, int control)
        {
            // telemetry
            WriteChecksumLine("ST;" +
                basicgps.ToString() + ";" +
                gyroaccraw.ToString() + ";" +
                gyroaccproc.ToString() + ";" +
                ppm.ToString() + ";" +
                pressuretemp.ToString() + ";" +
                attitude.ToString() + ";" +
                control.ToString() + "");
            /*Console.WriteLine("\nST;" +
                basicgps.ToString() + ";" +
                gyroaccraw.ToString() + ";" +
                gyroaccproc.ToString() + ";" +
                ppm.ToString() + ";" +
                pressuretemp.ToString() + ";" +
                attitude.ToString() + ";" +
                control.ToString() + "\n");*/
        }

        public override void SendServoReverse(bool a, bool b, bool c, bool d, bool e, bool f, bool manual_trim)
        {
            int s = 0;
            if (a)
                s += 1;
            if (b)
                s += 2;
            if (c)
                s += 4;
            if (d)
                s += 8;
            if (e)
                s += 16;
            if (f)
                s += 32;
            WriteChecksumLine("SR;" + s.ToString() + ";" + ((manual_trim==true)?"1":"0"));
            Console.WriteLine("send: " + manual_trim);
        }

        public override void SendServoMinNeutralMax(int nr, int min, int neutral, int max)
        {
            WriteChecksumLine("SM;" + nr + ";" + min + ";" + neutral + ";" + max); // nr = zero based
        }

        public override void SendConfigChannels(int is_ppm, int channel_ap, int channel_motor, int channel_pitch, int channel_roll, int channel_yaw)
        {
            WriteChecksumLine("SI;" +
               (1 - is_ppm).ToString() + ";" +
               Char.ConvertFromUtf32(97 + channel_ap - 1) + ";" +
               Char.ConvertFromUtf32(97 + channel_motor - 1) + ";" +
               Char.ConvertFromUtf32(97 + channel_pitch - 1) + ";" +
               Char.ConvertFromUtf32(97 + channel_roll - 1) + ";" +
               Char.ConvertFromUtf32(97 + channel_yaw - 1) + "");
            /*Console.WriteLine("\nSI;" +
                (1 - is_ppm).ToString() + ";" +
                Char.ConvertFromUtf32(97 + channel_ap - 1) + ";" +
                Char.ConvertFromUtf32(97 + channel_motor - 1) + ";" +
                Char.ConvertFromUtf32(97 + channel_pitch - 1) + ";" +
                Char.ConvertFromUtf32(97 + channel_roll - 1) + ";" +
                Char.ConvertFromUtf32(97 + channel_yaw - 1) + "\n");*/
        }


        public override void SendPidPitch2Elevator(double p, double i, double d, double imin, double imax, double dmin)
        {
            WriteChecksumLine("PP;" +
                p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "");
        }
    

        public override void SendPidRoll2Aileron(double p, double i, double d, double imin, double imax, double dmin)
        {
            WriteChecksumLine("PR;" +
                p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "");   
        }

        public override void SendAutoThrottleConfig(int auto_throttle_min_pct, int auto_throttle_max_pct, int auto_throttle_cruise_pct, int auto_throttle_p_gain_10, bool auto_throttle_enabled)
        {
            WriteChecksumLine("AT;" +
                auto_throttle_min_pct + ";" + auto_throttle_max_pct + ";" +
                auto_throttle_cruise_pct + ";" + auto_throttle_p_gain_10 + ";" +
                (auto_throttle_enabled ? "1" : "0"));
            /*Console.WriteLine("\nAT;" +
                auto_throttle_min_pct + ";" + auto_throttle_max_pct + ";" +
                auto_throttle_cruise_pct + ";" + auto_throttle_p_gain_10 + ";" +
                (auto_throttle_enabled ? "1" : "0"));*/
        }

        public override void SendControlSettings(int mixing, double max_pitch, double min_pitch, double max_roll, int aileron_differential, double waypoint_radius, double cruising_speed, bool stabilization_with_altitude_hold)
        {
            WriteChecksumLine("SC;" +
                mixing.ToString() + ";" +
                max_pitch.ToString(CultureInfo.InvariantCulture) + ";" +
                max_roll.ToString(CultureInfo.InvariantCulture) + ";" +
                aileron_differential.ToString() + ";" +
                waypoint_radius.ToString(CultureInfo.InvariantCulture) + ";" +
                cruising_speed.ToString(CultureInfo.InvariantCulture) + ";" +
                (stabilization_with_altitude_hold == false ? 0 : 1).ToString() + ";"+
                min_pitch.ToString(CultureInfo.InvariantCulture));


            /*Console.WriteLine("\nSC;" +
                mixing.ToString() + ";" +
                max_pitch.ToString(CultureInfo.InvariantCulture) + ";" +
                max_roll.ToString(CultureInfo.InvariantCulture) + ";" +
                waypoint_radius.ToString(CultureInfo.InvariantCulture) + ";" +
                cruising_speed.ToString(CultureInfo.InvariantCulture) + ";" +
                (stabilization_with_altitude_hold == false ? 0 : 1).ToString() + "\n");*/

        }

        /*!
         *    Sends the complete configuration set AllConfig to the gluonpilot.
         */
        public override void Send(AllConfig ac)
        {
            SendTelemetry(
                ac.telemetry_basicgps,
                ac.telemetry_gyroaccraw,
                ac.telemetry_gyroaccproc,
                ac.telemetry_ppm,
                ac.telemetry_pressuretemp,
                ac.telemetry_attitude,
                ac.telemetry_control);

            Thread.Sleep(200);
            
            // neutral acc
            WriteChecksumLine("SA;" +
                ac.acc_x_neutral.ToString() + ";" +
                ac.acc_y_neutral.ToString() + ";" +
                ac.acc_z_neutral.ToString() + "");
            /*Console.WriteLine("\nSA;" +
                ac.acc_x_neutral.ToString() + ";" +
                ac.acc_y_neutral.ToString() + ";" +
                ac.acc_z_neutral.ToString() + "\n");*/
            Thread.Sleep(200);

            // neutral gyro
            WriteChecksumLine("SY;" +
                ac.gyro_x_neutral.ToString() + ";" +
                ac.gyro_y_neutral.ToString() + ";" +
                ac.gyro_z_neutral.ToString() + "");
            /*Console.WriteLine("\nSY;" +
                ac.gyro_x_neutral.ToString() + ";" +
                ac.gyro_y_neutral.ToString() + ";" +
                ac.gyro_z_neutral.ToString() + "\n");*/
            Thread.Sleep(200);
            
            // channel config
            SendConfigChannels(ac.rc_ppm, ac.channel_ap, ac.channel_motor, ac.channel_pitch, ac.channel_roll, ac.channel_yaw);

            Thread.Sleep(200);
            // gps config
            WriteChecksumLine("SG;" + (ac.gps_initial_baudrate / 10).ToString() + "");
            //Console.WriteLine("\nSG;" + (ac.gps_initial_baudrate / 10).ToString() + "\n");

            Thread.Sleep(200);

            SendPidPitch2Elevator(ac.pid_pitch2elevator_p, ac.pid_pitch2elevator_i, ac.pid_pitch2elevator_d,
                                ac.pid_pitch2elevator_imin, ac.pid_pitch2elevator_imax, ac.pid_pitch2elevator_dmin);

            Thread.Sleep(200);

            SendPidRoll2Aileron(ac.pid_roll2aileron_p, ac.pid_roll2aileron_i, ac.pid_roll2aileron_d,
                                ac.pid_roll2aileron_imin, ac.pid_roll2aileron_imax, ac.pid_roll2aileron_dmin);

            Thread.Sleep(200);

            WriteChecksumLine("PH;" +
                ac.pid_heading2roll_p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "");
            /*Console.WriteLine("\nPH;" +
                ac.pid_heading2roll_p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_heading2roll_dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\n");
            */
            Thread.Sleep(200);

            WriteChecksumLine("PA;" +
                ac.pid_altitude2pitch_p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "");
            /*Console.WriteLine("\nPA;" +
                ac.pid_altitude2pitch_p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\n");
            */
            Thread.Sleep(200);

            SendServoReverse(ac.servo_reverse[0], ac.servo_reverse[1], ac.servo_reverse[2], ac.servo_reverse[3], ac.servo_reverse[4], ac.servo_reverse[5], ac.manual_trim);
            Thread.Sleep(200);

            if (ac.manual_trim)
            {
                for (int i = 0; i < 6; i++)
                {
                    SendServoMinNeutralMax(i, ac.servo_min[i], ac.servo_neutral[i], ac.servo_max[i]);
                    Thread.Sleep(200);
                }
            }


            SendControlSettings(ac.control_mixing,
                                ac.control_max_pitch,
                                ac.control_min_pitch,
                                ac.control_max_roll,
                                ac.control_aileron_differential,
                                ac.control_waypoint_radius,
                                ac.control_cruising_speed,
                                ac.control_stabilization_with_altitude_hold);

            Thread.Sleep(200);

            SendAutoThrottleConfig(ac.auto_throttle_min_pct, ac.auto_throttle_max_pct, ac.auto_throttle_cruise_pct, ac.auto_throttle_p_gain_10, ac.auto_throttle_enabled);
        }

        public override void ReadAllConfig()
        {
            WriteChecksumLine("RC;A");
        }

        public override void SendWriteTelemetry(int basicgps, int gyroaccraw, int gyroaccproc, int ppm, int pressuretemp, int attitude, int control)
        {
            WriteChecksumLine("ST;" +
                basicgps + ";" +
                gyroaccraw + ";" +
                gyroaccproc + ";" +
                ppm + ";" +
                pressuretemp + ";" +
                attitude + ";" +
                control + "");
            Thread.Sleep(200);
        }

        public override void SendFlashConfiguration()
        {
            WriteChecksumLine("FC;");
        }

        public override void SendLoadConfigurationFromFlash()
        {
            WriteChecksumLine("LC;");
        }

        public override void SendLoadConfigurationDefault()
        {
            WriteChecksumLine("LD;");
        }

        public override void SendDatalogFormat()
        {
            WriteChecksumLine("FF;");
        }

        public override void SendDatalogTableRequest()
        {
            _serialPort.WriteLine("\nFI;\n");
        }

        public override void SendDatalogTableRead(int i)
        {
            _serialPort.WriteLine("\nDR;" + i.ToString() + "\n");
        }

        public override void SendNavigationInstruction(NavigationInstruction ni)
        {
            WriteChecksumLine("WN;" + ni.line.ToString() + ";" + (int)ni.opcode + ";" + 
                ni.x.ToString(CultureInfo.InvariantCulture) + ";" + ni.y.ToString(CultureInfo.InvariantCulture) + ";" +
                ni.a.ToString() + ";" + ni.b.ToString());
            /*Console.WriteLine("\nWN;" + ni.line.ToString() + ";" +
                ni.x.ToString(CultureInfo.InvariantCulture) + ";" + ni.y.ToString(CultureInfo.InvariantCulture) + ";" +
                ni.a.ToString() + ";" + ni.b.ToString() + "\n");*/
        }

        public override void SendJumpToNavigationLine(int line)
        {
            //_serialPort.WriteLine("\nJN;" + line + "\n");
            WriteChecksumLine("JN;" + line);
            Console.WriteLine("\nJN;" + line + "\n");
        }

        public override void SendNavigationBurn()
        {
            WriteChecksumLine("FN;");
        }

        public override void SendNavigationRead()
        {
            WriteChecksumLine("RN;");
        }

        public override void SendNavigationLoad()
        {
            WriteChecksumLine("LN;");
        }

        public override void SendReboot()
        {
            _serialPort.WriteLine("\nZZ;1123\n");
        }

        public override void SetSimulationOn()
        {
            _serialPort.WriteLine("\nSE;\n");
        }

        public override void SendSimulationUpdate(double lat_rad, double lng_rad, double roll_rad, double pitch_rad, double altitude_m, double speed_ms, double heading_rad)
        {
            _serialPort.WriteLine("\nSW;" + lng_rad.ToString("#.######", CultureInfo.InvariantCulture) + ";" +
                                          lat_rad.ToString("#.######", CultureInfo.InvariantCulture) + ";" +
                                          heading_rad.ToString("#.####", CultureInfo.InvariantCulture) + ";" +
                                          speed_ms.ToString("#.###", CultureInfo.InvariantCulture) + ";" +
                                          ((int)altitude_m).ToString() + ";" +
                                          roll_rad.ToString("#.###", CultureInfo.InvariantCulture) + ";" +
                                          pitch_rad.ToString("#.###", CultureInfo.InvariantCulture) + "\n");
            Console.WriteLine("\nSW;" + lng_rad.ToString("#.######", CultureInfo.InvariantCulture) + ";" +
                                          lat_rad.ToString("#.######", CultureInfo.InvariantCulture) + ";" +
                                          heading_rad.ToString("#.####", CultureInfo.InvariantCulture) + ";" +
                                          speed_ms.ToString("#.###", CultureInfo.InvariantCulture) + ";" +
                                          ((int)altitude_m).ToString() + ";" +
                                          roll_rad.ToString("#.####", CultureInfo.InvariantCulture) + ";" +
                                          pitch_rad.ToString("#.###", CultureInfo.InvariantCulture) + "\n");
        }


        public override void SendCalibrateGyros()
        {
            WriteChecksumLine("CG;");
        }


        public override void SendCalibrateAcceleros()
        {
            WriteChecksumLine("CA;");
        }

        public int calculateChecksum(string s)
        {
            int c = 0;
            foreach (char h in s.ToCharArray())
            {
                c ^= (int)(h);
            }
            return c;
        }


        private void WriteChecksumLine(string s)
        {
            int chk = calculateChecksum(s);
            if (chk < 16)
            {
                if (_serialPort.BytesToWrite > 0)
                    Thread.Sleep(200); ;
                _serialPort.WriteLine("\n$" + s + "*0" + Convert.ToString(chk, 16) + "\n");
                //Console.WriteLine("\n$" + s + "*0" + Convert.ToString(chk, 16) + "\n");
            }
            else
            {
                _serialPort.WriteLine("\n$" + s + "*" + Convert.ToString(chk, 16) + "\n");
                //Console.WriteLine("\n$" + s + "*" + Convert.ToString(chk, 16) + "\n");
            }
        }
    }
}
