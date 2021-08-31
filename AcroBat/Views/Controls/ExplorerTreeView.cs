using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace AcroBat.Views.Controls
{
    /// <summary>
    /// エクスプローラーツリービューを表します。
    /// </summary>
    public class ExplorerTreeView : TreeView
    {
        private string _rootDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <summary>
        /// ルートノードのディレクトリパスを取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RootDirectoryPath
        {
            get { return this._rootDirectoryPath; }
            set
            {
                if (this._rootDirectoryPath.Equals(value) == false)
                {
                    this._rootDirectoryPath = value;
                    this.UpdateView();
                    this.OnRootDirectoryPathChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// ルートノードが選択されたときに発生します。
        /// </summary>
        [Category(DesignerCategory.PROPERTY_CHANGED)]
        [Description("RootDirectoryPath のプロパティが変更されたときに発生します。")]
        public event EventHandler RootDirectoryPathChanged;

        /// <summary>
        /// 末尾のノードが選択されたときに発生します。
        /// </summary>
        [Category(DesignerCategory.BEHAVIOR)]
        [Description("末尾のノードが選択されたときに発生します。")]
        public event EventHandler<TreeNodeEventArgs> LastNodeSelected;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExplorerTreeView()
        {
        }

        /// <summary>
        /// ツリーの状態を更新します。
        /// </summary>
        public void UpdateView()
        {
            this.Nodes.Clear();
            TreeNode rootNode = this.Nodes.Add(this.RootDirectoryPath, Path.GetFileName(this.RootDirectoryPath));
            this.CreateChildNodes(rootNode);
            rootNode.Expand();
        }

        /// <summary>
        /// 指定したノードに対して子ノードを構築します。
        /// </summary>
        /// <param name="root">ルートとなるノード</param>
        protected void CreateChildNodes(TreeNode root) => this.CreateChildNodes(root, false);

        /// <summary>
        /// 指定したノードに対して子ノードを構築します。
        /// </summary>
        /// <param name="root">ルートとなるノード</param>
        /// <param name="isRecursiveCall">再帰呼び出しであるかどうかを示す値</param>
        private void CreateChildNodes(TreeNode root, bool isRecursiveCall)
        {
            if (Directory.Exists(root.Name) == false)
            {
                return;
            }

            try
            {
                foreach (string path in Directory.GetFileSystemEntries(root.Name))
                {
                    FileAttributes attribute = File.GetAttributes(path);
                    if ((attribute & FileAttributes.System) == FileAttributes.System ||
                        (attribute & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue;
                    }

                    if (root.Nodes.ContainsKey(path) == false)
                    {
                        root.Nodes.Add(path, Path.GetFileName(path));
                    }

                    if (isRecursiveCall)
                    {
                        continue;
                    }
                    this.CreateChildNodes(root.Nodes[path], true);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }

            if (isRecursiveCall == false)
            {
                this.SelectedNode = root;
            }
        }

        /// <summary>
        /// <see cref="Control.MouseDown"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.SelectedNode = this.GetNodeAt(e.X, e.Y);
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// <see cref="TreeView.NodeMouseDoubleClick"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (File.Exists(this.SelectedNode?.Name))
            {
                this.OnLastNodeSelected(new TreeNodeEventArgs(this.SelectedNode));
            }
            base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// <see cref="TreeView.BeforeExpand"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            if (e.Node != null)
            {
                this.CreateChildNodes(e.Node);
            }
            base.OnBeforeExpand(e);
        }

        /// <summary>
        /// <see cref="RootDirectoryPathChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnRootDirectoryPathChanged(EventArgs e)
        {
            this.RootDirectoryPathChanged?.Invoke(this, e);
        }

        /// <summary>
        /// <see cref="LastNodeSelected"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnLastNodeSelected(TreeNodeEventArgs e)
        {
            this.LastNodeSelected?.Invoke(this, e);
        }

        /// <summary>
        /// コマンドキーを処理します。
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    {
                        if (this.SelectedNode == null)
                        {
                            break;
                        }

                        if (File.Exists(this.SelectedNode.Name))
                        {
                            this.OnLastNodeSelected(new TreeNodeEventArgs(this.SelectedNode));
                        }
                        else if (0 < this.SelectedNode.Nodes.Count)
                        {
                            if (this.SelectedNode.IsExpanded)
                            {
                                this.SelectedNode.Collapse();
                            }
                            else
                            {
                                this.SelectedNode.Expand();
                            }
                        }
                        else
                        {
                            break;
                        }
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
