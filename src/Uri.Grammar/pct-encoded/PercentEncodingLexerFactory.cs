namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class PercentEncodingLexerFactory : ILexerFactory<PercentEncoding>
    {
        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        public PercentEncodingLexerFactory(ITerminalLexerFactory terminalLexerFactory, ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory, ISequenceLexerFactory sequenceLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory", "Precondition: terminalLexerFactory != null");
            }

            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException("hexadecimalDigitLexerFactory", "Precondition: hexadecimalDigitLexerFactory != null");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory", "Precondition: sequenceLexerFactory != null");
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.hexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
        }

        public ILexer<PercentEncoding> Create()
        {
            var hexadecimalDigitLexer = this.hexadecimalDigitLexerFactory.Create();
            var percentEncodingAlternativeLexer = this.sequenceLexerFactory.Create(
                this.terminalLexerFactory.Create(@"%"),
                hexadecimalDigitLexer,
                hexadecimalDigitLexer);
            return new PercentEncodingLexer(percentEncodingAlternativeLexer);
        }
    }
}
