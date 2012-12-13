namespace Configuration
{
    partial class OsdConfig
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OsdConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this._cb_changing_channel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this._cbRssi = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btnLive = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._btnReadSettings = new System.Windows.Forms.ToolStripButton();
            this._btnWriteOsdSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._nud_rssi_low = new System.Windows.Forms.NumericUpDown();
            this._nud_rssi_high = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._panelVoltage = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._nud_rssi_low)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_rssi_high)).BeginInit();
            this._panelVoltage.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "View changing channel: ";
            // 
            // checkedListBox
            // 
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Items.AddRange(new object[] {
            "Artificial horizon",
            "Autopilot mode",
            "Voltage battery 1",
            "Current battery 1",
            "mAh battery 1",
            "Voltage battery 2",
            "GPS satellites",
            "Home arrow",
            "Distance to home",
            "RC-receiver RSSI",
            "Variometer",
            "Flight time",
            "Speed",
            "Altitude",
            "New block name"});
            this.checkedListBox.Location = new System.Drawing.Point(17, 101);
            this.checkedListBox.MultiColumn = true;
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(169, 229);
            this.checkedListBox.TabIndex = 1;
            this.checkedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_ItemCheck);
            this.checkedListBox.Click += new System.EventHandler(this.checkedListBox_Click);
            this.checkedListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkedListBox_MouseUp);
            // 
            // _cb_changing_channel
            // 
            this._cb_changing_channel.FormattingEnabled = true;
            this._cb_changing_channel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this._cb_changing_channel.Location = new System.Drawing.Point(144, 36);
            this._cb_changing_channel.Name = "_cb_changing_channel";
            this._cb_changing_channel.Size = new System.Drawing.Size(52, 21);
            this._cb_changing_channel.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "RSSI input:";
            // 
            // _cbRssi
            // 
            this._cbRssi.FormattingEnabled = true;
            this._cbRssi.Items.AddRange(new object[] {
            "No RSSI input",
            "Analog (S)",
            "PWM - ch 1",
            "PWM - ch 2",
            "PWM - ch 3",
            "PWM - ch 4",
            "PWM - ch 5",
            "PWM - ch 6",
            "PWM - ch 7",
            "PWM - ch 8",
            "PWM - ch 9",
            "PWM - ch 10"});
            this._cbRssi.Location = new System.Drawing.Point(81, 63);
            this._cbRssi.Name = "_cbRssi";
            this._cbRssi.Size = new System.Drawing.Size(115, 21);
            this._cbRssi.TabIndex = 5;
            this._cbRssi.SelectedIndexChanged += new System.EventHandler(this._cbRssi_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnLive,
            this.toolStripSeparator1,
            this._btnReadSettings,
            this._btnWriteOsdSettings,
            this.toolStripSeparator2,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(583, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _btnLive
            // 
            this._btnLive.Image = ((System.Drawing.Image)(resources.GetObject("_btnLive.Image")));
            this._btnLive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnLive.Name = "_btnLive";
            this._btnLive.Size = new System.Drawing.Size(93, 22);
            this._btnLive.Text = "Live updates";
            this._btnLive.Click += new System.EventHandler(this._btnLive_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnReadSettings
            // 
            this._btnReadSettings.Image = ((System.Drawing.Image)(resources.GetObject("_btnReadSettings.Image")));
            this._btnReadSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnReadSettings.Name = "_btnReadSettings";
            this._btnReadSettings.Size = new System.Drawing.Size(97, 22);
            this._btnReadSettings.Text = "Read settings";
            this._btnReadSettings.Click += new System.EventHandler(this._btnReadSettings_Click);
            // 
            // _btnWriteOsdSettings
            // 
            this._btnWriteOsdSettings.Image = ((System.Drawing.Image)(resources.GetObject("_btnWriteOsdSettings.Image")));
            this._btnWriteOsdSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnWriteOsdSettings.Name = "_btnWriteOsdSettings";
            this._btnWriteOsdSettings.Size = new System.Drawing.Size(125, 22);
            this._btnWriteOsdSettings.Text = "Write OSD settings";
            this._btnWriteOsdSettings.Click += new System.EventHandler(this._btnWriteOsdSettings_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton4.Text = "Burn settings";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "0% RSSI:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "100% RSSI:";
            // 
            // _nud_rssi_low
            // 
            this._nud_rssi_low.DecimalPlaces = 2;
            this._nud_rssi_low.Location = new System.Drawing.Point(61, 8);
            this._nud_rssi_low.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this._nud_rssi_low.Name = "_nud_rssi_low";
            this._nud_rssi_low.Size = new System.Drawing.Size(48, 20);
            this._nud_rssi_low.TabIndex = 10;
            this._nud_rssi_low.Value = new decimal(new int[] {
            6,
            0,
            0,
            65536});
            this._nud_rssi_low.ValueChanged += new System.EventHandler(this._nud_rssi_low_ValueChanged);
            // 
            // _nud_rssi_high
            // 
            this._nud_rssi_high.DecimalPlaces = 2;
            this._nud_rssi_high.Location = new System.Drawing.Point(202, 8);
            this._nud_rssi_high.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this._nud_rssi_high.Name = "_nud_rssi_high";
            this._nud_rssi_high.Size = new System.Drawing.Size(48, 20);
            this._nud_rssi_high.TabIndex = 11;
            this._nud_rssi_high.Value = new decimal(new int[] {
            16,
            0,
            0,
            65536});
            this._nud_rssi_high.ValueChanged += new System.EventHandler(this._nud_rssi_high_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(253, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "V";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "V";
            // 
            // _panelVoltage
            // 
            this._panelVoltage.Controls.Add(this.label2);
            this._panelVoltage.Controls.Add(this.label6);
            this._panelVoltage.Controls.Add(this.label4);
            this._panelVoltage.Controls.Add(this.label5);
            this._panelVoltage.Controls.Add(this._nud_rssi_low);
            this._panelVoltage.Controls.Add(this._nud_rssi_high);
            this._panelVoltage.Location = new System.Drawing.Point(201, 57);
            this._panelVoltage.Name = "_panelVoltage";
            this._panelVoltage.Size = new System.Drawing.Size(267, 36);
            this._panelVoltage.TabIndex = 14;
            // 
            // OsdConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelVoltage);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._cbRssi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._cb_changing_channel);
            this.Controls.Add(this.checkedListBox);
            this.Controls.Add(this.label1);
            this.Name = "OsdConfig";
            this.Size = new System.Drawing.Size(583, 359);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._nud_rssi_low)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_rssi_high)).EndInit();
            this._panelVoltage.ResumeLayout(false);
            this._panelVoltage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.ComboBox _cb_changing_channel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _cbRssi;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _btnLive;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _btnReadSettings;
        private System.Windows.Forms.ToolStripButton _btnWriteOsdSettings;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _nud_rssi_low;
        private System.Windows.Forms.NumericUpDown _nud_rssi_high;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel _panelVoltage;
    }
}
