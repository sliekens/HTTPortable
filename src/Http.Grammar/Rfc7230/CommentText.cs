namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using SLANG.Core;

    public class CommentText : Alternative<HorizontalTab, Space, Element, ObsoletedText>
    {
        public CommentText(HorizontalTab element, ITextContext context)
            : base(element, context)
        {
        }

        public CommentText(Space element, ITextContext context)
            : base(element, context)
        {
        }

        public CommentText(Element element, ITextContext context)
            : base(element, context)
        {
        }

        public CommentText(ObsoletedText element, ITextContext context)
            : base(element, context)
        {
        }
    }
}
