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
using System.Diagnostics;
using System.Xml.Serialization;
using Kml;


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

            _btn_burn.Enabled = false;
            _btn_read.Enabled = false;
            _btn_reload.Enabled = false;
            _btn_save.Enabled = false;
            _btn_format.Enabled = false;
            _gb_edit.Enabled = false;
            _btn_to_kml.Enabled = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

            if (serial != null)
                serial.NavigationInstructionCommunicationReceived -= new SerialCommunication.ReceiveNavigationInstructionCommunicationFrame(_serial_NavigationInstructionCommunicationReceived);

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
            serial.NavigationInstructionCommunicationReceived -= new SerialCommunication.ReceiveNavigationInstructionCommunicationFrame(_serial_NavigationInstructionCommunicationReceived);

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
            if (ni.line < 1)
                Console.WriteLine("ERROR: navigation line = 0");
            else
            {
                while (_lv_navigation.Items.Count <= ni.line)
                    _lv_navigation.Items.Add(new ListViewItem());
                _lv_navigation.Items[ni.line - 1].Tag = ni;
                if (_lv_navigation.Items[ni.line - 1].SubItems.Count <= 1)
                {
                    _lv_navigation.Items[ni.line - 1].SubItems.Add(new ListViewItem.ListViewSubItem());
                    _lv_navigation.Items[ni.line - 1].SubItems.Add(new ListViewItem.ListViewSubItem());
                }
                _lv_navigation.Items[ni.line - 1].SubItems[1].Text = ni.ToString();
            }
        }


        private void _btn_read_Click(object sender, EventArgs e)
        {
            dirty_list.Clear();
            foreach (ListViewItem i in _lv_navigation.Items)  // provide some visual feedback
                i.SubItems[1].Text = "?";

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
            serial.SendNavigationRead();
        }


        private void _cb_opcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _gb_edit.Enabled = true;

            NavigationInstruction ni = (NavigationInstruction)(NavigationInstruction)_lv_navigation.SelectedItems[0].Tag;

            ni.opcode = (NavigationInstruction.navigation_command)_cb_opcode.SelectedIndex;

            // already a navigationcommand on the panel? delete it so we can add a new one.
            if (tableLayoutPanel.Controls.Count > 1)
                tableLayoutPanel.Controls.RemoveAt(1);

            // Add the correct usercontrol
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
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FLARE_TO_ABS)
                c = new NavigationCommands.FlareToAbs(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FLARE_TO_REL)
                c = new NavigationCommands.FlareToRel(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.GLIDE_TO_ABS)
                c = new NavigationCommands.GlideToAbs(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.GLIDE_TO_REL)
                c = new NavigationCommands.GlideToRel(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.FROM_TO_ABS)
                c = new NavigationCommands.FromToAbs(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.CLIMB)
                c = new NavigationCommands.Climb(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.UNTIL_GR)
                c = new NavigationCommands.UntilGr(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.UNTIL_SM)
                c = new NavigationCommands.UntilSm(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.UNTIL_EQ)
                c = new NavigationCommands.UntilEq(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.UNTIL_NE)
                c = new NavigationCommands.UntilNe(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.IF_GR)
                c = new NavigationCommands.IfGr(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.IF_SM)
                c = new NavigationCommands.IfSm(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.IF_EQ)
                c = new NavigationCommands.IfEq(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.IF_NE)
                c = new NavigationCommands.IfNe(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.SERVO_SET)
                c = new NavigationCommands.ServoSet(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.SERVO_TRIGGER)
                c = new NavigationCommands.ServoTrigger(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.BLOCK)
                c = new NavigationCommands.Block(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.SET_LOITER_POSITION)
                c = new NavigationCommands.SetLoiterPosition(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.LOITER_CIRCLE)
                c = new NavigationCommands.LoiterCircle(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.CIRCLE_TO_REL)
                c = new NavigationCommands.CircleToRel(ni);
            else if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.CIRCLE_TO_ABS)
                c = new NavigationCommands.CircleToAbs(ni);
            else //if (_cb_opcode.SelectedIndex == (int)NavigationInstruction.navigation_command.EMPTY)
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
            {
                if ((int)((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag).opcode <= _cb_opcode.Items.Count)
                {
                    _cb_opcode.SelectedIndex = (int)((NavigationInstruction)_lv_navigation.SelectedItems[0].Tag).opcode;
                    _cb_opcode_SelectedIndexChanged(null, null); // in case it is the same as current selected index
                }
            }
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


        private List<NavigationInstruction> GetNavigationList()
        {
            List<NavigationInstruction> l_n = new List<NavigationInstruction>();

            foreach (ListViewItem lvi in _lv_navigation.Items)
            {
                ((NavigationInstruction)lvi.Tag).line = lvi.Index + 1;
                l_n.Add((NavigationInstruction)lvi.Tag);
            }
            return l_n;
        }

        private void _btn_to_kml_Click(object sender, EventArgs e)
        {
            StringBuilder placemarks = new StringBuilder();
            StringBuilder path = new StringBuilder();
            double lat_home_rad;
            double lon_home_rad;

            // we need a home position in case the navigation uses relative coordinates
            AskHome ah = new AskHome();
            if (ah.ShowDialog() != DialogResult.OK)
                return;
            lat_home_rad = ah.GetLatitudeRad();
            lon_home_rad = ah.GetLongitudeRad();

            // generate the KML
            string kml = KmlNavigation.BuildKml(GetNavigationList(), lat_home_rad, lon_home_rad);

            // save to file
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "kml";
            sfd.Filter = "KML files (*.kml)|*.kml|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream s = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(s);
                try
                {
                    sw.Write(kml.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                sw.Close();
                if (MessageBox.Show("Do you want to open the file in Google Earth?", "Open file?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    Process.Start(sfd.FileName);
            }
        }

        private void _btn_save_to_file_Click(object sender, EventArgs e)
        {
            /* Copy listview to array */
            NavigationInstruction[] list = GetNavigationList().ToArray();

            System.Windows.Forms.SaveFileDialog file = new System.Windows.Forms.SaveFileDialog();
            file.DefaultExt = "gnf";
            file.Filter = "Gluon navigation file (*.gnf)|*.gnf|All files (*.*)|*.*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                Stream stream = file.OpenFile();
                //BinaryFormatter bformatter = new BinaryFormatter();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(NavigationInstruction[]));

                Console.WriteLine("Writing model information");
                xmlSerializer.Serialize(stream, list);
                stream.Close();
            }
        }

        private void _btn_open_file_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog file = new System.Windows.Forms.OpenFileDialog();
            file.DefaultExt = "gnf";
            file.Filter = "Gluon navigation file (*.gnf)|*.gnf|All files (*.*)|*.*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                Stream stream = File.OpenRead(file.FileName);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(NavigationInstruction[]));

                Console.WriteLine("Reading model information");

                NavigationInstruction[] list = (NavigationInstruction[])xmlSerializer.Deserialize(stream);
                int i = 0;
                dirty_list.Clear();
                foreach (ListViewItem lvi in _lv_navigation.Items)
                {
                    if (list.Length > i)
                    {
                        lvi.Tag = list[i];
                        lvi.SubItems[1].Text = "* " + list[i].ToString();
                        dirty_list.Add(i);
                        i++;
                    }
                }
                stream.Close();
            }
            
            // update edit control
            _lv_navigation_SelectedIndexChanged(null, null);
        }

        private void _lv_navigation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.C | Keys.Control))
            {
                StringBuilder buffer = new StringBuilder();
                for (int i = 0; i < _lv_navigation.SelectedItems.Count; i++)
                {
                    buffer.Append(((NavigationInstruction)_lv_navigation.SelectedItems[i].Tag).ToString());
                    buffer.Append("\n");
                }

                Clipboard.SetText(buffer.ToString());  
            }
        }
    }
}
