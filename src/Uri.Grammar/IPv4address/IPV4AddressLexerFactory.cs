namespace Uri.Grammar.IPv4address
{
    using System;

    using SLANG;

    public class IPV4AddressLexerFactory : ILexerFactory<IPv4Address>
    {
        private readonly ILexerFactory<DecimalOctet> decimaOctetLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public IPV4AddressLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            IStringLexerFactory stringLexerFactory,
            ILexerFactory<DecimalOctet> decimaOctetLexerFactory)
        {
            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            if (decimaOctetLexerFactory == null)
            {
                throw new ArgumentNullException("decimaOctetLexerFactory");
            }

            this.sequenceLexerFactory = sequenceLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
            this.decimaOctetLexerFactory = decimaOctetLexerFactory;
        }

        public ILexer<IPv4Address> Create()
        {
            // dec-octet
            var a = this.decimaOctetLexerFactory.Create();

            // "."
            var b = this.stringLexerFactory.Create(@".");

            // dec-octet "." dec-octet "." dec-octet "." dec-octet
            var c = this.sequenceLexerFactory.Create(a, b, a, b, a, b, a);

            // IPv4address
            return new IPv4AddressLexer(c);
        }
    }
}