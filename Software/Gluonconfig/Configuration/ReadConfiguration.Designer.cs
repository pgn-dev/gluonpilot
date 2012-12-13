namespace Configuration
{
    partial class ReadConfiguration
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
            this._pb = new System.Windows.Forms.ProgressBar();
            this._btnCancel = new System.Windows.Forms.Button();
            this._lblProgress = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _pb
            // 
            this._pb.Location = new System.Drawing.Point(28, 66);
            this._pb.Name = "_pb";
            this._pb.Size = new System.Drawing.Size(226, 17);
            this._pb.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this._pb.TabIndex = 0;
            // 
            // _btnCancel
            // 
            this._btnCancel.Location = new System.Drawing.Point(105, 95);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 1;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
            // 
            // _lblProgress
            // 
            this._lblProgress.AutoSize = true;
            this._lblProgress.Location = new System.Drawing.Point(65, 30);
            this._lblProgress.Name = "_lblProgress";
            this._lblProgress.Size = new System.Drawing.Size(164, 13);
            this._lblProgress.TabIndex = 2;
            this._lblProgress.Text = "Reading configuration (attempt 1)";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // ReadConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 133);
            this.ControlBox = false;
            this.Controls.Add(this._lblProgress);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._pb);
            this.Name = "ReadConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this.Shown += new System.EventHandler(this.ReadConfiguration_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar _pb;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Label _lblProgress;
        private System.Windows.Forms.Timer timer;
    }
}