using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.start_line
{
    public class StartLine : Alternation
    {
        public StartLine([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
