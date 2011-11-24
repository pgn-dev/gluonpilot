namespace Configuration.NavigationCommands
{
    partial class Until
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
            this._ntb = new Configuration.NumericTextBox();
            this._cbOperator = new System.Windows.Forms.ComboBox();
            this._cb_variables = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _ntb
            // 
            this._ntb.AllowSpace = false;
            this._ntb.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this._ntb.DoubleValue = 0D;
            this._ntb.IntValue = 0;
            this._ntb.Location = new System.Drawing.Point(46, 30);
            this._ntb.Name = "_ntb";
            this._ntb.Size = new System.Drawing.Size(100, 20);
            this._ntb.TabIndex = 9;
            this._ntb.Text = "0";
            // 
            // _cbOperator
            // 
            this._cbOperator.FormattingEnabled = true;
            this._cbOperator.Items.AddRange(new object[] {
            ">",
            "<",
            "=",
            "<>"});
            this._cbOperator.Location = new System.Drawing.Point(5, 29);
            this._cbOperator.Name = "_cbOperator";
            this._cbOperator.Size = new System.Drawing.Size(35, 21);
            this._cbOperator.TabIndex = 8;
            this._cbOperator.Text = ">";
            // 
            // _cb_variables
            // 
            this._cb_variables.FormattingEnabled = true;
            this._cb_variables.Location = new System.Drawing.Point(5, 2);
            this._cb_variables.Name = "_cb_variables";
            this._cb_variables.Size = new System.Drawing.Size(141, 21);
            this._cb_variables.TabIndex = 7;
            // 
            // Until
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this._ntb);
            this.Controls.Add(this._cbOperator);
            this.Controls.Add(this._cb_variables);
            this.Name = "Until";
            this.Size = new System.Drawing.Size(149, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericTextBox _ntb;
        private System.Windows.Forms.ComboBox _cbOperator;
        private System.Windows.Forms.ComboBox _cb_variables;
    }
}
