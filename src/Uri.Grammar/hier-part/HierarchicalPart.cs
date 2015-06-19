namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class HierarchicalPart : Alternative
    {
        public HierarchicalPart(Alternative alternative)
            : base(alternative)
        {
        }
    }
}