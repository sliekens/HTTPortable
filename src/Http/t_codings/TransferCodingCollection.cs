using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.t_codings
{
    public class TransferCodingCollection : Alternation
    {
        public TransferCodingCollection([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
