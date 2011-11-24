namespace Configuration.NavigationCommands
{
    partial class FlyToAbs
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
            this.label1 = new System.Windows.Forms.Label();
            this._dtb_height = new Configuration.DistanceTextBox();
            this._ce = new Configuration.CoordinateEdit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Altitude";
            // 
            // _dtb_height
            // 
            this._dtb_height.Color = System.Drawing.SystemColors.Window;
            this._dtb_height.DistanceM = 0D;
            this._dtb_height.Location = new System.Drawing.Point(78, 59);
            this._dtb_height.Name = "_dtb_height";
            this._dtb_height.ReadOnly = false;
            this._dtb_height.Size = new System.Drawing.Size(99, 21);
            this._dtb_height.TabIndex = 1;
            this._dtb_height.UseAltitudeColoring = true;
            // 
            // _ce
            // 
            this._ce.Location = new System.Drawing.Point(3, 0);
            this._ce.Name = "_ce";
            this._ce.Size = new System.Drawing.Size(177, 53);
            this._ce.TabIndex = 0;
            // 
            // FlyToAbs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this._dtb_height);
            this.Controls.Add(this._ce);
            this.Name = "FlyToAbs";
            this.Size = new System.Drawing.Size(178, 81);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CoordinateEdit _ce;
        private DistanceTextBox _dtb_height;
        private System.Windows.Forms.Label label1;
    }
}
