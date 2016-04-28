using Txt.ABNF;

namespace Http.tchar
{
    public class TokenCharacter : Alternation
    {
        public TokenCharacter(Alternation alternation)
            : base(alternation)
        {
        }
    }
}