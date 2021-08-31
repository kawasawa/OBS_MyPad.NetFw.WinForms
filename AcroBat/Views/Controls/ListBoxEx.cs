using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AcroBat.Views.Controls
{
    /// <summary>
    /// リストボックスを表します。
    /// これは <see cref="ListBox"/> を拡張したコントロールです。
    /// </summary>
    public class ListBoxEx : ListBox
    {
        /// <summary>
        /// 描画の方法を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new DrawMode DrawMode
        {
            get { return base.DrawMode; }
            private set { base.DrawMode = value; }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public ListBoxEx()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// <see cref="ListBox.DrawItem"/> を発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            try
            {
                if (e.Index < 0 ||
                    this.Items.Count <= e.Index)
                {
                    return;
                }
                
                if (this.Focused == false &&
                    (e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
                    e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, SystemBrushes.WindowText, e.Bounds);
                }
                else
                {
                    using (Brush textBrush = new SolidBrush(e.ForeColor))
                    {
                        e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds);
                    }
                }
            }
            finally
            {
                e.DrawFocusRectangle();
                base.OnDrawItem(e);
            }
        }
    }
}
