namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    using Uri.Grammar;

    using UriHost = Uri.Grammar.Host;
    using OptionalPort = SLANG.Option<SLANG.Sequence<SLANG.Element, Uri.Grammar.Port>>;

    public class HostLexer : Lexer<Host>
    {
        private readonly ILexer<UriHost> uriHostLexer;
        private readonly ILexer<Port> portLexer;

        public HostLexer()
            : this(new global::Uri.Grammar.HostLexer(), new PortLexer())
        {
        }

        public HostLexer(ILexer<UriHost> uriHostLexer, ILexer<Port> portLexer)
            : base("Host")
        {
            Contract.Requires(uriHostLexer != null);
            Contract.Requires(portLexer != null);
            this.uriHostLexer = uriHostLexer;
            this.portLexer = portLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Host element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Host);
                return false;
            }

            var context = scanner.GetContext();
            UriHost uriHost;
            if (!this.uriHostLexer.TryRead(scanner, out uriHost))
            {
                element = default(Host);
                return false;
            }

            OptionalPort optionalPort;
            if (TryReadOptionalPort(scanner, out optionalPort))
            {
                element = new Host(uriHost, optionalPort, context);
            }
            else
            {
                element = new Host(uriHost, new OptionalPort(context), context);
            }

            return true;
        }

        private bool TryReadOptionalPort(ITextScanner scanner, out OptionalPort element)
        {
            if (scanner.EndOfInput)
            {
                element = default(OptionalPort);
                return false;
            }

            var context = scanner.GetContext();
            Element portSeparator;
            if (!TryReadTerminal(scanner, ":", out portSeparator))
            {
                element = default(OptionalPort);
                return false;
            }

            Port port;
            if (!this.portLexer.TryRead(scanner, out port))
            {
                scanner.PutBack(portSeparator.Data);
                element = default(OptionalPort);
                return false;
            }

            var sequence = new Sequence<Element, Port>(portSeparator, port, context);
            element = new OptionalPort(sequence, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.uriHostLexer != null);
            Contract.Invariant(this.portLexer != null);
        }
    }
}