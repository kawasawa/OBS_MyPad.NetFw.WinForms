using AcroBat.WindowsAPI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AcroBat.Views.Forms.Core
{
    /// <summary>
    /// フォームに共通して必要な機能を実装する基底クラスを表します。
    /// </summary>
    public class FormCore : Form, IControlProcess
    {
        protected SystemMenuCustomizer SystemMenuCustomizer { get; }

        /// <summary>
        /// 自動スケーリングモードを取得または設定します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new AutoScaleMode AutoScaleMode
        {
            get { return base.AutoScaleMode; }
            private set { base.AutoScaleMode = value; }
        }

        /// <summary>
        /// フォントを取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Font Font
        {
            get { return base.Font; }
            private set { base.Font = value; }
        }

        /// <summary>
        /// フォームが読み込まれたかどうかを示す値を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLoaded { get; private set; } = false;

        /// <summary>
        /// フォームが閉じられたかどうかを示す値を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFormClosed { get; private set; } = false;

        /// <summary>
        /// 他プロセスから Windows メッセージを受信したときに発生します。
        /// </summary>
        [Category(DesignerCategory.ACTION)]
        [Description("他プロセスから Windows メッセージを受信したときに発生します。")]
        public event WindowsAPIEventHandler WindowsMessageReceived;

        /// <summary>
        /// システムメニューが開いたときに発生します。
        /// </summary>
        [Category(DesignerCategory.ACTION)]
        [Description("システムメニューが開いたときに発生します。")]
        public event WindowsAPIEventHandler SystemMenuOpened;

        /// <summary>
        /// システムメニューが閉じたときに発生します。
        /// </summary>
        [Category(DesignerCategory.ACTION)]
        [Description("システムメニューが閉じたときに発生します。")]
        public event WindowsAPIEventHandler SystemMenuClosed;

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public FormCore()
        {
            this.SystemMenuCustomizer = new SystemMenuCustomizer(this);
            this.AutoScaleMode = AutoScaleMode.None;
            this.Font = SystemFonts.MenuFont;
        }

        /// <summary>
        /// <see cref="Form.Load"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnLoad(EventArgs e)
        {
            this.IsLoaded = true;
            base.OnLoad(e);
        }

        /// <summary>
        /// <see cref="Form.FormClosed"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            this.IsFormClosed = true;
            base.OnFormClosed(e);
        }

        /// <summary>
        /// <see cref="WindowsMessageReceived"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnWindowsMessageReceived(WindowsAPIEventArgs e) => this.WindowsMessageReceived?.Invoke(this, e);

        /// <summary>
        /// <see cref="SystemMenuOpened"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnSystemMenuOpened(WindowsAPIEventArgs e) => this.SystemMenuOpened?.Invoke(this, e);

        /// <summary>
        /// <see cref="SystemMenuClosed"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnSystemMenuClosed(WindowsAPIEventArgs e) => this.SystemMenuClosed?.Invoke(this, e);

        /// <summary>
        /// <see cref="Form.ProcessCmdKey"/> を呼び出します。
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        public virtual bool RaiseProcessCmdKey(ref Message msg, Keys keyData) => this.ProcessCmdKey(ref msg, keyData);

        /// <summary>
        /// Windows メッセージを処理します。
        /// </summary>
        /// <param name="m">メッセージ</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WindowsMessages.WM_COPYDATA:
                    this.OnWindowsMessageReceived(new WindowsAPIEventArgs(m));
                    break;
                case WindowsMessages.WM_INITMENUPOPUP:
                    this.OnSystemMenuOpened(new WindowsAPIEventArgs(m));
                    break;
                case WindowsMessages.WM_UNINITMENUPOPUP:
                    this.OnSystemMenuClosed(new WindowsAPIEventArgs(m));
                    break;
            }
            base.WndProc(ref m);
            this.SystemMenuCustomizer.InvokeMethod(ref m);
        }
    }
}
