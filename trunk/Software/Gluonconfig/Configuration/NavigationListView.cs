using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Communication;
using Communication.Frames.Configuration;
using Communication.Frames.Incoming;
using Configuration;
using System.IO;
using System.Threading;


namespace Configuration
{
    public partial class NavigationListView : UserControl
    {
        [NonSerialized]
        private SerialCommunication serial;

        private List<int> dirty_list = new List<int>();


        public NavigationListView()
        {
            InitializeComponent();

            foreach (ListViewItem lvi in _lv_navigation.Items)
            {
                lvi.Tag = new NavigationInstruction(0, 0, 0, 0, 0, 0);
            }

            Disconnect();
        }


        public void Connect(SerialCommunication serial)
        {
            if (serial != null)
            {
                this.serial = serial;
                serial.NavigationInstructionCommunicationReceived += new SerialCommunication.ReceiveNavigationInstructionCommunicationFrame(_serial_NavigationInstructionCommunicationReceived);

                _btn_burn.Enabled = true;
                _btn_read.Enabled = true;
                _btn_reload.Enabled = true;
                _btn_save.Enabled = true;
            }
        }

        public void Disconnect()
        {
            serial = null;

            _btn_burn.Enabled = false;
            _btn_read.Enabled = false;
            _btn_reload.Enabled = false;
            _btn_save.Enabled = false;

        }
        
        private void _serial_NavigationInstructionCommunicationReceived(NavigationInstruction ni)
        {
            this.BeginInvoke(new D_ReceiveNavigationInstruction(NavigationInstructionReceived), new object[] { ni });
        }
        private delegate void D_ReceiveNavigationInstruction(NavigationInstruction ni);
        private void NavigationInstructionReceived(NavigationInstruction ni)
        {
            dirty_list.Remove(ni.line);
            _lv_navigation.Items[ni.line - 1].Tag = ni;
            if (_lv_navigation.Items[ni.line - 1].SubItems.Count <= 1)
            {
                _lv_navigation.Items[ni.line - 1].SubItems.Add(new ListViewItem.ListViewSubItem());
                _lv_navigation.Items[ni.line - 1].SubItems.Add(new ListViewItem.ListViewSubItem());
            }
            _lv_navigation.Items[ni.line - 1].SubItems[1].Text = ni.ToString();
        }


        private void _btn_read_Click(object sender, EventArgs e)
        {
            dirty_list.Clear();
            serial.SendNavigationRead();
        }

        private void _lv_navigation_ItemActivate(object sender, EventArgs e)
        {
            NavigationInstructionEdit nie = new NavigationInstructionEdit((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag);
            nie.ShowDialog(this);
            _lv_navigation.SelectedItems[0].SubItems[1].Text = "* " + 
                ((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag).ToString();
            dirty_list.Add(((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag).line);
        }

        private void _btn_save_Click(object sender, EventArgs e)
        {
            _pb.Value = 0;

            foreach (ListViewItem lvi in _lv_navigation.Items)
            {
                if (dirty_list.Contains(((NavigationInstruction)lvi.Tag).line))
                {
                    serial.SendNavigationInstruction((NavigationInstruction)lvi.Tag);
                    Thread.Sleep(200);
                }
                _pb.Value += 100 / _lv_navigation.Items.Count;
            }
            _pb.Value = 100;
        }

        private void _btn_burn_Click(object sender, EventArgs e)
        {
            serial.SendNavigationBurn();

        }

        private void _btn_reload_Click(object sender, EventArgs e)
        {
            serial.SendNavigationLoad();
        }

    }
}
