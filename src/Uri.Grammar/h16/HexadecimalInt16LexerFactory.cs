namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class HexadecimalInt16LexerFactory : ILexerFactory<HexadecimalInt16>
    {
        private readonly ILexerFactory<HexadecimalDigit> hexadecimalLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public HexadecimalInt16LexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<HexadecimalDigit> hexadecimalLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (hexadecimalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.hexadecimalLexerFactory = hexadecimalLexerFactory;
        }

        public ILexer<HexadecimalInt16> Create()
        {
            // HEXDIG
            var a = hexadecimalLexerFactory.Create();

            // 1*4HEXDIG
            var b = repetitionLexerFactory.Create(a, 1, 4);

            // h16
            return new HexadecimalInt16Lexer(b);
        }
    }
}