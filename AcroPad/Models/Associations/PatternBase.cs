using Sgry.Azuki;

namespace AcroPad.Models.Associations
{
    public abstract class PatternBase
    {
        public CharClass[] HighlightMode { get; set; }
        public bool IsCaseSensitive { get; set; }

        protected PatternBase()
        {
        }
    }
}
