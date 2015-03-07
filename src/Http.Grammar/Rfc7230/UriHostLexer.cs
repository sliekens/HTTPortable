namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    using Uri.Grammar;

    public class UriHostLexer : Lexer<UriHost>
    {
        private readonly ILexer<Host> hostLexer;

        public UriHostLexer()
            : this(new HostLexer())
        {
        }

        public UriHostLexer(ILexer<Host> hostLexer)
            : base("uri-host")
        {
            Contract.Requires(hostLexer != null);
            this.hostLexer = hostLexer;
        }

        public override bool TryRead(ITextScanner scanner, out UriHost element)
        {
            if (scanner.EndOfInput)
            {
                element = default(UriHost);
                return false;
            }

            var context = scanner.GetContext();
            Host host;
            if (this.hostLexer.TryRead(scanner, out host))
            {
                element = new UriHost(host, context);
                return true;
            }

            element = default(UriHost);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hostLexer != null);
        }
    }
}