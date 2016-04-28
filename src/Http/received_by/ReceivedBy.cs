using Txt.ABNF;

namespace Http.received_by
{
    public class ReceivedBy : Alternation
    {
        public ReceivedBy(Alternation alternation)
            : base(alternation)
        {
        }
    }
}
