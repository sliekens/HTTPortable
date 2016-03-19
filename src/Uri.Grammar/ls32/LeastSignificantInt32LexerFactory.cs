namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class LeastSignificantInt32LexerFactory : ILexerFactory<LeastSignificantInt32>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory;

        private readonly ILexerFactory<IPv4Address> ipv4AddressLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public LeastSignificantInt32LexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory,
            ILexerFactory<IPv4Address> ipv4AddressLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (hexadecimalInt16LexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalInt16LexerFactory));
            }

            if (ipv4AddressLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ipv4AddressLexerFactory));
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.hexadecimalInt16LexerFactory = hexadecimalInt16LexerFactory;
            this.ipv4AddressLexerFactory = ipv4AddressLexerFactory;
        }

        public ILexer<LeastSignificantInt32> Create()
        {
            // h16
            var a = this.hexadecimalInt16LexerFactory.Create();

            // ":"
            var b = this.terminalLexerFactory.Create(@":", StringComparer.Ordinal);

            // h16 ":" h16
            var c = this.concatenationLexerFactory.Create(a, b, a);

            // IPv4address
            var d = this.ipv4AddressLexerFactory.Create();

            // ( h16 ":" h16 ) / IPv4address
            var e = this.alternativeLexerFactory.Create(c, d);

            // ls32
            return new LeastSignificantInt32Lexer(e);
        }
    }
}