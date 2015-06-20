namespace Http.Grammar
{
    using TextFx.ABNF;

    public class QuotedText : Alternative
    {
        public QuotedText(Alternative alternative)
            : base(alternative)
        {
        }
    }
}