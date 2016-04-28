using Txt.ABNF;

namespace Http.request_target
{
    public class RequestTarget : Alternation
    {
        public RequestTarget(Alternation alternation)
            : base(alternation)
        {
        }
    }
}