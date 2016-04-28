using Txt.ABNF;

namespace Http.transfer_coding
{
    public class TransferCoding : Alternation
    {
        public TransferCoding(Alternation alternation)
            : base(alternation)
        {
        }
    }
}
