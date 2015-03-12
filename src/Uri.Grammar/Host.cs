namespace Uri.Grammar
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using SLANG;

    public class Host : Alternative<IPLiteral, IPv4Address, RegisteredName>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool isIPv4;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool isIPv6;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool isRegisteredName;

        public Host(IPLiteral element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
            this.isIPv6 = true;
        }

        public Host(IPv4Address element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
            this.isIPv4 = true;
        }

        public Host(RegisteredName element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
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