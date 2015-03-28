namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Linq;

    using SLANG;
    using ProtocolName = Token;
    using ProtocolVersion = Token;

    public partial class ReceivedProtocolLexer : SequenceLexer<ReceivedProtocol, Option<Sequence<ProtocolName, Element>>, ProtocolVersion>
    {
        private readonly ILexer<Option<Sequence<ProtocolName, Element>>> element1Lexer;

        private readonly ILexer<ProtocolVersion> element2Lexer;

        public ReceivedProtocolLexer()
            : this(new ProtocolNameLexer(), new ProtocolVersionLexer())
        {
        }

        public ReceivedProtocolLexer(ILexer<ProtocolName> protocolNameLexer, ILexer<ProtocolVersion> protocolVersionLexer)
            : this(new Element1Lexer(new Element1Lexer.ElementLexer(protocolNameLexer, new Element1Lexer.ElementLexer.Element2Lexer())), protocolVersionLexer)
        {
        }

        public ReceivedProtocolLexer(ILexer<Option<Sequence<ProtocolName, Element>>> element1Lexer, ILexer<ProtocolVersion> element2Lexer)
            : base("received-protocol")
        {
            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
        }

        protected override ReceivedProtocol CreateInstance(Option<Sequence<ProtocolName, Element>> element1, ProtocolVersion element2, ITextContext context)
        {
            return new ReceivedProtocol(element1, element2, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out Option<Sequence<ProtocolName, Element>> element)
        {
            return this.element1Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out ProtocolVersion element)
        {
            return this.element2Lexer.TryRead(scanner, out element);
        }
    }

    public partial class ReceivedProtocolLexer
    {
        public partial class Element1Lexer : OptionLexer<Option<Sequence<ProtocolName, Element>>, Sequence<ProtocolName, Element>>
        {
            private readonly ILexer<Sequence<ProtocolName, Element>> elementLexer;

            public Element1Lexer(ILexer<Sequence<ProtocolName, Element>> elementLexer)
            {
                this.elementLexer = elementLexer;
            }

            protected override Option<Sequence<ProtocolName, Element>> CreateInstance(IList<Sequence<ProtocolName, Element>> elements, int lowerBound, int upperBound, ITextContext context)
            {
                var element = elements.SingleOrDefault();
                if (element == null)
                {
                    return new Option<Sequence<ProtocolName, Element>>(context);
                }

                return new Option<Sequence<ProtocolName, Element>>(element, context);
            }

            protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out Sequence<ProtocolName, Element> element)
            {
                return this.elementLexer.TryRead(scanner, out element);
            }
        }
    }

    public partial class ReceivedProtocolLexer
    {
        public partial class Element1Lexer
        {
            public partial class ElementLexer : SequenceLexer<Sequence<ProtocolName, Element>, ProtocolName, Element>
            {
                private readonly ILexer<ProtocolName> element1Lexer;

                private readonly ILexer<Element> element2Lexer;

                public ElementLexer(ILexer<ProtocolName> element1Lexer, ILexer<Element> element2Lexer)
                {
                    this.element1Lexer = element1Lexer;
                    this.element2Lexer = element2Lexer;
                }

                protected override Sequence<ProtocolName, Element> CreateInstance(ProtocolName element1, Element element2, ITextContext context)
                {
                    return new Sequence<ProtocolName, Element>(element1, element2, context);
                }

                protected override bool TryRead1(ITextScanner scanner, out ProtocolName element)
                {
                    return this.element1Lexer.TryRead(scanner, out element);
                }

                protected override bool TryRead2(ITextScanner scanner, out Element element)
                {
                    return this.element2Lexer.TryRead(scanner, out element);
                }
            }
        }
    }

    public partial class ReceivedProtocolLexer
    {
        public partial class Element1Lexer
        {
            public partial class ElementLexer
            {
                public class Element2Lexer : Lexer<Element>
                {
                    public override bool TryRead(ITextScanner scanner, out Element element)
                    {
                        return TryReadTerminal(scanner, @"/", out element);
                    }
                }
            }
        }
    }
}