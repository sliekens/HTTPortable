using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.quoted_pair
{
    public class QuotedPair : Concatenation
    {
        public QuotedPair([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}