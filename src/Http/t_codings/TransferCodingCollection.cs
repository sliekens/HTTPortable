using Txt.ABNF;

namespace Http.t_codings
{
    public class TransferCodingCollection : Alternative
    {
        public TransferCodingCollection(Alternative alternative)
            : base(alternative)
        {
        }
    }
}