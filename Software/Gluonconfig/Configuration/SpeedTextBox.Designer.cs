namespace Configuration
{
    partial class SpeedTextBox
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
            this.tb_speed = new Configuration.NumericTextBox();
            this.SuspendLayout();
            // 
            // cb_unit
            // 
            this.cb_unit.DisplayMember = "m/s";
            this.cb_unit.FormattingEnabled = true;
            this.cb_unit.Items.AddRange(new object[] {
            "m/s",
            "km/h",
            "mph"});
            this.cb_unit.Location = new System.Drawing.Point(59, 0);
            this.cb_unit.Name = "cb_unit";
            this.cb_unit.Size = new System.Drawing.Size(50, 21);
            this.cb_unit.TabIndex = 1;
            this.cb_unit.ValueMember = "m/s";
            this.cb_unit.SelectedIndexChanged += new System.EventHandler(this.cb_unit_SelectedIndexChanged);
            this.cb_unit.DropDown += new System.EventHandler(this.cb_unit_DropDown);
            // 
            // tb_speed
            // 
            this.tb_speed.AllowSpace = false;
            this.tb_speed.Location = new System.Drawing.Point(0, 0);
            this.tb_speed.Name = "tb_speed";
            this.tb_speed.Size = new System.Drawing.Size(53, 20);
            this.tb_speed.TabIndex = 0;
            this.tb_speed.Text = "0";
            this.tb_speed.TextChanged += new System.EventHandler(this.tb_speed_TextChanged);
            this.tb_speed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_speed_KeyPress);
            // 
            // SpeedTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_unit);
            this.Controls.Add(this.tb_speed);
            this.Name = "SpeedTextBox";
            this.Size = new System.Drawing.Size(110, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericTextBox tb_speed;
        private System.Windows.Forms.ComboBox cb_unit;
    }
}
