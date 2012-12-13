/*!
 *   SerialCommunication.c
 *   Implements the gluonpilot communication protocol.
 *   
 *   
 *   
 *   @author  Tom Pycke
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
using Communication.Frames.Configuration;
using Communication.Frames.Incoming;

namespace Communication
{
    public abstract class SerialCommunication
    {
        protected SerialPort _serialPort;   // COM communication port
        public abstract string LogToFilename { get; set; }

        // delegates used for the communication frames events
        public delegate void ReceiveCommunication(string line);
        public delegate void ReceiveNonParsedCommunication(string line);
        public delegate void ReceiveGyroAccRawCommunicationFrame(GyroAccRaw ga);
        public delegate void ReceiveGyroAccProcCommunicationFrame(GyroAccProcessed ga);
        public delegate void ReceivePressureTempCommunicationFrame(PressureTemp info);
        public delegate void ReceiveAllConfigCommunicationFrame(AllConfig config);
        public delegate void ReceiveRcInputCommunicationFrame(RcInput rcinput);
        public delegate void ReceiveGpsBasicCommunicationFrame(GpsBasic gpsbasic);
        public delegate void ReceiveAttitudeCommunicationFrame(Attitude attitude);
        public delegate void ReceiveDatalogTableCommunicationFrame(DatalogTable table);
        public delegate void ReceiveDatalogLineCommunicationFrame(DatalogLine line);
        public delegate void ReceiveNavigationInstructionCommunicationFrame(NavigationInstruction ni);
        public delegate void ReceiveControlInfoCommunicationFrame(ControlInfo ci);
        public delegate void ReceiveServosCommunicationFrame(Servos s);

        public delegate void LostCommunication();
        public delegate void EstablishedCommunication();
        public delegate void HomePositionFrame(double lat, double lon);


        // General: all lines received will be broadcasted by this event
        public abstract event ReceiveCommunication CommunicationReceived;
        public abstract event ReceiveNonParsedCommunication NonParsedCommunicationReceived;  
        // Gyro & Acc
        public abstract event ReceiveGyroAccRawCommunicationFrame GyroAccRawCommunicationReceived;
        public abstract event ReceiveGyroAccProcCommunicationFrame GyroAccProcCommunicationReceived;
        // Pressure & Temperature
        public abstract event ReceivePressureTempCommunicationFrame PressureTempCommunicationReceived;
        // Configuration
        public abstract event ReceiveAllConfigCommunicationFrame AllConfigCommunicationReceived;
        // RC transmitter
        public abstract event ReceiveRcInputCommunicationFrame RcInputCommunicationReceived;
        // Gps Basic
        public abstract event ReceiveGpsBasicCommunicationFrame GpsBasicCommunicationReceived;
        // Attitude
        public abstract event ReceiveAttitudeCommunicationFrame AttitudeCommunicationReceived;
        // Datalog
        public abstract event ReceiveDatalogTableCommunicationFrame DatalogTableCommunicationReceived;
        public abstract event ReceiveDatalogLineCommunicationFrame DatalogLineCommunicationReceived;
        // Navigation
        public abstract event ReceiveNavigationInstructionCommunicationFrame NavigationInstructionCommunicationReceived;
        // ControlInfo
        public abstract event ReceiveControlInfoCommunicationFrame ControlInfoCommunicationReceived;
        public abstract event ReceiveServosCommunicationFrame ServosCommunicationReceived;
        // Communication status
        public abstract event LostCommunication CommunicationLost;
        public abstract event EstablishedCommunication CommunicationEstablished;
        // Home position
        public abstract event HomePositionFrame HomePositionReceived;
        

        public abstract double SecondsConnectionLost();

        public static string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        public string PortName
        {
            get { if (_serialPort != null) return _serialPort.PortName; else return ""; }
        }

        public int BaudRate
        {
            get { if (_serialPort != null) return _serialPort.BaudRate; else return 0; }
        }

        public bool IsOpen
        {
            get
            {
                return _serialPort != null && _serialPort.IsOpen;
            }
        }

        public abstract void Close();

        public abstract void SendTelemetry(int basicgps, int gyroaccraw, int gyroaccproc, int ppm, int pressuretemp, int attitude, int control);

        public abstract void SendServoReverse(bool a, bool b, bool c, bool d, bool e, bool f, bool manual_trim);

        public abstract void SendServoMinNeutralMax(int nr, int min, int neutral, int max);

        public abstract void SendConfigChannels(int is_ppm, int channel_ap, int channel_motor, int channel_pitch, int channel_roll, int channel_yaw);

        public abstract void SendPidPitch2Elevator(double p, double i, double d, double imin, double imax, double dmin);

        public abstract void SendPidRoll2Aileron(double p, double i, double d, double imin, double imax, double dmin);

        public abstract void SendAutoThrottleConfig(int auto_throttle_min_pct, int auto_throttle_max_pct, int auto_throttle_cruise_pct, int auto_throttle_p_gain_10, bool auto_throttle_enabled);

        public abstract void SendControlSettings(int mixing, double max_pitch, double min_pitch, double max_roll, int aileron_differential, double waypoint_radius, double cruising_speed, bool stabilization_with_altitude_hold, int altitude_mode);

        public abstract void SendImuSettings(int neutral_pitch, int imu_rotated);

        public abstract void Send(AllConfig ac);

        public abstract void ReadAllConfig();

        public abstract void SendFlashConfiguration();

        public abstract void SendOsdConfiguration(int bitmask, int rssi_mode, double voltage_low, double voltage_high);

        public abstract void SendLoadConfigurationFromFlash();
        
        public abstract void SendLoadConfigurationDefault();

        public abstract void SendDatalogFormat();

        public abstract void SendDatalogTableRequest();

        public abstract void SendDatalogTableRead(int i);

        public abstract void SendNavigationInstruction(NavigationInstruction ni);

        public abstract void SendJumpToNavigationLine(int line);

        public abstract void SendNavigationBurn();

        public abstract void SendNavigationRead();

        public abstract void SendNavigationLoad();

        public abstract void SendReboot();

        public abstract void SendWriteTelemetry(int basicgps, int gyroaccraw, int gyroaccproc, int ppm, int pressuretemp, int attitude, int control);

        public abstract void SetSimulationOn();

        public abstract void SendSimulationUpdate(double lat_rad, double lng_rad, double roll_rad, double pitch_rad, double altitude_m, double speed_ms, double heading_rad);

        public abstract void SendCalibrateGyros();

        public abstract void SendCalibrateAcceleros();
    }
}
