using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HttpNameToken : Token
    {
        public HttpNameToken(ITextContext context)
            : base("HTTP", context)
        {
        }
    }
}
