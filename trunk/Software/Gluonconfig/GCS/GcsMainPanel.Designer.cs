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
            this._graphControl = new Graph.GraphControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btn_ge_server = new System.Windows.Forms.ToolStripButton();
            this._btn_goto_ge = new System.Windows.Forms.ToolStripButton();
            this.artificialHorizon = new ArtificialHorizon.ArtificialHorizon();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.artificialHorizon);
            this.toolStripContainer1.ContentPanel.Controls.Add(this._graphControl);
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
            // _graphControl
            // 
            this._graphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._graphControl.Location = new System.Drawing.Point(3, 167);
            this._graphControl.Name = "_graphControl";
            this._graphControl.Size = new System.Drawing.Size(524, 196);
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
            // artificialHorizon
            // 
            this.artificialHorizon.AutoScroll = true;
            this.artificialHorizon.ForeColor = System.Drawing.SystemColors.Control;
            this.artificialHorizon.Location = new System.Drawing.Point(185, 13);
            this.artificialHorizon.Name = "artificialHorizon";
            this.artificialHorizon.pitch_angle = 0;
            this.artificialHorizon.roll_angle = 0;
            this.artificialHorizon.Size = new System.Drawing.Size(160, 148);
            this.artificialHorizon.TabIndex = 1;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _btn_ge_server;
        private System.Windows.Forms.ToolStripButton _btn_goto_ge;
        private Graph.GraphControl _graphControl;
        private ArtificialHorizon.ArtificialHorizon artificialHorizon;
    }
}
