namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public partial class Comment : Sequence<Comment.Element1, Repetition<Sequence<CommentText, QuotedPair, Comment>>, CommentText.Element3>
    {
        public Comment(Element1 element1, Repetition<Sequence<CommentText, QuotedPair, Comment>> element2, CommentText.Element3 element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
        }
    }

    public partial class Comment
    {
        public class Element1 : Element
        {
            public Element1(char data, ITextContext context)
                : base(data, context)
            {
            }

            public Element1(string data, ITextContext context)
                : base(data, context)
            {
            }
        }
    }

    public partial class Comment
    {
        public class Element3 : Element
        {
            public Element3(char data, ITextContext context)
                : base(data, context)
            {
            }

            public Element3(string data, ITextContext context)
                : base(data, context)
            {
            }
        }
    }
}
