using Txt.ABNF;

namespace Http.transfer_parameter
{
    public class TransferParameter : Concatenation
    {
        public TransferParameter(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}