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

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        public PercentEncodingLexerFactory(ITerminalLexerFactory terminalLexerFactory, ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory, IConcatenationLexerFactory concatenationLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory", "Precondition: terminalLexerFactory != null");
            }

            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException("hexadecimalDigitLexerFactory", "Precondition: hexadecimalDigitLexerFactory != null");
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException("concatenationLexerFactory", "Precondition: concatenationLexerFactory != null");
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.hexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
        }

        public ILexer<PercentEncoding> Create()
        {
            var hexadecimalDigitLexer = this.hexadecimalDigitLexerFactory.Create();
            var percentEncodingAlternativeLexer = this.concatenationLexerFactory.Create(
                this.terminalLexerFactory.Create(@"%", StringComparer.Ordinal),
                hexadecimalDigitLexer,
                hexadecimalDigitLexer);
            return new PercentEncodingLexer(percentEncodingAlternativeLexer);
        }
    }
}
