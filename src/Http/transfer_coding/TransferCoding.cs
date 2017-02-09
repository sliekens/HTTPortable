using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_coding
{
    public class TransferCoding : Alternation
    {
        public TransferCoding([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
