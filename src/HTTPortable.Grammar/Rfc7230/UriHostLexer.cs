namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    using UriHost = Uri.Grammar.Host;

    public class UriHostLexer : Lexer<UriHost>
    {
        private readonly ILexer<UriHost> hostLexer;

        public UriHostLexer()
            : this(new Uri.Grammar.HostLexer())
        {
        }

        public UriHostLexer(ILexer<UriHost> hostLexer)
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

            if (this.hostLexer.TryRead(scanner, out element))
            {
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