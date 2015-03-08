namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;


    using IPvFutureCharacter = SLANG.Alternative<Unreserved, SubcomponentsDelimiter, SLANG.Element>;
    using Colon = SLANG.Element;

    public class IPvFutureLexer : Lexer<IPvFuture>
    {
        private readonly ILexer<HexadecimalDigit> hexadecimalDigitLexer;

        private readonly ILexer<Unreserved> unreservedLexer;

        private readonly ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer;

        public IPvFutureLexer()
            : this(new HexadecimalDigitLexer(), new UnreservedLexer(), new SubcomponentsDelimiterLexer())
        {
        }

        public IPvFutureLexer(ILexer<HexadecimalDigit> hexadecimalDigitLexer, ILexer<Unreserved> unreservedLexer, ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer)
            : base("IPvFuture")
        {
            Contract.Requires(hexadecimalDigitLexer != null);
            Contract.Requires(unreservedLexer != null);
            Contract.Requires(subcomponentsDelimiterLexer != null);
            this.hexadecimalDigitLexer = hexadecimalDigitLexer;
            this.unreservedLexer = unreservedLexer;
            this.subcomponentsDelimiterLexer = subcomponentsDelimiterLexer;
        }

        public override bool TryRead(ITextScanner scanner, out IPvFuture element)
        {
            if (scanner.EndOfInput)
            {
                element = default(IPvFuture);
                return false;
            }

            var context = scanner.GetContext();

            Element v;
            if (!this.TryReadLetterV(scanner, out v))
            {
                element = default(IPvFuture);
                return false;
            }

            var hexadecimalDigits = this.ReadHexadecimalDigits(scanner);
            if (hexadecimalDigits.Count == 0)
            {
                scanner.PutBack(v.Data);
                element = default(IPvFuture);
                return false;
            }

            Element fullStop;
            if (!this.TryReadFullStop(scanner, out fullStop))
            {
                for (int i = hexadecimalDigits.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(hexadecimalDigits[i].Data);
                }

                scanner.PutBack(v.Data);
                element = default(IPvFuture);
                return false;
            }

            var ipvFutureCharacters = this.ReadIPvFutureCharacters(scanner);
            if (ipvFutureCharacters.Count == 0)
            {
                scanner.PutBack(fullStop.Data);
                for (int i = hexadecimalDigits.Count - 1; i >= 0; i--)
                {
                    scanner.PutBack(hexadecimalDigits[i].Data);
                }

                scanner.PutBack(v.Data);
                element = default(IPvFuture);
                return false;
            }

            element = new IPvFuture(v, hexadecimalDigits, fullStop, ipvFutureCharacters, context);
            return true;
        }

        private bool TryReadLetterV(ITextScanner scanner, out Element v)
        {
            if (scanner.EndOfInput)
            {
                v = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            foreach (var c in new[] { 'v', 'V' })
            {
                if (scanner.TryMatch(c))
                {
                    v = new Element(c, context);
                    return true;
                }
            }

            v = default(Element);
            return false;
        }

        private IList<HexadecimalDigit> ReadHexadecimalDigits(ITextScanner scanner)
        {
            Contract.Requires(scanner != null);
            Contract.Ensures(Contract.Result<IList<HexadecimalDigit>>() != null);
            var elements = new List<HexadecimalDigit>();
            HexadecimalDigit digit;
            while (this.hexadecimalDigitLexer.TryRead(scanner, out digit))
            {
                elements.Add(digit);
            }

            return elements;
        }

        private bool TryReadFullStop(ITextScanner scanner, out Element fullStop)
        {
            if (scanner.EndOfInput)
            {
                fullStop = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('.'))
            {
                fullStop = new Element('.', context);
                return true;
            }

            fullStop = default(Element);
            return false;

        }

        private IList<IPvFutureCharacter> ReadIPvFutureCharacters(ITextScanner scanner)
        {
            var elements = new List<IPvFutureCharacter>();
            IPvFutureCharacter element;
            while (this.TryReadIPvFutureCharacter(scanner, out element))
            {
                elements.Add(element);
            }

            return elements;
        }

        private bool TryReadIPvFutureCharacter(ITextScanner scanner, out IPvFutureCharacter element)
        {
            if (scanner.EndOfInput)
            {
                element = default(IPvFutureCharacter);
                return false;
            }

            var context = scanner.GetContext();
            Unreserved unreserved;
            if (this.unreservedLexer.TryRead(scanner, out unreserved))
            {
                element = new IPvFutureCharacter(unreserved, context);
                return true;
            }

            SubcomponentsDelimiter subcomponentsDelimiter;
            if (this.subcomponentsDelimiterLexer.TryRead(scanner, out subcomponentsDelimiter))
            {
                element = new IPvFutureCharacter(subcomponentsDelimiter, context);
                return true;
            }

            Colon colon;
            if (this.TryReadColon(scanner, out colon))
            {
                element = new IPvFutureCharacter(colon, context);
                return true;
            }

            element = default(IPvFutureCharacter);
            return false;
        }

        private bool TryReadColon(ITextScanner scanner, out Colon colon)
        {
            if (scanner.EndOfInput)
            {
                colon = default(Colon);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch(':'))
            {
                colon = new Colon(':', context);
                return true;
            }

            colon = default(Colon);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hexadecimalDigitLexer != null);
            Contract.Invariant(this.unreservedLexer != null);
            Contract.Invariant(this.subcomponentsDelimiterLexer != null);
        }
    }
}
