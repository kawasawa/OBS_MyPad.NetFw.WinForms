using System;
using System.Windows.Forms;

namespace AcroBat.WindowsAPI
{
    /// <summary>
    /// WindowsAPI イベントの情報を表します。
    /// </summary>
    public class WindowsAPIEventArgs : EventArgs
    {
        /// <summary>
        /// Windows メッセージのコピーを取得します。
        /// </summary>
        public Message Message { get; protected set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="m">Windows メッセージ</param>
        public WindowsAPIEventArgs(Message m)
        {
            this.Message = m;
        }
    }
}
