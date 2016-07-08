using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.qdtext
{
    public sealed class QuotedTextLexer : CompositeLexer<Alternation, QuotedText>
    {
        public QuotedTextLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
