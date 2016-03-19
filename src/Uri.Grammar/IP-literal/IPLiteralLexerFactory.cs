namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPLiteralLexerFactory : ILexerFactory<IPLiteral>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<IPv6Address> ipv6AddressLexerFactory;

        private readonly ILexerFactory<IPvFuture> ipvFutureLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public IPLiteralLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<IPv6Address> ipv6AddressLexerFactory,
            ILexerFactory<IPvFuture> ipvFutureLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (ipv6AddressLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipv6AddressLexerFactory));
            }

            if (ipvFutureLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipvFutureLexerFactory));
            }

            this.concatenationLexerFactory = concatenationLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.ipv6AddressLexerFactory = ipv6AddressLexerFactory;
            this.ipvFutureLexerFactory = ipvFutureLexerFactory;
        }

        public ILexer<IPLiteral> Create()
        {
            var a = terminalLexerFactory.Create(@"[", StringComparer.Ordinal);
            var b = terminalLexerFactory.Create(@"]", StringComparer.Ordinal);
            var ipv6 = ipv6AddressLexerFactory.Create();
            var ipvFuture = ipvFutureLexerFactory.Create();
            var alt = alternativeLexerFactory.Create(ipv6, ipvFuture);
            var innerLexer = concatenationLexerFactory.Create(a, alt, b);
            return new IPLiteralLexer(innerLexer);
        }
    }
}