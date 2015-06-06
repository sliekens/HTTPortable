namespace Uri.Grammar.IP_literal
{
    using System;

    using SLANG;

    public class IPLiteralLexerFactory : ILexerFactory<IPLiteral>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<IPv6Address> ipv6AddressLexerFactory;

        private readonly ILexerFactory<IPvFuture> ipvFutureLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public IPLiteralLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            IStringLexerFactory stringLexerFactory,
            ILexerFactory<IPv6Address> ipv6AddressLexerFactory,
            ILexerFactory<IPvFuture> ipvFutureLexerFactory)
        {
            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            if (ipv6AddressLexerFactory == null)
            {
                throw new ArgumentNullException("ipv6AddressLexerFactory");
            }

            if (ipvFutureLexerFactory == null)
            {
                throw new ArgumentNullException("ipvFutureLexerFactory");
            }

            this.sequenceLexerFactory = sequenceLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
            this.ipv6AddressLexerFactory = ipv6AddressLexerFactory;
            this.ipvFutureLexerFactory = ipvFutureLexerFactory;
        }

        public ILexer<IPLiteral> Create()
        {
            var a = this.stringLexerFactory.Create(@"[");
            var b = this.stringLexerFactory.Create(@"]");
            var ipv6 = this.ipv6AddressLexerFactory.Create();
            var ipvFuture = this.ipvFutureLexerFactory.Create();
            var alt = this.alternativeLexerFactory.Create(ipv6, ipvFuture);
            var innerLexer = this.sequenceLexerFactory.Create(a, alt, b);
            return new IPLiteralLexer(innerLexer);
        }
    }
}