namespace AcroPad.Views.Contents
{
    partial class ExplorerContent
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
            this.explorerTreeView = new AcroBat.Views.Controls.ExplorerTreeView();
            this.cmsExplorer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdOpenIn_Document = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdOpenIn_Explorer = new System.Windows.Forms.ToolStripMenuItem();
            this.sep_1 = new AcroBat.Views.Controls.ToolStripSeparatorEx();
            this.cmdPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdRename = new System.Windows.Forms.ToolStripMenuItem();
            this.BasePanel.SuspendLayout();
            this.cmsExplorer.SuspendLayout();
            this.SuspendLayout();
            // 
            // BasePanel
            // 
            this.BasePanel.Controls.Add(this.explorerTreeView);
            this.BasePanel.Size = new System.Drawing.Size(234, 261);
            // 
            // explorerTreeView
            // 
            this.explorerTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.explorerTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.explorerTreeView.Location = new System.Drawing.Point(0, 0);
            this.explorerTreeView.Name = "explorerTreeView";
            this.explorerTreeView.Size = new System.Drawing.Size(234, 261);
            this.explorerTreeView.TabIndex = 2;
            // 
            // cmsExplorer
            // 
            this.cmsExplorer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdOpenIn_Document,
            this.cmdOpenIn_Explorer,
            this.sep_1,
            this.cmdCopy,
            this.cmdPaste,
            this.cmdDelete,
            this.cmdRename});
            this.cmsExplorer.Name = "cnmExplorer";
            this.cmsExplorer.Size = new System.Drawing.Size(239, 164);
            // 
            // cmdOpenIn_Document
            // 
            this.cmdOpenIn_Document.Name = "cmdOpenIn_Document";
            this.cmdOpenIn_Document.Size = new System.Drawing.Size(238, 22);
            this.cmdOpenIn_Document.Text = "開く(&O)";
            // 
            // cmdOpenIn_Explorer
            // 
            this.cmdOpenIn_Explorer.Name = "cmdOpenIn_Explorer";
            this.cmdOpenIn_Explorer.Size = new System.Drawing.Size(238, 22);
            this.cmdOpenIn_Explorer.Text = "エクスプローラーでフォルダーを開く(&X)";
            // 
            // sep_1
            // 
            this.sep_1.Name = "sep_1";
            this.sep_1.Size = new System.Drawing.Size(235, 6);
            // 
            // cmdPaste
            // 
            this.cmdPaste.Name = "cmdPaste";
            this.cmdPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.cmdPaste.Size = new System.Drawing.Size(238, 22);
            this.cmdPaste.Text = "貼り付け(&P)";
            // 
            // cmdCopy
            // 
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.cmdCopy.Size = new System.Drawing.Size(238, 22);
            this.cmdCopy.Text = "コピー(&C)";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.cmdDelete.Size = new System.Drawing.Size(238, 22);
            this.cmdDelete.Text = "削除(&D)";
            // 
            // cmdRename
            // 
            this.cmdRename.Name = "cmdRename";
            this.cmdRename.Size = new System.Drawing.Size(238, 22);
            this.cmdRename.Text = "名前の変更(&M)";
            // 
            // ExplorerContent
            // 
            this.ClientSize = new System.Drawing.Size(234, 261);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "ExplorerContent";
            this.TabText = "Explorer";
            this.Text = "ExplorerContent";
            this.Controls.SetChildIndex(this.BasePanel, 0);
            this.BasePanel.ResumeLayout(false);
            this.cmsExplorer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AcroBat.Views.Controls.ExplorerTreeView explorerTreeView;
        private System.Windows.Forms.ContextMenuStrip cmsExplorer;
        private System.Windows.Forms.ToolStripMenuItem cmdOpenIn_Document;
        private System.Windows.Forms.ToolStripMenuItem cmdOpenIn_Explorer;
        private AcroBat.Views.Controls.ToolStripSeparatorEx sep_1;
        private System.Windows.Forms.ToolStripMenuItem cmdPaste;
        private System.Windows.Forms.ToolStripMenuItem cmdCopy;
        private System.Windows.Forms.ToolStripMenuItem cmdDelete;
        private System.Windows.Forms.ToolStripMenuItem cmdRename;
    }
}