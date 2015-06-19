namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class IPv6Address : Alternative
    {
        public IPv6Address(Alternative alternative)
            : base(alternative)
        {
        }
    }
}