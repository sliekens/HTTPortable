using Txt.ABNF;

namespace Http.tchar
{
    public class TokenCharacter : Alternative
    {
        public TokenCharacter(Alternative alternative)
            : base(alternative)
        {
        }
    }
}