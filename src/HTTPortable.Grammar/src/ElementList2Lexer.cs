namespace Http.Grammar
{
    using System.Collections.Generic;
    using System.Linq;

    using Http.Grammar.Rfc7230;

    using SLANG;

    public abstract partial class ElementList2Lexer<TList, T> : Lexer<TList>
        where TList : ElementList2<T>
        where T : Element
    {
        private readonly ILexer<Sequence<Alternative<Element, T>, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>>> elementLexer;

        protected ElementList2Lexer(string ruleName, ILexer<T> elementLexer)
            : this(ruleName, new OptionalWhiteSpaceLexer(), elementLexer)
        {
        }

        protected ElementList2Lexer(string ruleName, ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<T> elementLexer)
            : this(ruleName, new ElementLexer(new ElementLexer.Element1Lexer(new ElementLexer.Element1Lexer.Element2Lexer(), elementLexer), new ElementLexer.Element2Lexer(new ElementLexer.Element2Lexer.ElementLexer(optionalWhiteSpaceLexer, new ElementLexer.Element2Lexer.ElementLexer.Element2Lexer(), new ElementLexer.Element2Lexer.ElementLexer.Element3Lexer(new ElementLexer.Element2Lexer.ElementLexer.Element3Lexer.ElementLexer(optionalWhiteSpaceLexer, elementLexer))))))
        {
        }

        protected ElementList2Lexer(string ruleName, ILexer<Sequence<Alternative<Element, T>, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>>> elementLexer)
            : base(ruleName)
        {
            this.elementLexer = elementLexer;
        }
    }

    public abstract partial class ElementList2Lexer<TList, T>
    {
        public partial class ElementLexer : SequenceLexer<Sequence<Alternative<Element, T>, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>>, Alternative<Element, T>, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>>
        {
            private readonly ILexer<Alternative<Element, T>> element1Lexer;

            private readonly ILexer<Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>> element2Lexer;

            public ElementLexer(ILexer<Alternative<Element, T>> element1Lexer, ILexer<Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>> element2Lexer)
            {
                this.element1Lexer = element1Lexer;
                this.element2Lexer = element2Lexer;
            }

            protected override Sequence<Alternative<Element, T>, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>> CreateInstance(Alternative<Element, T> element1, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>> element2, ITextContext context)
            {
                return new Sequence<Alternative<Element, T>, Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>>(element1, element2, context);
            }

            protected override bool TryRead1(ITextScanner scanner, out Alternative<Element, T> element)
            {
                return this.element1Lexer.TryRead(scanner, out element);
            }

            protected override bool TryRead2(ITextScanner scanner, out Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>> element)
            {
                return this.element2Lexer.TryRead(scanner, out element);
            }

        }
    }

    public abstract partial class ElementList2Lexer<TList, T>
    {
        public partial class ElementLexer
        {
            public partial class Element1Lexer : AlternativeLexer<Alternative<Element, T>, Element, T>
            {
                private readonly ILexer<Element> element1Lexer;

                private readonly ILexer<T> element2Lexer;

                public Element1Lexer(ILexer<Element> element1Lexer, ILexer<T> element2Lexer)
                {
                    this.element1Lexer = element1Lexer;
                    this.element2Lexer = element2Lexer;
                }

                protected override Alternative<Element, T> CreateInstance1(Element element, ITextContext context)
                {
                    return new Alternative<Element, T>(element, 1, context);
                }

                protected override Alternative<Element, T> CreateInstance2(T element, ITextContext context)
                {
                    return new Alternative<Element, T>(element, 2, context);
                }

                protected override bool TryRead1(ITextScanner scanner, out Element element)
                {
                    return this.element1Lexer.TryRead(scanner, out element);
                }

                protected override bool TryRead2(ITextScanner scanner, out T element)
                {
                    return this.element2Lexer.TryRead(scanner, out element);
                }
            }
        }
    }

    public abstract partial class ElementList2Lexer<TList, T>
    {
        public partial class ElementLexer
        {
            public partial class Element1Lexer
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

    public abstract partial class ElementList2Lexer<TList, T>
    {
        public partial class ElementLexer
        {
            public partial class Element2Lexer : RepetitionLexer<Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>, Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>
            {
                private readonly ILexer<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>> elementLexer;

                public Element2Lexer(ILexer<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>> elementLexer)
                    : base(0, int.MaxValue)
                {
                    this.elementLexer = elementLexer;
                }

                protected override Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>> CreateInstance(IList<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>> elements, int lowerBound, int upperBound, ITextContext context)
                {
                    return new Repetition<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>>(elements, context);
                }

                protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>> element)
                {
                    return this.elementLexer.TryRead(scanner, out element);
                }
            }
        }
    }

    public abstract partial class ElementList2Lexer<TList, T>
    {
        public partial class ElementLexer
        {
            public partial class Element2Lexer
            {
                public partial class ElementLexer : SequenceLexer<Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>, OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>
                {
                    private readonly ILexer<OptionalWhiteSpace> element1Lexer;
                    private readonly ILexer<Element> element2Lexer;
                    private readonly ILexer<Option<Sequence<OptionalWhiteSpace, T>>> element3Lexer;

                    public ElementLexer(ILexer<OptionalWhiteSpace> element1Lexer, ILexer<Element> element2Lexer, ILexer<Option<Sequence<OptionalWhiteSpace, T>>> element3Lexer)
                    {
                        this.element1Lexer = element1Lexer;
                        this.element2Lexer = element2Lexer;
                        this.element3Lexer = element3Lexer;
                    }

                    protected override Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>> CreateInstance(OptionalWhiteSpace element1, Element element2, Option<Sequence<OptionalWhiteSpace, T>> element3, ITextContext context)
                    {
                        return new Sequence<OptionalWhiteSpace, Element, Option<Sequence<OptionalWhiteSpace, T>>>(element1, element2, element3, context);
                    }

                    protected override bool TryRead1(ITextScanner scanner, out OptionalWhiteSpace element)
                    {
                        return this.element1Lexer.TryRead(scanner, out element);
                    }

                    protected override bool TryRead2(ITextScanner scanner, out Element element)
                    {
                        return this.element2Lexer.TryRead(scanner, out element);
                    }

                    protected override bool TryRead3(ITextScanner scanner, out Option<Sequence<OptionalWhiteSpace, T>> element)
                    {
                        return this.element3Lexer.TryRead(scanner, out element);
                    }
                }
            }
        }
    }

    public abstract partial class ElementList2Lexer<TList, T>
    {
        public partial class ElementLexer
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

    public abstract partial class ElementList2Lexer<TList, T>
    {
        public partial class ElementLexer
        {
            public partial class Element2Lexer
            {
                public partial class ElementLexer
                {
                    public partial class Element3Lexer : RepetitionLexer<Option<Sequence<OptionalWhiteSpace, T>>, Sequence<OptionalWhiteSpace, T>>
                    {
                        private readonly ILexer<Sequence<OptionalWhiteSpace, T>> elementLexer;

                        public Element3Lexer(ILexer<Sequence<OptionalWhiteSpace, T>> elementLexer)
                            : base(0, 1)
                        {
                            this.elementLexer = elementLexer;
                        }

                        protected override Option<Sequence<OptionalWhiteSpace, T>> CreateInstance(IList<Sequence<OptionalWhiteSpace, T>> elements, int lowerBound, int upperBound, ITextContext context)
                        {
                            var element = elements.FirstOrDefault();
                            if (element == null)
                            {
                                return new Option<Sequence<OptionalWhiteSpace, T>>(context);
                            }

                            return new Option<Sequence<OptionalWhiteSpace, T>>(element, context);
                        }

                        protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out Sequence<OptionalWhiteSpace, T> element)
                        {
                            return this.elementLexer.TryRead(scanner, out element);
                        }
                    }
                }
            }
        }
    }

    public abstract partial class ElementList2Lexer<TList, T>
    {
        public partial class ElementLexer
        {
            public partial class Element2Lexer
            {
                public partial class ElementLexer
                {
                    public partial class Element3Lexer
                    {
                        public class ElementLexer : SequenceLexer<Sequence<OptionalWhiteSpace, T>, OptionalWhiteSpace, T>
                        {
                            private readonly ILexer<OptionalWhiteSpace> element1Lexer;

                            private readonly ILexer<T> element2Lexer;

                            public ElementLexer(ILexer<OptionalWhiteSpace> element1Lexer, ILexer<T> element2Lexer)
                            {
                                this.element1Lexer = element1Lexer;
                                this.element2Lexer = element2Lexer;
                            }

                            protected override Sequence<OptionalWhiteSpace, T> CreateInstance(OptionalWhiteSpace element1, T element2, ITextContext context)
                            {
                                return new Sequence<OptionalWhiteSpace, T>(element1, element2, context);
                            }

                            protected override bool TryRead1(ITextScanner scanner, out OptionalWhiteSpace element)
                            {
                                return this.element1Lexer.TryRead(scanner, out element);
                            }

                            protected override bool TryRead2(ITextScanner scanner, out T element)
                            {
                                return this.element2Lexer.TryRead(scanner, out element);
                            }
                        }
                    }
                }
            }
        }
    }
}