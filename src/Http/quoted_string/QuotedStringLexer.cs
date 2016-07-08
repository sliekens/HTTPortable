using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.quoted_string
{
    public sealed class QuotedStringLexer : CompositeLexer<Concatenation, QuotedString>
    {
        public QuotedStringLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
