namespace Configuration
{
    partial class ConfigurationControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationControl));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btn_load = new System.Windows.Forms.ToolStripButton();
            this._btn_save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._btn_read = new System.Windows.Forms.ToolStripButton();
            this._btn_write = new System.Windows.Forms.ToolStripButton();
            this._btn_default = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._btn_burn = new System.Windows.Forms.ToolStripButton();
            this._btn_reload = new System.Windows.Forms.ToolStripButton();
            this.configurationTabpage1 = new Gluonpilot.ConfigurationTabpage();
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
            this.toolStripContainer1.ContentPanel.Controls.Add(this.configurationTabpage1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(606, 304);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(606, 329);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btn_load,
            this._btn_save,
            this.toolStripSeparator2,
            this._btn_read,
            this._btn_write,
            this._btn_default,
            this.toolStripSeparator1,
            this._btn_burn,
            this._btn_reload});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(429, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // _btn_load
            // 
            this._btn_load.Image = ((System.Drawing.Image)(resources.GetObject("_btn_load.Image")));
            this._btn_load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_load.Name = "_btn_load";
            this._btn_load.Size = new System.Drawing.Size(53, 22);
            this._btn_load.Text = "Open";
            this._btn_load.ToolTipText = "Load from file...";
            this._btn_load.Click += new System.EventHandler(this._btn_load_Click);
            // 
            // _btn_save
            // 
            this._btn_save.Image = ((System.Drawing.Image)(resources.GetObject("_btn_save.Image")));
            this._btn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_save.Name = "_btn_save";
            this._btn_save.Size = new System.Drawing.Size(51, 22);
            this._btn_save.Text = "Save";
            this._btn_save.ToolTipText = "Save to file...";
            this._btn_save.Click += new System.EventHandler(this._btn_save_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _btn_read
            // 
            this._btn_read.Image = ((System.Drawing.Image)(resources.GetObject("_btn_read.Image")));
            this._btn_read.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_read.Name = "_btn_read";
            this._btn_read.Size = new System.Drawing.Size(52, 22);
            this._btn_read.Text = "Read";
            this._btn_read.ToolTipText = "Read from module volatile memory";
            this._btn_read.Click += new System.EventHandler(this._btn_read_Click);
            // 
            // _btn_write
            // 
            this._btn_write.Image = ((System.Drawing.Image)(resources.GetObject("_btn_write.Image")));
            this._btn_write.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_write.Name = "_btn_write";
            this._btn_write.Size = new System.Drawing.Size(53, 22);
            this._btn_write.Text = "Write";
            this._btn_write.ToolTipText = "Write to module volatile memory";
            this._btn_write.Click += new System.EventHandler(this._btn_write_Click);
            // 
            // _btn_default
            // 
            this._btn_default.Image = ((System.Drawing.Image)(resources.GetObject("_btn_default.Image")));
            this._btn_default.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_default.Name = "_btn_default";
            this._btn_default.Size = new System.Drawing.Size(87, 22);
            this._btn_default.Text = "Load default";
            this._btn_default.ToolTipText = "Load default configuration to module volatile memory";
            this._btn_default.Click += new System.EventHandler(this._btn_default_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _btn_burn
            // 
            this._btn_burn.Image = ((System.Drawing.Image)(resources.GetObject("_btn_burn.Image")));
            this._btn_burn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_burn.Name = "_btn_burn";
            this._btn_burn.Size = new System.Drawing.Size(49, 22);
            this._btn_burn.Text = "Burn";
            this._btn_burn.ToolTipText = "Burn configuration in module memory to flash";
            this._btn_burn.Click += new System.EventHandler(this._btn_burn_Click);
            // 
            // _btn_reload
            // 
            this._btn_reload.Image = ((System.Drawing.Image)(resources.GetObject("_btn_reload.Image")));
            this._btn_reload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_reload.Name = "_btn_reload";
            this._btn_reload.Size = new System.Drawing.Size(60, 22);
            this._btn_reload.Text = "Reload";
            this._btn_reload.ToolTipText = "Reload from flash to module volatile memory";
            this._btn_reload.Click += new System.EventHandler(this._btn_reload_Click);
            // 
            // configurationTabpage1
            // 
            this.configurationTabpage1.AutoSize = true;
            this.configurationTabpage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationTabpage1.Location = new System.Drawing.Point(0, 0);
            this.configurationTabpage1.Name = "configurationTabpage1";
            this.configurationTabpage1.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.configurationTabpage1.Size = new System.Drawing.Size(606, 304);
            this.configurationTabpage1.TabIndex = 0;
            // 
            // ConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "ConfigurationControl";
            this.Size = new System.Drawing.Size(606, 329);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
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
        private Gluonpilot.ConfigurationTabpage configurationTabpage1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _btn_read;
        private System.Windows.Forms.ToolStripButton _btn_write;
        private System.Windows.Forms.ToolStripButton _btn_default;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _btn_burn;
        private System.Windows.Forms.ToolStripButton _btn_load;
        private System.Windows.Forms.ToolStripButton _btn_save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _btn_reload;
    }
}
