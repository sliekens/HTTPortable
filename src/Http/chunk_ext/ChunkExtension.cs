using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.chunk_ext
{
    public class ChunkExtension : Repetition
    {
        public ChunkExtension([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
