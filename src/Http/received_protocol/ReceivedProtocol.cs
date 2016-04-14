using Txt.ABNF;

namespace Http.received_protocol
{
    public class ReceivedProtocol : Concatenation
    {
        public ReceivedProtocol(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
