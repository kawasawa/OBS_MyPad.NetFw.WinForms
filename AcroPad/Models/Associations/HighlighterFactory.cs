using AcroBat;
using Sgry.Azuki;
using Sgry.Azuki.Highlighter;

namespace AcroPad.Models.Associations
{
    /// <summary>
    /// ハイライト設定を構築するファクトリークラスを表します。
    /// </summary>
    public static class HighlighterFactory
    {
        /// <summary>
        /// 新しいインスタンスを取得します。
        /// </summary>
        /// <param name="mode">言語の種類</param>
        /// <returns>インスタンス</returns>
        public static IHighlighter CreateInstance(LanguageKind mode)
        {
            KeywordHighlighter highlighter = new KeywordHighlighter();
            Association assoc = LanguageContainer.Instance.GetAssociation(mode);
            assoc.KeywordSet?.ForEach(k => highlighter.AddKeywordSet(k.Keywords, k.HighlightMode[0], !k.IsCaseSensitive));
            assoc.Regex?.ForEach(r => highlighter.AddRegex(r.Regex, !r.IsCaseSensitive, r.HighlightMode));
            assoc.Texts?.ForEach(p =>
            {
                if (p.IsEnclosure)
                {
                    highlighter.AddEnclosure(p.Open, p.Close, p.HighlightMode[0], p.IsMultiLine, p.Escap, !p.IsCaseSensitive);
                }
                else
                {
                    highlighter.AddLineHighlight(p.Open, p.HighlightMode[0], !p.IsCaseSensitive);
                }
            });
            return highlighter;
        }
    }
}
