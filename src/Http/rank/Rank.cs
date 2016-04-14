using Txt.ABNF;

namespace Http.rank
{
    public class Rank : Alternative
    {
        public Rank(Alternative alternative)
            : base(alternative)
        {
        }
    }
}