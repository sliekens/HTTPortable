using Txt.ABNF;

namespace Http.http_URI
{
    public class HttpUri : Concatenation
    {
        public HttpUri(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}