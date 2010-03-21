using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Communication;
using Communication.Frames.Configuration;
using System.Threading;
using System.IO;
using System.Xml.Serialization;

namespace Gluonpilot
{
    public partial class Form1 : Form
    {
        private SerialCommunication_CSV _serial;

        public Form1()
        {
            InitializeComponent();
            
            _serial = new SerialCommunication_CSV();
            InvalidateEnableds();
        }
      

        private delegate void UpdateTextBox(string line);
        private void ReceiveCommunication(string line)
        {
            if (! this._cb_hide_parsed.Checked)
                this.BeginInvoke(new UpdateTextBox(UpdateText), new object[] { line });
        }
        private void ReceiveNonParsedCommunication(string line)
        {
            if (this._cb_hide_parsed.Checked)
                this.BeginInvoke(new UpdateTextBox(UpdateText), new object[] { line });
        }
        private void UpdateText(string line)
        {
            if (_cb_print_timestamp.Checked)
                textBox1.AppendText("[" + DateTime.Now.ToString("hh:mm:ss.ff") + "]  ");
            textBox1.AppendText(line + "\r\n");
        }

        private void _btn_read_config_Click(object sender, EventArgs e)
        {
            _serial.ReadAllConfig();
        }

        private void _btn_connect_Click(object sender, EventArgs e)
        {
            if (!_serial.IsOpen)
            {
                ConnectDialog cd = new ConnectDialog();
                cd.ShowDialog();

                _serial = new SerialCommunication_CSV();
                _serial.Open(cd.SelectedPort(), cd.SelectedBaudrate());
                _serial.CommunicationReceived +=
                    new SerialCommunication_CSV.ReceiveCommunication(ReceiveCommunication);
                _serial.NonParsedCommunicationReceived += new SerialCommunication.ReceiveNonParsedCommunication(ReceiveNonParsedCommunication);

                configurationFrame.Connect(_serial);
            }
            else
                _serial.Close();
            InvalidateEnableds();
        }

        private void InvalidateEnableds()
        {
            _btn_read.Enabled = _serial.IsOpen;
            _btn_burn.Enabled = _serial.IsOpen;
            _btn_write.Enabled = _serial.IsOpen;
            _btn_connect.Checked = _serial.IsOpen;
            _btn_reload.Enabled = _serial.IsOpen;
            _btnChart.Enabled = _serial.IsOpen;
            _btnLoadDefault.Enabled = _serial.IsOpen;
            _btnOpenGl.Enabled = _serial.IsOpen;
        }

        private void _btn_read_Click(object sender, EventArgs e)
        {
            _serial.ReadAllConfig();
        }

        private void _btn_write_Click(object sender, EventArgs e)
        {
            _serial.Send(configurationFrame.Model.ToAllConfig());
        }

        private void _btn_burn_Click(object sender, EventArgs e)
        {
            _serial.SendFlashConfiguration();
        }

        private void _btn_reload_Click(object sender, EventArgs e)
        {
            _serial.SendLoadConfigurationFromFlash();
        }

        private void _btnChart_Click(object sender, EventArgs e)
        {
            Graph.GraphForm gf = new Graph.GraphForm(_serial);
            gf.Activate();
            gf.Show(this);
        }

        private void _btnLoadDefault_Click(object sender, EventArgs e)
        {
            _serial.SendLoadConfigurationDefault();
            Thread.Sleep(100);
            _serial.ReadAllConfig();
        }

        private void _btn_save_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog file = new System.Windows.Forms.SaveFileDialog();
            file.DefaultExt = "gcf";
            file.Filter = "Gluon configuration file (*.gcf)|*.gcf|All files (*.*)|*.*";
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream stream = file.OpenFile();
                //BinaryFormatter bformatter = new BinaryFormatter();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConfigurationModel));

                Console.WriteLine("Writing model information");
                xmlSerializer.Serialize(stream, configurationFrame.Model);
                stream.Close();
            }
        }

        private void _btn_open_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog file = new System.Windows.Forms.OpenFileDialog();
            file.Filter = "Gluon configuration file (*.gcf)|*.gcf|All files (*.*)|*.*";
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream stream = File.OpenRead(file.FileName);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConfigurationModel));

                Console.WriteLine("Reading model information");

                ConfigurationModel model = (ConfigurationModel)xmlSerializer.Deserialize(stream);
                configurationFrame.Model = model;
                stream.Close();
            }
            
        }

        private void _btnOpenGl_Click(object sender, EventArgs e)
        {
            ModuleImu3D.Imu3D.Run(_serial);
        }
     

    }
}
