namespace Configuration.NavigationCommands
{
    partial class CircleRel
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._dtb_altitude = new Configuration.DistanceTextBox();
            this._dtb_radius = new Configuration.DistanceTextBox();
            this.distanceTextBoxEast = new Configuration.DistanceTextBox();
            this.distanceTextBoxNorth = new Configuration.DistanceTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Circle center";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "E of home";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Circle radius";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Altitude";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Circle center";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(177, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "N of home";
            // 
            // _dtb_altitude
            // 
            this._dtb_altitude.Color = System.Drawing.SystemColors.Window;
            this._dtb_altitude.DistanceM = 0D;
            this._dtb_altitude.Location = new System.Drawing.Point(72, 82);
            this._dtb_altitude.Name = "_dtb_altitude";
            this._dtb_altitude.ReadOnly = false;
            this._dtb_altitude.Size = new System.Drawing.Size(99, 21);
            this._dtb_altitude.TabIndex = 5;
            this._dtb_altitude.UseAltitudeColoring = true;
            // 
            // _dtb_radius
            // 
            this._dtb_radius.Color = System.Drawing.SystemColors.Window;
            this._dtb_radius.DistanceM = 0D;
            this._dtb_radius.Location = new System.Drawing.Point(72, 55);
            this._dtb_radius.Name = "_dtb_radius";
            this._dtb_radius.ReadOnly = false;
            this._dtb_radius.Size = new System.Drawing.Size(99, 21);
            this._dtb_radius.TabIndex = 4;
            this._dtb_radius.UseAltitudeColoring = false;
            this._dtb_radius.DistanceChanged += new System.EventHandler(this._dtb_radius_DistanceChanged);
            // 
            // distanceTextBoxEast
            // 
            this.distanceTextBoxEast.Color = System.Drawing.SystemColors.Window;
            this.distanceTextBoxEast.DistanceM = 0D;
            this.distanceTextBoxEast.Location = new System.Drawing.Point(72, 28);
            this.distanceTextBoxEast.Name = "distanceTextBoxEast";
            this.distanceTextBoxEast.ReadOnly = false;
            this.distanceTextBoxEast.Size = new System.Drawing.Size(99, 21);
            this.distanceTextBoxEast.TabIndex = 3;
            this.distanceTextBoxEast.UseAltitudeColoring = false;
            // 
            // distanceTextBoxNorth
            // 
            this.distanceTextBoxNorth.Color = System.Drawing.SystemColors.Window;
            this.distanceTextBoxNorth.DistanceM = 0D;
            this.distanceTextBoxNorth.Location = new System.Drawing.Point(72, 1);
            this.distanceTextBoxNorth.Name = "distanceTextBoxNorth";
            this.distanceTextBoxNorth.ReadOnly = false;
            this.distanceTextBoxNorth.Size = new System.Drawing.Size(99, 21);
            this.distanceTextBoxNorth.TabIndex = 2;
            this.distanceTextBoxNorth.UseAltitudeColoring = false;
            // 
            // CircleRel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._dtb_altitude);
            this.Controls.Add(this._dtb_radius);
            this.Controls.Add(this.distanceTextBoxEast);
            this.Controls.Add(this.distanceTextBoxNorth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CircleRel";
            this.Size = new System.Drawing.Size(233, 103);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DistanceTextBox distanceTextBoxNorth;
        private DistanceTextBox distanceTextBoxEast;
        private DistanceTextBox _dtb_radius;
        private DistanceTextBox _dtb_altitude;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
