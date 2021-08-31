namespace AcroPad.Views.Forms
{
    //
    // 外部からメソッドを呼び出すためのInvokeメソッドを提供します。
    //
    public partial class MainForm
    {
        /// <summary>
        /// 新しいドキュメントを追加します。
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <param name="isReadOnly">読み取り専用かどうか</param>
        public void InvokeAddDocument(string path, bool isReadOnly)
        {
            if (this.IsLoaded)
            {
                this.AddDocument(path, isReadOnly);
            }
            else
            {
                this._settingsAppliedOnLoad += (sender, e) => this.AddDocument(path, isReadOnly);
            }
        }

        /// <summary>
        /// アクティブなドキュメントを閉じます。
        /// </summary>
        /// <returns>閉じることができたかどうかを示す値</returns>
        public bool InvokeCloseDocument() => this.CloseDocument();

        /// <summary>
        /// アクティブなドキュメントを除くすべてのドキュメントを閉じます。
        /// </summary>
        /// <returns>閉じることができたかどうかを示す値</returns>
        public bool InvokeCloseAllButThisDocument() => this.CloseAllButThisDocument();

        /// <summary>
        /// すべてのドキュメントを閉じます。
        /// </summary>
        /// <returns>閉じることができたかどうかを示す値</returns>
        public bool InvokeCloseAllDocument() => this.CloseAllDocument();
    }
}
