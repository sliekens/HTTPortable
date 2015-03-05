namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class Host : Element
    {
        public Host(IPLiteral ipLiteral, ITextContext context)
            : base(ipLiteral.Data, context)
        {
            Contract.Requires(ipLiteral != null);
            Contract.Requires(context != null);
        }

        public Host(IPv4Address ipv4Address, ITextContext context)
            : base(ipv4Address.Data, context)
        {
            Contract.Requires(ipv4Address != null);
            Contract.Requires(context != null);
        }

        public Host(RegisteredName registeredName, ITextContext context)
            : base(registeredName.Data, context)
        {
            Contract.Requires(registeredName != null);
            Contract.Requires(context != null);
        }
    }
}
