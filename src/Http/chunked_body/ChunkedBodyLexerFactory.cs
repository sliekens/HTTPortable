using Http.chunk;
using Http.last_chunk;
using Http.trailer_part;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;

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
