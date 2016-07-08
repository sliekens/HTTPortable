using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_ext_val
{
    public sealed class ChunkExtensionValueLexer : CompositeLexer<Alternation, ChunkExtensionValue>
    {
        public ChunkExtensionValueLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
