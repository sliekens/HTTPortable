using Http.token;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.pseudonym
{
    public sealed class PseudonymLexer : CompositeLexer<Token, Pseudonym>
    {
        public PseudonymLexer([NotNull] ILexer<Token> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
