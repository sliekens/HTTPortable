using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.chunk
{
    public class Chunk : Concatenation
    {
        public Chunk([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
