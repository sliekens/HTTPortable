using Txt.ABNF;

namespace Http.t_codings
{
    public class TransferCodingCollection : Alternation
    {
        public TransferCodingCollection(Alternation alternation)
            : base(alternation)
        {
        }
    }
}