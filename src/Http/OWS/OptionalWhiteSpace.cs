using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.OWS
{
    public class OptionalWhiteSpace : Repetition
    {
        public OptionalWhiteSpace([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
