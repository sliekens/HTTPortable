using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HttpName : Element
    {
        public HttpName(ITextContext context)
            : base("HTTP", context)
        {
        }
    }
}
