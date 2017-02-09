using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.token
{
    public class Token : Repetition
    {
        public Token([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
