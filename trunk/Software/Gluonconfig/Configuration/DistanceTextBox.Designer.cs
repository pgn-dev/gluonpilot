namespace Configuration
{
    partial class DistanceTextBox
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
            this.cb_unit = new System.Windows.Forms.ComboBox();
            this.tb_distance = new Configuration.NumericTextBox();
            this.SuspendLayout();
            // 
            // cb_unit
            // 
            this.cb_unit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_unit.FormattingEnabled = true;
            this.cb_unit.Items.AddRange(new object[] {
            "m",
            "km",
            "ft",
            "mile"});
            this.cb_unit.Location = new System.Drawing.Point(55, 0);
            this.cb_unit.Name = "cb_unit";
            this.cb_unit.Size = new System.Drawing.Size(43, 21);
            this.cb_unit.TabIndex = 1;
            this.cb_unit.SelectedIndexChanged += new System.EventHandler(this.cb_unit_SelectedIndexChanged);
            this.cb_unit.DropDown += new System.EventHandler(this.cb_unit_DropDown);
            // 
            // tb_distance
            // 
            this.tb_distance.AllowSpace = false;
            this.tb_distance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_distance.Location = new System.Drawing.Point(0, 0);
            this.tb_distance.Name = "tb_distance";
            this.tb_distance.Size = new System.Drawing.Size(49, 20);
            this.tb_distance.TabIndex = 0;
            this.tb_distance.TextChanged += new System.EventHandler(this.tb_distance_TextChanged);
            this.tb_distance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_distance_KeyPress);
            // 
            // DistanceTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_unit);
            this.Controls.Add(this.tb_distance);
            this.Name = "DistanceTextBox";
            this.Size = new System.Drawing.Size(99, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericTextBox tb_distance;
        private System.Windows.Forms.ComboBox cb_unit;
    }
}
