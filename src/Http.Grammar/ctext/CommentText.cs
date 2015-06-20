namespace Http.Grammar
{
    using TextFx.ABNF;

    public class CommentText : Alternative
    {
        public CommentText(Alternative alternative)
            : base(alternative)
        {
        }
    }
}