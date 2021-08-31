using System.Windows.Forms;

namespace AcroBat.Views
{
    /// <summary>
    /// コントロールに対するコマンドキーの公開を提供します。
    /// </summary>
    public interface IControlProcess
    {
        /// <summary>
        /// <see cref="Control.ProcessCmdKey"/> を呼び出します。
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        bool RaiseProcessCmdKey(ref Message msg, Keys keyData);
    }
}
