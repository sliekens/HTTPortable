using Txt.ABNF;

namespace Http.quoted_string
{
    public class QuotedString : Concatenation
    {
        public QuotedString(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}