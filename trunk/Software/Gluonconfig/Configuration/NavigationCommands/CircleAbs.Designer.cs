namespace Configuration.NavigationCommands
{
    partial class CircleAbs
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
            this.label3 = new System.Windows.Forms.Label();
            this._dtb_height = new Configuration.DistanceTextBox();
            this._dtb_radius = new Configuration.DistanceTextBox();
            this._ce = new Configuration.CoordinateEdit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Circle radius";
            // 
            // _dtb_height
            // 
            this._dtb_height.DistanceM = 0;
            this._dtb_height.Location = new System.Drawing.Point(78, 89);
            this._dtb_height.Name = "_dtb_height";
            this._dtb_height.ReadOnly = false;
            this._dtb_height.Size = new System.Drawing.Size(99, 21);
            this._dtb_height.TabIndex = 9;
            // 
            // _dtb_radius
            // 
            this._dtb_radius.DistanceM = 0;
            this._dtb_radius.Location = new System.Drawing.Point(78, 62);
            this._dtb_radius.Name = "_dtb_radius";
            this._dtb_radius.ReadOnly = false;
            this._dtb_radius.Size = new System.Drawing.Size(99, 21);
            this._dtb_radius.TabIndex = 8;
            // 
            // _ce
            // 
            this._ce.Location = new System.Drawing.Point(3, 3);
            this._ce.Name = "_ce";
            this._ce.Size = new System.Drawing.Size(174, 53);
            this._ce.TabIndex = 0;
            // 
            // CircleAbs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._dtb_height);
            this.Controls.Add(this._dtb_radius);
            this.Controls.Add(this._ce);
            this.Name = "CircleAbs";
            this.Size = new System.Drawing.Size(180, 114);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CoordinateEdit _ce;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DistanceTextBox _dtb_height;
        private DistanceTextBox _dtb_radius;
    }
}
