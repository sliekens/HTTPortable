using Txt.ABNF;

namespace Http.chunk_ext_val
{
    public class ChunkExtensionValue : Alternation
    {
        public ChunkExtensionValue(Alternation alternation)
            : base(alternation)
        {
        }
    }
}