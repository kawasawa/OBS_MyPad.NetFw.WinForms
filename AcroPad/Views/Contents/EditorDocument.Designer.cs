namespace AcroPad.Views.Contents
{
    partial class EditorDocument
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
            this.cmdUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.msep_1 = new AcroBat.Views.Controls.ToolStripSeparatorEx();
            this.cmdCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTextEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdClose = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCloseAllButThis = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textEditor = new AcroPad.Views.Controls.TextEditor();
            this.BasePanel.SuspendLayout();
            this.cmsTextEditor.SuspendLayout();
            this.cmsTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // BasePanel
            // 
            this.BasePanel.Controls.Add(this.textEditor);
            // 
            // cmdUndo
            // 
            this.cmdUndo.Name = "cmdUndo";
            this.cmdUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.cmdUndo.Size = new System.Drawing.Size(183, 22);
            this.cmdUndo.Text = "元に戻す(&U)";
            // 
            // cmdRedo
            // 
            this.cmdRedo.Name = "cmdRedo";
            this.cmdRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.cmdRedo.Size = new System.Drawing.Size(183, 22);
            this.cmdRedo.Text = "やり直し(&R)";
            // 
            // msep_1
            // 
            this.msep_1.Name = "msep_1";
            this.msep_1.Size = new System.Drawing.Size(180, 6);
            // 
            // cmdCut
            // 
            this.cmdCut.Name = "cmdCut";
            this.cmdCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cmdCut.Size = new System.Drawing.Size(183, 22);
            this.cmdCut.Text = "切り取り(&T)";
            // 
            // cmdCopy
            // 
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.cmdCopy.Size = new System.Drawing.Size(183, 22);
            this.cmdCopy.Text = "コピー(&C)";
            // 
            // cmdPaste
            // 
            this.cmdPaste.Name = "cmdPaste";
            this.cmdPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.cmdPaste.Size = new System.Drawing.Size(183, 22);
            this.cmdPaste.Text = "貼り付け(&P)";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.cmdDelete.Size = new System.Drawing.Size(183, 22);
            this.cmdDelete.Text = "削除(&D)";
            // 
            // cmdSelectAll
            // 
            this.cmdSelectAll.Name = "cmdSelectAll";
            this.cmdSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.cmdSelectAll.Size = new System.Drawing.Size(183, 22);
            this.cmdSelectAll.Text = "すべて選択(&A)";
            // 
            // cmsTextEditor
            // 
            this.cmsTextEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdUndo,
            this.cmdRedo,
            this.msep_1,
            this.cmdCut,
            this.cmdCopy,
            this.cmdPaste,
            this.cmdDelete,
            this.cmdSelectAll});
            this.cmsTextEditor.Name = "contextMenu";
            this.cmsTextEditor.Size = new System.Drawing.Size(184, 164);
            // 
            // cmdClose
            // 
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.cmdClose.Size = new System.Drawing.Size(266, 22);
            this.cmdClose.Text = "閉じる(&C)";
            // 
            // cmdCloseAllButThis
            // 
            this.cmdCloseAllButThis.Name = "cmdCloseAllButThis";
            this.cmdCloseAllButThis.Size = new System.Drawing.Size(266, 22);
            this.cmdCloseAllButThis.Text = "アクティブなウィンドウ以外すべて閉じる(&A)";
            // 
            // cmdCloseAll
            // 
            this.cmdCloseAll.Name = "cmdCloseAll";
            this.cmdCloseAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F4)));
            this.cmdCloseAll.Size = new System.Drawing.Size(266, 22);
            this.cmdCloseAll.Text = "すべて閉じる(&L)";
            // 
            // cmsTabPage
            // 
            this.cmsTabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdClose,
            this.cmdCloseAllButThis,
            this.cmdCloseAll});
            this.cmsTabPage.Name = "contextMenuStrip1";
            this.cmsTabPage.Size = new System.Drawing.Size(267, 92);
            // 
            // textEditor
            // 
            this.textEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(240)))));
            this.textEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textEditor.CleanedLineBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(239)))), ((int)(((byte)(175)))));
            this.textEditor.CommentCharColor = System.Drawing.Color.Green;
            this.textEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textEditor.DirtyLineBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(241)))), ((int)(((byte)(15)))));
            this.textEditor.DocCommentCharColor = System.Drawing.Color.Gray;
            this.textEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditor.EofColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(169)))), ((int)(((byte)(214)))));
            this.textEditor.EolColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(169)))), ((int)(((byte)(214)))));
            this.textEditor.FirstVisibleLine = 0;
            this.textEditor.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.textEditor.ForeColor = System.Drawing.Color.Black;
            this.textEditor.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(98)))), ((int)(((byte)(87)))));
            this.textEditor.Keyword2CharColor = System.Drawing.Color.Maroon;
            this.textEditor.Keyword3CharColor = System.Drawing.Color.Navy;
            this.textEditor.KeywordCharColor = System.Drawing.Color.Blue;
            this.textEditor.LineNumberBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.textEditor.LineNumberForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.textEditor.Location = new System.Drawing.Point(0, 0);
            this.textEditor.MatchedBracketBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textEditor.MatchedBracketForeColor = System.Drawing.Color.Transparent;
            this.textEditor.Name = "textEditor";
            this.textEditor.NumeberCharColor = System.Drawing.Color.Black;
            this.textEditor.RightEdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(222)))), ((int)(((byte)(211)))));
            this.textEditor.ScrollPos = new System.Drawing.Point(0, 0);
            this.textEditor.SearchPatternsBackColor = System.Drawing.Color.Empty;
            this.textEditor.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(98)))), ((int)(((byte)(87)))));
            this.textEditor.SelectionForeColor = System.Drawing.Color.White;
            this.textEditor.Size = new System.Drawing.Size(284, 261);
            this.textEditor.StringCharColor = System.Drawing.Color.Teal;
            this.textEditor.TabIndex = 1;
            this.textEditor.WhiteSpaceColor = System.Drawing.Color.Silver;
            // 
            // EditorDocument
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "EditorDocument";
            this.Controls.SetChildIndex(this.BasePanel, 0);
            this.BasePanel.ResumeLayout(false);
            this.cmsTextEditor.ResumeLayout(false);
            this.cmsTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TextEditor textEditor;
        private System.Windows.Forms.ToolStripMenuItem cmdUndo;
        private System.Windows.Forms.ToolStripMenuItem cmdRedo;
        private AcroBat.Views.Controls.ToolStripSeparatorEx msep_1;
        private System.Windows.Forms.ToolStripMenuItem cmdCut;
        private System.Windows.Forms.ToolStripMenuItem cmdCopy;
        private System.Windows.Forms.ToolStripMenuItem cmdPaste;
        private System.Windows.Forms.ToolStripMenuItem cmdDelete;
        private System.Windows.Forms.ToolStripMenuItem cmdSelectAll;
        private System.Windows.Forms.ContextMenuStrip cmsTextEditor;
        private System.Windows.Forms.ToolStripMenuItem cmdClose;
        private System.Windows.Forms.ToolStripMenuItem cmdCloseAllButThis;
        private System.Windows.Forms.ToolStripMenuItem cmdCloseAll;
        private System.Windows.Forms.ContextMenuStrip cmsTabPage;
    }
}