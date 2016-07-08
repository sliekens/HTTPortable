using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_size
{
    public sealed class ChunkSizeLexer : CompositeLexer<Repetition, ChunkSize>
    {
        public ChunkSizeLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
