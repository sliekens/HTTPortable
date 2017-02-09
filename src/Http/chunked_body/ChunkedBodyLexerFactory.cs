using System;
using Http.chunk;
using Http.last_chunk;
using Http.trailer_part;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.Core;

namespace Http.chunked_body
{
    public sealed class ChunkedBodyLexerFactory : RuleLexerFactory<ChunkedBody>
    {
        static ChunkedBodyLexerFactory()
        {
            Default = new ChunkedBodyLexerFactory(
                chunk.ChunkLexerFactory.Default.Singleton(),
                last_chunk.LastChunkLexerFactory.Default.Singleton(),
                trailer_part.TrailerPartLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.CRLF.NewLineLexerFactory.Default.Singleton());
        }

        public ChunkedBodyLexerFactory(
            [NotNull] ILexerFactory<Chunk> chunkLexerFactory,
            [NotNull] ILexerFactory<LastChunk> lastChunkLexerFactory,
            [NotNull] ILexerFactory<TrailerPart> trailerPartLexerFactory,
            [NotNull] ILexerFactory<NewLine> newLineLexerFactory)
        {
            if (chunkLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(chunkLexerFactory));
            }
            if (lastChunkLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(lastChunkLexerFactory));
            }
            if (trailerPartLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(trailerPartLexerFactory));
            }
            if (newLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(newLineLexerFactory));
            }
            ChunkLexerFactory = chunkLexerFactory;
            LastChunkLexerFactory = lastChunkLexerFactory;
            TrailerPartLexerFactory = trailerPartLexerFactory;
            NewLineLexerFactory = newLineLexerFactory;
        }

        [NotNull]
        public static ChunkedBodyLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Chunk> ChunkLexerFactory { get; }

        [NotNull]
        public ILexerFactory<LastChunk> LastChunkLexerFactory { get; }

        [NotNull]
        public ILexerFactory<NewLine> NewLineLexerFactory { get; }

        [NotNull]
        public ILexerFactory<TrailerPart> TrailerPartLexerFactory { get; }

        public override ILexer<ChunkedBody> Create()
        {
            var innerLexer = Concatenation.Create(
                Repetition.Create(ChunkLexerFactory.Create(), 0, int.MaxValue),
                LastChunkLexerFactory.Create(),
                TrailerPartLexerFactory.Create(),
                NewLineLexerFactory.Create());
            return new ChunkedBodyLexer(innerLexer);
        }
    }
}
