using Txt.ABNF;

namespace Http.ctext
{
    public class CommentText : Alternative
    {
        public CommentText(Alternative alternative)
            : base(alternative)
        {
        }
    }
}