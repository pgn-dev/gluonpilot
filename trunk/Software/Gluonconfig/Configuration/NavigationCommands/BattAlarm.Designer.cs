namespace Configuration.NavigationCommands
{
    partial class BattAlarm
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
            this._ntbPanic = new Configuration.NumericTextBox();
            this._ntbWarning = new Configuration.NumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._cbPanicLine = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.orientedTextLabel1 = new OrientedTextLabel();
            this.orientedTextLabel2 = new OrientedTextLabel();
            this.orientedTextLabel3 = new OrientedTextLabel();
            this.SuspendLayout();
            // 
            // _ntbPanic
            // 
            this._ntbPanic.AllowSpace = false;
            this._ntbPanic.DecimalValue = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this._ntbPanic.DoubleValue = 6D;
            this._ntbPanic.IntValue = 6;
            this._ntbPanic.Location = new System.Drawing.Point(24, 39);
            this._ntbPanic.Name = "_ntbPanic";
            this._ntbPanic.Size = new System.Drawing.Size(32, 20);
            this._ntbPanic.TabIndex = 1;
            this._ntbPanic.Text = "6";
            this._ntbPanic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._ntbPanic.TextChanged += new System.EventHandler(this._ntbPanic_TextChanged);
            this._ntbPanic.KeyDown += new System.Windows.Forms.KeyEventHandler(this._ntbPanic_KeyDown);
            // 
            // _ntbWarning
            // 
            this._ntbWarning.AllowSpace = false;
            this._ntbWarning.DecimalValue = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this._ntbWarning.DoubleValue = 7D;
            this._ntbWarning.IntValue = 7;
            this._ntbWarning.Location = new System.Drawing.Point(24, 103);
            this._ntbWarning.Name = "_ntbWarning";
            this._ntbWarning.Size = new System.Drawing.Size(32, 20);
            this._ntbWarning.TabIndex = 4;
            this._ntbWarning.Text = "7";
            this._ntbWarning.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._ntbWarning.TextChanged += new System.EventHandler(this._ntbWarning_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(73, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 26);
            this.label5.TabIndex = 6;
            this.label5.Text = "Send warning messages\r\nto the ground station";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(73, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Goto line";
            // 
            // _cbPanicLine
            // 
            this._cbPanicLine.FormattingEnabled = true;
            this._cbPanicLine.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34"});
            this._cbPanicLine.Location = new System.Drawing.Point(128, 14);
            this._cbPanicLine.Name = "_cbPanicLine";
            this._cbPanicLine.Size = new System.Drawing.Size(57, 21);
            this._cbPanicLine.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "0 V";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "V";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "V";
            // 
            // orientedTextLabel1
            // 
            this.orientedTextLabel1.BackColor = System.Drawing.Color.Red;
            this.orientedTextLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orientedTextLabel1.Location = new System.Drawing.Point(3, 0);
            this.orientedTextLabel1.Name = "orientedTextLabel1";
            this.orientedTextLabel1.RotationAngle = 270D;
            this.orientedTextLabel1.Size = new System.Drawing.Size(15, 51);
            this.orientedTextLabel1.TabIndex = 13;
            this.orientedTextLabel1.Text = "P a n i c";
            this.orientedTextLabel1.TextDirection = Direction.Clockwise;
            this.orientedTextLabel1.TextOrientation = Orientation.Rotate;
            // 
            // orientedTextLabel2
            // 
            this.orientedTextLabel2.BackColor = System.Drawing.Color.Orange;
            this.orientedTextLabel2.Location = new System.Drawing.Point(3, 48);
            this.orientedTextLabel2.Name = "orientedTextLabel2";
            this.orientedTextLabel2.RotationAngle = 270D;
            this.orientedTextLabel2.Size = new System.Drawing.Size(15, 65);
            this.orientedTextLabel2.TabIndex = 14;
            this.orientedTextLabel2.Text = " W a r n i n g";
            this.orientedTextLabel2.TextDirection = Direction.Clockwise;
            this.orientedTextLabel2.TextOrientation = Orientation.Rotate;
            // 
            // orientedTextLabel3
            // 
            this.orientedTextLabel3.BackColor = System.Drawing.Color.Chartreuse;
            this.orientedTextLabel3.Location = new System.Drawing.Point(3, 113);
            this.orientedTextLabel3.Name = "orientedTextLabel3";
            this.orientedTextLabel3.RotationAngle = 270D;
            this.orientedTextLabel3.Size = new System.Drawing.Size(15, 26);
            this.orientedTextLabel3.TabIndex = 15;
            this.orientedTextLabel3.Text = " O k";
            this.orientedTextLabel3.TextDirection = Direction.Clockwise;
            this.orientedTextLabel3.TextOrientation = Orientation.Rotate;
            // 
            // BattAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.orientedTextLabel3);
            this.Controls.Add(this.orientedTextLabel2);
            this.Controls.Add(this.orientedTextLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._ntbPanic);
            this.Controls.Add(this._cbPanicLine);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._ntbWarning);
            this.Controls.Add(this.label9);
            this.Name = "BattAlarm";
            this.Size = new System.Drawing.Size(200, 143);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericTextBox _ntbPanic;
        private NumericTextBox _ntbWarning;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox _cbPanicLine;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private OrientedTextLabel orientedTextLabel1;
        private OrientedTextLabel orientedTextLabel2;
        private OrientedTextLabel orientedTextLabel3;
    }
}
