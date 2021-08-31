using Sgry.Azuki;

namespace AcroPad.Models.Associations
{
    public class KeywordPattern : PatternBase
    {
        public string[] Keywords { get; set; }

        protected KeywordPattern()
        {
        }

        public KeywordPattern(string[] keywords, CharClass mode, bool isCaseSensitive = true)
        {
            this.HighlightMode = new[] { mode };
            this.IsCaseSensitive = isCaseSensitive;
            this.Keywords = keywords;
        }
    }
}
