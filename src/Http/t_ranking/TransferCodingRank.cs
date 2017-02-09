using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.t_ranking
{
    public class TransferCodingRank : Concatenation
    {
        public TransferCodingRank([NotNull] Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
