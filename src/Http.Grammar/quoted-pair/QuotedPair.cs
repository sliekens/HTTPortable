namespace Http.Grammar
{
    using TextFx.ABNF;

    public class QuotedPair : Concatenation
    {
        public QuotedPair(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}