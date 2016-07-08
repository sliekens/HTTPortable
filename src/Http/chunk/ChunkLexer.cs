using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk
{
    public sealed class ChunkLexer : CompositeLexer<Concatenation, Chunk>
    {
        public ChunkLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
