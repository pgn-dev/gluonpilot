namespace Configuration.NavigationCommands
{
    partial class FromToAbs
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
            this.label4 = new System.Windows.Forms.Label();
            this._dtb_height = new Configuration.DistanceTextBox();
            this._ce = new Configuration.CoordinateEdit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-3, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Altitude";
            // 
            // _dtb_height
            // 
            this._dtb_height.Color = System.Drawing.SystemColors.Window;
            this._dtb_height.DistanceM = 0D;
            this._dtb_height.Location = new System.Drawing.Point(75, 59);
            this._dtb_height.Name = "_dtb_height";
            this._dtb_height.ReadOnly = false;
            this._dtb_height.Size = new System.Drawing.Size(99, 21);
            this._dtb_height.TabIndex = 14;
            this._dtb_height.UseAltitudeColoring = true;
            // 
            // _ce
            // 
            this._ce.Location = new System.Drawing.Point(0, 0);
            this._ce.Name = "_ce";
            this._ce.Size = new System.Drawing.Size(174, 53);
            this._ce.TabIndex = 12;
            // 
            // FromToAbs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this._dtb_height);
            this.Controls.Add(this._ce);
            this.Name = "FromToAbs";
            this.Size = new System.Drawing.Size(178, 81);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private DistanceTextBox _dtb_height;
        private CoordinateEdit _ce;
    }
}
