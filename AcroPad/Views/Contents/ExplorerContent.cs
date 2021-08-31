using AcroBat;
using AcroBat.Views.Controls;
using AcroPad.Views.Forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AcroPad.Views.Contents
{
    /// <summary>
    /// エクスプローラーコンテンツを表します。
    /// </summary>
    public partial class ExplorerContent : ToolContentBase
    {
        /// <summary>
        /// メインフォームを取得します。
        /// </summary>
        private MainForm MainForm => this.DockPanel.FindForm() as MainForm;

        /// <summary>
        /// エクスプローラーツリービューを取得します。
        /// </summary>
        public ExplorerTreeView ExplorerTreeView => this.explorerTreeView;

        /// <summary>
        /// ルートノードのディレクトリパスを取得または設定します。
        /// </summary>
        public string RootDirectoryPath
        {
            get { return this.ExplorerTreeView.RootDirectoryPath; }
            set { this.ExplorerTreeView.RootDirectoryPath = value; }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public ExplorerContent()
        {
            this.InitializeComponent();
            this.InitializeItems();
            this.AddEventHandler();
        }

        /// <summary>
        /// 特定のコントロールを初期化します。
        /// </summary>
        private void InitializeItems()
        {
            this.SetFormIcon();
            this.SetItemImage();
            this.ExplorerTreeView.HideSelection = false;
            this.ExplorerTreeView.ContextMenuStrip = this.cmsExplorer;
            this.ExplorerTreeView.UpdateView();
            this.DockAreas = DockAreas.DockLeft |
                             DockAreas.DockRight |
                             DockAreas.Float;
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEventHandler()
        {
            this.ExplorerTreeView.LastNodeSelected += this.ExplorerTreeView_LastNodeSelected;
            this.cmsExplorer.Opening += this.ContextMenu_Opening;
            this.cmdOpenIn_Document.Click += this.OpenInDocumentCommand_Executed;
            this.cmdOpenIn_Explorer.Click += this.OpenInExplorerCommand_Executed;
            this.cmdCopy.Click += this.CopyCommand_Executed;
            this.cmdPaste.Click += this.PasteCommand_Executed;
            this.cmdDelete.Click += this.DeleteCommand_Executed;
            this.cmdRename.Click += this.RenameCommand_Executed;
        }

        private void ExplorerTreeView_LastNodeSelected(object sender, TreeNodeEventArgs e)
        {
            this.MainForm.InvokeAddDocument(e.Node.Name, false);
        }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            TreeNode node = this.explorerTreeView.SelectedNode;
            if (node == null)
            {
                this.cmdOpenIn_Document.Visible = false;
                this.cmdOpenIn_Explorer.Visible = false;
                this.sep_1.Visible = false;
                this.cmdCopy.Visible = false;
                this.cmdPaste.Visible = true;
                this.cmdDelete.Visible = false;
                this.cmdRename.Visible = false;
            }
            else
            {
                this.cmdOpenIn_Document.Visible = !Directory.Exists(this.ExplorerTreeView.SelectedNode.Name);
                this.cmdOpenIn_Explorer.Visible = true;
                this.sep_1.Visible = true;
                this.cmdCopy.Visible = true;
                this.cmdPaste.Visible = true;
                this.cmdDelete.Visible = true;
                this.cmdRename.Visible = true;
            }
        }

        private void OpenInDocumentCommand_Executed(object sender, EventArgs e)
        {
            this.MainForm.InvokeAddDocument(this.ExplorerTreeView.SelectedNode.Name, false);
        }

        private void OpenInExplorerCommand_Executed(object sender, EventArgs e)
        {
            string path = this.ExplorerTreeView.SelectedNode.Name;
            if (Directory.Exists(path))
            {
                Process.Start("Explorer.exe", $@"/e,/root,""{path}""");
            }
            else
            {
                Process.Start("Explorer.exe", $@"/select,""{path}""");
            }
        }

        private void PasteCommand_Executed(object sender, EventArgs e)
        {
        }

        private void CopyCommand_Executed(object sender, EventArgs e)
        {
            FileEx.ClipboardCopy(new[] { this.ExplorerTreeView.SelectedNode.Name });
        }

        private void DeleteCommand_Executed(object sender, EventArgs e)
        {
            FileEx.Delete(this.ExplorerTreeView.SelectedNode.Name);
        }

        private void RenameCommand_Executed(object sender, EventArgs e)
        {
        }
    }
}