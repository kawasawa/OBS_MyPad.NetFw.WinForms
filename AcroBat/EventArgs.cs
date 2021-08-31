using System;
using System.Windows.Forms;

namespace AcroBat
{
    /// <summary>
    /// ツリーノードのイベント情報を表します。
    /// </summary>
    public class TreeNodeEventArgs : EventArgs
    {
        /// <summary>
        /// ノードを取得します。
        /// </summary>
        public TreeNode Node { get; protected set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="node">ノード</param>
        public TreeNodeEventArgs(TreeNode node)
        {
            this.Node = node;
        }
    }
}
