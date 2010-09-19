namespace Configuration
{
    partial class CoordinateEdit
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
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this._tb_lat = new System.Windows.Forms.TextBox();
            this._tb_lon = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Latitude";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(-3, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Longitude";
            // 
            // _tb_lat
            // 
            this._tb_lat.Location = new System.Drawing.Point(75, 0);
            this._tb_lat.Name = "_tb_lat";
            this._tb_lat.Size = new System.Drawing.Size(90, 20);
            this._tb_lat.TabIndex = 14;
            this._tb_lat.Leave += new System.EventHandler(this._tb_lat_Leave);
            // 
            // _tb_lon
            // 
            this._tb_lon.Location = new System.Drawing.Point(75, 30);
            this._tb_lon.Name = "_tb_lon";
            this._tb_lon.Size = new System.Drawing.Size(90, 20);
            this._tb_lon.TabIndex = 15;
            this._tb_lon.Leave += new System.EventHandler(this._tb_lon_Leave);
            // 
            // CoordinateEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._tb_lon);
            this.Controls.Add(this._tb_lat);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Name = "CoordinateEdit";
            this.Size = new System.Drawing.Size(167, 51);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox _tb_lat;
        private System.Windows.Forms.TextBox _tb_lon;
    }
}
