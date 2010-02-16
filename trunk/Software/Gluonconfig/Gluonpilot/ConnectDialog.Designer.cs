namespace Gluonpilot
{
    partial class ConnectDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._cbPorts = new System.Windows.Forms.ComboBox();
            this._serial = new System.IO.Ports.SerialPort(this.components);
            this._cbBaud = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _btnConnect
            // 
            this._btnConnect.Location = new System.Drawing.Point(69, 84);
            this._btnConnect.Name = "_btnConnect";
            this._btnConnect.Size = new System.Drawing.Size(75, 23);
            this._btnConnect.TabIndex = 0;
            this._btnConnect.Text = "Connect";
            this._btnConnect.UseVisualStyleBackColor = true;
            this._btnConnect.Click += new System.EventHandler(this._btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Com port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Baudrate:";
            // 
            // _cbPorts
            // 
            this._cbPorts.FormattingEnabled = true;
            this._cbPorts.Location = new System.Drawing.Point(71, 10);
            this._cbPorts.Name = "_cbPorts";
            this._cbPorts.Size = new System.Drawing.Size(121, 21);
            this._cbPorts.TabIndex = 3;
            // 
            // _cbBaud
            // 
            this._cbBaud.FormattingEnabled = true;
            this._cbBaud.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400"});
            this._cbBaud.Location = new System.Drawing.Point(71, 38);
            this._cbBaud.Name = "_cbBaud";
            this._cbBaud.Size = new System.Drawing.Size(121, 21);
            this._cbBaud.TabIndex = 4;
            // 
            // ConnectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 119);
            this.Controls.Add(this._cbBaud);
            this.Controls.Add(this._cbPorts);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._btnConnect);
            this.Name = "ConnectDialog";
            this.Text = "ConnectDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _cbPorts;
        private System.IO.Ports.SerialPort _serial;
        private System.Windows.Forms.ComboBox _cbBaud;
    }
}