using System;
using Http.chunk_ext;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;

namespace Http.last_chunk
{
    public class LastChunkLexerFactory : ILexerFactory<LastChunk>
    {
        private readonly ILexer<ChunkExtension> chunkExtensionLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<NewLine> newLineLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public LastChunkLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<ChunkExtension> chunkExtensionLexer,
            [NotNull] ILexer<NewLine> newLineLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (chunkExtensionLexer == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionLexer));
            }
            if (newLineLexer == null)
            {
                throw new ArgumentNullException(nameof(newLineLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.chunkExtensionLexer = chunkExtensionLexer;
            this.newLineLexer = newLineLexer;
        }

        public ILexer<LastChunk> Create()
        {
            var innerLexer =
                concatenationLexerFactory.Create(
                    repetitionLexerFactory.Create(
                        terminalLexerFactory.Create(@"0", StringComparer.Ordinal),
                        1,
                        int.MaxValue),
                    optionLexerFactory.Create(chunkExtensionLexer),
                    newLineLexer);
            return new LastChunkLexer(innerLexer);
        }
    }
}
