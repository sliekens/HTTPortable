using Txt.ABNF;

namespace Http.ctext
{
    public class CommentText : Alternation
    {
        public CommentText(Alternation alternation)
            : base(alternation)
        {
        }
    }
}