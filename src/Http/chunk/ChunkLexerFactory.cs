using System;
using Http.chunk_data;
using Http.chunk_ext;
using Http.chunk_size;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.Core;

namespace Http.chunk
{
    public class ChunkLexerFactory : ILexerFactory<Chunk>
    {
        private readonly ILexer<ChunkData> chunkDataLexer;

        private readonly ILexer<ChunkExtension> chunkExtensionLexer;

        private readonly ILexer<ChunkSize> chunkSizeLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<NewLine> newLineLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        public ChunkLexerFactory(
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<ChunkSize> chunkSizeLexer,
            [NotNull] ILexer<ChunkExtension> chunkExtensionLexer,
            [NotNull] ILexer<NewLine> newLineLexer,
            [NotNull] ILexer<ChunkData> chunkDataLexer)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (chunkSizeLexer == null)
            {
                throw new ArgumentNullException(nameof(chunkSizeLexer));
            }
            if (chunkExtensionLexer == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionLexer));
            }
            if (newLineLexer == null)
            {
                throw new ArgumentNullException(nameof(newLineLexer));
            }
            if (chunkDataLexer == null)
            {
                throw new ArgumentNullException(nameof(chunkDataLexer));
            }
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.chunkSizeLexer = chunkSizeLexer;
            this.chunkExtensionLexer = chunkExtensionLexer;
            this.newLineLexer = newLineLexer;
            this.chunkDataLexer = chunkDataLexer;
        }

        public ILexer<Chunk> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                chunkSizeLexer,
                optionLexerFactory.Create(chunkExtensionLexer),
                newLineLexer,
                chunkDataLexer,
                newLineLexer);
            return new ChunkLexer(innerLexer);
        }
    }
}
