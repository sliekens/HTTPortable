using Txt.ABNF;

namespace Http.qdtext
{
    public class QuotedText : Alternation
    {
        public QuotedText(Alternation alternation)
            : base(alternation)
        {
        }
    }
}