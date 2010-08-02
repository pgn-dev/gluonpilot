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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._lbl_x = new System.Windows.Forms.Label();
            this._tb_x = new System.Windows.Forms.TextBox();
            this._lbl_y = new System.Windows.Forms.Label();
            this._tb_y = new System.Windows.Forms.TextBox();
            this._lbl_a = new System.Windows.Forms.Label();
            this._tb_a = new System.Windows.Forms.TextBox();
            this._lbl_b = new System.Windows.Forms.Label();
            this._tb_b = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btn_ok
            // 
            this._btn_ok.Location = new System.Drawing.Point(205, 233);
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
            this._btn_cancel.Location = new System.Drawing.Point(12, 233);
            this._btn_cancel.Name = "_btn_cancel";
            this._btn_cancel.Size = new System.Drawing.Size(75, 23);
            this._btn_cancel.TabIndex = 1;
            this._btn_cancel.Text = "Cancel";
            this._btn_cancel.UseVisualStyleBackColor = true;
            this._btn_cancel.Click += new System.EventHandler(this._btn_cancel_Click);
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
            this._cb_opcode.Location = new System.Drawing.Point(12, 41);
            this._cb_opcode.Name = "_cb_opcode";
            this._cb_opcode.Size = new System.Drawing.Size(268, 21);
            this._cb_opcode.TabIndex = 2;
            this._cb_opcode.SelectedIndexChanged += new System.EventHandler(this._cb_opcode_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._lbl_x);
            this.flowLayoutPanel1.Controls.Add(this._tb_x);
            this.flowLayoutPanel1.Controls.Add(this._lbl_y);
            this.flowLayoutPanel1.Controls.Add(this._tb_y);
            this.flowLayoutPanel1.Controls.Add(this._lbl_a);
            this.flowLayoutPanel1.Controls.Add(this._tb_a);
            this.flowLayoutPanel1.Controls.Add(this._lbl_b);
            this.flowLayoutPanel1.Controls.Add(this._tb_b);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(43, 78);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 110);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // _lbl_x
            // 
            this._lbl_x.AutoEllipsis = true;
            this._lbl_x.AutoSize = true;
            this._lbl_x.Location = new System.Drawing.Point(3, 6);
            this._lbl_x.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this._lbl_x.MaximumSize = new System.Drawing.Size(60, 15);
            this._lbl_x.MinimumSize = new System.Drawing.Size(80, 15);
            this._lbl_x.Name = "_lbl_x";
            this._lbl_x.Size = new System.Drawing.Size(80, 15);
            this._lbl_x.TabIndex = 0;
            this._lbl_x.Text = "Lat";
            // 
            // _tb_x
            // 
            this._tb_x.Location = new System.Drawing.Point(89, 3);
            this._tb_x.Name = "_tb_x";
            this._tb_x.Size = new System.Drawing.Size(100, 20);
            this._tb_x.TabIndex = 1;
            // 
            // _lbl_y
            // 
            this._lbl_y.Location = new System.Drawing.Point(3, 32);
            this._lbl_y.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this._lbl_y.MaximumSize = new System.Drawing.Size(60, 15);
            this._lbl_y.MinimumSize = new System.Drawing.Size(80, 15);
            this._lbl_y.Name = "_lbl_y";
            this._lbl_y.Size = new System.Drawing.Size(80, 15);
            this._lbl_y.TabIndex = 2;
            this._lbl_y.Text = "Lon";
            // 
            // _tb_y
            // 
            this._tb_y.Location = new System.Drawing.Point(89, 29);
            this._tb_y.Name = "_tb_y";
            this._tb_y.Size = new System.Drawing.Size(100, 20);
            this._tb_y.TabIndex = 3;
            // 
            // _lbl_a
            // 
            this._lbl_a.Location = new System.Drawing.Point(3, 58);
            this._lbl_a.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this._lbl_a.MaximumSize = new System.Drawing.Size(60, 15);
            this._lbl_a.MinimumSize = new System.Drawing.Size(80, 15);
            this._lbl_a.Name = "_lbl_a";
            this._lbl_a.Size = new System.Drawing.Size(80, 15);
            this._lbl_a.TabIndex = 4;
            this._lbl_a.Text = "Radius";
            // 
            // _tb_a
            // 
            this._tb_a.Location = new System.Drawing.Point(89, 55);
            this._tb_a.Name = "_tb_a";
            this._tb_a.Size = new System.Drawing.Size(100, 20);
            this._tb_a.TabIndex = 5;
            // 
            // _lbl_b
            // 
            this._lbl_b.Location = new System.Drawing.Point(3, 84);
            this._lbl_b.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this._lbl_b.MaximumSize = new System.Drawing.Size(60, 15);
            this._lbl_b.MinimumSize = new System.Drawing.Size(80, 15);
            this._lbl_b.Name = "_lbl_b";
            this._lbl_b.Size = new System.Drawing.Size(80, 15);
            this._lbl_b.TabIndex = 6;
            this._lbl_b.Text = "Height";
            // 
            // _tb_b
            // 
            this._tb_b.Location = new System.Drawing.Point(89, 81);
            this._tb_b.Name = "_tb_b";
            this._tb_b.Size = new System.Drawing.Size(100, 20);
            this._tb_b.TabIndex = 7;
            // 
            // NavigationInstructionEdit
            // 
            this.AcceptButton = this._btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btn_cancel;
            this.ClientSize = new System.Drawing.Size(292, 268);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this._cb_opcode);
            this.Controls.Add(this._btn_cancel);
            this.Controls.Add(this._btn_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NavigationInstructionEdit";
            this.Text = "Edit navigation instruction";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btn_ok;
        private System.Windows.Forms.Button _btn_cancel;
        private System.Windows.Forms.ComboBox _cb_opcode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label _lbl_x;
        private System.Windows.Forms.TextBox _tb_x;
        private System.Windows.Forms.Label _lbl_y;
        private System.Windows.Forms.TextBox _tb_y;
        private System.Windows.Forms.Label _lbl_a;
        private System.Windows.Forms.TextBox _tb_a;
        private System.Windows.Forms.Label _lbl_b;
        private System.Windows.Forms.TextBox _tb_b;
    }
}