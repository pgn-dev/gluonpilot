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
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._btnBasicConfiguration = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
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
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.BottomToolStripPanel, "toolStripContainer1.BottomToolStripPanel");
            // 
            // toolStripContainer1.ContentPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.LeftToolStripPanel, "toolStripContainer1.LeftToolStripPanel");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.RightToolStripPanel, "toolStripContainer1.RightToolStripPanel");
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.TopToolStripPanel, "toolStripContainer1.TopToolStripPanel");
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this._tc_main);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this._statusStrip);
            this.splitContainer1.Panel2.Controls.Add(this._tb_logging);
            this.splitContainer1.Panel2.Controls.Add(this._cb_print_timestamp);
            this.splitContainer1.Panel2.Controls.Add(this._cb_hide_parsed);
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // _tc_main
            // 
            resources.ApplyResources(this._tc_main, "_tc_main");
            this._tc_main.Controls.Add(this.config);
            this._tc_main.Controls.Add(this.Navigation);
            this._tc_main.Controls.Add(this.datalog);
            this._tc_main.Controls.Add(this.gcs);
            this._tc_main.ImageList = this.imageList;
            this._tc_main.Name = "_tc_main";
            this._tc_main.SelectedIndex = 0;
            // 
            // config
            // 
            resources.ApplyResources(this.config, "config");
            this.config.Controls.Add(this.configurationControl);
            this.config.Name = "config";
            this.config.UseVisualStyleBackColor = true;
            // 
            // configurationControl
            // 
            resources.ApplyResources(this.configurationControl, "configurationControl");
            this.configurationControl.Name = "configurationControl";
            // 
            // Navigation
            // 
            resources.ApplyResources(this.Navigation, "Navigation");
            this.Navigation.Controls.Add(this.navigationListView1);
            this.Navigation.Name = "Navigation";
            this.Navigation.UseVisualStyleBackColor = true;
            // 
            // navigationListView1
            // 
            resources.ApplyResources(this.navigationListView1, "navigationListView1");
            this.navigationListView1.Name = "navigationListView1";
            // 
            // datalog
            // 
            resources.ApplyResources(this.datalog, "datalog");
            this.datalog.Controls.Add(this.datalogging);
            this.datalog.Name = "datalog";
            this.datalog.UseVisualStyleBackColor = true;
            // 
            // datalogging
            // 
            resources.ApplyResources(this.datalogging, "datalogging");
            this.datalogging.Name = "datalogging";
            // 
            // gcs
            // 
            resources.ApplyResources(this.gcs, "gcs");
            this.gcs.Controls.Add(this._gcsMainPanel);
            this.gcs.Name = "gcs";
            this.gcs.UseVisualStyleBackColor = true;
            // 
            // _gcsMainPanel
            // 
            resources.ApplyResources(this._gcsMainPanel, "_gcsMainPanel");
            this._gcsMainPanel.Name = "_gcsMainPanel";
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
            resources.ApplyResources(this._statusStrip, "_statusStrip");
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._tssl_time,
            this.toolStripStatusLabel2,
            this._tssl_downloadspeed});
            this._statusStrip.Name = "_statusStrip";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // _tssl_time
            // 
            resources.ApplyResources(this._tssl_time, "_tssl_time");
            this._tssl_time.Name = "_tssl_time";
            this._tssl_time.Spring = true;
            // 
            // toolStripStatusLabel2
            // 
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // _tssl_downloadspeed
            // 
            resources.ApplyResources(this._tssl_downloadspeed, "_tssl_downloadspeed");
            this._tssl_downloadspeed.Name = "_tssl_downloadspeed";
            // 
            // _tb_logging
            // 
            resources.ApplyResources(this._tb_logging, "_tb_logging");
            this._tb_logging.Name = "_tb_logging";
            this._tb_logging.ReadOnly = true;
            // 
            // _cb_print_timestamp
            // 
            resources.ApplyResources(this._cb_print_timestamp, "_cb_print_timestamp");
            this._cb_print_timestamp.Checked = true;
            this._cb_print_timestamp.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cb_print_timestamp.Name = "_cb_print_timestamp";
            this._cb_print_timestamp.UseVisualStyleBackColor = true;
            // 
            // _cb_hide_parsed
            // 
            resources.ApplyResources(this._cb_hide_parsed, "_cb_hide_parsed");
            this._cb_hide_parsed.Checked = true;
            this._cb_hide_parsed.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cb_hide_parsed.Name = "_cb_hide_parsed";
            this._cb_hide_parsed.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btn_connect,
            this.toolStripSeparator1,
            this._btn_showlogging,
            this.toolStripSeparator2,
            this._btn_reboot,
            this.toolStripSeparator3,
            this._btn_firmware_upgrade,
            this.toolStripSeparator4,
            this._btnBasicConfiguration});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // _btn_connect
            // 
            resources.ApplyResources(this._btn_connect, "_btn_connect");
            this._btn_connect.Name = "_btn_connect";
            this._btn_connect.Click += new System.EventHandler(this._btn_connect_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // _btn_showlogging
            // 
            resources.ApplyResources(this._btn_showlogging, "_btn_showlogging");
            this._btn_showlogging.Checked = true;
            this._btn_showlogging.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btn_showlogging.Name = "_btn_showlogging";
            this._btn_showlogging.Click += new System.EventHandler(this._btn_showlogging_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // _btn_reboot
            // 
            resources.ApplyResources(this._btn_reboot, "_btn_reboot");
            this._btn_reboot.Name = "_btn_reboot";
            this._btn_reboot.Click += new System.EventHandler(this._btn_reboot_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // _btn_firmware_upgrade
            // 
            resources.ApplyResources(this._btn_firmware_upgrade, "_btn_firmware_upgrade");
            this._btn_firmware_upgrade.Name = "_btn_firmware_upgrade";
            this._btn_firmware_upgrade.Click += new System.EventHandler(this._btn_firmware_upgrade_Click);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // _btnBasicConfiguration
            // 
            resources.ApplyResources(this._btnBasicConfiguration, "_btnBasicConfiguration");
            this._btnBasicConfiguration.Name = "_btnBasicConfiguration";
            this._btnBasicConfiguration.Click += new System.EventHandler(this._btnBasicConfiguration_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // GluonConfig
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "GluonConfig";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GluonConfig_FormClosing);
            this.Resize += new System.EventHandler(this.GluonConfig_Resize);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton _btnBasicConfiguration;



    }
}