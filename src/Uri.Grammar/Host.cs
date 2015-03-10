namespace Uri.Grammar
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using SLANG;

    public class Host : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool isIPv4;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool isIPv6;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool isRegisteredName;

        public Host(IPLiteral ipLiteral, ITextContext context)
            : base(ipLiteral.Data, context)
        {
            Contract.Requires(ipLiteral != null);
            Contract.Requires(context != null);
            this.isIPv6 = true;
        }

        public Host(IPv4Address ipv4Address, ITextContext context)
            : base(ipv4Address.Data, context)
        {
            Contract.Requires(ipv4Address != null);
            Contract.Requires(context != null);
            this.isIPv4 = true;
        }

        public Host(RegisteredName registeredName, ITextContext context)
            : base(registeredName.Data, context)
        {
            Contract.Requires(registeredName != null);
            Contract.Requires(context != null);
            this.isRegisteredName = true;
        }

        public bool IsIPv4
        {
            get
            {
                return this.isIPv4;
            }
        }

        public bool IsIPv6
        {
            get
            {
                return this.isIPv6;
            }
        }

        public bool IsRegisteredName
        {
            get
            {
                return this.isRegisteredName;
            }
        }
    }
}