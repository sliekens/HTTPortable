using System;
using Http.chunk_ext_name;
using Http.chunk_ext_val;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_ext
{
    public class ChunkExtensionLexerFactory : ILexerFactory<ChunkExtension>
    {
        private readonly ILexer<ChunkExtensionName> chunkExtensionNameLexer;

        private readonly ILexer<ChunkExtensionValue> chunkExtensionValueLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public ChunkExtensionLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<ChunkExtensionName> chunkExtensionNameLexer,
            [NotNull] ILexer<ChunkExtensionValue> chunkExtensionValueLexer)
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
            if (chunkExtensionNameLexer == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionNameLexer));
            }
            if (chunkExtensionValueLexer == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionValueLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.chunkExtensionNameLexer = chunkExtensionNameLexer;
            this.chunkExtensionValueLexer = chunkExtensionValueLexer;
        }

        public ILexer<ChunkExtension> Create()
        {
            var innerLexer =
                repetitionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@";", StringComparer.Ordinal),
                        chunkExtensionNameLexer,
                        optionLexerFactory.Create(
                            concatenationLexerFactory.Create(
                                terminalLexerFactory.Create(@"=", StringComparer.Ordinal),
                                chunkExtensionValueLexer))),
                    0,
                    int.MaxValue);
            return new ChunkExtensionLexer(innerLexer);
        }
    }
}
