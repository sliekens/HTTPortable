using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.received_protocol
{
    public class ReceivedProtocol : Concatenation
    {
        public ReceivedProtocol([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
