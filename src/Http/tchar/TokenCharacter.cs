using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.tchar
{
    public class TokenCharacter : Alternation
    {
        public TokenCharacter([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}