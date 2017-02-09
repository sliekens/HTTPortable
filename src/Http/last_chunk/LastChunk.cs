using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.last_chunk
{
    public class LastChunk : Concatenation
    {
        public LastChunk([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
