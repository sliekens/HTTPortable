namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Text.Scanning.Core;

    using SchemeCharacter = Text.Scanning.Alternative<Text.Scanning.Core.Alpha, Text.Scanning.Core.Digit, Text.Scanning.Element>;

    public class SchemeLexer : Lexer<Scheme>
    {
        private readonly ILexer<Alpha> alphaLexer;

        private readonly ILexer<Digit> digitLexer;

        public SchemeLexer()
            : this(new AlphaLexer(), new DigitLexer())
        {
        }

        public SchemeLexer(ILexer<Alpha> alphaLexer, ILexer<Digit> digitLexer)
            : base("scheme")
        {
            Contract.Requires(alphaLexer != null);
            Contract.Requires(digitLexer != null);
            this.alphaLexer = alphaLexer;
            this.digitLexer = digitLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Scheme element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Scheme);
                return false;
            }

            var context = scanner.GetContext();
            Alpha alpha;
            if (!this.alphaLexer.TryRead(scanner, out alpha))
            {
                element = default(Scheme);
                return false;
            }

            var elements = new List<SchemeCharacter>
            {
                new SchemeCharacter(alpha, context)
            };

            while (!scanner.EndOfInput)
            {
                var innerContext = scanner.GetContext();
                if (this.alphaLexer.TryRead(scanner, out alpha))
                {
                    elements.Add(new SchemeCharacter(alpha, innerContext));
                }
                else
                {
                    Digit digit;
                    if (this.digitLexer.TryRead(scanner, out digit))
                    {
                        elements.Add(new SchemeCharacter(digit, innerContext));
                    }
                    else
                    {
                        Element symbol;
                        if (this.TryReadPlusSign(scanner, out symbol) || this.TryReadMinusSign(scanner, out symbol) || this.TryReadFullStop(scanner, out symbol))
                        {
                            elements.Add(new SchemeCharacter(symbol, innerContext));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            element = new Scheme(string.Concat(elements), context);
            return true;
        }

        private bool TryReadPlusSign(ITextScanner scanner, out Element element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('+'))
            {
                element = new Element("+", context);
                return true;
            }

            element = default(Element);
            return false;
        }

        private bool TryReadMinusSign(ITextScanner scanner, out Element element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('-'))
            {
                element = new Element("-", context);
                return true;
            }

            element = default(Element);
            return false;
        }

        private bool TryReadFullStop(ITextScanner scanner, out Element element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('.'))
            {
                element = new Element(".", context);
                return true;
            }

            element = default(Element);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.alphaLexer != null);
            Contract.Invariant(this.digitLexer != null);
        }
    }
}
