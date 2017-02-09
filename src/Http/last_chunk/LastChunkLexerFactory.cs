using System;
using Http.chunk_ext;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.Core;

namespace Http.last_chunk
{
    public sealed class LastChunkLexerFactory : RuleLexerFactory<LastChunk>
    {
        static LastChunkLexerFactory()
        {
            Default = new LastChunkLexerFactory(
                chunk_ext.ChunkExtensionLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.CRLF.NewLineLexerFactory.Default.Singleton());
        }

        public LastChunkLexerFactory(
            [NotNull] ILexerFactory<ChunkExtension> chunkExtensionLexerFactory,
            [NotNull] ILexerFactory<NewLine> newLineLexerFactory)
        {
            if (chunkExtensionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionLexerFactory));
            }
            if (newLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(newLineLexerFactory));
            }
            ChunkExtensionLexerFactory = chunkExtensionLexerFactory;
            NewLineLexerFactory = newLineLexerFactory;
        }

        [NotNull]
        public static LastChunkLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<ChunkExtension> ChunkExtensionLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<NewLine> NewLineLexerFactory { get; set; }

        public override ILexer<LastChunk> Create()
        {
            var innerLexer = Concatenation.Create(
                Repetition.Create(
                    Terminal.Create(@"0", StringComparer.Ordinal),
                    1,
                    int.MaxValue),
                Option.Create(ChunkExtensionLexerFactory.Create()),
                NewLineLexerFactory.Create());
            return new LastChunkLexer(innerLexer);
        }
    }
}
