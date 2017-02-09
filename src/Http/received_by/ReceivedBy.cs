using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.received_by
{
    public class ReceivedBy : Alternation
    {
        public ReceivedBy([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
