using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AcroBat.Views.Controls
{
    /// <summary>
    /// ツールセパレーターを表します。
    /// これは <see cref="ToolStripSeparator"/> を拡張したコントロールです。
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
    public class ToolStripSeparatorEx : ToolStripSeparator
    {
        /// <summary>
        /// 活性状態かどうかを示す値を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool Enabled
        {
            get { return base.Enabled; }
            private set { base.Enabled = value; }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public ToolStripSeparatorEx()
        {
            this.Enabled = false;
        }
    }
}
