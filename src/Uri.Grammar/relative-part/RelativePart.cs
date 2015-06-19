namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class RelativePart : Alternative
    {
        public RelativePart(Alternative alternative)
            : base(alternative)
        {
        }
    }
}