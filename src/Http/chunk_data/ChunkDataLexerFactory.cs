using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.OCTET;
using Txt.Core;

namespace Http.chunk_data
{
    public class ChunkDataLexerFactory : ILexerFactory<ChunkData>
    {
        private readonly ILexer<Octet> octetLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public ChunkDataLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Octet> octetLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (octetLexer == null)
            {
                throw new ArgumentNullException(nameof(octetLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.octetLexer = octetLexer;
        }

        public ILexer<ChunkData> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(octetLexer, 1, int.MaxValue);
            return new ChunkDataLexer(innerLexer);
        }
    }
}
