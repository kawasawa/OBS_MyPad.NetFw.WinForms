using System;
using System.Windows.Forms;

namespace AcroBat.WindowsAPI
{
    /// <summary>
    /// 拡張されたメッセージボックスを表します。
    /// </summary>
    public static class MessageBoxEx
    {
        /// <summary>
        /// フックプロシージャのポインタを取得します。
        /// </summary>
        private static IntPtr H_HOOK = IntPtr.Zero;

        /// <summary>
        /// フックプロシージャを削除します。
        /// </summary>
        private static void Unhook()
        {
            if (H_HOOK != IntPtr.Zero)
            {
                User32.UnhookWindowsHookEx(H_HOOK);
                H_HOOK = IntPtr.Zero;
            }
        }

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="owner">オーナーウィンドウ</param>
        /// <param name="text">メッセージ</param>
        /// <param name="caption">キャプション</param>
        /// <param name="buttons">ボタン</param>
        /// <param name="icon">アイコン</param>
        /// <param name="buttonText">ボタンテキスト</param>
        /// <param name="defaultButton">既定のボタン</param>
        /// <returns>ダイアログの戻り値</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, ButtonText buttonText)
        {
            const int HCBT_ACTIVATE = 5;
            const int ID_BUT_OK = 1;
            const int ID_BUT_CANCEL = 2;
            const int ID_BUT_ABORT = 3;
            const int ID_BUT_RETRY = 4;
            const int ID_BUT_IGNORE = 5;
            const int ID_BUT_YES = 6;
            const int ID_BUT_NO = 7;
            const int WH_CBT = 5;

            HOOKPROC hookproc = new HOOKPROC((nCode, wParam, lParam) =>
            {
                if (nCode != HCBT_ACTIVATE)
                {
                    return User32.CallNextHookEx(H_HOOK, nCode, wParam, lParam);
                }

                if (buttonText?.Abort != null)
                    User32.SetDlgItemText(wParam, ID_BUT_ABORT, buttonText.Abort);
                if (buttonText?.Cancel != null)
                    User32.SetDlgItemText(wParam, ID_BUT_CANCEL, buttonText.Cancel);
                if (buttonText?.Ignore != null)
                    User32.SetDlgItemText(wParam, ID_BUT_IGNORE, buttonText.Ignore);
                if (buttonText?.No != null)
                    User32.SetDlgItemText(wParam, ID_BUT_NO, buttonText.No);
                if (buttonText?.OK != null)
                    User32.SetDlgItemText(wParam, ID_BUT_OK, buttonText.OK);
                if (buttonText?.Retry != null)
                    User32.SetDlgItemText(wParam, ID_BUT_RETRY, buttonText.Retry);
                if (buttonText?.Yes != null)
                    User32.SetDlgItemText(wParam, ID_BUT_YES, buttonText.Yes);

                Unhook();
                return User32.CallNextHookEx(H_HOOK, nCode, wParam, lParam);
            });

            try
            {
                Unhook();
                H_HOOK = User32.SetWindowsHookEx(WH_CBT, hookproc, IntPtr.Zero, Karnel32.GetCurrentThreadId());
                return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton);
            }
            finally
            {
                Unhook();
            }
        }

        /// <summary>
        /// メッセージボックスのボタンに表示するテキストを表します。
        /// </summary>
        public sealed class ButtonText
        {
            /// <summary>
            /// 「OK」ボタンに表示するテキストを取得または設定します。
            /// </summary>
            public string OK { get; set; }

            /// <summary>
            /// 「キャンセル」ボタンに表示するテキストを取得または設定します。
            /// </summary>
            public string Cancel { get; set; }

            /// <summary>
            /// 「中止」ボタンに表示するテキストを取得または設定します。
            /// </summary>
            public string Abort { get; set; }

            /// <summary>
            /// 「再試行」ボタンに表示するテキストを取得または設定します。
            /// </summary>
            public string Retry { get; set; }

            /// <summary>
            /// 「無視」ボタンに表示するテキストを取得または設定します。
            /// </summary>
            public string Ignore { get; set; }

            /// <summary>
            /// 「はい」ボタンに表示するテキストを取得または設定します。
            /// </summary>
            public string Yes { get; set; }

            /// <summary>
            /// 「いいえ」ボタンに表示するテキストを取得または設定します。
            /// </summary>
            public string No { get; set; }
        }
    }
}