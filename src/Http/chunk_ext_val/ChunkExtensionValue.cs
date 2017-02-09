using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.chunk_ext_val
{
    public class ChunkExtensionValue : Alternation
    {
        public ChunkExtensionValue([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
