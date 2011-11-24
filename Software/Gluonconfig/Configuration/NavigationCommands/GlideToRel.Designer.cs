namespace Configuration.NavigationCommands
{
    partial class GlideToRel
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
            this._nud_throttle = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._dtb_east = new Configuration.DistanceTextBox();
            this._dtb_north = new Configuration.DistanceTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._dtb_height = new Configuration.DistanceTextBox();
            ((System.ComponentModel.ISupportInitialize)(this._nud_throttle)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "%";
            // 
            // _nud_throttle
            // 
            this._nud_throttle.Location = new System.Drawing.Point(75, 79);
            this._nud_throttle.Name = "_nud_throttle";
            this._nud_throttle.Size = new System.Drawing.Size(49, 20);
            this._nud_throttle.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Throttle";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(180, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "N of home";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Waypoint";
            // 
            // _dtb_east
            // 
            this._dtb_east.Color = System.Drawing.SystemColors.Window;
            this._dtb_east.DistanceM = 0D;
            this._dtb_east.Location = new System.Drawing.Point(75, 25);
            this._dtb_east.Name = "_dtb_east";
            this._dtb_east.ReadOnly = false;
            this._dtb_east.Size = new System.Drawing.Size(99, 21);
            this._dtb_east.TabIndex = 28;
            this._dtb_east.UseAltitudeColoring = false;
            // 
            // _dtb_north
            // 
            this._dtb_north.Color = System.Drawing.SystemColors.Window;
            this._dtb_north.DistanceM = 0D;
            this._dtb_north.Location = new System.Drawing.Point(75, -2);
            this._dtb_north.Name = "_dtb_north";
            this._dtb_north.ReadOnly = false;
            this._dtb_north.Size = new System.Drawing.Size(99, 21);
            this._dtb_north.TabIndex = 27;
            this._dtb_north.UseAltitudeColoring = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "E of home";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Waypoint";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Altitude";
            // 
            // _dtb_height
            // 
            this._dtb_height.Color = System.Drawing.SystemColors.Window;
            this._dtb_height.DistanceM = 0D;
            this._dtb_height.Location = new System.Drawing.Point(75, 52);
            this._dtb_height.Name = "_dtb_height";
            this._dtb_height.ReadOnly = false;
            this._dtb_height.Size = new System.Drawing.Size(99, 21);
            this._dtb_height.TabIndex = 23;
            this._dtb_height.UseAltitudeColoring = false;
            // 
            // GlideToRel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this._nud_throttle);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._dtb_east);
            this.Controls.Add(this._dtb_north);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._dtb_height);
            this.Name = "GlideToRel";
            this.Size = new System.Drawing.Size(248, 103);
            ((System.ComponentModel.ISupportInitialize)(this._nud_throttle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _nud_throttle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private DistanceTextBox _dtb_east;
        private DistanceTextBox _dtb_north;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DistanceTextBox _dtb_height;
    }
}
