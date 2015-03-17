namespace Http.Grammar.Rfc7230
{
    using System;

    using SLANG;
    using SLANG.Core;

    public partial class CommentTextLexer : AlternativeLexer<CommentText, HorizontalTab, Space, CommentText.Element3, CommentText.Element4, CommentText.Element5, ObsoletedText>
    {
        private readonly ILexer<HorizontalTab> element1Lexer;

        private readonly ILexer<Space> element2Lexer;

        private readonly ILexer<CommentText.Element3> element3Lexer;

        private readonly ILexer<CommentText.Element4> element4Lexer;

        private readonly ILexer<CommentText.Element5> element5Lexer;

        private readonly ILexer<ObsoletedText> element6Lexer;

        public CommentTextLexer(ILexer<HorizontalTab> element1Lexer, ILexer<Space> element2Lexer, ILexer<CommentText.Element3> element3Lexer, ILexer<CommentText.Element4> element4Lexer, ILexer<CommentText.Element5> element5Lexer, ILexer<ObsoletedText> element6Lexer)
            : base("ctext")
        {
            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
            this.element3Lexer = element3Lexer;
            this.element4Lexer = element4Lexer;
            this.element5Lexer = element5Lexer;
            this.element6Lexer = element6Lexer;
        }

        protected override CommentText CreateInstance1(HorizontalTab element, ITextContext context)
        {
            return new CommentText(element, context);
        }

        protected override CommentText CreateInstance2(Space element, ITextContext context)
        {
            return new CommentText(element, context);
        }

        protected override CommentText CreateInstance3(CommentText.Element3 element, ITextContext context)
        {
            return new CommentText(element, context);
        }

        protected override CommentText CreateInstance4(CommentText.Element4 element, ITextContext context)
        {
            return new CommentText(element, context);
        }

        protected override CommentText CreateInstance5(CommentText.Element5 element, ITextContext context)
        {
            return new CommentText(element, context);
        }

        protected override CommentText CreateInstance6(ObsoletedText element, ITextContext context)
        {
            return new CommentText(element, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out HorizontalTab element)
        {
            return this.element1Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Space element)
        {
            return this.element2Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead3(ITextScanner scanner, out CommentText.Element3 element)
        {
            return this.element3Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead4(ITextScanner scanner, out CommentText.Element4 element)
        {
            return this.element4Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead5(ITextScanner scanner, out CommentText.Element5 element)
        {
            return this.element5Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead6(ITextScanner scanner, out ObsoletedText element)
        {
            return this.element6Lexer.TryRead(scanner, out element);
        }
    }

    public partial class CommentTextLexer
    {
        public class Element3Lexer : Lexer<CommentText.Element3>
        {
            public Element3Lexer()
                : base("anonymous")
            {
            }

            public override bool TryRead(ITextScanner scanner, out CommentText.Element3 element)
            {
                if (scanner.EndOfInput)
                {
                    element = default(CommentText.Element3);
                    return false;
                }

                var context = scanner.GetContext();
                for (char c = '\x21'; c < '\x27'; c++)
                {
                    Element terminal;
                    if (TryReadTerminal(scanner, c, out terminal))
                    {
                        element = new CommentText.Element3(terminal.Data, context);
                        return true;
                    }
                }

                element = default(CommentText.Element3);
                return false;
            }
        }
    }

    public partial class CommentTextLexer
    {
        public class Element4Lexer : Lexer<CommentText.Element4>
        {
            public Element4Lexer()
                : base("anonymous")
            {
            }

            public override bool TryRead(ITextScanner scanner, out CommentText.Element4 element)
            {
                if (scanner.EndOfInput)
                {
                    element = default(CommentText.Element4);
                    return false;
                }

                var context = scanner.GetContext();
                for (char c = '\x2A'; c < '\x5B'; c++)
                {
                    Element terminal;
                    if (TryReadTerminal(scanner, c, out terminal))
                    {
                        element = new CommentText.Element4(terminal.Data, context);
                        return true;
                    }
                }

                element = default(CommentText.Element4);
                return false;
            }
        }
    }

    public partial class CommentTextLexer
    {
        public class Element5Lexer : Lexer<CommentText.Element5>
        {
            public Element5Lexer()
                : base("anonymous")
            {
            }

            public override bool TryRead(ITextScanner scanner, out CommentText.Element5 element)
            {
                if (scanner.EndOfInput)
                {
                    element = default(CommentText.Element5);
                    return false;
                }

                var context = scanner.GetContext();
                for (char c = '\x5D'; c < '\x7E'; c++)
                {
                    Element terminal;
                    if (TryReadTerminal(scanner, c, out terminal))
                    {
                        element = new CommentText.Element5(terminal.Data, context);
                        return true;
                    }
                }

                element = default(CommentText.Element5);
                return false;
            }
        }
    }
}
