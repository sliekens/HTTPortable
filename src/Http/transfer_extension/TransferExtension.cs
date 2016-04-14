using Txt.ABNF;

namespace Http.transfer_extension
{
    public class TransferExtension : Concatenation
    {
        public TransferExtension(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}