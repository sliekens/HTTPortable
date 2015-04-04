namespace Uri.Grammar
{
    using SLANG;

    public class IPv6Address : Element
    {
        public IPv6Address(string address, ITextContext context)
            : base(address, context)
        {
            // TODO: validate that 'address' is actually a valid IPv6 address
            // TODO: figure out how to validate IPv6 addresses without re-implementing the entire parser
        }
    }
}