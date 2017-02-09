using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.RWS
{
    public class RequiredWhiteSpace : Repetition
    {
        public RequiredWhiteSpace([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}