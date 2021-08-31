using System;
using System.Drawing;
using System.Windows.Forms;

namespace AcroPad.Views.Forms
{
    /// <summary>
    /// 【このクラスは System を除く他のアセンブリと依存関係を持つことができません。】
    /// 例外通知フォームを表します。
    /// </summary>
    public partial class ExceptionForm : Form
    {
        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        public ExceptionForm(string message)
        {
            this.InitializeComponent();
            this.InitializeWindowProperties();
            this.AddEventHandler();
            this.UpdateControlProperties();
            this.CancelButton = this.btnClose;
            this.MinimumSize = this.Size;
            this.Text = this.ProductName;
            this.txtDetail.Text = message;

            Bitmap bitmap = new Bitmap(this.picIcon.Width, this.picIcon.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawIcon(SystemIcons.Error, 0, 0);
            }
            this.picIcon.Image = bitmap;

            this.btnClose.Select();
        }

        /// <summary>
        /// プロパティを初期化します。
        /// </summary>
        private void InitializeWindowProperties()
        {
            this.AutoScaleMode = AutoScaleMode.None;
            this.Font = SystemFonts.MenuFont;
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEventHandler()
        {
            this.chkWordWrap.CheckedChanged += this.ChkWordWrap_CheckedChanged;
        }

        /// <summary>
        /// コントロールのプロパティを更新します。
        /// </summary>
        private void UpdateControlProperties()
        {
            if (this.chkWordWrap.Checked)
            {
                this.txtDetail.ScrollBars = ScrollBars.Vertical;
                this.txtDetail.WordWrap = true;
            }
            else
            {
                this.txtDetail.ScrollBars = ScrollBars.Both;
                this.txtDetail.WordWrap = false;
            }
        }

        private void ChkWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateControlProperties();
        }
    }
}
