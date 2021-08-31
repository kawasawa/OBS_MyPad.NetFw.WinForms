using AcroBat.WindowsAPI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AcroBat.Views.Controls
{
    /// <summary>
    /// ツールバーを表します。
    /// これは <see cref="ToolStrip"/> を拡張したコントロールです。
    /// </summary>
    public class ToolStripEx : ToolStrip
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
        public ToolStripEx()
        {
            ToolStripProfessionalRenderer renderer = new ToolStripProfessionalRenderer()
            {
                RoundedEdges = false,
            };
            this.Renderer = renderer;
            this.BackColor = SystemColors.Control;
            this.GripStyle = ToolStripGripStyle.Hidden;
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