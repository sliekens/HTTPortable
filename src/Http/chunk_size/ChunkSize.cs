using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.chunk_size
{
    public class ChunkSize : Repetition
    {
        public ChunkSize([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
