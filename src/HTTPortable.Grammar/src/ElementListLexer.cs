namespace Http.Grammar
{
    using System.Collections.Generic;

    using Http.Grammar.Rfc7230;

    using SLANG;

    public abstract partial class ElementListLexer<TList, TElement> : SequenceLexer<TList, TElement, Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>>>
        where TList : ElementList<TElement>
        where TElement : Element
    {
        private readonly ILexer<TElement> element1Lexer;
        private readonly ILexer<Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>>> element2Lexer;

        protected ElementListLexer(string ruleName, ILexer<TElement> elementLexer)
            : this(ruleName, elementLexer, new OptionalWhiteSpaceLexer())
        {
        }

        protected ElementListLexer(string ruleName, ILexer<TElement> elementLexer, ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer)
            : this(ruleName, elementLexer, new Element2Lexer(new Element2Lexer.ElementLexer(optionalWhiteSpaceLexer, new Element2Lexer.ElementLexer.Element2Lexer(), optionalWhiteSpaceLexer, elementLexer)))
        {
        }

        protected ElementListLexer(string ruleName, ILexer<TElement> element1Lexer, ILexer<Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>>> element2Lexer)
            : base(ruleName)
        {
            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
        }

        protected override bool TryRead1(ITextScanner scanner, out TElement element)
        {
            return this.element1Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>> element)
        {
            return this.element2Lexer.TryRead(scanner, out element);
        }
    }

    public abstract partial class ElementListLexer<TList, TElement>
    {
        public partial class Element2Lexer : RepetitionLexer<Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>>, Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>>
        {
            private readonly ILexer<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>> elementLexer;

            public Element2Lexer(ILexer<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>> elementLexer)
                : base(0, int.MaxValue)
            {
                this.elementLexer = elementLexer;
            }

            protected override Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>> CreateInstance(IList<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>> elements, int lowerBound, int upperBound, ITextContext context)
            {
                return new Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>>(elements, context);
            }

            protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement> element)
            {
                return this.elementLexer.TryRead(scanner, out element);
            }
        }
    }

    public abstract partial class ElementListLexer<TList, TElement>
    {
        public partial class Element2Lexer
        {
            public partial class ElementLexer : SequenceLexer<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>, OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>
            {
                private readonly ILexer<OptionalWhiteSpace> element1Lexer;
                private readonly ILexer<Element> element2Lexer;
                private readonly ILexer<OptionalWhiteSpace> element3Lexer;
                private readonly ILexer<TElement> element4Lexer;

                public ElementLexer(ILexer<OptionalWhiteSpace> element1Lexer, ILexer<Element> element2Lexer, ILexer<OptionalWhiteSpace> element3Lexer, ILexer<TElement> element4Lexer)
                {
                    this.element1Lexer = element1Lexer;
                    this.element2Lexer = element2Lexer;
                    this.element3Lexer = element3Lexer;
                    this.element4Lexer = element4Lexer;
                }

                protected override Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement> CreateInstance(
                    OptionalWhiteSpace element1,
                    Element element2,
                    OptionalWhiteSpace element3,
                    TElement element4,
                    ITextContext context)
                {
                    return new Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, TElement>(element1, element2, element3, element4, context);
                }

                protected override bool TryRead1(ITextScanner scanner, out OptionalWhiteSpace element)
                {
                    return this.element1Lexer.TryRead(scanner, out element);
                }

                protected override bool TryRead2(ITextScanner scanner, out Element element)
                {
                    return this.element2Lexer.TryRead(scanner, out element);
                }

                protected override bool TryRead3(ITextScanner scanner, out OptionalWhiteSpace element)
                {
                    return this.element3Lexer.TryRead(scanner, out element);
                }

                protected override bool TryRead4(ITextScanner scanner, out TElement element)
                {
                    return this.element4Lexer.TryRead(scanner, out element);
                }
            }
        }
    }

    public abstract partial class ElementListLexer<TList, TElement>
    {
        public partial class Element2Lexer
        {
            public partial class ElementLexer
            {
                public class Element2Lexer : Lexer<Element>
                {
                    public override bool TryRead(ITextScanner scanner, out Element element)
                    {
                        return TryReadTerminal(scanner, @",", out element);
                    }
                }
            }
        }
    }
}