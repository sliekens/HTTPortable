using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.request_line
{
    public class RequestLine : Concatenation
    {
        public RequestLine([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
