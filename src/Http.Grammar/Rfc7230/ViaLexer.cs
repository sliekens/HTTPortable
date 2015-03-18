namespace Http.Grammar.Rfc7230
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SLANG;

    public partial class ViaLexer : ElementList3Lexer<Via, Via.Sequence>
    {
        public ViaLexer()
            : this(new ElementLexer(new ReceivedProtocolLexer(), new RequiredWhiteSpaceLexer(), new ReceivedByLexer(), new ElementLexer.OptionLexer(new ElementLexer.OptionLexer.SequenceLexer(new RequiredWhiteSpaceLexer(), new CommentLexer()))))
        {
        }

        public ViaLexer(ILexer<Via.Sequence> elementLexer)
            : base("Via", elementLexer)
        {
        }

        public ViaLexer(ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<Via.Sequence> elementLexer)
            : base("Via", optionalWhiteSpaceLexer, elementLexer)
        {
        }

        public ViaLexer(ILexer<Repetition<Sequence<Element, OptionalWhiteSpace>>> element1Lexer, ILexer<Via.Sequence> element2Lexer, ILexer<Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Via.Sequence>>>>> element3Lexer)
            : base("Via", element1Lexer, element2Lexer, element3Lexer)
        {
        }

        protected override Via CreateInstance(Repetition<Sequence<Element, OptionalWhiteSpace>> element1, Via.Sequence element2, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, Via.Sequence>>>> element3, ITextContext context)
        {
            return new Via(element1, element2, element3, context);
        }
    }

    public partial class ViaLexer
    {
        public partial class ElementLexer : SequenceLexer<Via.Sequence, ReceivedProtocol, RequiredWhiteSpace, ReceivedBy, Via.Sequence.Option>
        {
            private readonly ILexer<ReceivedProtocol> element1Lexer;

            private readonly ILexer<RequiredWhiteSpace> element2Lexer;

            private readonly ILexer<ReceivedBy> element3Lexer;

            private readonly ILexer<Via.Sequence.Option> element4Lexer;

            public ElementLexer(ILexer<ReceivedProtocol> element1Lexer,
                ILexer<RequiredWhiteSpace> element2Lexer,
                ILexer<ReceivedBy> element3Lexer,
                ILexer<Via.Sequence.Option> element4Lexer
                )
                : base("anonymous")
            {
                this.element1Lexer = element1Lexer;
                this.element2Lexer = element2Lexer;
                this.element3Lexer = element3Lexer;
                this.element4Lexer = element4Lexer;
            }

            protected override Via.Sequence CreateInstance(
                ReceivedProtocol element1,
                RequiredWhiteSpace element2,
                ReceivedBy element3,
                Via.Sequence.Option element4,
                ITextContext context)
            {
                return new Via.Sequence(element1, element2, element3, element4, context);
            }

            protected override bool TryRead1(ITextScanner scanner, out ReceivedProtocol element)
            {
                return this.element1Lexer.TryRead(scanner, out element);
            }

            protected override bool TryRead2(ITextScanner scanner, out RequiredWhiteSpace element)
            {
                return this.element2Lexer.TryRead(scanner, out element);
            }

            protected override bool TryRead3(ITextScanner scanner, out ReceivedBy element)
            {
                return this.element3Lexer.TryRead(scanner, out element);
            }

            protected override bool TryRead4(ITextScanner scanner, out Via.Sequence.Option element)
            {
                return this.element4Lexer.TryRead(scanner, out element);
            }
        }
    }

    public partial class ViaLexer
    {
        public partial class ElementLexer
        {
            public partial class OptionLexer : OptionLexer<Via.Sequence.Option, Via.Sequence.Option.Sequence>
            {
                private readonly ILexer<Via.Sequence.Option.Sequence> elementLexer;

                public OptionLexer(ILexer<Via.Sequence.Option.Sequence> elementLexer)
                    : base("anonymous")
                {
                    this.elementLexer = elementLexer;
                }

                protected override Via.Sequence.Option CreateInstance(IList<Via.Sequence.Option.Sequence> elements, int lowerBound, int upperBound, ITextContext context)
                {
                    var element = elements.SingleOrDefault();
                    if (element == null)
                    {
                        return new Via.Sequence.Option(context);
                    }

                    return new Via.Sequence.Option(element, context);
                }

                protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out Via.Sequence.Option.Sequence element)
                {
                    return this.elementLexer.TryRead(scanner, out element);
                }
            }
        }
    }

    public partial class ViaLexer
    {
        public partial class ElementLexer
        {
            public partial class OptionLexer
            {
                public class SequenceLexer : SequenceLexer<Via.Sequence.Option.Sequence, RequiredWhiteSpace, Comment>
                {
                    private readonly ILexer<RequiredWhiteSpace> element1Lexer;

                    private readonly ILexer<Comment> element2Lexer;

                    public SequenceLexer(ILexer<RequiredWhiteSpace> element1Lexer, ILexer<Comment> element2Lexer)
                        : base("anonymous")
                    {
                        this.element1Lexer = element1Lexer;
                        this.element2Lexer = element2Lexer;
                    }

                    protected override Via.Sequence.Option.Sequence CreateInstance(RequiredWhiteSpace element1, Comment element2, ITextContext context)
                    {
                        return new Via.Sequence.Option.Sequence(element1, element2, context);
                    }

                    protected override bool TryRead1(ITextScanner scanner, out RequiredWhiteSpace element)
                    {
                        return this.element1Lexer.TryRead(scanner, out element);
                    }

                    protected override bool TryRead2(ITextScanner scanner, out Comment element)
                    {
                        return this.element2Lexer.TryRead(scanner, out element);
                    }
                }
            }
        }
    }
}
