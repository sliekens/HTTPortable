using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.token
{
    public sealed class TokenLexer : CompositeLexer<Repetition, Token>
    {
        public TokenLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
