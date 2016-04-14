using Txt.ABNF;

namespace Http.last_chunk
{
    public class LastChunk : Concatenation
    {
        public LastChunk(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}