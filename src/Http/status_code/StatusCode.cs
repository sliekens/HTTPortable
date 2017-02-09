using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.status_code
{
    public class StatusCode : Repetition
    {
        public StatusCode([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
