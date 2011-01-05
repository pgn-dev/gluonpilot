namespace GCS
{
    partial class GcsMainPanel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GcsMainPanel));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.label7 = new System.Windows.Forms.Label();
            this._tb_battvoltage = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this._tb_navigationline = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._lbl_control_mode = new System.Windows.Forms.Label();
            this._tb_gps_sattellites = new System.Windows.Forms.TextBox();
            this._dtb_altitude = new Configuration.DistanceTextBox();
            this._stb_speed = new Configuration.SpeedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.artificialHorizon = new ArtificialHorizon.ArtificialHorizon();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._zgc_height = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._zgc_speed = new ZedGraph.ZedGraphControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this._graphControl = new Graph.GraphControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btn_ge_server = new System.Windows.Forms.ToolStripButton();
            this._btn_goto_ge = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label7);
            this.toolStripContainer1.ContentPanel.Controls.Add(this._tb_battvoltage);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label6);
            this.toolStripContainer1.ContentPanel.Controls.Add(this._tb_navigationline);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label5);
            this.toolStripContainer1.ContentPanel.Controls.Add(this._lbl_control_mode);
            this.toolStripContainer1.ContentPanel.Controls.Add(this._tb_gps_sattellites);
            this.toolStripContainer1.ContentPanel.Controls.Add(this._dtb_altitude);
            this.toolStripContainer1.ContentPanel.Controls.Add(this._stb_speed);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label4);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label3);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label2);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.label1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.artificialHorizon);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(599, 320);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(599, 345);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(513, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "V";
            // 
            // _tb_battvoltage
            // 
            this._tb_battvoltage.Location = new System.Drawing.Point(473, 4);
            this._tb_battvoltage.Name = "_tb_battvoltage";
            this._tb_battvoltage.ReadOnly = true;
            this._tb_battvoltage.Size = new System.Drawing.Size(34, 20);
            this._tb_battvoltage.TabIndex = 16;
            this._tb_battvoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(386, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Battery voltage:";
            // 
            // _tb_navigationline
            // 
            this._tb_navigationline.Location = new System.Drawing.Point(93, 109);
            this._tb_navigationline.Name = "_tb_navigationline";
            this._tb_navigationline.Size = new System.Drawing.Size(34, 20);
            this._tb_navigationline.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Navigation line:";
            // 
            // _lbl_control_mode
            // 
            this._lbl_control_mode.Location = new System.Drawing.Point(94, 8);
            this._lbl_control_mode.Name = "_lbl_control_mode";
            this._lbl_control_mode.Size = new System.Drawing.Size(109, 13);
            this._lbl_control_mode.TabIndex = 12;
            this._lbl_control_mode.Text = "Unknown";
            this._lbl_control_mode.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _tb_gps_sattellites
            // 
            this._tb_gps_sattellites.Location = new System.Drawing.Point(93, 82);
            this._tb_gps_sattellites.Name = "_tb_gps_sattellites";
            this._tb_gps_sattellites.Size = new System.Drawing.Size(34, 20);
            this._tb_gps_sattellites.TabIndex = 11;
            // 
            // _dtb_altitude
            // 
            this._dtb_altitude.DistanceM = 0;
            this._dtb_altitude.Location = new System.Drawing.Point(93, 29);
            this._dtb_altitude.Name = "_dtb_altitude";
            this._dtb_altitude.ReadOnly = false;
            this._dtb_altitude.Size = new System.Drawing.Size(110, 21);
            this._dtb_altitude.TabIndex = 10;
            // 
            // _stb_speed
            // 
            this._stb_speed.Location = new System.Drawing.Point(93, 55);
            this._stb_speed.Name = "_stb_speed";
            this._stb_speed.Size = new System.Drawing.Size(110, 21);
            this._stb_speed.SpeedMS = 0;
            this._stb_speed.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ground speed:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Altitude:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "GPS satellites:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Control mode:";
            // 
            // artificialHorizon
            // 
            this.artificialHorizon.AutoScroll = true;
            this.artificialHorizon.Location = new System.Drawing.Point(225, 7);
            this.artificialHorizon.Name = "artificialHorizon";
            this.artificialHorizon.pitch_angle = 0;
            this.artificialHorizon.roll_angle = 0;
            this.artificialHorizon.Size = new System.Drawing.Size(145, 138);
            this.artificialHorizon.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 151);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(593, 166);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._zgc_height);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(585, 140);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Height";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _zgc_height
            // 
            this._zgc_height.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._zgc_height.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._zgc_height.Location = new System.Drawing.Point(2, 4);
            this._zgc_height.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._zgc_height.Name = "_zgc_height";
            this._zgc_height.ScrollGrace = 0;
            this._zgc_height.ScrollMaxX = 0;
            this._zgc_height.ScrollMaxY = 0;
            this._zgc_height.ScrollMaxY2 = 0;
            this._zgc_height.ScrollMinX = 0;
            this._zgc_height.ScrollMinY = 0;
            this._zgc_height.ScrollMinY2 = 0;
            this._zgc_height.Size = new System.Drawing.Size(578, 131);
            this._zgc_height.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._zgc_speed);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(585, 140);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Speed";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _zgc_speed
            // 
            this._zgc_speed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._zgc_speed.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._zgc_speed.Location = new System.Drawing.Point(3, 5);
            this._zgc_speed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this._zgc_speed.Name = "_zgc_speed";
            this._zgc_speed.ScrollGrace = 0;
            this._zgc_speed.ScrollMaxX = 0;
            this._zgc_speed.ScrollMaxY = 0;
            this._zgc_speed.ScrollMaxY2 = 0;
            this._zgc_speed.ScrollMinX = 0;
            this._zgc_speed.ScrollMinY = 0;
            this._zgc_speed.ScrollMinY2 = 0;
            this._zgc_speed.Size = new System.Drawing.Size(578, 131);
            this._zgc_speed.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this._graphControl);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(585, 140);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Custom...";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // _graphControl
            // 
            this._graphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._graphControl.Location = new System.Drawing.Point(2, 2);
            this._graphControl.Name = "_graphControl";
            this._graphControl.Size = new System.Drawing.Size(580, 135);
            this._graphControl.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btn_ge_server,
            this._btn_goto_ge});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(214, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // _btn_ge_server
            // 
            this._btn_ge_server.Image = ((System.Drawing.Image)(resources.GetObject("_btn_ge_server.Image")));
            this._btn_ge_server.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_ge_server.Name = "_btn_ge_server";
            this._btn_ge_server.Size = new System.Drawing.Size(149, 22);
            this._btn_ge_server.Text = "Start google earth server";
            this._btn_ge_server.Click += new System.EventHandler(this._btn_ge_server_Click);
            // 
            // _btn_goto_ge
            // 
            this._btn_goto_ge.Enabled = false;
            this._btn_goto_ge.Image = ((System.Drawing.Image)(resources.GetObject("_btn_goto_ge.Image")));
            this._btn_goto_ge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_goto_ge.Name = "_btn_goto_ge";
            this._btn_goto_ge.Size = new System.Drawing.Size(53, 22);
            this._btn_goto_ge.Text = "Open";
            this._btn_goto_ge.Click += new System.EventHandler(this._btn_goto_ge_Click);
            // 
            // GcsMainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "GcsMainPanel";
            this.Size = new System.Drawing.Size(599, 345);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _btn_ge_server;
        private System.Windows.Forms.ToolStripButton _btn_goto_ge;
        //private ArtificialHorizon.ArtificialHorizon artificialHorizon;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ArtificialHorizon.ArtificialHorizon artificialHorizon;
        private ZedGraph.ZedGraphControl _zgc_height;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Configuration.SpeedTextBox _stb_speed;
        private Configuration.DistanceTextBox _dtb_altitude;
        private System.Windows.Forms.TextBox _tb_gps_sattellites;
        private ZedGraph.ZedGraphControl _zgc_speed;
        private System.Windows.Forms.Label _lbl_control_mode;
        private System.Windows.Forms.TextBox _tb_navigationline;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage3;
        private Graph.GraphControl _graphControl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _tb_battvoltage;
    }
}
