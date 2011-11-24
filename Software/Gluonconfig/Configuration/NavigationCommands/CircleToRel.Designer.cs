namespace Configuration.NavigationCommands
{
    partial class CircleToRel
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.distanceTextBoxHeight = new Configuration.DistanceTextBox();
            this.distanceTextBoxEast = new Configuration.DistanceTextBox();
            this.distanceTextBoxNorth = new Configuration.DistanceTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(162, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "N of home";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-1, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Waypoint";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-1, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Altitude";
            // 
            // distanceTextBoxHeight
            // 
            this.distanceTextBoxHeight.Color = System.Drawing.SystemColors.Window;
            this.distanceTextBoxHeight.DistanceM = 0D;
            this.distanceTextBoxHeight.Location = new System.Drawing.Point(57, 54);
            this.distanceTextBoxHeight.Name = "distanceTextBoxHeight";
            this.distanceTextBoxHeight.ReadOnly = false;
            this.distanceTextBoxHeight.Size = new System.Drawing.Size(99, 21);
            this.distanceTextBoxHeight.TabIndex = 24;
            this.distanceTextBoxHeight.UseAltitudeColoring = true;
            // 
            // distanceTextBoxEast
            // 
            this.distanceTextBoxEast.Color = System.Drawing.SystemColors.Window;
            this.distanceTextBoxEast.DistanceM = 0D;
            this.distanceTextBoxEast.Location = new System.Drawing.Point(57, 27);
            this.distanceTextBoxEast.Name = "distanceTextBoxEast";
            this.distanceTextBoxEast.ReadOnly = false;
            this.distanceTextBoxEast.Size = new System.Drawing.Size(99, 21);
            this.distanceTextBoxEast.TabIndex = 23;
            this.distanceTextBoxEast.UseAltitudeColoring = false;
            // 
            // distanceTextBoxNorth
            // 
            this.distanceTextBoxNorth.Color = System.Drawing.SystemColors.Window;
            this.distanceTextBoxNorth.DistanceM = 0D;
            this.distanceTextBoxNorth.Location = new System.Drawing.Point(57, 0);
            this.distanceTextBoxNorth.Name = "distanceTextBoxNorth";
            this.distanceTextBoxNorth.ReadOnly = false;
            this.distanceTextBoxNorth.Size = new System.Drawing.Size(99, 21);
            this.distanceTextBoxNorth.TabIndex = 22;
            this.distanceTextBoxNorth.UseAltitudeColoring = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "E of home";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Waypoint";
            // 
            // CircleToRel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.distanceTextBoxHeight);
            this.Controls.Add(this.distanceTextBoxEast);
            this.Controls.Add(this.distanceTextBoxNorth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CircleToRel";
            this.Size = new System.Drawing.Size(217, 77);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DistanceTextBox distanceTextBoxHeight;
        private DistanceTextBox distanceTextBoxEast;
        private DistanceTextBox distanceTextBoxNorth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
