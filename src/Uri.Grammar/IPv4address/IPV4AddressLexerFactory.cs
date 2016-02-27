namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPv4AddressLexerFactory : ILexerFactory<IPv4Address>
    {
        private readonly ILexerFactory<DecimalOctet> decimaOctetLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public IPv4AddressLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<DecimalOctet> decimaOctetLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException("concatenationLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (decimaOctetLexerFactory == null)
            {
                throw new ArgumentNullException("decimaOctetLexerFactory");
            }

            this.concatenationLexerFactory = concatenationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.decimaOctetLexerFactory = decimaOctetLexerFactory;
        }

        public ILexer<IPv4Address> Create()
        {
            // dec-octet
            var a = this.decimaOctetLexerFactory.Create();

            // "."
            var b = this.terminalLexerFactory.Create(@".", StringComparer.Ordinal);

            // dec-octet "." dec-octet "." dec-octet "." dec-octet
            var c = this.concatenationLexerFactory.Create(a, b, a, b, a, b, a);

            // IPv4address
            return new IPv4AddressLexer(c);
        }
    }
}