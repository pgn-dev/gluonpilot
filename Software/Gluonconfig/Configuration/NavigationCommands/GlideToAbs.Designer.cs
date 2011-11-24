namespace Configuration.NavigationCommands
{
    partial class GlideToAbs
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
            this.label2 = new System.Windows.Forms.Label();
            this._nud_throttle = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._dtb_height = new Configuration.DistanceTextBox();
            this._ce = new Configuration.CoordinateEdit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_throttle)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "%";
            // 
            // _nud_throttle
            // 
            this._nud_throttle.Location = new System.Drawing.Point(77, 90);
            this._nud_throttle.Name = "_nud_throttle";
            this._nud_throttle.Size = new System.Drawing.Size(49, 20);
            this._nud_throttle.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Throttle";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-1, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Altitude";
            // 
            // _dtb_height
            // 
            this._dtb_height.Color = System.Drawing.SystemColors.Window;
            this._dtb_height.DistanceM = 0D;
            this._dtb_height.Location = new System.Drawing.Point(77, 62);
            this._dtb_height.Name = "_dtb_height";
            this._dtb_height.ReadOnly = false;
            this._dtb_height.Size = new System.Drawing.Size(99, 21);
            this._dtb_height.TabIndex = 21;
            this._dtb_height.UseAltitudeColoring = false;
            // 
            // _ce
            // 
            this._ce.Location = new System.Drawing.Point(2, 3);
            this._ce.Name = "_ce";
            this._ce.Size = new System.Drawing.Size(174, 53);
            this._ce.TabIndex = 20;
            // 
            // GlideToAbs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this._nud_throttle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._dtb_height);
            this.Controls.Add(this._ce);
            this.Name = "GlideToAbs";
            this.Size = new System.Drawing.Size(179, 114);
            ((System.ComponentModel.ISupportInitialize)(this._nud_throttle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown _nud_throttle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private DistanceTextBox _dtb_height;
        private CoordinateEdit _ce;
    }
}
