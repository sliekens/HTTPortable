using Txt.ABNF;

namespace Http.request_target
{
    public class RequestTarget : Alternative
    {
        public RequestTarget(Alternative alternative)
            : base(alternative)
        {
        }
    }
}