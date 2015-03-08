namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using SLANG;

    

    public class HostLexer : Lexer<Host>
    {
        private readonly ILexer<IPLiteral> ipLiteralLexer;

        private readonly ILexer<IPv4Address> ipv4AddressLexer;

        private readonly ILexer<RegisteredName> registeredNameLexer;

        public HostLexer()
            : this(new IPLiteralLexer(), new IPv4AddressLexer(), new RegisteredNameLexer())
        {
        }

        public HostLexer(ILexer<IPLiteral> ipLiteralLexer, ILexer<IPv4Address> ipv4AddressLexer, ILexer<RegisteredName> registeredNameLexer)
            : base("host")
        {
            Contract.Requires(ipLiteralLexer != null);
            Contract.Requires(ipv4AddressLexer != null);
            Contract.Requires(registeredNameLexer != null);
            this.ipLiteralLexer = ipLiteralLexer;
            this.ipv4AddressLexer = ipv4AddressLexer;
            this.registeredNameLexer = registeredNameLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Host element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Host);
                return false;
            }

            var context = scanner.GetContext();
            IPLiteral ipLiteral;
            if (this.ipLiteralLexer.TryRead(scanner, out ipLiteral))
            {
                element = new Host(ipLiteral, context);
                return true;
            }

            IPv4Address ipv4Address;
            if (this.ipv4AddressLexer.TryRead(scanner, out ipv4Address))
            {
                element = new Host(ipv4Address, context);
                return true;
            }

            RegisteredName registeredName;
            if (this.registeredNameLexer.TryRead(scanner, out registeredName))
            {
                element = new Host(registeredName, context);
                return true;
            }

            element = default(Host);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ipLiteralLexer != null);
            Contract.Invariant(this.ipv4AddressLexer != null);
            Contract.Invariant(this.registeredNameLexer != null);
        }
    }
}
