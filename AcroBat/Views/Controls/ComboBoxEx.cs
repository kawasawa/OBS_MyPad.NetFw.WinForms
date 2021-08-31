using AcroBat.WindowsAPI;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AcroBat.Views.Controls
{
    /// <summary>
    /// コンボボックスを表します。
    /// これは <see cref="ComboBox"/> を拡張したコントロールです。
    /// </summary>
    /// <remarks>
    /// 既存の <see cref="ComboBox"/> には不可解な動作仕様が多いため、
    /// それらを吸収する処理を組み込んである。
    /// </remarks>
    public class ComboBoxEx : ComboBox
    {
        /// <summary>
        /// ドロップダウンが展開されている間に <see cref="ComboBox.SelectionChangeCommitted"/> イベントが発生したかどうかを示します。
        /// イベントが重複発生を抑止するために用います。
        /// </summary>
        private bool _selectionChangeCommitedRaisedWhileDroppedDown = false;

        /// <summary>
        /// ドロップダウンが展開された時に選択されていた項目を取得します。
        /// </summary>
        public object SelectedItemOnDorpDown { get; protected set; } = null;

        /// <summary>
        /// コントロールのサイズを自動調整するかどうかを示す値を取得します。
        /// 
        /// Windows Vista 以降の Windows OS において、
        /// このプロパティが true に設定されている状態では
        /// MaxDropDownItems の設定が適用されない現象が確認されている。
        /// このため、このプロパティは読み取り専用とし、常に false を指定する。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool IntegralHeight
        {
            get { return base.IntegralHeight; }
            private set { base.IntegralHeight = value; }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public ComboBoxEx()
        {
            this.IntegralHeight = false;
        }

        /// <summary>
        /// <see cref="ComboBox.DropDown"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnDropDown(EventArgs e)
        {
            this._selectionChangeCommitedRaisedWhileDroppedDown = false;
            this.SelectedItemOnDorpDown = this.SelectedItem;
            base.OnDropDown(e);
        }

        /// <summary>
        /// <see cref="ComboBox.DropDownClosed"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnDropDownClosed(EventArgs e)
        {
            // Win32.DropDownList コントロールには潜在的なバグがある。
            // ESCキーやTABキーの押下、▼ボタン、コントロール外のクリックなど
            // 特定の操作によりドロップダウンが閉じられた場合、
            // 選択項目が変更されていたとしても SelectionChangeCommitted イベントが発生しない。
            // このため、閉じる際に選択項目に変更があり、
            // イベントが未発生の状態であれば、明示的に発生させている。
            if (this.SelectedItem?.Equals(this.SelectedItemOnDorpDown) == false &&
                this._selectionChangeCommitedRaisedWhileDroppedDown == false)
            {
                this.OnSelectionChangeCommitted(e);
            }

            this._selectionChangeCommitedRaisedWhileDroppedDown = false;
            this.SelectedItemOnDorpDown = null;
            base.OnDropDownClosed(e);
        }

        /// <summary>
        /// <see cref="ComboBox.SelectionChangeCommitted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        public virtual void RaiseSelectionChangeCommitted(EventArgs e) => this.OnSelectionChangeCommitted(e);

        /// <summary>
        /// <see cref="ComboBox.SelectionChangeCommitted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnSelectionChangeCommitted(EventArgs e)
        {
            if (this.DroppedDown)
            {
                this._selectionChangeCommitedRaisedWhileDroppedDown = true;
            }
            base.OnSelectionChangeCommitted(e);
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
                case Keys.Control | Keys.X:
                    User32.SendMessage(msg.HWnd, WindowsMessages.WM_CUT, msg.WParam, msg.LParam);
                    return true;
                case Keys.Control | Keys.C:
                    User32.SendMessage(msg.HWnd, WindowsMessages.WM_COPY, msg.WParam, msg.LParam);
                    return true;
                case Keys.Control | Keys.V:
                    User32.SendMessage(msg.HWnd, WindowsMessages.WM_PASTE, msg.WParam, msg.LParam);
                    return true;
                case Keys.Control | Keys.Y:
                case Keys.Control | Keys.Z:
                    User32.SendMessage(msg.HWnd, WindowsMessages.WM_UNDO, msg.WParam, msg.LParam);
                    return true;
                case Keys.Control | Keys.A:
                    this.SelectAll();
                    return true;
                case Keys.Delete:
                    if (this.SelectionLength == 0)
                    {
                        this.SelectionLength = 1;
                    }
                    if (0 < this.SelectionLength)
                    {
                        this.SelectedText = string.Empty;
                    }
                    return true;
                case Keys.Escape:
                    if (this.DroppedDown == false)
                    {
                        break;
                    }
                    if (this.SelectedItem?.Equals(this.SelectedItemOnDorpDown) == false)
                    {
                        this.SelectedItem = this.SelectedItemOnDorpDown;
                    }
                    this.DroppedDown = false;
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
