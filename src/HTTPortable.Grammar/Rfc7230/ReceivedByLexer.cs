namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Linq;

    using SLANG;

    using Uri.Grammar;

    using UriHost = Uri.Grammar.Host;
    using Pseudonym = Token;

    public partial class ReceivedByLexer : AlternativeLexer<ReceivedBy, Sequence<UriHost, Option<Sequence<Element, Port>>>, Pseudonym>
    {
        private readonly ILexer<Sequence<UriHost, Option<Sequence<Element, Port>>>> element1Lexer;

        private readonly ILexer<Token> element2Lexer;

        public ReceivedByLexer()
            : this(new UriHostLexer(), new PortLexer(), new PseudonymLexer())
        {
        }

        public ReceivedByLexer(ILexer<UriHost> uriHostLexer, ILexer<Port> portLexer, ILexer<Pseudonym> pseudonymLexer)
            : this(new Element1Lexer(uriHostLexer, new Element1Lexer.Element2Lexer(new Element1Lexer.Element2Lexer.ElementLexer(new Element1Lexer.Element2Lexer.ElementLexer.Element1Lexer(), portLexer))), pseudonymLexer)
        {
        }

        public ReceivedByLexer(ILexer<Sequence<UriHost, Option<Sequence<Element, Port>>>> element1Lexer, ILexer<Pseudonym> element2Lexer)
            : base("received-by")
        {
            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
        }

        protected override ReceivedBy CreateInstance1(Sequence<UriHost, Option<Sequence<Element, Port>>> element, ITextContext context)
        {
            return new ReceivedBy(element, context);
        }

        protected override ReceivedBy CreateInstance2(Pseudonym element, ITextContext context)
        {
            return new ReceivedBy(element, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out Sequence<UriHost, Option<Sequence<Element, Port>>> element)
        {
            return this.element1Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Pseudonym element)
        {
            return this.element2Lexer.TryRead(scanner, out element);
        }
    }

    public partial class ReceivedByLexer
    {
        public partial class Element1Lexer : SequenceLexer<Sequence<UriHost, Option<Sequence<Element, Port>>>, UriHost, Option<Sequence<Element, Port>>>
        {
            private readonly ILexer<UriHost> element1Lexer;

            private readonly ILexer<Option<Sequence<Element, Port>>> element2Lexer;

            public Element1Lexer(ILexer<UriHost> element1Lexer, ILexer<Option<Sequence<Element, Port>>> element2Lexer)
            {
                this.element1Lexer = element1Lexer;
                this.element2Lexer = element2Lexer;
            }

            protected override Sequence<UriHost, Option<Sequence<Element, Port>>> CreateInstance(UriHost element1, Option<Sequence<Element, Port>> element2, ITextContext context)
            {
                return new Sequence<UriHost, Option<Sequence<Element, Port>>>(element1, element2, context);
            }

            protected override bool TryRead1(ITextScanner scanner, out UriHost element)
            {
                return this.element1Lexer.TryRead(scanner, out element);
            }

            protected override bool TryRead2(ITextScanner scanner, out Option<Sequence<Element, Port>> element)
            {
                return this.element2Lexer.TryRead(scanner, out element);
            }
        }
    }

    public partial class ReceivedByLexer
    {
        public partial class Element1Lexer
        {
            public partial class Element2Lexer : OptionLexer<Option<Sequence<Element, Port>>, Sequence<Element, Port>>
            {
                private readonly ILexer<Sequence<Element, Port>> elementLexer;

                public Element2Lexer(ILexer<Sequence<Element, Port>> elementLexer)
                {
                    this.elementLexer = elementLexer;
                }

                protected override Option<Sequence<Element, Port>> CreateInstance(IList<Sequence<Element, Port>> elements, int lowerBound, int upperBound, ITextContext context)
                {
                    var element = elements.SingleOrDefault();
                    if (element == null)
                    {
                        return new Option<Sequence<Element, Port>>(context);
                    }

                    return new Option<Sequence<Element, Port>>(element, context);
                }

                protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out Sequence<Element, Port> element)
                {
                    return this.elementLexer.TryRead(scanner, out element);
                }
            }
        }
    }

    public partial class ReceivedByLexer
    {
        public partial class Element1Lexer
        {
            public partial class Element2Lexer
            {
                public partial class ElementLexer : SequenceLexer<Sequence<Element, Port>, Element, Port>
                {
                    private readonly ILexer<Element> element1Lexer;

                    private readonly ILexer<Port> element2Lexer;

                    public ElementLexer(ILexer<Element> element1Lexer, ILexer<Port> element2Lexer)
                    {
                        this.element1Lexer = element1Lexer;
                        this.element2Lexer = element2Lexer;
                    }

                    protected override Sequence<Element, Port> CreateInstance(Element element1, Port element2, ITextContext context)
                    {
                        return new Sequence<Element, Port>(element1, element2, context);
                    }

                    protected override bool TryRead1(ITextScanner scanner, out Element element)
                    {
                        return this.element1Lexer.TryRead(scanner, out element);
                    }

                    protected override bool TryRead2(ITextScanner scanner, out Port element)
                    {
                        return this.element2Lexer.TryRead(scanner, out element);
                    }
                }
            }
        }
    }

    public partial class ReceivedByLexer
    {
        public partial class Element1Lexer
        {
            public partial class Element2Lexer
            {
                public partial class ElementLexer
                {
                    public class Element1Lexer : Lexer<Element>
                    {
                        public override bool TryRead(ITextScanner scanner, out Element element)
                        {
                            return TryReadTerminal(scanner, @":", out element);
                        }
                    }
                }
            }
        }
    }
}