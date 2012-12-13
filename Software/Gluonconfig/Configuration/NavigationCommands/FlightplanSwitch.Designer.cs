namespace Configuration.NavigationCommands
{
    partial class FlightplanSwitch
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
            this._cbChannel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._nud_14 = new System.Windows.Forms.NumericUpDown();
            this._nud_15 = new System.Windows.Forms.NumericUpDown();
            this._nud_16 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._nud_14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_16)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Channel input:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // _cbChannel
            // 
            this._cbChannel.FormattingEnabled = true;
            this._cbChannel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this._cbChannel.Location = new System.Drawing.Point(82, 6);
            this._cbChannel.Name = "_cbChannel";
            this._cbChannel.Size = new System.Drawing.Size(55, 21);
            this._cbChannel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Input position:";
            // 
            // _nud_14
            // 
            this._nud_14.BackColor = System.Drawing.Color.White;
            this._nud_14.Location = new System.Drawing.Point(79, 61);
            this._nud_14.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
            this._nud_14.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._nud_14.Name = "_nud_14";
            this._nud_14.Size = new System.Drawing.Size(42, 20);
            this._nud_14.TabIndex = 3;
            this._nud_14.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _nud_15
            // 
            this._nud_15.Location = new System.Drawing.Point(127, 61);
            this._nud_15.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
            this._nud_15.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._nud_15.Name = "_nud_15";
            this._nud_15.Size = new System.Drawing.Size(42, 20);
            this._nud_15.TabIndex = 4;
            this._nud_15.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _nud_16
            // 
            this._nud_16.Location = new System.Drawing.Point(175, 61);
            this._nud_16.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
            this._nud_16.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._nud_16.Name = "_nud_16";
            this._nud_16.Size = new System.Drawing.Size(42, 20);
            this._nud_16.TabIndex = 5;
            this._nud_16.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(124, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "center";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(76, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "< 1.4ms";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(172, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "> 1.6ms";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Goto line:";
            // 
            // FlightplanSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._nud_16);
            this.Controls.Add(this._nud_15);
            this.Controls.Add(this._nud_14);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._cbChannel);
            this.Controls.Add(this.label1);
            this.Name = "FlightplanSwitch";
            this.Size = new System.Drawing.Size(233, 97);
            ((System.ComponentModel.ISupportInitialize)(this._nud_14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nud_16)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _cbChannel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown _nud_14;
        private System.Windows.Forms.NumericUpDown _nud_15;
        private System.Windows.Forms.NumericUpDown _nud_16;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
