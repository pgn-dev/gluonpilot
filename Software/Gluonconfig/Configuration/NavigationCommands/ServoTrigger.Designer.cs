namespace Configuration.NavigationCommands
{
    partial class ServoTrigger
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
            this._nud_channel = new System.Windows.Forms.NumericUpDown();
            this._nud_us = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._nud_position_hold = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this._nud_channel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_us)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_position_hold)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Pulse width (us):";
            // 
            // _nud_channel
            // 
            this._nud_channel.Location = new System.Drawing.Point(101, 7);
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
            this._nud_channel.TabIndex = 6;
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
            this._nud_us.Location = new System.Drawing.Point(101, 33);
            this._nud_us.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this._nud_us.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._nud_us.Name = "_nud_us";
            this._nud_us.Size = new System.Drawing.Size(62, 20);
            this._nud_us.TabIndex = 5;
            this._nud_us.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Servo channel:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Position hold (ms):";
            // 
            // _nud_position_hold
            // 
            this._nud_position_hold.Location = new System.Drawing.Point(101, 62);
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
            this._nud_position_hold.TabIndex = 9;
            this._nud_position_hold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ServoTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._nud_position_hold);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._nud_channel);
            this.Controls.Add(this._nud_us);
            this.Controls.Add(this.label1);
            this.Name = "ServoTrigger";
            this.Size = new System.Drawing.Size(172, 87);
            ((System.ComponentModel.ISupportInitialize)(this._nud_channel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_us)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_position_hold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown _nud_channel;
        private System.Windows.Forms.NumericUpDown _nud_us;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown _nud_position_hold;
    }
}
