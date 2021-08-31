namespace AcroPad.Models.Associations
{
    /// <summary>
    /// 言語別の関連付け情報を表します。
    /// </summary>
    public class Association
    {
        /// <summary>
        /// 言語の種類を取得または設定します。
        /// </summary>
        public LanguageKind LanguageMode { get; set; }

        /// <summary>
        /// 自動インデントの種類を取得または設定します。
        /// </summary>
        public AutoIndentKind AutoIndentMode { get; set; }

        /// <summary>
        /// 表示名を取得または設定します。
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 拡張子を取得または設定します。
        /// </summary>
        public string[] Extensions { get; set; }

        /// <summary>
        /// 強調するキーワードを取得または設定します。
        /// </summary>
        public KeywordPattern[] KeywordSet { get; set; }

        /// <summary>
        /// テキストとして扱うパターンを取得または設定します。
        /// </summary>
        public TextPattern[] Texts { get; set; }

        /// <summary>
        /// 強調する正規表現パターンを取得または設定します。
        /// </summary>
        public RegexPattern[] Regex { get; set; }
    }
}
