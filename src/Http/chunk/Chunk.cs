using Txt.ABNF;

namespace Http.chunk
{
    public class Chunk : Concatenation
    {
        public Chunk(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}