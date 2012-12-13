namespace Configuration
{
    partial class NavigationInstructionEdit
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
            this._btn_ok = new System.Windows.Forms.Button();
            this._btn_cancel = new System.Windows.Forms.Button();
            this._cb_opcode = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this._gbParameters = new System.Windows.Forms.GroupBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this._gbParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btn_ok
            // 
            this._btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btn_ok.Location = new System.Drawing.Point(534, 233);
            this._btn_ok.Name = "_btn_ok";
            this._btn_ok.Size = new System.Drawing.Size(75, 23);
            this._btn_ok.TabIndex = 0;
            this._btn_ok.Text = "Ok";
            this._btn_ok.UseVisualStyleBackColor = true;
            this._btn_ok.Click += new System.EventHandler(this._btn_ok_Click);
            // 
            // _btn_cancel
            // 
            this._btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btn_cancel.Location = new System.Drawing.Point(22, 233);
            this._btn_cancel.Name = "_btn_cancel";
            this._btn_cancel.Size = new System.Drawing.Size(75, 23);
            this._btn_cancel.TabIndex = 1;
            this._btn_cancel.Text = "Cancel";
            this._btn_cancel.UseVisualStyleBackColor = true;
            this._btn_cancel.Click += new System.EventHandler(this._btn_cancel_Click);
            // 
            // _cb_opcode
            // 
            this._cb_opcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._cb_opcode.FormattingEnabled = true;
            this._cb_opcode.Items.AddRange(new object[] {
            "- Structure -",
            "CALL",
            "BLOCK",
            "EMPTY",
            "GOTO",
            "RETURN",
            "",
            "- Waypoints -",
            "CIRCLE",
            "CIRCLE_TO",
            "CLIMB",
            "FLARE_TO",
            "FLY_TO",
            "FROM_TO",
            "GLIDE_TO",
            "",
            "- Conditional -",
            "IF",
            "UNTIL",
            "",
            "- Servo -",
            "SERVO_SET",
            "SERVO_TRIGGER",
            "SERVO_TRIGGER_START",
            "SERVO_TRIGGER_STOP",
            "",
            "- Loiter -",
            "SET_LOITER_POSITION",
            "LOITER_CIRCLE",
            "",
            "- Settings -",
            "SET_BATTERY_ALARM",
            "SET_FLIGHTPLAN_SWITCH",
            "SET_MAXIMUM_RANGE"});
            this._cb_opcode.Location = new System.Drawing.Point(72, 12);
            this._cb_opcode.MaxDropDownItems = 20;
            this._cb_opcode.Name = "_cb_opcode";
            this._cb_opcode.Size = new System.Drawing.Size(164, 21);
            this._cb_opcode.TabIndex = 6;
            this._cb_opcode.SelectedIndexChanged += new System.EventHandler(this._cb_opcode_SelectedIndexChanged);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoScroll = true;
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(10, 20);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(10, 0);
            this.tableLayoutPanel.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Command";
            // 
            // _gbParameters
            // 
            this._gbParameters.AutoSize = true;
            this._gbParameters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._gbParameters.Controls.Add(this.tableLayoutPanel);
            this._gbParameters.Location = new System.Drawing.Point(16, 36);
            this._gbParameters.Margin = new System.Windows.Forms.Padding(0);
            this._gbParameters.MaximumSize = new System.Drawing.Size(270, 195);
            this._gbParameters.MinimumSize = new System.Drawing.Size(220, 0);
            this._gbParameters.Name = "_gbParameters";
            this._gbParameters.Padding = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this._gbParameters.Size = new System.Drawing.Size(220, 33);
            this._gbParameters.TabIndex = 12;
            this._gbParameters.TabStop = false;
            this._gbParameters.Text = "Parameters";
            this._gbParameters.Enter += new System.EventHandler(this.groupBox1_Enter);
            this._gbParameters.Resize += new System.EventHandler(this._gbParameters_Resize);
            // 
            // webBrowser
            // 
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(242, 12);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(367, 215);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // NavigationInstructionEdit
            // 
            this.AcceptButton = this._btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this._btn_cancel;
            this.ClientSize = new System.Drawing.Size(623, 268);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this._gbParameters);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._cb_opcode);
            this.Controls.Add(this._btn_ok);
            this.Controls.Add(this._btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NavigationInstructionEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit navigation instruction";
            this.Resize += new System.EventHandler(this.NavigationInstructionEdit_Resize);
            this._gbParameters.ResumeLayout(false);
            this._gbParameters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btn_ok;
        private System.Windows.Forms.Button _btn_cancel;
        private System.Windows.Forms.ComboBox _cb_opcode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox _gbParameters;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}