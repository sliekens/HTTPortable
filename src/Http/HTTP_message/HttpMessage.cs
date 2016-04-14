using Txt.ABNF;

namespace Http.HTTP_message
{
    public class HttpMessage : Concatenation
    {
        public HttpMessage(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}