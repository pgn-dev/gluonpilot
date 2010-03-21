namespace Gluonpilot
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btn_open = new System.Windows.Forms.ToolStripButton();
            this._btn_save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._btn_connect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this._btn_read = new System.Windows.Forms.ToolStripButton();
            this._btn_write = new System.Windows.Forms.ToolStripButton();
            this._btnLoadDefault = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this._btn_burn = new System.Windows.Forms.ToolStripButton();
            this._btn_reload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._btnChart = new System.Windows.Forms.ToolStripButton();
            this._btnOpenGl = new System.Windows.Forms.ToolStripButton();
            this.configurationFrame = new Gluonpilot.ConfigurationFrame();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._cb_print_timestamp = new System.Windows.Forms.CheckBox();
            this._cb_hide_parsed = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(5, 21);
            this.textBox1.MaxLength = 327670;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(603, 102);
            this.textBox1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 482);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(613, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btn_open,
            this._btn_save,
            this.toolStripSeparator2,
            this._btn_connect,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this._btn_read,
            this._btn_write,
            this._btnLoadDefault,
            this.toolStripSeparator4,
            this.toolStripLabel2,
            this._btn_burn,
            this._btn_reload,
            this.toolStripSeparator3,
            this._btnChart,
            this._btnOpenGl});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(613, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip";
            // 
            // _btn_open
            // 
            this._btn_open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btn_open.Image = ((System.Drawing.Image)(resources.GetObject("_btn_open.Image")));
            this._btn_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_open.Name = "_btn_open";
            this._btn_open.Size = new System.Drawing.Size(23, 22);
            this._btn_open.Text = "Open...";
            this._btn_open.Click += new System.EventHandler(this._btn_open_Click);
            // 
            // _btn_save
            // 
            this._btn_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btn_save.Image = ((System.Drawing.Image)(resources.GetObject("_btn_save.Image")));
            this._btn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_save.Name = "_btn_save";
            this._btn_save.Size = new System.Drawing.Size(23, 22);
            this._btn_save.Text = "Save...";
            this._btn_save.Click += new System.EventHandler(this._btn_save_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _btn_connect
            // 
            this._btn_connect.CheckOnClick = true;
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
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(52, 22);
            this.toolStripLabel1.Text = "Memory: ";
            // 
            // _btn_read
            // 
            this._btn_read.Image = ((System.Drawing.Image)(resources.GetObject("_btn_read.Image")));
            this._btn_read.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_read.Name = "_btn_read";
            this._btn_read.Size = new System.Drawing.Size(52, 22);
            this._btn_read.Text = "Read";
            this._btn_read.Click += new System.EventHandler(this._btn_read_Click);
            // 
            // _btn_write
            // 
            this._btn_write.Image = ((System.Drawing.Image)(resources.GetObject("_btn_write.Image")));
            this._btn_write.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_write.Name = "_btn_write";
            this._btn_write.Size = new System.Drawing.Size(53, 22);
            this._btn_write.Text = "Write";
            this._btn_write.Click += new System.EventHandler(this._btn_write_Click);
            // 
            // _btnLoadDefault
            // 
            this._btnLoadDefault.Image = ((System.Drawing.Image)(resources.GetObject("_btnLoadDefault.Image")));
            this._btnLoadDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnLoadDefault.Name = "_btnLoadDefault";
            this._btnLoadDefault.Size = new System.Drawing.Size(62, 22);
            this._btnLoadDefault.Text = "Default";
            this._btnLoadDefault.Click += new System.EventHandler(this._btnLoadDefault_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel2.Text = "Flash: ";
            // 
            // _btn_burn
            // 
            this._btn_burn.Image = ((System.Drawing.Image)(resources.GetObject("_btn_burn.Image")));
            this._btn_burn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_burn.Name = "_btn_burn";
            this._btn_burn.Size = new System.Drawing.Size(49, 22);
            this._btn_burn.Text = "Burn";
            this._btn_burn.Click += new System.EventHandler(this._btn_burn_Click);
            // 
            // _btn_reload
            // 
            this._btn_reload.Image = ((System.Drawing.Image)(resources.GetObject("_btn_reload.Image")));
            this._btn_reload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_reload.Name = "_btn_reload";
            this._btn_reload.Size = new System.Drawing.Size(50, 22);
            this._btn_reload.Text = "Load";
            this._btn_reload.Click += new System.EventHandler(this._btn_reload_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnChart
            // 
            this._btnChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnChart.Image = ((System.Drawing.Image)(resources.GetObject("_btnChart.Image")));
            this._btnChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnChart.Name = "_btnChart";
            this._btnChart.Size = new System.Drawing.Size(23, 22);
            this._btnChart.Text = "Graph window";
            this._btnChart.Click += new System.EventHandler(this._btnChart_Click);
            // 
            // _btnOpenGl
            // 
            this._btnOpenGl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._btnOpenGl.Image = ((System.Drawing.Image)(resources.GetObject("_btnOpenGl.Image")));
            this._btnOpenGl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btnOpenGl.Name = "_btnOpenGl";
            this._btnOpenGl.Size = new System.Drawing.Size(23, 22);
            this._btnOpenGl.Text = "3D orientation";
            this._btnOpenGl.Click += new System.EventHandler(this._btnOpenGl_Click);
            // 
            // configurationFrame
            // 
            this.configurationFrame.AutoSize = true;
            this.configurationFrame.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.configurationFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationFrame.Location = new System.Drawing.Point(5, 5);
            this.configurationFrame.Model = ((Gluonpilot.ConfigurationModel)(resources.GetObject("configurationFrame.Model")));
            this.configurationFrame.Name = "configurationFrame";
            this.configurationFrame.Size = new System.Drawing.Size(603, 315);
            this.configurationFrame.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.configurationFrame);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._cb_print_timestamp);
            this.splitContainer1.Panel2.Controls.Add(this._cb_hide_parsed);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer1.Size = new System.Drawing.Size(613, 457);
            this.splitContainer1.SplitterDistance = 325;
            this.splitContainer1.TabIndex = 7;
            // 
            // _cb_print_timestamp
            // 
            this._cb_print_timestamp.AutoSize = true;
            this._cb_print_timestamp.Checked = true;
            this._cb_print_timestamp.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cb_print_timestamp.Location = new System.Drawing.Point(122, 4);
            this._cb_print_timestamp.Name = "_cb_print_timestamp";
            this._cb_print_timestamp.Size = new System.Drawing.Size(97, 17);
            this._cb_print_timestamp.TabIndex = 3;
            this._cb_print_timestamp.Text = "Print timestamp";
            this._cb_print_timestamp.UseVisualStyleBackColor = true;
            // 
            // _cb_hide_parsed
            // 
            this._cb_hide_parsed.AutoSize = true;
            this._cb_hide_parsed.Checked = true;
            this._cb_hide_parsed.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cb_hide_parsed.Location = new System.Drawing.Point(8, 4);
            this._cb_hide_parsed.Name = "_cb_hide_parsed";
            this._cb_hide_parsed.Size = new System.Drawing.Size(107, 17);
            this._cb_hide_parsed.TabIndex = 2;
            this._cb_hide_parsed.Text = "Hide parsed lines";
            this._cb_hide_parsed.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 504);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Gluonpilot config";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ConfigurationFrame configurationFrame;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _btn_connect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _btn_read;
        private System.Windows.Forms.ToolStripButton _btn_write;
        private System.Windows.Forms.ToolStripButton _btn_burn;
        private System.Windows.Forms.ToolStripButton _btn_open;
        private System.Windows.Forms.ToolStripButton _btn_save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _btn_reload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton _btnChart;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton _btnLoadDefault;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton _btnOpenGl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox _cb_hide_parsed;
        private System.Windows.Forms.CheckBox _cb_print_timestamp;
    }
}

