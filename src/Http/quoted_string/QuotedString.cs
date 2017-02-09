using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.quoted_string
{
    public class QuotedString : Concatenation
    {
        public QuotedString([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}