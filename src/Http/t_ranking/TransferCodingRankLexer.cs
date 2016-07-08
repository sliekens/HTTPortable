using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.t_ranking
{
    public sealed class TransferCodingRankLexer : CompositeLexer<Concatenation, TransferCodingRank>
    {
        public TransferCodingRankLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
