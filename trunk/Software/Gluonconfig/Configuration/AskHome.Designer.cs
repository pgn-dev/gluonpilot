namespace Configuration
{
    partial class AskHome
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._ce = new Configuration.CoordinateEdit();
            this._btn_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _ce
            // 
            this._ce.Location = new System.Drawing.Point(65, 12);
            this._ce.Name = "_ce";
            this._ce.Size = new System.Drawing.Size(167, 51);
            this._ce.TabIndex = 0;
            // 
            // _btn_ok
            // 
            this._btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btn_ok.Location = new System.Drawing.Point(117, 78);
            this._btn_ok.Name = "_btn_ok";
            this._btn_ok.Size = new System.Drawing.Size(75, 23);
            this._btn_ok.TabIndex = 1;
            this._btn_ok.Text = "OK";
            this._btn_ok.UseVisualStyleBackColor = true;
            this._btn_ok.Click += new System.EventHandler(this._btn_ok_Click);
            // 
            // AskHome
            // 
            this.AcceptButton = this._btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 113);
            this.Controls.Add(this._btn_ok);
            this.Controls.Add(this._ce);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AskHome";
            this.Text = "What is your home location?";
            this.ResumeLayout(false);

        }

        #endregion

        private CoordinateEdit _ce;
        private System.Windows.Forms.Button _btn_ok;
    }
}