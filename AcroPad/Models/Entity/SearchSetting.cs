namespace AcroPad.Models.Entity
{
    /// <summary>
    /// 検索設定を表します。
    /// </summary>
    public class SearchSetting : SettingBase
    {
        /// <summary>
        /// 検索コンテンツのふるまいを取得または設定します。
        /// </summary>
        public SearchContentBehaviorKind BehaviorMode { get; set; } = SearchContentBehaviorKind.Search;

        /// <summary>
        /// 大文字と小文字を区別するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool CaseSensitive { get; set; } = false;

        /// <summary>
        /// 正規表現を使用するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool UseRegex { get; set; } = false;

        /// <summary>
        /// 先頭(末尾)から再検索するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool GoRound { get; set; } = false;
    }
}
