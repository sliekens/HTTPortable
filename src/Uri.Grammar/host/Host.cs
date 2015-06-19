namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Host : Alternative
    {
        public Host(Alternative alternative)
            : base(alternative)
        {
        }
    }
}