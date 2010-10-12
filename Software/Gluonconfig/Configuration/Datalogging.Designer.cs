namespace Configuration
{
    partial class Datalogging
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Datalogging));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this._btn_readloggings = new System.Windows.Forms.Button();
            this._dgv_datalog = new System.Windows.Forms.DataGridView();
            this._lv_datalogtable = new System.Windows.Forms.ListView();
            this.columnHeaderIndex = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPage = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTime = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._btn_read = new System.Windows.Forms.ToolStripButton();
            this._btn_download = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._btn_open = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._btn_kml_classic = new System.Windows.Forms.ToolStripMenuItem();
            this._btn_kml_track = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._btn_format = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._pb = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgv_datalog)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this._btn_readloggings);
            this.toolStripContainer1.ContentPanel.Controls.Add(this._dgv_datalog);
            this.toolStripContainer1.ContentPanel.Controls.Add(this._lv_datalogtable);
            this.toolStripContainer1.ContentPanel.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(603, 268);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(603, 293);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // _btn_readloggings
            // 
            this._btn_readloggings.Image = ((System.Drawing.Image)(resources.GetObject("_btn_readloggings.Image")));
            this._btn_readloggings.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._btn_readloggings.Location = new System.Drawing.Point(218, 133);
            this._btn_readloggings.Name = "_btn_readloggings";
            this._btn_readloggings.Size = new System.Drawing.Size(50, 39);
            this._btn_readloggings.TabIndex = 10;
            this._btn_readloggings.Text = "Load";
            this._btn_readloggings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._btn_readloggings.UseVisualStyleBackColor = true;
            this._btn_readloggings.Click += new System.EventHandler(this._btn_readloggings_Click);
            // 
            // _dgv_datalog
            // 
            this._dgv_datalog.AllowUserToAddRows = false;
            this._dgv_datalog.AllowUserToDeleteRows = false;
            this._dgv_datalog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._dgv_datalog.Location = new System.Drawing.Point(274, 13);
            this._dgv_datalog.Name = "_dgv_datalog";
            this._dgv_datalog.ReadOnly = true;
            this._dgv_datalog.Size = new System.Drawing.Size(315, 247);
            this._dgv_datalog.TabIndex = 9;
            // 
            // _lv_datalogtable
            // 
            this._lv_datalogtable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._lv_datalogtable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderIndex,
            this.columnHeaderPage,
            this.columnHeaderDate,
            this.columnHeaderTime});
            this._lv_datalogtable.FullRowSelect = true;
            this._lv_datalogtable.GridLines = true;
            this._lv_datalogtable.HideSelection = false;
            this._lv_datalogtable.Location = new System.Drawing.Point(8, 13);
            this._lv_datalogtable.MultiSelect = false;
            this._lv_datalogtable.Name = "_lv_datalogtable";
            this._lv_datalogtable.ShowItemToolTips = true;
            this._lv_datalogtable.Size = new System.Drawing.Size(204, 247);
            this._lv_datalogtable.TabIndex = 3;
            this._lv_datalogtable.UseCompatibleStateImageBehavior = false;
            this._lv_datalogtable.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderIndex
            // 
            this.columnHeaderIndex.Text = "Index";
            this.columnHeaderIndex.Width = 22;
            // 
            // columnHeaderPage
            // 
            this.columnHeaderPage.Text = "Page start";
            this.columnHeaderPage.Width = 49;
            // 
            // columnHeaderDate
            // 
            this.columnHeaderDate.Text = "Date";
            this.columnHeaderDate.Width = 51;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Time";
            this.columnHeaderTime.Width = 68;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btn_read,
            this._btn_download,
            this.toolStripSeparator2,
            this._btn_open,
            this.toolStripDropDownButton1,
            this.toolStripSeparator3,
            this._btn_format,
            this.toolStripSeparator4,
            this._pb});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(465, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // _btn_read
            // 
            this._btn_read.Image = ((System.Drawing.Image)(resources.GetObject("_btn_read.Image")));
            this._btn_read.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_read.Name = "_btn_read";
            this._btn_read.Size = new System.Drawing.Size(81, 22);
            this._btn_read.Text = "Read index";
            this._btn_read.ToolTipText = "Read loggings table";
            this._btn_read.Click += new System.EventHandler(this._btn_read_Click);
            // 
            // _btn_download
            // 
            this._btn_download.Image = ((System.Drawing.Image)(resources.GetObject("_btn_download.Image")));
            this._btn_download.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_download.Name = "_btn_download";
            this._btn_download.Size = new System.Drawing.Size(74, 22);
            this._btn_download.Text = "Download";
            this._btn_download.ToolTipText = "Download selected logging";
            this._btn_download.Click += new System.EventHandler(this._btn_download_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _btn_open
            // 
            this._btn_open.Image = ((System.Drawing.Image)(resources.GetObject("_btn_open.Image")));
            this._btn_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_open.Name = "_btn_open";
            this._btn_open.Size = new System.Drawing.Size(53, 22);
            this._btn_open.Text = "Open";
            this._btn_open.Click += new System.EventHandler(this._btn_open_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xMLToolStripMenuItem,
            this.xMLToolStripMenuItem1});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripDropDownButton1.Text = "Save";
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btn_kml_classic,
            this._btn_kml_track});
            this.xMLToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("xMLToolStripMenuItem.Image")));
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.xMLToolStripMenuItem.Text = "KML";
            this.xMLToolStripMenuItem.Click += new System.EventHandler(this.xMLToolStripMenuItem_Click);
            // 
            // _btn_kml_classic
            // 
            this._btn_kml_classic.Image = ((System.Drawing.Image)(resources.GetObject("_btn_kml_classic.Image")));
            this._btn_kml_classic.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._btn_kml_classic.Name = "_btn_kml_classic";
            this._btn_kml_classic.Size = new System.Drawing.Size(221, 126);
            this._btn_kml_classic.Text = "Classic";
            this._btn_kml_classic.Click += new System.EventHandler(this._btn_kml_classic_Click);
            // 
            // _btn_kml_track
            // 
            this._btn_kml_track.Image = ((System.Drawing.Image)(resources.GetObject("_btn_kml_track.Image")));
            this._btn_kml_track.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._btn_kml_track.Name = "_btn_kml_track";
            this._btn_kml_track.Size = new System.Drawing.Size(221, 126);
            this._btn_kml_track.Text = "Track";
            this._btn_kml_track.Click += new System.EventHandler(this._btn_kml_track_Click);
            // 
            // xMLToolStripMenuItem1
            // 
            this.xMLToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("xMLToolStripMenuItem1.Image")));
            this.xMLToolStripMenuItem1.Name = "xMLToolStripMenuItem1";
            this.xMLToolStripMenuItem1.Size = new System.Drawing.Size(104, 22);
            this.xMLToolStripMenuItem1.Text = "XML";
            this.xMLToolStripMenuItem1.Click += new System.EventHandler(this.xMLToolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _btn_format
            // 
            this._btn_format.Image = ((System.Drawing.Image)(resources.GetObject("_btn_format.Image")));
            this._btn_format.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._btn_format.Name = "_btn_format";
            this._btn_format.Size = new System.Drawing.Size(61, 22);
            this._btn_format.Text = "Format";
            this._btn_format.ToolTipText = "Format flash used for datalogging";
            this._btn_format.Click += new System.EventHandler(this._btn_format_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // _pb
            // 
            this._pb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this._pb.AutoSize = false;
            this._pb.Margin = new System.Windows.Forms.Padding(1, 2, 5, 1);
            this._pb.Name = "_pb";
            this._pb.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this._pb.Padding = new System.Windows.Forms.Padding(5);
            this._pb.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._pb.Size = new System.Drawing.Size(100, 22);
            this._pb.Value = 100;
            // 
            // Datalogging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "Datalogging";
            this.Size = new System.Drawing.Size(603, 293);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgv_datalog)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView _lv_datalogtable;
        private System.Windows.Forms.ColumnHeader columnHeaderIndex;
        private System.Windows.Forms.ColumnHeader columnHeaderPage;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.DataGridView _dgv_datalog;
        private System.Windows.Forms.Button _btn_readloggings;
        private System.Windows.Forms.ToolStripButton _btn_read;
        private System.Windows.Forms.ToolStripButton _btn_download;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton _btn_open;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _btn_kml_classic;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton _btn_format;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripProgressBar _pb;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _btn_kml_track;
    }
}
