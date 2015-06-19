namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class UriReference : Alternative
    {
        public UriReference(Alternative alternative)
            : base(alternative)
        {
        }
    }
}