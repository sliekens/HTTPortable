using Txt.ABNF;

namespace Http.rank
{
    public class Rank : Alternation
    {
        public Rank(Alternation alternation)
            : base(alternation)
        {
        }
    }
}