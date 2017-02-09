using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.transfer_parameter
{
    public class TransferParameter : Concatenation
    {
        public TransferParameter([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
