using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.last_chunk
{
    public sealed class LastChunkLexer : CompositeLexer<Concatenation, LastChunk>
    {
        public LastChunkLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
