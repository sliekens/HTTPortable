using System;
using Http.chunk_data;
using Http.chunk_ext;
using Http.chunk_size;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;

namespace Http.chunk
{
    public class ChunkLexerFactory : ILexerFactory<Chunk>
    {
        private readonly ILexerFactory<ChunkData> chunkDataLexerFactory;

        private readonly ILexerFactory<ChunkExtension> chunkExtensionLexerFactory;

        private readonly ILexerFactory<ChunkSize> chunkSizeLexerFactory;

        private readonly ILexerFactory<EndOfLine> endOfLineLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        public ChunkLexerFactory(
            IConcatenationLexerFactory concatenationLexerFactory,
            ILexerFactory<ChunkSize> chunkSizeLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ILexerFactory<ChunkExtension> chunkExtensionLexerFactory,
            ILexerFactory<EndOfLine> endOfLineLexerFactory,
            ILexerFactory<ChunkData> chunkDataLexerFactory)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (chunkSizeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(chunkSizeLexerFactory));
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (chunkExtensionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionLexerFactory));
            }

            if (endOfLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(endOfLineLexerFactory));
            }

            if (chunkDataLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(chunkDataLexerFactory));
            }

            this.concatenationLexerFactory = concatenationLexerFactory;
            this.chunkSizeLexerFactory = chunkSizeLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.chunkExtensionLexerFactory = chunkExtensionLexerFactory;
            this.endOfLineLexerFactory = endOfLineLexerFactory;
            this.chunkDataLexerFactory = chunkDataLexerFactory;
        }

        public ILexer<Chunk> Create()
        {
            var a = chunkSizeLexerFactory.Create();
            var b = chunkExtensionLexerFactory.Create();
            var c = optionLexerFactory.Create(b);
            var d = endOfLineLexerFactory.Create();
            var e = chunkDataLexerFactory.Create();
            var innerLexer = concatenationLexerFactory.Create(a, c, d, e, d);
            return new ChunkLexer(innerLexer);
        }
    }
}