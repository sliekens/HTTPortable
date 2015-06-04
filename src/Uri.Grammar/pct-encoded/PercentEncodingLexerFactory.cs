namespace Uri.Grammar.pct_encoded
{
    using System;

    using SLANG;
    using SLANG.Core.HEXDIG;

    public class PercentEncodingLexerFactory : ILexerFactory<PercentEncoding>
    {
        private readonly IStringLexerFactory stringLexerFactory;

        private readonly ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        public PercentEncodingLexerFactory(IStringLexerFactory stringLexerFactory, ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory, ISequenceLexerFactory sequenceLexerFactory)
        {
            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException("hexadecimalDigitLexerFactory", "Precondition: hexadecimalDigitLexerFactory != null");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory", "Precondition: sequenceLexerFactory != null");
            }

            this.stringLexerFactory = stringLexerFactory;
            this.hexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
        }

        public ILexer<PercentEncoding> Create()
        {
            var hexadecimalDigitLexer = this.hexadecimalDigitLexerFactory.Create();
            var percentEncodingAlternativeLexer = this.sequenceLexerFactory.Create(
                this.stringLexerFactory.Create(@"%"),
                hexadecimalDigitLexer,
                hexadecimalDigitLexer);
            return new PercentEncodingLexer(percentEncodingAlternativeLexer);
        }
    }
}
