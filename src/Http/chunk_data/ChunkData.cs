using Txt.ABNF;

namespace Http.chunk_data
{
    public class ChunkData : Repetition
    {
        public ChunkData(Repetition repetition)
            : base(repetition)
        {
        }
    }
}