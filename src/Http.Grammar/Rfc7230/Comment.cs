namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class Comment : Sequence<Element, Repetition<Sequence<CommentText, QuotedPair, Comment>>, Element>
    {
        public Comment(Element element1, Repetition<Sequence<CommentText, QuotedPair, Comment>> element2, Element element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
        }
    }
}
