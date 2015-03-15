namespace Http.Grammar
{
    using System.Collections.Generic;

    using Http.Grammar.Rfc7230;

    using SLANG;

    public abstract class ElementListLexer<TList, TElement> : Lexer<TList>
        where TList : ElementList<TElement>
        where TElement : Element
    {
        private readonly int lowerBound;

        private readonly int upperBound;

        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;

        protected ElementListLexer(string ruleName, int lowerBound, int upperBound)
            : this(ruleName, lowerBound, upperBound, new OptionalWhiteSpaceLexer())
        {
        }

        protected ElementListLexer(string ruleName, int lowerBound, int upperBound, ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer)
            : base(ruleName)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
        }

        public override bool TryRead(ITextScanner scanner, out TList element)
        {
            if (scanner.EndOfInput)
            {
                element = default(TList);
                return false;
            }

            var context = scanner.GetContext();
            TElement element1;
            if (!this.TryRead(scanner, this.lowerBound, this.upperBound, 0, out element1))
            {
                element = default(TList);
                return false;
            }

            Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>> element2;
            if (!this.TryRead(scanner, out element2))
            {
                scanner.PutBack(element1.Data);
                element = default(TList);
                return false;
            }

            element = this.CreateInstance(element1, element2, context);
            return true;
        }

        private bool TryRead(ITextScanner scanner, out Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>> element)
        {
            var context = scanner.GetContext();
            var elements = new List<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>>(this.lowerBound);
            for (int i = 0; i < this.upperBound; i++)
            {
                Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element> sequence;
                if (this.TryRead(scanner, this.lowerBound, this.upperBound, i + 1, out sequence))
                {
                    elements.Add(sequence);
                }
                else
                {
                    break;
                }
            }

            if (elements.Count < this.lowerBound)
            {
                if (elements.Count != 0)
                {
                    for (int i = elements.Count - 1; i >= 0; i--)
                    {
                        scanner.PutBack(elements[i].Data);
                    }
                }

                element = default(Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>>);
                return false;
            }

            element = new Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>>(elements, this.lowerBound, this.upperBound, context);
            return true;
        }

        private bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>);
                return false;
            }

            var context = scanner.GetContext();
            OptionalWhiteSpace element1;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out element1))
            {
                element = default(Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>);
                return false;
            }

            Element element2;
            if (!TryReadTerminal(scanner, @",", out element2))
            {
                scanner.PutBack(element1.Data);
                element = default(Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>);
                return false;
            }

            OptionalWhiteSpace element3;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out element3))
            {
                scanner.PutBack(element2.Data);
                scanner.PutBack(element1.Data);
                element = default(Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>);
                return false;
            }

            TElement element4;
            if (!this.TryRead(scanner, lowerBound, upperBound, current, out element4))
            {
                scanner.PutBack(element3.Data);
                scanner.PutBack(element2.Data);
                scanner.PutBack(element1.Data);
                element = default(Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>);
                return false;
            }

            element = new Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>(element1, element2, element3, element4, context);
            return true;
        }

        protected abstract TList CreateInstance(TElement element1, Repetition<Sequence<OptionalWhiteSpace, Element, OptionalWhiteSpace, Element>> element2, ITextContext context);

        protected abstract bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out TElement element);
    }
}