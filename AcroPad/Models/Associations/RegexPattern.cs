using Sgry.Azuki;

namespace AcroPad.Models.Associations
{
    public class RegexPattern : PatternBase
    {
        public string Regex { get; set; }

        protected RegexPattern()
        {
        }

        public RegexPattern(string regex, CharClass mode, bool isCaseSensitive = true)
            : this(regex, new[] { mode }, isCaseSensitive)
        {
        }

        public RegexPattern(string regex, CharClass[] mode, bool isCaseSensitive = true)
        {
            this.HighlightMode = mode;
            this.IsCaseSensitive = isCaseSensitive;
            this.Regex = regex;
        }
    }
}
