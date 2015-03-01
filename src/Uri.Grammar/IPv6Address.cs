namespace Uri.Grammar
{
    using Text.Scanning;

    public class IPv6Address : Element
    {
        public IPv6Address(string address, ITextContext context)
            : base(address, context)
        {
        }
    }
}
