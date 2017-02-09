using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.rank
{
    public class Rank : Alternation
    {
        public Rank([NotNull] Alternation alternation)
            : base(alternation)
        {
        }
    }
}
