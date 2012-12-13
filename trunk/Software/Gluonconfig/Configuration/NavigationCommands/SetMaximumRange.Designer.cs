namespace Configuration.NavigationCommands
{
    partial class SetMaximumRange
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
            this._cbPanicLine = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._dtb_max_distance = new Configuration.DistanceTextBox();
            this.SuspendLayout();
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
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72"});
            this._cbPanicLine.Location = new System.Drawing.Point(86, 47);
            this._cbPanicLine.Name = "_cbPanicLine";
            this._cbPanicLine.Size = new System.Drawing.Size(57, 21);
            this._cbPanicLine.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Then goto line";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "When distance to home > ";
            // 
            // _dtb_max_distance
            // 
            this._dtb_max_distance.Color = System.Drawing.SystemColors.Window;
            this._dtb_max_distance.DistanceM = 0D;
            this._dtb_max_distance.Location = new System.Drawing.Point(34, 20);
            this._dtb_max_distance.Name = "_dtb_max_distance";
            this._dtb_max_distance.ReadOnly = false;
            this._dtb_max_distance.Size = new System.Drawing.Size(109, 21);
            this._dtb_max_distance.TabIndex = 12;
            this._dtb_max_distance.UseAltitudeColoring = false;
            // 
            // SetMaximumRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dtb_max_distance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._cbPanicLine);
            this.Controls.Add(this.label7);
            this.Name = "SetMaximumRange";
            this.Size = new System.Drawing.Size(157, 79);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _cbPanicLine;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private DistanceTextBox _dtb_max_distance;
    }
}
