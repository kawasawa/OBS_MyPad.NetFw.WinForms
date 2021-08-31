using Sgry.Azuki;

namespace AcroPad.Models.Associations
{
    public class TextPattern : PatternBase
    {
        public string Open { get; set; }
        public string Close { get; set; }
        public char Escap { get; set; }
        public bool IsMultiLine { get; set; }
        public bool IsEnclosure => !string.IsNullOrEmpty(this.Close);

        protected TextPattern()
        {
        }

        public TextPattern(string open, CharClass mode, bool isCaseSensitive = true)
            : this(open, string.Empty, '\0', mode, false, isCaseSensitive)
        {
        }

        public TextPattern(string open, string close, CharClass mode, bool isMultiLine = false, bool isCaseSensitive = true)
            : this(open, close, '\0', mode, isMultiLine, isCaseSensitive)
        {
        }

        public TextPattern(string open, string close, char escape, CharClass mode, bool isMultiLine = false, bool isCaseSensitive = true)
        {
            this.HighlightMode = new[] { mode };
            this.IsCaseSensitive = isCaseSensitive;
            this.Open = open;
            this.Close = close;
            this.Escap = escape;
            this.IsMultiLine = isMultiLine;
        }
    }
}
