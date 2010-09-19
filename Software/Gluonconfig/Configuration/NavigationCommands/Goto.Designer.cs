namespace Configuration.NavigationCommands
{
    partial class Goto
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
            this._ntb_linenr = new Configuration.NumericTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Line #";
            // 
            // _ntb_linenr
            // 
            this._ntb_linenr.AllowSpace = false;
            this._ntb_linenr.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._ntb_linenr.DoubleValue = 0;
            this._ntb_linenr.IntValue = 0;
            this._ntb_linenr.Location = new System.Drawing.Point(59, 0);
            this._ntb_linenr.Name = "_ntb_linenr";
            this._ntb_linenr.Size = new System.Drawing.Size(48, 20);
            this._ntb_linenr.TabIndex = 1;
            this._ntb_linenr.Text = "0";
            // 
            // Goto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ntb_linenr);
            this.Controls.Add(this.label1);
            this.Name = "Goto";
            this.Size = new System.Drawing.Size(118, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private NumericTextBox _ntb_linenr;
    }
}
