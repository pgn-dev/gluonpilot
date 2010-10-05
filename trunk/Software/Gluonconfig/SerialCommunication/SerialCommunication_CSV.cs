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
using System.Globalization;

namespace Communication
{
    public class SerialCommunication_CSV : SerialCommunication
    {
        private SmartThreadPool _smartThreadPool = new SmartThreadPool();
        private int bytes_read = 0;
        private DateTime last_throughput_calculation;

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

        private string[] DatalogHeader;

        public SerialCommunication_CSV()
        {
            _serialPort = new SerialPort();
            last_throughput_calculation = DateTime.Now;
        }

        public double ThroughputKbS()
        {
            double s;

            lock (this)
            {
                s = bytes_read;
                bytes_read = 0;
            }
            s /= (double)(DateTime.Now - last_throughput_calculation).Seconds;
            last_throughput_calculation = DateTime.Now;
            return s;
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
            _serialPort.ReadTimeout = 1000;
            while (_serialPort.IsOpen)
            {
                try
                {
                    while (_serialPort == null || !_serialPort.IsOpen || _serialPort.BytesToRead < 3)
                        Thread.Sleep(100);

                    string line = _serialPort.ReadLine();

                    lock (this)
                    {
                        bytes_read += line.Length + 1;
                    }

                    line = line.Replace("\r", "");

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
                            int.Parse(lines[7]),
                            double.Parse(lines[5]) / 100,
                            double.Parse(lines[4]) / 10,
                            int.Parse(lines[6]),
                            int.Parse(lines[1])
                            );
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
                    // Telemetry control
                    else if (lines[0].EndsWith("TC") && lines.Length >= 3)
                    {
                        ControlInfo ci =
                            new ControlInfo();
                        ci.FlightMode = (ControlInfo.FlightModes)int.Parse(lines[1]);
                        ci.CurrentNavigationLine = int.Parse(lines[2]);
                        ci.HeightAboveStartGround = int.Parse(lines[3]);

                        if (ControlInfoCommunicationReceived != null)
                            ControlInfoCommunicationReceived(ci);
                    }
                    else
                    {
                        NonParsedCommunicationReceived(line);
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
                ac.telemetry_pressuretemp.ToString() + ";" +
                ac.telemetry_attitude.ToString() + ";" +
                ac.telemetry_control.ToString() + "\n");
            Console.WriteLine("\nST;" +
                ac.telemetry_basicgps.ToString() + ";" +
                ac.telemetry_gyroaccraw.ToString() + ";" +
                ac.telemetry_gyroaccproc.ToString() + ";" +
                ac.telemetry_ppm.ToString() + ";" +
                ac.telemetry_pressuretemp.ToString() + ";" +
                ac.telemetry_attitude.ToString() + ";" +
                ac.telemetry_control.ToString() + "\n");

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
                (1 - ac.rc_ppm).ToString() + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_ap - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_motor - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_pitch - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_roll - 1) + ";" +
                Char.ConvertFromUtf32(97 + ac.channel_yaw - 1) + "\n");
            Console.WriteLine("\nSI;" +
                (1 - ac.rc_ppm).ToString() + ";" +
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

            _serialPort.WriteLine("\nPA;" +
                ac.pid_altitude2pitch_p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\n\n");
            Console.WriteLine("\nPA;" +
                ac.pid_altitude2pitch_p.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_i.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_d.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_imin.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_imax.ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" +
                ac.pid_altitude2pitch_dmin.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\n");

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

            Thread.Sleep(200);

            _serialPort.WriteLine("\nSC;" +
                ac.control_mixing.ToString() + ";" + 
                ac.control_max_pitch.ToString(CultureInfo.InvariantCulture) + ";" +
                ac.control_max_roll.ToString(CultureInfo.InvariantCulture) + ";" +
                ac.control_aileron_differential.ToString() + ";" +
                ac.control_waypoint_radius.ToString(CultureInfo.InvariantCulture) + ";" +
                ac.control_cruising_speed.ToString(CultureInfo.InvariantCulture) + ";" +
                (ac.control_stabilization_with_altitude_hold == false? 0:1).ToString() + "\n");


            Console.WriteLine("\nSC;" +
                ac.control_mixing.ToString() + ";" +
                ac.control_max_pitch.ToString(CultureInfo.InvariantCulture) + ";" +
                ac.control_max_roll.ToString(CultureInfo.InvariantCulture) + ";" +
                ac.control_waypoint_radius.ToString(CultureInfo.InvariantCulture) + ";" +
                ac.control_cruising_speed.ToString(CultureInfo.InvariantCulture) + ";" +
                (ac.control_stabilization_with_altitude_hold == false ? 0 : 1).ToString() + "\n");
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

        public override void SendDatalogFormat()
        {
            _serialPort.WriteLine("\nFF;\n");
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
            _serialPort.WriteLine("\nWN;" + ni.line.ToString() + ";" + (int)ni.opcode + ";" + 
                ni.x.ToString(CultureInfo.InvariantCulture) + ";" + ni.y.ToString(CultureInfo.InvariantCulture) + ";" +
                ni.a.ToString() + ";" + ni.b.ToString() + "\n");
            Console.WriteLine("\nWN;" + ni.line.ToString() + ";" +
                ni.x.ToString(CultureInfo.InvariantCulture) + ";" + ni.y.ToString(CultureInfo.InvariantCulture) + ";" +
                ni.a.ToString() + ";" + ni.b.ToString() + "\n");

        }

        public override void SendNavigationBurn()
        {
            _serialPort.WriteLine("\nFN;\n");
        }

        public override void SendNavigationRead()
        {
            _serialPort.WriteLine("\nRN;\n");
        }

        public override void SendNavigationLoad()
        {
            _serialPort.WriteLine("\nLN;\n");
        }

        public override void SendReboot()
        {
            _serialPort.WriteLine("\nZZ;1123\n");
        }
    }
}
