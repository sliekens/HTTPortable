using Txt.ABNF;

namespace Http.chunked_body
{
    public class ChunkedBody : Concatenation
    {
        public ChunkedBody(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}