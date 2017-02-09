using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.qdtext
{
    public class QuotedText : Alternation
    {
        public QuotedText([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
