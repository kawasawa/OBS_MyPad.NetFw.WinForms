using System.ComponentModel;
using System.Windows.Forms;

namespace AcroBat.Views.Forms
{
    /// <summary>
    /// 大きさが固定されたダイアログボックスの基底クラスを表します。
    /// </summary>
    public class FixedSizeDialogBase : DialogBase
    {
        /// <summary>
        /// フォームのキャプション バーに最小化ボタンを表示するかどうかを示す値を取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool MinimizeBox
        {
            get { return base.MinimizeBox; }
            private set { base.MinimizeBox = value; }
        }

        /// <summary>
        /// フォームのキャプション バーに最大化ボタンを表示するかどうかを示す値を取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool MaximizeBox
        {
            get { return base.MaximizeBox; }
            private set { base.MaximizeBox = value; }
        }

        /// <summary>
        /// フォームの右下隅に表示するサイズ変更グリップのスタイルを取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new SizeGripStyle SizeGripStyle
        {
            get { return base.SizeGripStyle; }
            private set { base.SizeGripStyle = value; }
        }

        /// <summary>
        /// フォームの境界線スタイルを取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            private set { base.FormBorderStyle = value; }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public FixedSizeDialogBase()
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
