using System;
using Http.chunk_ext_name;
using Http.chunk_ext_val;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_ext
{
    public sealed class ChunkExtensionLexerFactory : RuleLexerFactory<ChunkExtension>
    {
        static ChunkExtensionLexerFactory()
        {
            Default = new ChunkExtensionLexerFactory(
                ChunkExtensionNameLexerFactory.Default.Singleton(),
                ChunkExtensionValueLexerFactory.Default.Singleton());
        }

        public ChunkExtensionLexerFactory(
            [NotNull] ILexerFactory<ChunkExtensionName> chunkExtensionName,
            [NotNull] ILexerFactory<ChunkExtensionValue> chunkExtensionValue)
        {
            if (chunkExtensionName == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionName));
            }
            if (chunkExtensionValue == null)
            {
                throw new ArgumentNullException(nameof(chunkExtensionValue));
            }
            ChunkExtensionName = chunkExtensionName;
            ChunkExtensionValue = chunkExtensionValue;
        }

        [NotNull]
        public static ChunkExtensionLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<ChunkExtensionName> ChunkExtensionName { get; set; }

        [NotNull]
        public ILexerFactory<ChunkExtensionValue> ChunkExtensionValue { get; set; }

        public override ILexer<ChunkExtension> Create()
        {
            var innerLexer = Repetition.Create(
                Concatenation.Create(
                    Terminal.Create(@";", StringComparer.Ordinal),
                    ChunkExtensionName.Create(),
                    Option.Create(
                        Concatenation.Create(
                            Terminal.Create(@"=", StringComparer.Ordinal),
                            ChunkExtensionValue.Create()))),
                0,
                int.MaxValue);
            return new ChunkExtensionLexer(innerLexer);
        }
    }
}
