namespace Configuration.NavigationCommands
{
    partial class NavigationCommandEditor
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
            this._cb_opcode = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._btn_ok = new System.Windows.Forms.Button();
            this._btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _cb_opcode
            // 
            this._cb_opcode.FormattingEnabled = true;
            this._cb_opcode.Items.AddRange(new object[] {
            "EMPTY",
            "CLIMB",
            "FROM_TO_REL",
            "FROM_TO_ABS",
            "FLY_TO_REL",
            "FLY_TO_ABS",
            "GOTO",
            "CIRCLE_ABS",
            "CIRCLE_REL",
            "IF_EQ",
            "IF_SM",
            "IF_GR",
            "IF_NE",
            "WHILE_EQ",
            "WHILE_NE",
            "WHILE_GR",
            "WHILE_SM"});
            this._cb_opcode.Location = new System.Drawing.Point(57, 12);
            this._cb_opcode.Name = "_cb_opcode";
            this._cb_opcode.Size = new System.Drawing.Size(268, 21);
            this._cb_opcode.TabIndex = 3;
            this._cb_opcode.SelectedIndexChanged += new System.EventHandler(this._cb_opcode_SelectedIndexChanged);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(40, 39);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(305, 134);
            this.flowLayoutPanel.TabIndex = 4;
            // 
            // _btn_ok
            // 
            this._btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btn_ok.Location = new System.Drawing.Point(106, 184);
            this._btn_ok.Name = "_btn_ok";
            this._btn_ok.Size = new System.Drawing.Size(75, 23);
            this._btn_ok.TabIndex = 1;
            this._btn_ok.Text = "&OK";
            this._btn_ok.UseVisualStyleBackColor = true;
            this._btn_ok.Click += new System.EventHandler(this._btn_ok_Click);
            // 
            // _btn_cancel
            // 
            this._btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btn_cancel.Location = new System.Drawing.Point(201, 184);
            this._btn_cancel.Name = "_btn_cancel";
            this._btn_cancel.Size = new System.Drawing.Size(75, 23);
            this._btn_cancel.TabIndex = 5;
            this._btn_cancel.Text = "&Cancel";
            this._btn_cancel.UseVisualStyleBackColor = true;
            this._btn_cancel.Click += new System.EventHandler(this._btn_cancel_Click);
            // 
            // NavigationCommandEditor
            // 
            this.AcceptButton = this._btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btn_cancel;
            this.ClientSize = new System.Drawing.Size(385, 219);
            this.Controls.Add(this._btn_cancel);
            this.Controls.Add(this._btn_ok);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this._cb_opcode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NavigationCommandEditor";
            this.Text = "Navigation command editor";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox _cb_opcode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button _btn_ok;
        private System.Windows.Forms.Button _btn_cancel;
    }
}