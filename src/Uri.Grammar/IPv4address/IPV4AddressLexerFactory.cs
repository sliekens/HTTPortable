namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPV4AddressLexerFactory : ILexerFactory<IPv4Address>
    {
        private readonly ILexerFactory<DecimalOctet> decimaOctetLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public IPV4AddressLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<DecimalOctet> decimaOctetLexerFactory)
        {
            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (decimaOctetLexerFactory == null)
            {
                throw new ArgumentNullException("decimaOctetLexerFactory");
            }

            this.sequenceLexerFactory = sequenceLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.decimaOctetLexerFactory = decimaOctetLexerFactory;
        }

        public ILexer<IPv4Address> Create()
        {
            // dec-octet
            var a = this.decimaOctetLexerFactory.Create();

            // "."
            var b = this.terminalLexerFactory.Create(@".");

            // dec-octet "." dec-octet "." dec-octet "." dec-octet
            var c = this.sequenceLexerFactory.Create(a, b, a, b, a, b, a);

            // IPv4address
            return new IPv4AddressLexer(c);
        }
    }
}