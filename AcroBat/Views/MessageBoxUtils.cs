using AcroBat.Properties;
using AcroBat.WindowsAPI;
using System.Windows.Forms;

namespace AcroBat.Views
{
    /// <summary>
    /// メッセージボックスを表示する処理を提供します。
    /// </summary>
    public static class MessageBoxUtils
    {
        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="kind">メッセージの種類</param>
        /// <param name="text">メッセージ</param>
        /// <returns>ダイアログの戻り値</returns>
        public static DialogResult ShowMessageBox(MessageKind kind, string text)
            => ShowMessageBox(null, kind, text);

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="kind">メッセージの種類</param>
        /// <param name="text">メッセージ</param>
        /// <param name="caption">キャプション</param>
        /// <returns>ダイアログの戻り値</returns>
        public static DialogResult ShowMessageBox(MessageKind kind, string text, string caption) 
            => ShowMessageBox(null, kind, text, caption);

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="owner">オーナーウィンドウ</param>
        /// <param name="kind">メッセージの種類</param>
        /// <param name="text">メッセージ</param>
        /// <returns>ダイアログの戻り値</returns>
        public static DialogResult ShowMessageBox(IWin32Window owner, MessageKind kind, string text) 
            => ShowMessageBox(owner, kind, text, GetMessageBoxCaption(kind));

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="owner">オーナーウィンドウ</param>
        /// <param name="kind">メッセージの種類</param>
        /// <param name="text">メッセージ</param>
        /// <param name="caption">キャプション</param>
        /// <returns>ダイアログの戻り値</returns>
        public static DialogResult ShowMessageBox(IWin32Window owner, MessageKind kind, string text, string caption)
            => MessageBoxEx.Show(owner, text, caption, GetMessageBoxButtons(kind), GetMessageBoxIcon(kind), MessageBoxDefaultButton.Button1, null);

        /// <summary>
        /// メッセージボックスのキャプションを取得します。
        /// </summary>
        /// <param name="kind">メッセージの種類</param>
        /// <returns>キャプション</returns>
        public static string GetMessageBoxCaption(MessageKind kind)
        {
            switch (kind)
            {
                case MessageKind.Information:
                    return $"{Resources.TITLE_INFORMAITION} - {AssemblyEntity.Title}";
                case MessageKind.Confirm:
                case MessageKind.ConfirmCancelable:
                    return $"{Resources.TITLE_CONFIRM} - {AssemblyEntity.Title}";
                case MessageKind.Warning:
                case MessageKind.WarningCancelable:
                    return $"{Resources.TITLE_WARNING} - {AssemblyEntity.Title}";
                case MessageKind.Error:
                    return $"{Resources.TITLE_ERROR} - {AssemblyEntity.Title}";
                case MessageKind.UnKnown:
                default:
                    return $"{AssemblyEntity.Title}";
            }
        }

        /// <summary>
        /// メッセージボックスのアイコンを取得します。
        /// </summary>
        /// <param name="kind">メッセージの種類</param>
        /// <returns>アイコン</returns>
        public static MessageBoxIcon GetMessageBoxIcon(MessageKind kind)
        {
            switch (kind)
            {
                case MessageKind.Information:
                    return MessageBoxIcon.Information;
                case MessageKind.Confirm:
                case MessageKind.ConfirmCancelable:
                    return MessageBoxIcon.Question;
                case MessageKind.Warning:
                case MessageKind.WarningCancelable:
                    return MessageBoxIcon.Warning;
                case MessageKind.Error:
                    return MessageBoxIcon.Error;
                default:
                    return MessageBoxIcon.None;
            }
        }

        /// <summary>
        /// メッセージボックスのボタンを取得します。
        /// </summary>
        /// <param name="kind">メッセージの種類</param>
        /// <returns>ボタン</returns>
        public static MessageBoxButtons GetMessageBoxButtons(MessageKind kind)
        {
            switch (kind)
            {
                case MessageKind.Confirm:
                    return MessageBoxButtons.YesNo;
                case MessageKind.ConfirmCancelable:
                    return MessageBoxButtons.YesNoCancel;
                case MessageKind.WarningCancelable:
                    return MessageBoxButtons.OKCancel;
                default:
                    return MessageBoxButtons.OK;
            }
        }
    }

    /// <summary>
    /// メッセージの種類を表します。
    /// </summary>
    public enum MessageKind
    {
        /// <summary>
        /// 情報を表します。
        /// </summary>
        Information,

        /// <summary>
        /// 確認を表します。
        /// </summary>
        Confirm,

        /// <summary>
        /// キャンセル可能な確認を表します。
        /// </summary>
        ConfirmCancelable,

        /// <summary>
        /// 警告を表します。
        /// </summary>
        Warning,

        /// <summary>
        /// キャンセル可能な警告を表します。
        /// </summary>
        WarningCancelable,

        /// <summary>
        /// エラーを表します。
        /// </summary>
        Error,

        /// <summary>
        /// 指定されていません。
        /// メッセージのみを表します。
        /// </summary>
        UnKnown,
    }
}
