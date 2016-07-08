using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.quoted_pair
{
    public sealed class QuotedPairLexer : CompositeLexer<Concatenation, QuotedPair>
    {
        public QuotedPairLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
