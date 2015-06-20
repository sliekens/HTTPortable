namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Rank : Alternative
    {
        public Rank(Alternative alternative)
            : base(alternative)
        {
        }
    }
}