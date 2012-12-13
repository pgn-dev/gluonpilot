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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationControl));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btn_load = new System.Windows.Forms.ToolStripButton();
            this._btn_save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._btn_read = new System.Windows.Forms.ToolStripButton();
            this._btn_write = new System.Windows.Forms.ToolStripButton();
            this._btn_burn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._btn_default = new System.Windows.Forms.ToolStripButton();
            this._btn_reload = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
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
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btn_load,
            this._btn_save,
            this.toolStripSeparator2,
            this._btn_read,
            this._btn_write,
            this._btn_burn,
            this.toolStripSeparator1,
            this._btn_default,
            this._btn_reload});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // _btn_load
            // 
            resources.ApplyResources(this._btn_load, "_btn_load");
            this._btn_load.Name = "_btn_load";
            this._btn_load.Click += new System.EventHandler(this._btn_load_Click);
            // 
            // _btn_save
            // 
            resources.ApplyResources(this._btn_save, "_btn_save");
            this._btn_save.Name = "_btn_save";
            this._btn_save.Click += new System.EventHandler(this._btn_save_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // _btn_read
            // 
            resources.ApplyResources(this._btn_read, "_btn_read");
            this._btn_read.Name = "_btn_read";
            this._btn_read.Click += new System.EventHandler(this._btn_read_Click);
            // 
            // _btn_write
            // 
            resources.ApplyResources(this._btn_write, "_btn_write");
            this._btn_write.Name = "_btn_write";
            this._btn_write.Click += new System.EventHandler(this._btn_write_Click);
            // 
            // _btn_burn
            // 
            resources.ApplyResources(this._btn_burn, "_btn_burn");
            this._btn_burn.Name = "_btn_burn";
            this._btn_burn.Click += new System.EventHandler(this._btn_burn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // _btn_default
            // 
            resources.ApplyResources(this._btn_default, "_btn_default");
            this._btn_default.Name = "_btn_default";
            this._btn_default.Click += new System.EventHandler(this._btn_default_Click);
            // 
            // _btn_reload
            // 
            resources.ApplyResources(this._btn_reload, "_btn_reload");
            this._btn_reload.Name = "_btn_reload";
            this._btn_reload.Click += new System.EventHandler(this._btn_reload_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // configurationTabpage1
            // 
            resources.ApplyResources(this.configurationTabpage1, "configurationTabpage1");
            this.configurationTabpage1.Name = "configurationTabpage1";
            // 
            // ConfigurationControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "ConfigurationControl";
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
        private System.Windows.Forms.Timer timer1;
    }
}
