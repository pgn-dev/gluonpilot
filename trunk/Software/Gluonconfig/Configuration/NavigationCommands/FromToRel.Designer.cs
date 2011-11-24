namespace Configuration.NavigationCommands
{
    partial class FromToRel
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._dtb_east = new Configuration.DistanceTextBox();
            this._dtb_north = new Configuration.DistanceTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Altitude";
            // 
            // _dtb_height
            // 
            this._dtb_height.Color = System.Drawing.SystemColors.Window;
            this._dtb_height.DistanceM = 0D;
            this._dtb_height.Location = new System.Drawing.Point(73, 55);
            this._dtb_height.Name = "_dtb_height";
            this._dtb_height.ReadOnly = false;
            this._dtb_height.Size = new System.Drawing.Size(99, 21);
            this._dtb_height.TabIndex = 4;
            this._dtb_height.UseAltitudeColoring = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(178, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "N of home";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Waypoint";
            // 
            // _dtb_east
            // 
            this._dtb_east.Color = System.Drawing.SystemColors.Window;
            this._dtb_east.DistanceM = 0D;
            this._dtb_east.Location = new System.Drawing.Point(73, 28);
            this._dtb_east.Name = "_dtb_east";
            this._dtb_east.ReadOnly = false;
            this._dtb_east.Size = new System.Drawing.Size(99, 21);
            this._dtb_east.TabIndex = 13;
            this._dtb_east.UseAltitudeColoring = false;
            // 
            // _dtb_north
            // 
            this._dtb_north.Color = System.Drawing.SystemColors.Window;
            this._dtb_north.DistanceM = 0D;
            this._dtb_north.Location = new System.Drawing.Point(73, 1);
            this._dtb_north.Name = "_dtb_north";
            this._dtb_north.ReadOnly = false;
            this._dtb_north.Size = new System.Drawing.Size(99, 21);
            this._dtb_north.TabIndex = 12;
            this._dtb_north.UseAltitudeColoring = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "E of home";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Waypoint";
            // 
            // FromToRel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._dtb_east);
            this.Controls.Add(this._dtb_north);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._dtb_height);
            this.Name = "FromToRel";
            this.Size = new System.Drawing.Size(237, 79);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DistanceTextBox _dtb_height;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private DistanceTextBox _dtb_east;
        private DistanceTextBox _dtb_north;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
