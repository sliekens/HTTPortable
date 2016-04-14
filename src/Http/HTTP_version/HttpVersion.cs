using Txt.ABNF;

namespace Http.HTTP_version
{
    public class HttpVersion : Concatenation
    {
        public HttpVersion(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}