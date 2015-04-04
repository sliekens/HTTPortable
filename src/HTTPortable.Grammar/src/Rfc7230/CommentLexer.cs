namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;

    using SLANG;

    public partial class CommentLexer : SequenceLexer<Comment, Comment.Element1, Repetition<Sequence<CommentText, QuotedPair, Comment>>, CommentText.Element3>
    {
        private readonly ILexer<Comment.Element1> element1Lexer;

        private readonly ILexer<Repetition<Sequence<CommentText, QuotedPair, Comment>>> element2Lexer;

        private readonly ILexer<CommentText.Element3> element3Lexer;

        public CommentLexer()
            : this(new Element1Lexer(), new Element2Lexer(new Element2Lexer.ElementLexer(new CommentTextLexer(), new QuotedPairLexer(), new CommentLexer())), new CommentTextLexer.Element3Lexer())
        {
        }

        public CommentLexer(
            ILexer<Comment.Element1> element1Lexer,
            ILexer<Repetition<Sequence<CommentText, QuotedPair, Comment>>> element2Lexer,
            ILexer<CommentText.Element3> element3Lexer)
            : base("comment")
        {
            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
            this.element3Lexer = element3Lexer;
        }

        protected override Comment CreateInstance(Comment.Element1 element1, Repetition<Sequence<CommentText, QuotedPair, Comment>> element2, CommentText.Element3 element3, ITextContext context)
        {
            return new Comment(element1, element2, element3, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out Comment.Element1 element)
        {
            return this.element1Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Repetition<Sequence<CommentText, QuotedPair, Comment>> element)
        {
            return this.element2Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead3(ITextScanner scanner, out CommentText.Element3 element)
        {
            return this.element3Lexer.TryRead(scanner, out element);
        }
    }

    public partial class CommentLexer
    {
        public class Element1Lexer : Lexer<Comment.Element1>
        {
            public override bool TryRead(ITextScanner scanner, out Comment.Element1 element)
            {
                var context = scanner.GetContext();
                Element terminal;
                if (TryReadTerminal(scanner, @"(", out terminal))
                {
                    element = new Comment.Element1(terminal.Data, context);
                    return true;
                }

                element = default(Comment.Element1);
                return false;
            }
        }
    }

    public partial class CommentLexer
    {
        public partial class Element2Lexer : RepetitionLexer<Repetition<Sequence<CommentText, QuotedPair, Comment>>, Sequence<CommentText, QuotedPair, Comment>>
        {
            private readonly ILexer<Sequence<CommentText, QuotedPair, Comment>> elementLexer;

            public Element2Lexer(ILexer<Sequence<CommentText, QuotedPair, Comment>> elementLexer)
                : base(0, int.MaxValue)
            {
                this.elementLexer = elementLexer;
            }

            protected override Repetition<Sequence<CommentText, QuotedPair, Comment>> CreateInstance(IList<Sequence<CommentText, QuotedPair, Comment>> elements, int lowerBound, int upperBound, ITextContext context)
            {
                return new Repetition<Sequence<CommentText, QuotedPair, Comment>>(elements, lowerBound, upperBound, context);
            }

            protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out Sequence<CommentText, QuotedPair, Comment> element)
            {
                return this.elementLexer.TryRead(scanner, out element);
            }
        }
    }

    public partial class CommentLexer
    {
        public partial class Element2Lexer
        {
            public partial class ElementLexer : SequenceLexer<Sequence<CommentText, QuotedPair, Comment>, CommentText, QuotedPair, Comment>
            {
                private readonly ILexer<CommentText> element1Lexer;

                private readonly ILexer<QuotedPair> element2Lexer;

                private readonly ILexer<Comment> element3Lexer;

                public ElementLexer(ILexer<CommentText> element1Lexer, ILexer<QuotedPair> element2Lexer, ILexer<Comment> element3Lexer)
                {
                    this.element1Lexer = element1Lexer;
                    this.element2Lexer = element2Lexer;
                    this.element3Lexer = element3Lexer;
                }

                protected override Sequence<CommentText, QuotedPair, Comment> CreateInstance(CommentText element1, QuotedPair element2, Comment element3, ITextContext context)
                {
                    return new Sequence<CommentText, QuotedPair, Comment>(element1, element2, element3, context);
                }

                protected override bool TryRead1(ITextScanner scanner, out CommentText element)
                {
                    return this.element1Lexer.TryRead(scanner, out element);
                }

                protected override bool TryRead2(ITextScanner scanner, out QuotedPair element)
                {
                    return this.element2Lexer.TryRead(scanner, out element);
                }

                protected override bool TryRead3(ITextScanner scanner, out Comment element)
                {
                    return this.element3Lexer.TryRead(scanner, out element);
                }
            }
        }
    }

    public partial class CommentLexer
    {
        public class Element3Lexer : Lexer<Comment.Element1>
        {
            public override bool TryRead(ITextScanner scanner, out Comment.Element1 element)
            {
                var context = scanner.GetContext();
                Element terminal;
                if (TryReadTerminal(scanner, @")", out terminal))
                {
                    element = new Comment.Element1(terminal.Data, context);
                    return true;
                }

                element = default(Comment.Element1);
                return false;
            }
        }
    }
}
