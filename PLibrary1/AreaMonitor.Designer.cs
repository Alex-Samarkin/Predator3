namespace PLibrary1
{
    partial class AreaMonitor
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            splitContainer1 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            plotView1 = new OxyPlot.WindowsForms.PlotView();
            plotView2 = new OxyPlot.WindowsForms.PlotView();
            plotView3 = new OxyPlot.WindowsForms.PlotView();
            plotView4 = new OxyPlot.WindowsForms.PlotView();
            plotView5 = new OxyPlot.WindowsForms.PlotView();
            plotView6 = new OxyPlot.WindowsForms.PlotView();
            plotView7 = new OxyPlot.WindowsForms.PlotView();
            plotView8 = new OxyPlot.WindowsForms.PlotView();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1739, 869);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1731, 841);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "SEIRDV";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(plotView8);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1731, 841);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "AgentsXY";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(plotView7);
            splitContainer1.Size = new Size(1725, 835);
            splitContainer1.SplitterDistance = 1246;
            splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(plotView1, 0, 0);
            tableLayoutPanel1.Controls.Add(plotView2, 1, 0);
            tableLayoutPanel1.Controls.Add(plotView3, 2, 0);
            tableLayoutPanel1.Controls.Add(plotView4, 0, 1);
            tableLayoutPanel1.Controls.Add(plotView5, 1, 1);
            tableLayoutPanel1.Controls.Add(plotView6, 2, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1246, 835);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // plotView1
            // 
            plotView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plotView1.Location = new Point(3, 3);
            plotView1.Name = "plotView1";
            plotView1.PanCursor = Cursors.Hand;
            plotView1.Size = new Size(409, 411);
            plotView1.TabIndex = 0;
            plotView1.Text = "plotViewS";
            plotView1.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView1.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView1.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // plotView2
            // 
            plotView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plotView2.Location = new Point(418, 3);
            plotView2.Name = "plotView2";
            plotView2.PanCursor = Cursors.Hand;
            plotView2.Size = new Size(409, 411);
            plotView2.TabIndex = 1;
            plotView2.Text = "plotViewE";
            plotView2.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView2.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView2.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // plotView3
            // 
            plotView3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plotView3.Location = new Point(833, 3);
            plotView3.Name = "plotView3";
            plotView3.PanCursor = Cursors.Hand;
            plotView3.Size = new Size(410, 411);
            plotView3.TabIndex = 2;
            plotView3.Text = "plotViewI";
            plotView3.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView3.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView3.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // plotView4
            // 
            plotView4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plotView4.Location = new Point(3, 420);
            plotView4.Name = "plotView4";
            plotView4.PanCursor = Cursors.Hand;
            plotView4.Size = new Size(409, 412);
            plotView4.TabIndex = 3;
            plotView4.Text = "plotViewR";
            plotView4.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView4.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView4.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // plotView5
            // 
            plotView5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plotView5.Location = new Point(418, 420);
            plotView5.Name = "plotView5";
            plotView5.PanCursor = Cursors.Hand;
            plotView5.Size = new Size(409, 412);
            plotView5.TabIndex = 4;
            plotView5.Text = "plotViewD";
            plotView5.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView5.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView5.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // plotView6
            // 
            plotView6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plotView6.Location = new Point(833, 420);
            plotView6.Name = "plotView6";
            plotView6.PanCursor = Cursors.Hand;
            plotView6.Size = new Size(410, 412);
            plotView6.TabIndex = 5;
            plotView6.Text = "plotViewV";
            plotView6.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView6.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView6.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // plotView7
            // 
            plotView7.Dock = DockStyle.Fill;
            plotView7.Location = new Point(0, 0);
            plotView7.Name = "plotView7";
            plotView7.PanCursor = Cursors.Hand;
            plotView7.Size = new Size(475, 835);
            plotView7.TabIndex = 0;
            plotView7.Text = "plotViewSEIRDV";
            plotView7.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView7.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView7.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // plotView8
            // 
            plotView8.Dock = DockStyle.Fill;
            plotView8.Location = new Point(3, 3);
            plotView8.Name = "plotView8";
            plotView8.PanCursor = Cursors.Hand;
            plotView8.Size = new Size(1725, 835);
            plotView8.TabIndex = 0;
            plotView8.Text = "plotViewA";
            plotView8.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView8.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView8.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // AreaMonitor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1739, 869);
            Controls.Add(tabControl1);
            Name = "AreaMonitor";
            Text = "AreaMonitor";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private SplitContainer splitContainer1;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel1;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private OxyPlot.WindowsForms.PlotView plotView2;
        private OxyPlot.WindowsForms.PlotView plotView3;
        private OxyPlot.WindowsForms.PlotView plotView4;
        private OxyPlot.WindowsForms.PlotView plotView5;
        private OxyPlot.WindowsForms.PlotView plotView6;
        private OxyPlot.WindowsForms.PlotView plotView7;
        private OxyPlot.WindowsForms.PlotView plotView8;
    }
}