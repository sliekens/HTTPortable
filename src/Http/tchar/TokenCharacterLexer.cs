using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.tchar
{
    public sealed class TokenCharacterLexer : CompositeLexer<Alternation, TokenCharacter>
    {
        public TokenCharacterLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
