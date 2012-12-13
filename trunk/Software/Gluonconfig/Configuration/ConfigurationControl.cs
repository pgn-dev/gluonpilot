using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Communication;
using Gluonpilot;
using System.IO;
using System.Xml.Serialization;

namespace Configuration
{
    public partial class ConfigurationControl : UserControl
    {
        private SerialCommunication _serial;

        public ConfigurationControl()
        {
            InitializeComponent();
            _serial = null;
            _btn_burn.Enabled = false;
            _btn_default.Enabled = false;
            _btn_read.Enabled = false;
            _btn_reload.Enabled = false;
            _btn_write.Enabled = false;
        }

        public void Connect(SerialCommunication serial)
        {
            _serial = serial;
            _btn_burn.Enabled = true;
            _btn_default.Enabled = true;
            _btn_read.Enabled = true;
            _btn_reload.Enabled = true;
            _btn_write.Enabled = true;
            
            configurationTabpage1.Connect(serial);
            timer1.Start();
        }

        public void Disconnect()
        {
            timer1.Stop();
            _serial = null;
            _btn_burn.Enabled = false;
            _btn_default.Enabled = false;
            _btn_read.Enabled = false;
            _btn_reload.Enabled = false;
            _btn_write.Enabled = false;

            configurationTabpage1.Disconnect();
        }

        public void SetModel(ConfigurationModel model)
        {
            configurationTabpage1.SetModel(model);
        }

        private void _btn_read_Click(object sender, EventArgs e)
        {
            _serial.ReadAllConfig();
        }

        private void _btn_write_Click(object sender, EventArgs e)
        {
            _serial.Send(configurationTabpage1.GetModel().ToAllConfig());
            _serial.SendImuSettings(configurationTabpage1.GetModel().NeutralPitch, configurationTabpage1.GetModel().ImuRotated);
        }

        private void _btn_default_Click(object sender, EventArgs e)
        {
            _serial.SendLoadConfigurationDefault();
        }

        private void _btn_burn_Click(object sender, EventArgs e)
        {
            _serial.SendFlashConfiguration();
        }

        private void _btn_reload_Click(object sender, EventArgs e)
        {
            _serial.SendLoadConfigurationFromFlash();
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
                xmlSerializer.Serialize(stream, configurationTabpage1.GetModel());
                stream.Close();
            }
        }

        private void _btn_load_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog file = new System.Windows.Forms.OpenFileDialog();
            file.Filter = "Gluon configuration file (*.gcf)|*.gcf|All files (*.*)|*.*";
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream stream = File.OpenRead(file.FileName);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConfigurationModel));

                Console.WriteLine("Reading model information");

                ConfigurationModel model = (ConfigurationModel)xmlSerializer.Deserialize(stream);
                configurationTabpage1.SetModel(model);
                stream.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_serial != null && _serial.IsOpen)
            {
                _btn_burn.Enabled = true;
                _btn_default.Enabled = true;
                _btn_read.Enabled = true;
                _btn_reload.Enabled = true;
                _btn_write.Enabled = true;
            }
            else
            {
                _btn_burn.Enabled = false;
                _btn_default.Enabled = false;
                _btn_read.Enabled = false;
                _btn_reload.Enabled = false;
                _btn_write.Enabled = false;
            }
        }
    }
}
