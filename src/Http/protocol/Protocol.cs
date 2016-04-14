using Txt.ABNF;

namespace Http.protocol
{
    public class Protocol : Concatenation
    {
        public Protocol(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}