using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_ext
{
    public sealed class ChunkExtensionLexer : CompositeLexer<Repetition, ChunkExtension>
    {
        public ChunkExtensionLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
