using Txt.ABNF;

namespace Http.chunk_ext
{
    public class ChunkExtension : Repetition
    {
        public ChunkExtension(Repetition repetition)
            : base(repetition)
        {
        }
    }
}