using AcroBat.WindowsAPI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AcroBat.Views.Controls
{
    /// <summary>
    /// メニューバーを表します。
    /// これは <see cref="MenuStrip"/> を拡張したコントロールです。
    /// </summary>
    public class MenuStripEx : MenuStrip, IControlProcess
    {
        /// <summary>
        /// レンダラモードを取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ToolStripRenderMode RenderMode
        {
            get { return base.RenderMode; }
            private set { base.RenderMode = value; }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public MenuStripEx()
        {
            this.BackColor = SystemColors.Control;
            this.CanOverflow = true;
            this.ItemAdded += this.MenuStripEx_ItemAdded;
        }

        private void MenuStripEx_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            e.Item.Overflow = ToolStripItemOverflow.AsNeeded;
        }

        /// <summary>
        /// Windows メッセージを処理します。
        /// </summary>
        /// <param name="m">メッセージ</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WindowsMessages.WM_MOUSEACTIVATE:
                    m.Result = new IntPtr(WindowsMessages.WM_CREATE);
                    return;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// <see cref="MenuStrip.ProcessCmdKey"/> を呼び出します。
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        public virtual bool RaiseProcessCmdKey(ref Message msg, Keys keyData) => this.ProcessCmdKey(ref msg, keyData);
    }
}
