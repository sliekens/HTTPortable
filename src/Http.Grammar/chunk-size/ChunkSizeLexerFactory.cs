namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class ChunkSizeLexerFactory : ILexerFactory<ChunkSize>
    {
        private readonly ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public ChunkSizeLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalDigitLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.hexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
        }

        public ILexer<ChunkSize> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(
                hexadecimalDigitLexerFactory.Create(),
                1,
                int.MaxValue);
            return new ChunkSizeLexer(innerLexer);
        }
    }
}