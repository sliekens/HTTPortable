namespace Uri.Grammar.host
{
    using System;

    using SLANG;

    public class HostLexerFactory : ILexerFactory<Host>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<IPLiteral> ipLiteralLexerFactory;

        private readonly ILexerFactory<IPv4Address> ipv4AddressLexerFactory;

        private readonly ILexerFactory<RegisteredName> registeredNameLexerFactory;

        public HostLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<IPLiteral> ipLiteralLexerFactory,
            ILexerFactory<IPv4Address> ipv4AddressLexerFactory,
            ILexerFactory<RegisteredName> registeredNameLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (ipLiteralLexerFactory == null)
            {
                throw new ArgumentNullException("ipLiteralLexerFactory");
            }

            if (ipv4AddressLexerFactory == null)
            {
                throw new ArgumentNullException("ipv4AddressLexerFactory");
            }

            if (registeredNameLexerFactory == null)
            {
                throw new ArgumentNullException("registeredNameLexerFactory");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.ipLiteralLexerFactory = ipLiteralLexerFactory;
            this.ipv4AddressLexerFactory = ipv4AddressLexerFactory;
            this.registeredNameLexerFactory = registeredNameLexerFactory;
        }

        public ILexer<Host> Create()
        {
            var ipliteral = this.ipLiteralLexerFactory.Create();
            var ipv4 = this.ipv4AddressLexerFactory.Create();
            var regName = this.registeredNameLexerFactory.Create();
            var innerLexer = this.alternativeLexerFactory.Create(ipliteral, ipv4, regName);
            return new HostLexer(innerLexer);
        }
    }
}