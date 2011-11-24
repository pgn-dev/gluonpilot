namespace Configuration.NavigationCommands
{
    partial class LoiterCircle
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
            this.label3 = new System.Windows.Forms.Label();
            this._dtb_radius = new Configuration.DistanceTextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Circle radius";
            // 
            // _dtb_radius
            // 
            this._dtb_radius.Color = System.Drawing.SystemColors.Window;
            this._dtb_radius.DistanceM = 0D;
            this._dtb_radius.Location = new System.Drawing.Point(81, 2);
            this._dtb_radius.Name = "_dtb_radius";
            this._dtb_radius.ReadOnly = false;
            this._dtb_radius.Size = new System.Drawing.Size(99, 21);
            this._dtb_radius.TabIndex = 11;
            this._dtb_radius.UseAltitudeColoring = false;
            // 
            // LoiterCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this._dtb_radius);
            this.Name = "LoiterCircle";
            this.Size = new System.Drawing.Size(182, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private DistanceTextBox _dtb_radius;
    }
}
