namespace Http.Grammar
{
    using TextFx.ABNF;

    public class QuotedString : Concatenation
    {
        public QuotedString(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}