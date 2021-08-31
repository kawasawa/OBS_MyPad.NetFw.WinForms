using AcroBat.Views.Forms.Core;
using System.ComponentModel;
using System.Windows.Forms;

namespace AcroBat.Views.Forms
{
    /// <summary>
    /// ダイアログボックスの基底クラスを表します。
    /// </summary>
    public class DialogBase : FormCore
    {
        /// <summary>
        /// フォームを Windows タスク バーに表示するかどうかを示す値を取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ShowInTaskbar
        {
            get { return base.ShowInTaskbar; }
            private set { base.ShowInTaskbar = value; }
        }

        /// <summary>
        /// フォームのキャプション バーにアイコンを表示するかどうかを示す値を取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ShowIcon
        {
            get { return base.ShowIcon; }
            private set { base.ShowIcon = value; }
        }

        /// <summary>
        /// フォームの開始位置を取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormStartPosition StartPosition
        {
            get { return base.StartPosition; }
            private set { base.StartPosition = value; }
        }

        /// <summary>
        /// フォームのウィンドウ状態を取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormWindowState WindowState
        {
            get { return base.WindowState; }
            private set { base.WindowState = value; }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public DialogBase()
        {
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Normal;
        }
    }
}
