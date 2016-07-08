using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_data
{
    public sealed class ChunkDataLexer : CompositeLexer<Repetition, ChunkData>
    {
        public ChunkDataLexer([NotNull] ILexer<Repetition> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
