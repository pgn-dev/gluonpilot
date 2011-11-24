using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Communication;
using Communication.Frames.Incoming;
using System.IO;
using System.Diagnostics;

namespace Configuration
{
    public partial class Datalogging : UserControl
    {
        private SerialCommunication serial;
        private DataSet loglines;

        public Datalogging()
        {
            InitializeComponent();

            Disconnect();
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

            if (serial != null)
            {
                serial.DatalogTableCommunicationReceived -= new SerialCommunication.ReceiveDatalogTableCommunicationFrame(ReceiveDatalogTable);
                serial.DatalogLineCommunicationReceived -= new SerialCommunication.ReceiveDatalogLineCommunicationFrame(ReceiveDatalogLine);
            }
        }

        public void Connect(SerialCommunication serial)
        {
            this.serial = serial;
            _btn_read.Enabled = true;
            _btn_download.Enabled = true;
            _btn_format.Enabled = true;


            serial.DatalogTableCommunicationReceived += new SerialCommunication.ReceiveDatalogTableCommunicationFrame(ReceiveDatalogTable);
            serial.DatalogLineCommunicationReceived += new SerialCommunication.ReceiveDatalogLineCommunicationFrame(ReceiveDatalogLine);
        }

        public void Disconnect()
        {
            _btn_read.Enabled = false;
            _btn_download.Enabled = false;
            _btn_format.Enabled = false;
        }

        private void _btn_read_Click(object sender, EventArgs e)
        {
            serial.SendDatalogTableRequest();
        }

        private void _btn_download_Click(object sender, EventArgs e)
        {
            loglines = null;
            if (_lv_datalogtable.SelectedIndices.Count != 1 || 
                _lv_datalogtable.SelectedIndices[0] < 0 || 
                _lv_datalogtable.SelectedIndices[0] > 15)
                MessageBox.Show("Please select 1 row from the index table.");
            else
                serial.SendDatalogTableRead(_lv_datalogtable.SelectedIndices[0]);
        }

        private void _btn_readloggings_Click(object sender, EventArgs e)
        {
            _btn_download_Click(sender, e);
        }


        void ReceiveDatalogTable(DatalogTable table)
        {
            this.BeginInvoke(new Action<DatalogTable>(DatalogTable), new object[] { table });
        }
        private void DatalogTable(DatalogTable table)
        {
            // Create columns if needed
            while (_lv_datalogtable.Items.Count <= table.Index)
                _lv_datalogtable.Items.Add("");
            _lv_datalogtable.Items[table.Index] = new ListViewItem();

            // Create row subitems if needed
            while (_lv_datalogtable.Items[table.Index].SubItems.Count <= 4)
                _lv_datalogtable.Items[table.Index].SubItems.Add("");

            // Assign data to row
            _lv_datalogtable.Items[table.Index].SubItems[0] = new ListViewItem.ListViewSubItem(_lv_datalogtable.Items[table.Index], table.Index.ToString());
            _lv_datalogtable.Items[table.Index].SubItems[1] = new ListViewItem.ListViewSubItem(_lv_datalogtable.Items[table.Index], table.StartPage.ToString());
            string date = table.Date / 10000 + "." + (table.Date / 100) % 100 + "." + table.Date % 100;
            string time = table.Time / 10000 + ":" + (table.Time / 100) % 100 + ":" + table.Time % 100;
            _lv_datalogtable.Items[table.Index].SubItems[2] = new ListViewItem.ListViewSubItem(_lv_datalogtable.Items[table.Index], date);
            _lv_datalogtable.Items[table.Index].SubItems[3] = new ListViewItem.ListViewSubItem(_lv_datalogtable.Items[table.Index], time);

            try
            {
                _lv_datalogtable.Items[table.Index].Tag =
                    new DateTime((int)table.Date % 100 + 2000,
                                 (int)(table.Date / 100) % 100,
                                 (int)table.Date / 10000,
                                 (int)table.Time / 10000,
                                 (int)(table.Time / 100) % 100,
                                 (int)table.Time % 100);
            }
            catch (Exception ex) // datetime exception -> no valid date set
            {
                _lv_datalogtable.Items[table.Index].Tag = DateTime.Now;
            }
        }

        void ReceiveDatalogLine(DatalogLine line)
        {
            this.BeginInvoke(new D_ReceiveDatalogLine(DatalogLine), new object[] { line });
        }
        private delegate void D_ReceiveDatalogLine(DatalogLine line);
        private void DatalogLine(DatalogLine line)
        {
            // first call: init dataset & headers
            if (loglines == null)
            {
                loglines = new DataSet();
                loglines.Tables.Add("Data");

                foreach (string s in line.Header)
                    loglines.Tables["Data"].Columns.Add(s);

                _dgv_datalog.SelectionMode = DataGridViewSelectionMode.CellSelect;
                _dgv_datalog.DataSource = loglines;
                _dgv_datalog.DataMember = "Data";
                foreach (DataGridViewColumn dgvc in _dgv_datalog.Columns)
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                _dgv_datalog.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            }
            // add row
            DataRow dr = loglines.Tables["Data"].NewRow();
            dr.ItemArray = new String[line.Line.Length];
            for (int i = 0; i < line.Line.Length; i++)
                dr[i] = line.Line[i];
            loglines.Tables["Data"].Rows.Add(dr);

            _pb.Value = (_pb.Value + 1) % 100;

        }

        private void _btn_format_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to erase all loggings?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                serial.SendDatalogFormat();
            }
        }

        private void _btn_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (loglines != null)
            {
                loglines.Tables.Clear();
            }
            ofd.DefaultExt = "xml";
            ofd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (loglines == null)
                    loglines = new DataSet();
                Stream s = ofd.OpenFile();
                loglines.ReadXml(s);
                _dgv_datalog.SelectionMode = DataGridViewSelectionMode.CellSelect;
                _dgv_datalog.DataSource = loglines;
                _dgv_datalog.DataMember = "Data";

                foreach (DataGridViewColumn dgvc in _dgv_datalog.Columns)
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                _dgv_datalog.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                s.Close();
            }
        }


        private void _btn_kml_classic_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "kml";
            sfd.Filter = "KML files (*.kml)|*.kml|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream s = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(s);
                try
                {
                    sw.Write(Kml.KmlClassicGenerator.BuildKml(loglines));
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

        private void xMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Stream s;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "xml";
            sfd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                s = sfd.OpenFile();
                loglines.WriteXml(s);
                s.Close();
            }
        }

        private void _btn_kml_track_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "kml";
            sfd.Filter = "KML files (*.kml)|*.kml|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DateTime timestamp = DateTime.Now;
                try
                {
                    timestamp = (DateTime)_lv_datalogtable.SelectedItems[0].Tag;
                }
                catch (Exception ex)
                { }

                Stream s = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(s);
                try
                {
                    sw.Write(Kml.KmlTrackGenerator.BuildKml(loglines, timestamp));
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
    }
}
