using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HEXDIG;
using Txt.Core;

namespace Http.chunk_size
{
    public class ChunkSizeLexerFactory : ILexerFactory<ChunkSize>
    {
        private readonly ILexer<HexadecimalDigit> hexadecimalDigitLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public ChunkSizeLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<HexadecimalDigit> hexadecimalDigitLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (hexadecimalDigitLexer == null)
            {
                throw new ArgumentNullException(nameof(hexadecimalDigitLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.hexadecimalDigitLexer = hexadecimalDigitLexer;
        }

        public ILexer<ChunkSize> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(
                hexadecimalDigitLexer,
                1,
                int.MaxValue);
            return new ChunkSizeLexer(innerLexer);
        }
    }
}
