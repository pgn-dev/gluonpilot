namespace Configuration.NavigationCommands
{
    partial class ServoStartTrigger
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
            this._nud_position_hold = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._nud_channel = new System.Windows.Forms.NumericUpDown();
            this._nud_us = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._nud_time_between_triggers_ms = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this._cb_trigger_mode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this._nud_position_hold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_channel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_us)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_time_between_triggers_ms)).BeginInit();
            this.SuspendLayout();
            // 
            // _nud_position_hold
            // 
            this._nud_position_hold.Location = new System.Drawing.Point(133, 124);
            this._nud_position_hold.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this._nud_position_hold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._nud_position_hold.Name = "_nud_position_hold";
            this._nud_position_hold.Size = new System.Drawing.Size(62, 20);
            this._nud_position_hold.TabIndex = 15;
            this._nud_position_hold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Position hold (ms):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Pulse width (us):";
            // 
            // _nud_channel
            // 
            this._nud_channel.Location = new System.Drawing.Point(141, 1);
            this._nud_channel.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this._nud_channel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._nud_channel.Name = "_nud_channel";
            this._nud_channel.Size = new System.Drawing.Size(62, 20);
            this._nud_channel.TabIndex = 12;
            this._nud_channel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _nud_us
            // 
            this._nud_us.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._nud_us.Location = new System.Drawing.Point(141, 27);
            this._nud_us.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this._nud_us.Name = "_nud_us";
            this._nud_us.Size = new System.Drawing.Size(62, 20);
            this._nud_us.TabIndex = 11;
            this._nud_us.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Servo channel:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-1, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Time between triggers (ms):";
            // 
            // _nud_time_between_triggers_ms
            // 
            this._nud_time_between_triggers_ms.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._nud_time_between_triggers_ms.Location = new System.Drawing.Point(141, 53);
            this._nud_time_between_triggers_ms.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this._nud_time_between_triggers_ms.Name = "_nud_time_between_triggers_ms";
            this._nud_time_between_triggers_ms.Size = new System.Drawing.Size(62, 20);
            this._nud_time_between_triggers_ms.TabIndex = 17;
            this._nud_time_between_triggers_ms.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-1, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Trigger mode:";
            // 
            // _cb_trigger_mode
            // 
            this._cb_trigger_mode.FormattingEnabled = true;
            this._cb_trigger_mode.Items.AddRange(new object[] {
            "PWM",
            "CHDK"});
            this._cb_trigger_mode.Location = new System.Drawing.Point(133, 79);
            this._cb_trigger_mode.Name = "_cb_trigger_mode";
            this._cb_trigger_mode.Size = new System.Drawing.Size(70, 21);
            this._cb_trigger_mode.TabIndex = 19;
            // 
            // ServoStartTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._cb_trigger_mode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._nud_time_between_triggers_ms);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._nud_position_hold);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._nud_channel);
            this.Controls.Add(this._nud_us);
            this.Controls.Add(this.label1);
            this.Name = "ServoStartTrigger";
            this.Size = new System.Drawing.Size(210, 105);
            ((System.ComponentModel.ISupportInitialize)(this._nud_position_hold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_channel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_us)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_time_between_triggers_ms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown _nud_position_hold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown _nud_channel;
        private System.Windows.Forms.NumericUpDown _nud_us;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _nud_time_between_triggers_ms;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox _cb_trigger_mode;
    }
}
