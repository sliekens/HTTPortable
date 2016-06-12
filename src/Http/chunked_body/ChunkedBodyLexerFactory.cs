using System;
using Http.chunk;
using Http.last_chunk;
using Http.trailer_part;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.Core;

namespace Http.chunked_body
{
    public class ChunkedBodyLexerFactory : ILexerFactory<ChunkedBody>
    {
        private readonly ILexer<Chunk> chunkLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<LastChunk> lastChunkLexer;

        private readonly ILexer<NewLine> newLineLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<TrailerPart> trailerPartLexer;

        public ChunkedBodyLexerFactory(
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Chunk> chunkLexer,
            [NotNull] ILexer<LastChunk> lastChunkLexer,
            [NotNull] ILexer<TrailerPart> trailerPartLexer,
            [NotNull] ILexer<NewLine> newLineLexer)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (chunkLexer == null)
            {
                throw new ArgumentNullException(nameof(chunkLexer));
            }
            if (lastChunkLexer == null)
            {
                throw new ArgumentNullException(nameof(lastChunkLexer));
            }
            if (trailerPartLexer == null)
            {
                throw new ArgumentNullException(nameof(trailerPartLexer));
            }
            if (newLineLexer == null)
            {
                throw new ArgumentNullException(nameof(newLineLexer));
            }
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.chunkLexer = chunkLexer;
            this.lastChunkLexer = lastChunkLexer;
            this.trailerPartLexer = trailerPartLexer;
            this.newLineLexer = newLineLexer;
        }

        public ILexer<ChunkedBody> Create()
        {
            return
                new ChunkedBodyLexer(
                    concatenationLexerFactory.Create(
                        repetitionLexerFactory.Create(chunkLexer, 0, int.MaxValue),
                        lastChunkLexer,
                        trailerPartLexer,
                        newLineLexer));
        }
    }
}
