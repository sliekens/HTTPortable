namespace Uri.Grammar
{
    using System;

    using SLANG;
    using SLANG.Core;

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
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (hexadecimalLexerFactory == null)
            {
                throw new ArgumentNullException("hexadecimalLexerFactory");
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.hexadecimalLexerFactory = hexadecimalLexerFactory;
        }

        public ILexer<HexadecimalInt16> Create()
        {
            // HEXDIG
            var a = this.hexadecimalLexerFactory.Create();

            // 1*4HEXDIG
            var b = this.repetitionLexerFactory.Create(a, 1, 4);

            // h16
            return new HexadecimalInt16Lexer(b);
        }
    }
}