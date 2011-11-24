namespace Configuration.NavigationCommands
{
    partial class FromTo
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._cbRelToHome = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 17);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(207, 113);
            this.tableLayoutPanel.TabIndex = 5;
            // 
            // _cbRelToHome
            // 
            this._cbRelToHome.AutoSize = true;
            this._cbRelToHome.Dock = System.Windows.Forms.DockStyle.Top;
            this._cbRelToHome.Location = new System.Drawing.Point(0, 0);
            this._cbRelToHome.Name = "_cbRelToHome";
            this._cbRelToHome.Size = new System.Drawing.Size(207, 17);
            this._cbRelToHome.TabIndex = 4;
            this._cbRelToHome.Text = "Relative to home position";
            this._cbRelToHome.UseVisualStyleBackColor = true;
            this._cbRelToHome.CheckedChanged += new System.EventHandler(this._cbRelToHome_CheckedChanged);
            // 
            // FromTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this._cbRelToHome);
            this.Name = "FromTo";
            this.Size = new System.Drawing.Size(207, 130);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.CheckBox _cbRelToHome;
    }
}
