using System;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.OCTET;

namespace Http.chunk_data
{
    public class ChunkDataLexerFactory : ILexerFactory<ChunkData>
    {
        private readonly ILexerFactory<Octet> octetLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public ChunkDataLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<Octet> octetLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (octetLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(octetLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.octetLexerFactory = octetLexerFactory;
        }

        public ILexer<ChunkData> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(octetLexerFactory.Create(), 1, int.MaxValue);
            return new ChunkDataLexer(innerLexer);
        }
    }
}