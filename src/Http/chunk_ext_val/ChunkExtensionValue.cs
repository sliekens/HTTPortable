using Txt.ABNF;

namespace Http.chunk_ext_val
{
    public class ChunkExtensionValue : Alternative
    {
        public ChunkExtensionValue(Alternative alternative)
            : base(alternative)
        {
        }
    }
}