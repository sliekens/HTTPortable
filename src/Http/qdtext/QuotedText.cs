using Txt.ABNF;

namespace Http.qdtext
{
    public class QuotedText : Alternative
    {
        public QuotedText(Alternative alternative)
            : base(alternative)
        {
        }
    }
}