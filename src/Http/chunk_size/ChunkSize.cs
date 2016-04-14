using Txt.ABNF;

namespace Http.chunk_size
{
    public class ChunkSize : Repetition
    {
        public ChunkSize(Repetition repetition)
            : base(repetition)
        {
        }
    }
}