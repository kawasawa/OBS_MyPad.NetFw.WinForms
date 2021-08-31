using AcroBat;
using System.Drawing;
using System.Windows.Forms;

namespace AcroPad.Models.Entity
{
    /// <summary>
    /// ウィンドウの設定を表します。
    /// </summary>
    public class WindowSetting : SettingBase
    {
        /// <summary>
        /// 表示位置を取得または設定します。
        /// </summary>
        public Point Location { get; set; } = GetInitialRect().Location;

        /// <summary>
        /// サイズを取得または設定します。
        /// </summary>
        public Size Size { get; set; } = GetInitialRect().Size;

        /// <summary>
        /// 表示方法を取得または設定します。
        /// </summary>
        public FormWindowState WindowState { get; set; } = FormWindowState.Normal;

        /// <summary>
        /// メニューバーを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool MenuBarVisible { get; set; } = true;

        /// <summary>
        /// ツールバーを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool ToolBarVisible { get; set; } = true;

        /// <summary>
        /// ステータスバーを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool StatusBarVisible { get; set; } = true;

        /// <summary>
        /// 最前面に表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool TopMost { get; set; } = false;

        /// <summary>
        /// 初期状態での表示位置を取得します。
        /// </summary>
        /// <returns>表示位置</returns>
        public static Rectangle GetInitialRect() => new Rectangle(new Point(100, 100), new Size(700, 500));

        /// <summary>
        /// 指定したオブジェクトから同名のプロパティの値を転記します。
        /// </summary>
        /// <param name="target">オブジェクト</param>
        public override void GetValue(object target)
        {
            base.GetValue(target);
            Form form = target as Form;
            if (form != null)
            {
                switch (form.WindowState)
                {
                    case FormWindowState.Minimized:
                        this.WindowState = FormWindowState.Normal;
                        this.Location = form.RestoreBounds.Location;
                        this.Size = form.RestoreBounds.Size;
                        break;
                    case FormWindowState.Maximized:
                        this.Location = form.RestoreBounds.Location;
                        this.Size = form.RestoreBounds.Size;
                        break;
                }
            }
        }

        /// <summary>
        /// 指定したオブジェクトに同名のプロパティの値を転記します。
        /// </summary>
        /// <param name="target">オブジェクト</param>
        public override void SetValue(object target)
        {
            base.SetValue(target);
            Form form = target as Form;
            if (form != null)
            {
                if (Screen.FromControl(form).Bounds.SomeContains(form.Bounds) == false)
                {
                    form.Bounds = GetInitialRect();
                }
            }
        }
    }
}
