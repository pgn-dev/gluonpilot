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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GcsMainPanel));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            //this.artificialHorizon = new ArtificialHorizon.ArtificialHorizon();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btn_ge_server = new System.Windows.Forms.ToolStripButton();
            this._btn_goto_ge = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
            //this.toolStripContainer1.ContentPanel.Controls.Add(this.artificialHorizon);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(530, 366);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(530, 391);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // artificialHorizon
            // 
            /*this.artificialHorizon.AutoScroll = true;
            this.artificialHorizon.BackColor = System.Drawing.SystemColors.Info;
            this.artificialHorizon.ForeColor = System.Drawing.SystemColors.Control;
            this.artificialHorizon.Location = new System.Drawing.Point(185, 13);
            this.artificialHorizon.Name = "artificialHorizon";
            this.artificialHorizon.pitch_angle = 0;
            this.artificialHorizon.roll_angle = 0;
            this.artificialHorizon.Size = new System.Drawing.Size(160, 148);
            this.artificialHorizon.TabIndex = 1;*/
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 167);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(524, 196);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(516, 170);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Height";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(516, 170);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Speed";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GcsMainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "GcsMainPanel";
            this.Size = new System.Drawing.Size(530, 391);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
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
    }
}
