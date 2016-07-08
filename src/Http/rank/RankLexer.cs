using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.rank
{
    public sealed class RankLexer : CompositeLexer<Alternation, Rank>
    {
        public RankLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
