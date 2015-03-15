namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;
    using LessThanMaximum = SLANG.Sequence<SLANG.Element, SLANG.Option<SLANG.Sequence<SLANG.Element, SLANG.Repetition<SLANG.Core.Digit>>>>;
    using Maximum = SLANG.Sequence<SLANG.Element, SLANG.Option<SLANG.Sequence<SLANG.Element, SLANG.Repetition<SLANG.Element>>>>;

    public class RankLexer : AlternativeLexer<Rank, LessThanMaximum, Maximum>
    {
        private readonly ILexer<Digit> digitLexer;

        public RankLexer()
            : this(new DigitLexer())
        {
        }

        public RankLexer(ILexer<Digit> digitLexer)
            : base("rank")
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        protected override Rank CreateInstance1(LessThanMaximum element, ITextContext context)
        {
            return new Rank(element, context);
        }

        protected override Rank CreateInstance2(Maximum element, ITextContext context)
        {
            return new Rank(element, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out LessThanMaximum element)
        {
            if (scanner.EndOfInput)
            {
                element = default(LessThanMaximum);
                return false;
            }

            var context = scanner.GetContext();
            Element element1;
            if (!TryReadTerminal(scanner, "0", out element1))
            {
                element = default(LessThanMaximum);
                return false;
            }

            Option<Sequence<Element, Repetition<Digit>>> element2;
            if (!this.TryReadOptionalSignificantFractionalPart(scanner, out element2))
            {
                scanner.PutBack(element1.Data);
                element = default(LessThanMaximum);
                return false;
            }

            element = new LessThanMaximum(element1, element2, context);
            return true;
        }

        protected override bool TryRead2(ITextScanner scanner, out Maximum element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Maximum);
                return false;
            }

            var context = scanner.GetContext();
            Element element1;
            if (!TryReadTerminal(scanner, "1", out element1))
            {
                element = default(Maximum);
                return false;
            }

            Option<Sequence<Element, Repetition<Element>>> element2;
            if (!this.TryReadOptionalInsignificantFractionalPart(scanner, out element2))
            {
                scanner.PutBack(element1.Data);
                element = default(Maximum);
                return false;
            }

            element = new Maximum(element1, element2, context);
            return true;
        }

        private bool TryReadOptionalSignificantFractionalPart(ITextScanner scanner, out Option<Sequence<Element, Repetition<Digit>>> element)
        {
            var context = scanner.GetContext();
            Sequence<Element, Repetition<Digit>> fractionalPart;
            if (this.TryReadSignificantFractionalPart(scanner, out fractionalPart))
            {
                element = new Option<Sequence<Element, Repetition<Digit>>>(fractionalPart, context);
            }
            else
            {
                element = new Option<Sequence<Element, Repetition<Digit>>>(context);
            }

            return true;
        }

        private bool TryReadOptionalInsignificantFractionalPart(ITextScanner scanner, out Option<Sequence<Element, Repetition<Element>>> element)
        {
            var context = scanner.GetContext();
            Sequence<Element, Repetition<Element>> fractionalPart;
            if (this.TryReadInsignificantFractionalPart(scanner, out fractionalPart))
            {
                element = new Option<Sequence<Element, Repetition<Element>>>(fractionalPart, context);
            }
            else
            {
                element = new Option<Sequence<Element, Repetition<Element>>>(context);
            }

            return true;
        }

        private bool TryReadSignificantFractionalPart(ITextScanner scanner, out Sequence<Element, Repetition<Digit>> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<Element, Repetition<Digit>>);
                return false;
            }

            var context = scanner.GetContext();
            Element element1;
            if (!TryReadTerminal(scanner, ".", out element1))
            {
                element = default(Sequence<Element, Repetition<Digit>>);
                return false;
            }

            Repetition<Digit> element2;
            if (!this.TryReadSignificantDigits(scanner, out element2))
            {
                scanner.PutBack(element1.Data);
                element = default(Sequence<Element, Repetition<Digit>>);
                return false;
            }

            element = new Sequence<Element, Repetition<Digit>>(element1, element2, context);
            return true;
        }

        private bool TryReadInsignificantFractionalPart(ITextScanner scanner, out Sequence<Element, Repetition<Element>> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<Element, Repetition<Element>>);
                return false;
            }

            var context = scanner.GetContext();
            Element element1;
            if (!TryReadTerminal(scanner, ".", out element1))
            {
                element = default(Sequence<Element, Repetition<Element>>);
                return false;
            }

            Repetition<Element> element2;
            if (!this.TryReadInsignificantDigits(scanner, out element2))
            {
                scanner.PutBack(element1.Data);
                element = default(Sequence<Element, Repetition<Element>>);
                return false;
            }

            element = new Sequence<Element, Repetition<Element>>(element1, element2, context);
            return true;
        }

        private bool TryReadSignificantDigits(ITextScanner scanner, out Repetition<Digit> element)
        {
            var context = scanner.GetContext();
            var elements = new List<Digit>(3);
            for (int i = 0; i < 3; i++)
            {
                Digit digit;
                if (!this.digitLexer.TryRead(scanner, out digit))
                {
                    break;
                }

                elements.Add(digit);

            }

            element = new Repetition<Digit>(elements, 0, 3, context);
            return true;
        }

        private bool TryReadInsignificantDigits(ITextScanner scanner, out Repetition<Element> element)
        {
            var context = scanner.GetContext();
            var elements = new List<Element>(3);
            for (int i = 0; i < 3; i++)
            {
                Element digit;
                if (!TryReadTerminal(scanner, "0", out digit))
                {
                    break;
                }

                elements.Add(digit);
            }

            element = new Repetition<Element>(elements, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}