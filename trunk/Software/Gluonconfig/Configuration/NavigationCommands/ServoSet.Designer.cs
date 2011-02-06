namespace Configuration.NavigationCommands
{
    partial class ServoSet
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
            this._nud_us = new System.Windows.Forms.NumericUpDown();
            this._nud_channel = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._nud_us)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_channel)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Servo channel:";
            // 
            // _nud_us
            // 
            this._nud_us.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this._nud_us.Location = new System.Drawing.Point(98, 30);
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
            this._nud_us.TabIndex = 1;
            this._nud_us.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // _nud_channel
            // 
            this._nud_channel.Location = new System.Drawing.Point(98, 4);
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
            this._nud_channel.TabIndex = 2;
            this._nud_channel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pulse width (us):";
            // 
            // ServoSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this._nud_channel);
            this.Controls.Add(this._nud_us);
            this.Controls.Add(this.label1);
            this.Name = "ServoSet";
            this.Size = new System.Drawing.Size(166, 56);
            ((System.ComponentModel.ISupportInitialize)(this._nud_us)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_channel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _nud_us;
        private System.Windows.Forms.NumericUpDown _nud_channel;
        private System.Windows.Forms.Label label2;
    }
}
