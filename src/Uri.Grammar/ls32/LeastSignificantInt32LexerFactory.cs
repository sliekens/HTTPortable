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

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public LeastSignificantInt32LexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<HexadecimalInt16> hexadecimalInt16LexerFactory,
            ILexerFactory<IPv4Address> ipv4AddressLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (hexadecimalInt16LexerFactory == null)
            {
                throw new ArgumentNullException("hexadecimalInt16LexerFactory");
            }

            if (ipv4AddressLexerFactory == null)
            {
                throw new ArgumentNullException("ipv4AddressLexerFactory");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.hexadecimalInt16LexerFactory = hexadecimalInt16LexerFactory;
            this.ipv4AddressLexerFactory = ipv4AddressLexerFactory;
        }

        public ILexer<LeastSignificantInt32> Create()
        {
            // h16
            var a = this.hexadecimalInt16LexerFactory.Create();

            // ":"
            var b = this.terminalLexerFactory.Create(@":");

            // h16 ":" h16
            var c = this.sequenceLexerFactory.Create(a, b, a);

            // IPv4address
            var d = this.ipv4AddressLexerFactory.Create();

            // ( h16 ":" h16 ) / IPv4address
            var e = this.alternativeLexerFactory.Create(c, d);

            // ls32
            return new LeastSignificantInt32Lexer(e);
        }
    }
}