using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.status_line
{
    public class StatusLine : Concatenation
    {
        public StatusLine([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
