namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class LeastSignificantInt32 : Element
    {
        public LeastSignificantInt32(HexadecimalInt16 leftInt16, HexadecimalInt16 rightInt16, ITextContext context)
            : base(string.Concat(leftInt16.Data, ":", rightInt16.Data), context)
        {
            Contract.Requires(leftInt16 != null);
            Contract.Requires(rightInt16 != null);
            Contract.Requires(context != null);
        }

        public LeastSignificantInt32(IPv4Address ipv4Address, ITextContext context)
            : base(ipv4Address.Data, context)
        {
            Contract.Requires(ipv4Address != null);
            Contract.Requires(context != null);
        }
    }
}
