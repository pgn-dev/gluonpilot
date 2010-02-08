namespace Graph
{
    partial class GraphControl
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._nud_timewindow = new System.Windows.Forms.NumericUpDown();
            this._clb_variables = new System.Windows.Forms.CheckedListBox();
            this._zed_graph = new ZedGraph.ZedGraphControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._nud_timewindow)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this._nud_timewindow);
            this.splitContainer1.Panel1.Controls.Add(this._clb_variables);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._zed_graph);
            this.splitContainer1.Size = new System.Drawing.Size(640, 372);
            this.splitContainer1.SplitterDistance = 165;
            this.splitContainer1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time window:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "seconds";
            // 
            // _nud_timewindow
            // 
            this._nud_timewindow.Location = new System.Drawing.Point(18, 29);
            this._nud_timewindow.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._nud_timewindow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._nud_timewindow.Name = "_nud_timewindow";
            this._nud_timewindow.Size = new System.Drawing.Size(48, 20);
            this._nud_timewindow.TabIndex = 1;
            this._nud_timewindow.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._nud_timewindow.ValueChanged += new System.EventHandler(this._nud_timewindow_ValueChanged);
            // 
            // _clb_variables
            // 
            this._clb_variables.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._clb_variables.FormattingEnabled = true;
            this._clb_variables.Items.AddRange(new object[] {
            "Height (SCP1000)",
            "Pressure (SCP1000)",
            "Temperature (SCP1000)",
            "Raw acceleration X",
            "Raw acceleration Y",
            "Raw acceleration Z",
            "Raw gyro X",
            "Raw gyro Y",
            "Raw gyro Z",
            "Acceleration X",
            "Acceleration Y",
            "Acceleration Z",
            "Rotationrate X",
            "Rotationrate Y",
            "Rotationrate Z",
            "Roll",
            "Pitch",
            "Roll acc",
            "Pitch acc",
            "Yaw"});
            this._clb_variables.Location = new System.Drawing.Point(0, 68);
            this._clb_variables.Name = "_clb_variables";
            this._clb_variables.Size = new System.Drawing.Size(165, 304);
            this._clb_variables.TabIndex = 0;
            this._clb_variables.SelectedIndexChanged += new System.EventHandler(this._clb_variables_SelectedIndexChanged);
            // 
            // _zed_graph
            // 
            this._zed_graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this._zed_graph.EditButtons = System.Windows.Forms.MouseButtons.None;
            this._zed_graph.IsAntiAlias = true;
            this._zed_graph.Location = new System.Drawing.Point(0, 0);
            this._zed_graph.Name = "_zed_graph";
            this._zed_graph.ScrollGrace = 0;
            this._zed_graph.ScrollMaxX = 0;
            this._zed_graph.ScrollMaxY = 0;
            this._zed_graph.ScrollMaxY2 = 0;
            this._zed_graph.ScrollMinX = 0;
            this._zed_graph.ScrollMinY = 0;
            this._zed_graph.ScrollMinY2 = 0;
            this._zed_graph.Size = new System.Drawing.Size(471, 372);
            this._zed_graph.TabIndex = 0;
            // 
            // GraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "GraphControl";
            this.Size = new System.Drawing.Size(640, 372);
            this.Load += new System.EventHandler(this.Graph_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._nud_timewindow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ZedGraph.ZedGraphControl _zed_graph;
        private System.Windows.Forms.CheckedListBox _clb_variables;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _nud_timewindow;

    }
}
