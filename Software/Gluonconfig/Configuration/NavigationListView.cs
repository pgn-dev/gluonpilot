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
using Configuration.NavigationCommands;


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
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem());
                lvi.SubItems.Add(new ListViewItem.ListViewSubItem());
            }

            Disconnect();
            _gb_edit.Enabled = false;
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
                _btn_format.Enabled = true;
            }
        }

        public void Disconnect()
        {
            serial = null;

            _btn_burn.Enabled = false;
            _btn_read.Enabled = false;
            _btn_reload.Enabled = false;
            _btn_save.Enabled = false;
            _btn_format.Enabled = false;
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
            //NavigationInstructionEdit nie = new NavigationInstructionEdit((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag);
            //nie.ShowDialog(this);

            NavigationCommands.NavigationCommandEditor nce = new Configuration.NavigationCommands.NavigationCommandEditor((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag);
            nce.ShowDialog(this);
            _lv_navigation.SelectedItems[0].Tag = nce.GetNavigationInstruction();


            _lv_navigation.SelectedItems[0].SubItems[1].Text = "* " + 
                ((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag).ToString();
            dirty_list.Add(((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag).line);

        }

        private void _btn_save_Click(object sender, EventArgs e)
        {
            _pb.Value = 0;
            int count = 0;

            // only write lines that have been changed
            foreach (ListViewItem lvi in _lv_navigation.Items)
            {
                if (dirty_list.Contains(((NavigationInstruction)lvi.Tag).line))
                {
                    serial.SendNavigationInstruction((NavigationInstruction)lvi.Tag);
                    Thread.Sleep(200);
                    count++;
                }
                _pb.Value += 100 / _lv_navigation.Items.Count;
            }

            // if no lines have been saved:
            if (count == 0)
            {
                _pb.Value = 0;
                DialogResult r = MessageBox.Show("No changed lines found.\r\nLines that have been changed are marked with a *, and only those will be written to the module. \r\n\r\nWould you like to write all the lines to the module?", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (r == DialogResult.Yes)
                {
                    foreach (ListViewItem lvi in _lv_navigation.Items)
                    {
                        serial.SendNavigationInstruction((NavigationInstruction)lvi.Tag);
                        Thread.Sleep(200);
                        _pb.Value += 100 / _lv_navigation.Items.Count;
                    }
                }
            }

            _pb.Value = 100;

            serial.SendNavigationRead();
        }

        private void _btn_burn_Click(object sender, EventArgs e)
        {
            serial.SendNavigationBurn();
        }

        private void _btn_reload_Click(object sender, EventArgs e)
        {
            serial.SendNavigationLoad();
        }


        private void _cb_opcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _gb_edit.Enabled = true;

            NavigationInstruction ni = (NavigationInstruction)(NavigationInstruction)_lv_navigation.SelectedItems[0].Tag;

            ni.opcode = (NavigationInstruction.navigation_command)_cb_opcode.SelectedIndex;

            // already a navigationcommand on the panel? delete it so we can add a new one.
            if (tableLayoutPanel.Controls.Count > 1)
                tableLayoutPanel.Controls.RemoveAt(1);

            Control c;

            if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.CIRCLE_REL)
                c = new NavigationCommands.CircleRel(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FLY_TO_REL)
                c = new NavigationCommands.FlyToRel(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.CIRCLE_ABS)
                c = new NavigationCommands.CircleAbs(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FLY_TO_ABS)
                c = new NavigationCommands.FlyToAbs(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FROM_TO_REL)
                c = new NavigationCommands.FromToRel(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.GOTO)
                c = new NavigationCommands.Goto(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FROM_TO_ABS)
                c = new NavigationCommands.FromToAbs(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.CLIMB)
                c = new NavigationCommands.Climb(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.UNTIL_GR)
                c = new NavigationCommands.UntilGr(ni);
            else// if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.EMPTY)
                c = new NavigationCommands.Empty(ni);

            // add our edit-control
            tableLayoutPanel.Controls.Add(c);
            tableLayoutPanel.SetCellPosition(c, new TableLayoutPanelCellPosition(0, 0));

            // Do some lay-outin'
            c.Anchor = AnchorStyles.None;
            tableLayoutPanel.RowStyles[0].Height = c.Height;
            tableLayoutPanel.Height = _btn_set.Height + c.Height + 30;
        }

        private void _lv_navigation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lv_navigation.SelectedItems.Count > 0)
                _cb_opcode.SelectedIndex = (int)((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag).opcode;
        }

        private void _btn_set_Click(object sender, EventArgs e)
        {
            _lv_navigation.SelectedItems[0].Tag = ((INavigationCommandViewer)tableLayoutPanel.GetControlFromPosition(0, 0)).GetNavigationInstruction();

            _lv_navigation.SelectedItems[0].SubItems[1].Text = "* " + 
                ((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag).ToString();
            dirty_list.Add(((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag).line);
        }

        private void _btn_format_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "All navigation data will be deleted on the module's memory AND flash.", "Are you sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            _pb.Value = 0;
            if (r == DialogResult.Yes)
            {
                foreach (ListViewItem lvi in _lv_navigation.Items)
                {
                    ((NavigationInstruction)lvi.Tag).opcode = NavigationInstruction.navigation_command.EMPTY;
                    ((NavigationInstruction)lvi.Tag).a = 0;
                    ((NavigationInstruction)lvi.Tag).b = 0;
                    ((NavigationInstruction)lvi.Tag).x = 0.0;
                    ((NavigationInstruction)lvi.Tag).y = 0.0;
                    serial.SendNavigationInstruction((NavigationInstruction)lvi.Tag);
                    Thread.Sleep(200);
                    _pb.Value += 100 / _lv_navigation.Items.Count;
                }
                _pb.Value = 100;

                // Burn these empty lines
                serial.SendNavigationBurn();

                // Read the data
                serial.SendNavigationRead();
            }
        }
    }
}
