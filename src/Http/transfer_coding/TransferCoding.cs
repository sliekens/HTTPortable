using Txt.ABNF;

namespace Http.transfer_coding
{
    public class TransferCoding : Alternative
    {
        public TransferCoding(Alternative alternative)
            : base(alternative)
        {
        }
    }
}
