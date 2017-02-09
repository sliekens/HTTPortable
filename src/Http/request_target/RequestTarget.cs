using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.request_target
{
    public class RequestTarget : Alternation
    {
        public RequestTarget([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
