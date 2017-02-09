using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.reason_phrase
{
    public class ReasonPhrase : Repetition
    {
        public ReasonPhrase([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
