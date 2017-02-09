using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunked_body
{
    public class ChunkedBody : Concatenation
    {
        public ChunkedBody([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
