namespace Http.Grammar
{
    using TextFx.ABNF;

    public class TokenCharacter : Alternative
    {
        public TokenCharacter(Alternative alternative)
            : base(alternative)
        {
        }
    }
}