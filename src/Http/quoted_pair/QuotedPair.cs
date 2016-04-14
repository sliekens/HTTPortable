using Txt.ABNF;

namespace Http.quoted_pair
{
    public class QuotedPair : Concatenation
    {
        public QuotedPair(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}