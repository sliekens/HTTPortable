using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.chunk_data
{
    public class ChunkData : Repetition
    {
        public ChunkData([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
