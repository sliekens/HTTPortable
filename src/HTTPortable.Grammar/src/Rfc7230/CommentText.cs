namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using SLANG.Core;

    public partial class CommentText : Alternative<HorizontalTab, Space, CommentText.Element3, CommentText.Element4, CommentText.Element5, ObsoletedText>
    {
        public CommentText(HorizontalTab element, ITextContext context)
            : base(element, 1, context)
        {
        }

        public CommentText(Space element, ITextContext context)
            : base(element, 2, context)
        {
        }

        public CommentText(Element3 element, ITextContext context)
            : base(element, 3, context)
        {
        }

        public CommentText(Element4 element, ITextContext context)
            : base(element, 4, context)
        {
        }

        public CommentText(Element5 element, ITextContext context)
            : base(element, 5, context)
        {
        }

        public CommentText(ObsoletedText element, ITextContext context)
            : base(element, 6, context)
        {
        }
    }

    public partial class CommentText
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

    public partial class CommentText
    {
        public class Element4 : Element
        {
            public Element4(char data, ITextContext context)
                : base(data, context)
            {
            }

            public Element4(string data, ITextContext context)
                : base(data, context)
            {
            }
        }
    }
    
    public partial class CommentText
    {
        public class Element5 : Element
        {
            public Element5(char data, ITextContext context)
                : base(data, context)
            {
            }

            public Element5(string data, ITextContext context)
                : base(data, context)
            {
            }
        }
    }
}
