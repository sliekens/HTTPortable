using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.trailer_part
{
    public class TrailerPart : Repetition
    {
        public TrailerPart([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
