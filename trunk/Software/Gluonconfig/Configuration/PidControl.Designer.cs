namespace Configuration
{
    partial class PidControl
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
            this._tb_P = new Configuration.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._tb_I = new Configuration.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._tb_D = new Configuration.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._tb_Imin = new Configuration.NumericTextBox();
            this._tb_Imax = new Configuration.NumericTextBox();
            this._tb_Dmin = new Configuration.NumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "P:";
            // 
            // _tb_P
            // 
            this._tb_P.AllowSpace = false;
            this._tb_P.Location = new System.Drawing.Point(67, 3);
            this._tb_P.Name = "_tb_P";
            this._tb_P.Size = new System.Drawing.Size(44, 20);
            this._tb_P.TabIndex = 1;
            this._tb_P.TextChanged += new System.EventHandler(this._tb_P_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "I:";
            // 
            // _tb_I
            // 
            this._tb_I.AllowSpace = false;
            this._tb_I.Location = new System.Drawing.Point(136, 3);
            this._tb_I.Name = "_tb_I";
            this._tb_I.Size = new System.Drawing.Size(44, 20);
            this._tb_I.TabIndex = 3;
            this._tb_I.TextChanged += new System.EventHandler(this._tb_I_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "D:";
            // 
            // _tb_D
            // 
            this._tb_D.AllowSpace = false;
            this._tb_D.Location = new System.Drawing.Point(211, 3);
            this._tb_D.Name = "_tb_D";
            this._tb_D.Size = new System.Drawing.Size(44, 20);
            this._tb_D.TabIndex = 5;
            this._tb_D.TextChanged += new System.EventHandler(this._tb_D_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Gains:";
            // 
            // _tb_Imin
            // 
            this._tb_Imin.AllowSpace = false;
            this._tb_Imin.Location = new System.Drawing.Point(72, 38);
            this._tb_Imin.Name = "_tb_Imin";
            this._tb_Imin.Size = new System.Drawing.Size(44, 20);
            this._tb_Imin.TabIndex = 7;
            this._tb_Imin.TextChanged += new System.EventHandler(this._tb_Imin_TextChanged);
            // 
            // _tb_Imax
            // 
            this._tb_Imax.AllowSpace = false;
            this._tb_Imax.Location = new System.Drawing.Point(156, 37);
            this._tb_Imax.Name = "_tb_Imax";
            this._tb_Imax.Size = new System.Drawing.Size(44, 20);
            this._tb_Imax.TabIndex = 8;
            this._tb_Imax.TextChanged += new System.EventHandler(this._tb_Imax_TextChanged);
            // 
            // _tb_Dmin
            // 
            this._tb_Dmin.AllowSpace = false;
            this._tb_Dmin.Location = new System.Drawing.Point(72, 64);
            this._tb_Dmin.Name = "_tb_Dmin";
            this._tb_Dmin.Size = new System.Drawing.Size(44, 20);
            this._tb_Dmin.TabIndex = 9;
            this._tb_Dmin.TextChanged += new System.EventHandler(this._tb_Dmin_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(122, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "< |D|";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(122, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "< I <";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Boundaries:";
            // 
            // PidControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._tb_Dmin);
            this.Controls.Add(this._tb_Imax);
            this.Controls.Add(this._tb_Imin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._tb_D);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._tb_I);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._tb_P);
            this.Controls.Add(this.label1);
            this.Name = "PidControl";
            this.Size = new System.Drawing.Size(296, 87);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private NumericTextBox _tb_P;
        private System.Windows.Forms.Label label2;
        private NumericTextBox _tb_I;
        private System.Windows.Forms.Label label3;
        private NumericTextBox _tb_D;
        private System.Windows.Forms.Label label4;
        private NumericTextBox _tb_Imin;
        private NumericTextBox _tb_Imax;
        private NumericTextBox _tb_Dmin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
    }
}
