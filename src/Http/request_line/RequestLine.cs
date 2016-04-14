using Txt.ABNF;

namespace Http.request_line
{
    public class RequestLine : Concatenation
    {
        public RequestLine(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
