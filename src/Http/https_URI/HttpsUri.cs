using Txt.ABNF;

namespace Http.https_URI
{
    public class HttpsUri : Concatenation
    {
        public HttpsUri(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}