using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.transfer_extension
{
    public class TransferExtension : Concatenation
    {
        public TransferExtension([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
