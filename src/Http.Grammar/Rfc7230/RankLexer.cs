namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;
    using LessThanMaximum = SLANG.Sequence<SLANG.Element, SLANG.Option<SLANG.Sequence<SLANG.Element, SLANG.Repetition<SLANG.Core.Digit>>>>;
    using Maximum = SLANG.Sequence<SLANG.Element, SLANG.Option<SLANG.Sequence<SLANG.Element, SLANG.Repetition<SLANG.Element>>>>;

    public class RankLexer : Lexer<Rank>
    {
        private readonly ILexer<Digit> digitLexer;

        public RankLexer(ILexer<Digit> digitLexer)
            : base("rank")
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Rank element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Rank);
                return false;
            }

            var context = scanner.GetContext();
            LessThanMaximum lessThanMaximum;
            if (this.TryReadLessThanMaximum(scanner, out lessThanMaximum))
            {
                element = new Rank(lessThanMaximum, context);
                return true;
            }

            Maximum maximum;
            if (this.TryReadMaximum(scanner, out maximum))
            {
                element = new Rank(maximum, context);
                return true;
            }

            element = default(Rank);
            return false;
        }

        private bool TryReadLessThanMaximum(ITextScanner scanner, out LessThanMaximum element)
        {
            if (scanner.EndOfInput)
            {
                element = default(LessThanMaximum);
                return false;
            }

            var context = scanner.GetContext();
            Element integer;
            if (!TryReadTerminal(scanner, "0", out integer))
            {
                element = default(LessThanMaximum);
                return false;
            }

            Option<Sequence<Element, Repetition<Digit>>> fractionalPart;
            if (!this.TryReadOptionalSignificantFractionalPart(scanner, out fractionalPart))
            {
                scanner.PutBack(integer.Data);
                element = default(LessThanMaximum);
                return false;
            }

            element = new LessThanMaximum(integer, fractionalPart, context);
            return true;
        }


        private bool TryReadMaximum(ITextScanner scanner, out Maximum element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Maximum);
                return false;
            }

            var context = scanner.GetContext();
            Element integer;
            if (!TryReadTerminal(scanner, "1", out integer))
            {
                element = default(Maximum);
                return false;
            }

            Option<Sequence<Element, Repetition<Element>>> fractionalPart;
            if (!this.TryReadOptionalInsignificantFractionalPart(scanner, out fractionalPart))
            {
                scanner.PutBack(integer.Data);
                element = default(Maximum);
                return false;
            }

            element = new Maximum(integer, fractionalPart, context);
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
            Element decimalSeparator;
            if (!TryReadTerminal(scanner, ".", out decimalSeparator))
            {
                element = default(Sequence<Element, Repetition<Digit>>);
                return false;
            }

            Repetition<Digit> significantDigits;
            if (!this.TryReadSignificantDigits(scanner, out significantDigits))
            {
                scanner.PutBack(decimalSeparator.Data);
                element = default(Sequence<Element, Repetition<Digit>>);
                return false;
            }

            element = new Sequence<Element, Repetition<Digit>>(decimalSeparator, significantDigits, context);
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
            Element decimalSeparator;
            if (!TryReadTerminal(scanner, ".", out decimalSeparator))
            {
                element = default(Sequence<Element, Repetition<Element>>);
                return false;
            }

            Repetition<Element> insignificantDigits;
            if (!this.TryReadInsignificantDigits(scanner, out insignificantDigits))
            {
                scanner.PutBack(decimalSeparator.Data);
                element = default(Sequence<Element, Repetition<Element>>);
                return false;
            }

            element = new Sequence<Element, Repetition<Element>>(decimalSeparator, insignificantDigits, context);
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