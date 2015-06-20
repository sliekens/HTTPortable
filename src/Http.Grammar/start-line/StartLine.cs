namespace Http.Grammar
{
    using TextFx.ABNF;

    public class StartLine : Alternative
    {
        public StartLine(Alternative alternative)
            : base(alternative)
        {
        }
    }
}
