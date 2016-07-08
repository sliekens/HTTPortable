using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunked_body
{
    public sealed class ChunkedBodyLexer : CompositeLexer<Concatenation, ChunkedBody>
    {
        public ChunkedBodyLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
