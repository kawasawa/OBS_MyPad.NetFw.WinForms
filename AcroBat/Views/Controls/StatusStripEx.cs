using AcroBat.WindowsAPI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AcroBat.Views.Controls
{
    /// <summary>
    /// ステータスバーを表します。
    /// これは <see cref="StatusStrip"/> を拡張したコントロールです。
    /// </summary>
    public class StatusStripEx : StatusStrip
    {
        /// <summary>
        /// 前景色を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// 背景色を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public StatusStripEx()
        {
            this.ForeColor = Color.White;
            this.BackColor = AccentColors.Blue;
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
    }
}
