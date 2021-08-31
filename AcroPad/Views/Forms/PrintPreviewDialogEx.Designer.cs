namespace AcroPad.Views.Forms
{
    partial class PrintPreviewDialogEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPreviewDialogEx));
            this._statusBar = new AcroBat.Views.Controls.StatusStripEx();
            this._menuBar = new AcroBat.Views.Controls.MenuStripEx();
            this.mihFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mcmdPrintOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorEx1 = new AcroBat.Views.Controls.ToolStripSeparatorEx();
            this.mcmdClose = new System.Windows.Forms.ToolStripMenuItem();
            this._toolBar = new AcroBat.Views.Controls.ToolStripEx();
            this.tcmdPrintOut = new System.Windows.Forms.ToolStripButton();
            this._printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this._menuBar.SuspendLayout();
            this._toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _statusBar
            // 
            this._statusBar.Location = new System.Drawing.Point(0, 439);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Size = new System.Drawing.Size(634, 22);
            this._statusBar.TabIndex = 4;
            this._statusBar.Text = "statusStripEx1";
            // 
            // _menuBar
            // 
            this._menuBar.BackColor = System.Drawing.SystemColors.Control;
            this._menuBar.CanOverflow = true;
            this._menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mihFile});
            this._menuBar.Location = new System.Drawing.Point(0, 0);
            this._menuBar.Name = "_menuBar";
            this._menuBar.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this._menuBar.Size = new System.Drawing.Size(634, 24);
            this._menuBar.TabIndex = 1;
            this._menuBar.Text = "menuStripEx1";
            // 
            // mihFile
            // 
            this.mihFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mcmdPrintOut,
            this.toolStripSeparatorEx1,
            this.mcmdClose});
            this.mihFile.Name = "mihFile";
            this.mihFile.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
            this.mihFile.Size = new System.Drawing.Size(66, 23);
            this.mihFile.Text = "ファイル(&F)";
            // 
            // mcmdPrintOut
            // 
            this.mcmdPrintOut.Name = "mcmdPrintOut";
            this.mcmdPrintOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mcmdPrintOut.Size = new System.Drawing.Size(162, 22);
            this.mcmdPrintOut.Text = "印刷(&P)...";
            // 
            // toolStripSeparatorEx1
            // 
            this.toolStripSeparatorEx1.Name = "toolStripSeparatorEx1";
            this.toolStripSeparatorEx1.Size = new System.Drawing.Size(159, 6);
            // 
            // mcmdClose
            // 
            this.mcmdClose.Name = "mcmdClose";
            this.mcmdClose.ShortcutKeyDisplayString = "ESC";
            this.mcmdClose.Size = new System.Drawing.Size(162, 22);
            this.mcmdClose.Text = "閉じる(&C)";
            // 
            // _toolBar
            // 
            this._toolBar.BackColor = System.Drawing.SystemColors.Control;
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tcmdPrintOut});
            this._toolBar.Location = new System.Drawing.Point(0, 24);
            this._toolBar.Name = "_toolBar";
            this._toolBar.Size = new System.Drawing.Size(634, 25);
            this._toolBar.TabIndex = 2;
            this._toolBar.Text = "toolStripEx1";
            // 
            // tcmdPrintOut
            // 
            this.tcmdPrintOut.Image = ((System.Drawing.Image)(resources.GetObject("tcmdPrintOut.Image")));
            this.tcmdPrintOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tcmdPrintOut.Name = "tcmdPrintOut";
            this.tcmdPrintOut.Size = new System.Drawing.Size(51, 22);
            this.tcmdPrintOut.Text = "印刷";
            // 
            // _printPreviewControl
            // 
            this._printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._printPreviewControl.Location = new System.Drawing.Point(0, 49);
            this._printPreviewControl.Name = "_printPreviewControl";
            this._printPreviewControl.Size = new System.Drawing.Size(634, 390);
            this._printPreviewControl.TabIndex = 3;
            // 
            // PrintPreviewDialogEx
            // 
            this.ClientSize = new System.Drawing.Size(634, 461);
            this.Controls.Add(this._printPreviewControl);
            this.Controls.Add(this._toolBar);
            this.Controls.Add(this._menuBar);
            this.Controls.Add(this._statusBar);
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "PrintPreviewDialogEx";
            this.Text = "印刷プレビュー";
            this._menuBar.ResumeLayout(false);
            this._menuBar.PerformLayout();
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AcroBat.Views.Controls.StatusStripEx _statusBar;
        private AcroBat.Views.Controls.MenuStripEx _menuBar;
        private System.Windows.Forms.ToolStripMenuItem mihFile;
        private System.Windows.Forms.ToolStripMenuItem mcmdPrintOut;
        private AcroBat.Views.Controls.ToolStripSeparatorEx toolStripSeparatorEx1;
        private System.Windows.Forms.ToolStripMenuItem mcmdClose;
        private AcroBat.Views.Controls.ToolStripEx _toolBar;
        private System.Windows.Forms.ToolStripButton tcmdPrintOut;
        private System.Windows.Forms.PrintPreviewControl _printPreviewControl;
    }
}