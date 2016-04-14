using Txt.ABNF;

namespace Http.received_by
{
    public class ReceivedBy : Alternative
    {
        public ReceivedBy(Alternative alternative)
            : base(alternative)
        {
        }
    }
}
