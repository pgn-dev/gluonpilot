namespace Gluonpilot
{
    partial class GluonConfig
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GluonConfig));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tc_main = new System.Windows.Forms.TabControl();
            this.config = new System.Windows.Forms.TabPage();
            this.configurationControl = new Configuration.ConfigurationControl();
            this.Navigation = new System.Windows.Forms.TabPage();
            this.navigationListView1 = new Configuration.NavigationListView();
            this.datalog = new System.Windows.Forms.TabPage();
            this.datalogging = new Configuration.Datalogging();
            this.gcs = new System.Windows.Forms.TabPage();
            this._gcsMainPanel = new GCS.GcsMainPanel();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._tssl_time = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this._tssl_downloadspeed = new System.Windows.Forms.ToolStripStatusLabel();
            this._tb_logging = new System.Windows.Forms.TextBox();
            this._cb_print_timestamp = new System.Windows.Forms.CheckBox();
            this._cb_hide_parsed = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btn_connect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._btn_showlogging = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._btn_reboot = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._btn_firmware_upgrade = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._tc_main.SuspendLayout();
            this.config.SuspendLayout();
            this.Navigation.SuspendLayout();
            this.datalog.SuspendLayout();
            this.gcs.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(645, 493);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(645, 518);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tc_main);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._statusStrip);
            this.splitContainer1.Panel2.Controls.Add(this._tb_logging);
            this.splitContainer1.Panel2.Controls.Add(this._cb_print_timestamp);
            this.splitContainer1.Panel2.Controls.Add(this._cb_hide_parsed);
            this.splitContainer1.Size = new System.Drawing.Size(645, 493);
            this.splitContainer1.SplitterDistance = 393;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // _tc_main
            // 
            this._tc_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tc_main.Controls.Add(this.config);
            this._tc_main.Controls.Add(this.Navigation);
            this._tc_main.Controls.Add(this.datalog);
            this._tc_main.Controls.Add(this.gcs);
            this._tc_main.ImageList = this.imageList;
            this._tc_main.ItemSize = new System.Drawing.Size(100, 25);
            this._tc_main.Location = new System.Drawing.Point(12, 8);
            this._tc_main.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this._tc_main.Name = "_tc_main";
            this._tc_main.SelectedIndex = 0;
            this._tc_main.Size = new System.Drawing.Size(621, 382);
            this._tc_main.TabIndex = 0;
            // 
            // config
            // 
            this.config.Controls.Add(this.configurationControl);
            this.config.ImageIndex = 1;
            this.config.Location = new System.Drawing.Point(4, 29);
            this.config.Name = "config";
            this.config.Padding = new System.Windows.Forms.Padding(3);
            this.config.Size = new System.Drawing.Size(613, 349);
            this.config.TabIndex = 0;
            this.config.Text = "Configuration";
            this.config.UseVisualStyleBackColor = true;
            // 
            // configurationControl
            // 
            this.configurationControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationControl.Location = new System.Drawing.Point(3, 3);
            this.configurationControl.Name = "configurationControl";
            this.configurationControl.Size = new System.Drawing.Size(607, 343);
            this.configurationControl.TabIndex = 0;
            // 
            // Navigation
            // 
            this.Navigation.Controls.Add(this.navigationListView1);
            this.Navigation.ImageIndex = 2;
            this.Navigation.Location = new System.Drawing.Point(4, 29);
            this.Navigation.Name = "Navigation";
            this.Navigation.Padding = new System.Windows.Forms.Padding(3);
            this.Navigation.Size = new System.Drawing.Size(613, 349);
            this.Navigation.TabIndex = 1;
            this.Navigation.Text = "Navigation";
            this.Navigation.UseVisualStyleBackColor = true;
            // 
            // navigationListView1
            // 
            this.navigationListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationListView1.Location = new System.Drawing.Point(3, 3);
            this.navigationListView1.Name = "navigationListView1";
            this.navigationListView1.Size = new System.Drawing.Size(607, 343);
            this.navigationListView1.TabIndex = 0;
            // 
            // datalog
            // 
            this.datalog.Controls.Add(this.datalogging);
            this.datalog.ImageIndex = 3;
            this.datalog.Location = new System.Drawing.Point(4, 29);
            this.datalog.Name = "datalog";
            this.datalog.Padding = new System.Windows.Forms.Padding(3);
            this.datalog.Size = new System.Drawing.Size(613, 349);
            this.datalog.TabIndex = 2;
            this.datalog.Text = "Datalogging";
            this.datalog.UseVisualStyleBackColor = true;
            // 
            // datalogging
            // 
            this.datalogging.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.datalogging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datalogging.Location = new System.Drawing.Point(3, 3);
            this.datalogging.Name = "datalogging";
            this.datalogging.Size = new System.Drawing.Size(607, 343);
            this.datalogging.TabIndex = 0;
            // 
            // gcs
            // 
            this.gcs.Controls.Add(this._gcsMainPanel);
            this.gcs.ImageIndex = 5;
            this.gcs.Location = new System.Drawing.Point(4, 29);
            this.gcs.Name = "gcs";
            this.gcs.Size = new System.Drawing.Size(613, 349);
            this.gcs.TabIndex = 3;
            this.gcs.Text = "Dashboard";
            this.gcs.UseVisualStyleBackColor = true;
            // 
            // _gcsMainPanel
            // 
            this._gcsMainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._gcsMainPanel.Location = new System.Drawing.Point(3, 3);
            this._gcsMainPanel.Name = "_gcsMainPanel";
            this._gcsMainPanel.Size = new System.Drawing.Size(607, 343);
            this._gcsMainPanel.TabIndex = 2;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "cog.png");
            this.imageList.Images.SetKeyName(1, "wrench.png");
            this.imageList.Images.SetKeyName(2, "world_edit.png");
            this.imageList.Images.SetKeyName(3, "folder_table.png");
            this.imageList.Images.SetKeyName(4, "transmit.png");
            this.imageList.Images.SetKeyName(5, "map.png");
            // 
            // _statusStrip
            // 
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._tssl_time,
            this.toolStripStatusLabel2,
            this._tssl_downloadspeed});
            this._statusStrip.Location = new System.Drawing.Point(0, 74);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Size = new System.Drawing.Size(645, 22);
            this._statusStrip.TabIndex = 7;
            this._statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusLabel1.Text = "Time connected:";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tssl_time
            // 
            this._tssl_time.Name = "_tssl_time";
            this._tssl_time.Size = new System.Drawing.Size(404, 17);
            this._tssl_time.Spring = true;
            this._tssl_time.Text = "00:00:00";
            this._tssl_time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(90, 17);
            this.toolStripStatusLabel2.Text = "Download speed:";
            // 
            // _tssl_downloadspeed
            // 
            this._tssl_downloadspeed.AutoSize = false;
            this._tssl_downloadspeed.Name = "_tssl_downloadspeed";
            this._tssl_downloadspeed.Size = new System.Drawing.Size(50, 17);
            this._tssl_downloadspeed.Text = "0 kB/s";
            this._tssl_downloadspeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tb_logging
            // 
            this._tb_logging.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tb_logging.Location = new System.Drawing.Point(19, 26);
            this._tb_logging.Multiline = true;
            this._tb_logging.Name = "_tb_logging";
            this._tb_logging.ReadOnly = true;
            this._tb_logging.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._tb_logging.Size = new System.Drawing.Size(607, 45);
            this._tb_logging.TabIndex = 6;
            // 
            // _cb_print_timestamp
            // 
            this._cb_print_timestamp.AutoSize = true;
            this._cb_print_timestamp.Checked = true;
            this._cb_print_timestamp.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cb_print_timestamp.Location = new System.Drawing.Point(126, 3);
            this._cb_print_timestamp.Name = "_cb_print_timestamp";
            this._cb_print_timestamp.Size = new System.Drawing.Size(97, 17);
            this._cb_print_timestamp.TabIndex = 5;
            this._cb_print_timestamp.Text = "Print timestamp";
            this._cb_print_timestamp.UseVisualStyleBackColor = true;
            // 
            // _cb_hide_parsed
            // 
            this._cb_hide_parsed.AutoSize = true;
            this._cb_hide_parsed.Checked = true;
            this._cb_hide_parsed.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cb_hide_parsed.Location = new System.Drawing.Point(12, 3);
            this._cb_hide_parsed.Name = "_cb_hide_parsed";
            this._cb_hide_parsed.Size = new System.Drawing.Size(107, 17);
            this._cb_hide_parsed.TabIndex = 4;
            this._cb_hide_parsed.Text = "Hide parsed lines";
            this._cb_hide_parsed.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btn_connect,
            this.toolStripSeparator1,
            this._btn_showlogging,
            this.toolStripSeparator2,
            this._btn_reboot,
            this.toolStripSeparator3,
            this._btn_firmware_upgrade});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(374, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // _btn_connect
            // 
            this._btn_connect.Image = ((System.Drawing.Image)(resources.GetObject("_btn_connect.Image")));
            this._btn_connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_connect.Name = "_btn_connect";
            this._btn_connect.Size = new System.Drawing.Size(67, 22);
            this._btn_connect.Text = "Connect";
            this._btn_connect.Click += new System.EventHandler(this._btn_connect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _btn_showlogging
            // 
            this._btn_showlogging.Checked = true;
            this._btn_showlogging.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btn_showlogging.Image = ((System.Drawing.Image)(resources.GetObject("_btn_showlogging.Image")));
            this._btn_showlogging.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_showlogging.Name = "_btn_showlogging";
            this._btn_showlogging.Size = new System.Drawing.Size(64, 22);
            this._btn_showlogging.Text = "Logging";
            this._btn_showlogging.Click += new System.EventHandler(this._btn_showlogging_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _btn_reboot
            // 
            this._btn_reboot.Image = ((System.Drawing.Image)(resources.GetObject("_btn_reboot.Image")));
            this._btn_reboot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_reboot.Name = "_btn_reboot";
            this._btn_reboot.Size = new System.Drawing.Size(99, 22);
            this._btn_reboot.Text = "Reboot module";
            this._btn_reboot.Click += new System.EventHandler(this._btn_reboot_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _btn_firmware_upgrade
            // 
            this._btn_firmware_upgrade.Image = ((System.Drawing.Image)(resources.GetObject("_btn_firmware_upgrade.Image")));
            this._btn_firmware_upgrade.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_firmware_upgrade.Name = "_btn_firmware_upgrade";
            this._btn_firmware_upgrade.Size = new System.Drawing.Size(114, 22);
            this._btn_firmware_upgrade.Text = "Firmware upgrade";
            this._btn_firmware_upgrade.Click += new System.EventHandler(this._btn_firmware_upgrade_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // GluonConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 518);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "GluonConfig";
            this.Text = "GluonConfig v0.5.2";
            this.Resize += new System.EventHandler(this.GluonConfig_Resize);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this._tc_main.ResumeLayout(false);
            this.config.ResumeLayout(false);
            this.Navigation.ResumeLayout(false);
            this.datalog.ResumeLayout(false);
            this.gcs.ResumeLayout(false);
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _btn_connect;
        private System.Windows.Forms.TabControl _tc_main;
        private System.Windows.Forms.TabPage config;
        private System.Windows.Forms.TabPage Navigation;
        private System.Windows.Forms.TabPage datalog;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TabPage gcs;
        private System.Windows.Forms.ToolStripButton _btn_showlogging;
        private Configuration.NavigationListView navigationListView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Configuration.ConfigurationControl configurationControl;
        private Configuration.Datalogging datalogging;
        private System.Windows.Forms.CheckBox _cb_print_timestamp;
        private System.Windows.Forms.CheckBox _cb_hide_parsed;
        private System.Windows.Forms.TextBox _tb_logging;
        private GCS.GcsMainPanel _gcsMainPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _btn_reboot;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton _btn_firmware_upgrade;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel _tssl_time;
        private System.Windows.Forms.ToolStripStatusLabel _tssl_downloadspeed;
        private System.Windows.Forms.Timer timer;



    }
}