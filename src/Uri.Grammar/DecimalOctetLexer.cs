namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Text.Scanning.Core;

    public class DecimalOctetLexer : Lexer<DecimalOctet>
    {
        private readonly ILexer<Digit> digitLexer;

        public DecimalOctetLexer()
            : this(new DigitLexer())
        {
        }

        public DecimalOctetLexer(ILexer<Digit> digitLexer)
            : base("dec-octet")
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        public override bool TryRead(ITextScanner scanner, out DecimalOctet element)
        {
            if (scanner.EndOfInput)
            {
                element = default(DecimalOctet);
                return false;
            }

            var context = scanner.GetContext();

            if (scanner.TryMatch('2'))
            {
                // 2
                if (scanner.TryMatch('5'))
                {
                    // 25
                    for (char c = '\x0030'; c < '\x0035'; c++)
                    {
                        if (scanner.TryMatch(c))
                        {
                            // 25c
                            element = new DecimalOctet("25", c, context);
                            return true;
                        }
                    }

                    // 25
                    scanner.PutBack('5');

                    // 2
                    scanner.PutBack('2');
                }
                else
                {
                    // 2
                    for (char c = '\x0030'; c < '\x0034'; c++)
                    {
                        if (scanner.TryMatch(c))
                        {
                            // 2c
                            Digit digit;
                            if (this.digitLexer.TryRead(scanner, out digit))
                            {
                                // 2cd
                                element = new DecimalOctet('2', c, digit, context);
                                return true;
                            }

                            // 2c
                            scanner.PutBack(c);
                            break;
                        }
                    }

                    // 2
                    scanner.PutBack('2');
                }
            }
            else if (scanner.TryMatch('1'))
            {
                // 1
                Digit digit1;
                if (this.digitLexer.TryRead(scanner, out digit1))
                {
                    // 1d
                    Digit digit2;
                    if (this.digitLexer.TryRead(scanner, out digit2))
                    {
                        // 1dd
                        element = new DecimalOctet('1', digit1, digit2, context);
                        return true;
                    }

                    // 1d
                    this.digitLexer.PutBack(scanner, digit1);
                }

                // 1
                scanner.PutBack('1');
            }

            for (char c = '\x0031'; c < '\x0039'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    // c
                    Digit digit;
                    if (this.digitLexer.TryRead(scanner, out digit))
                    {
                        // cd
                        element = new DecimalOctet(c, digit, context);
                        return true;
                    }

                    // c
                    scanner.PutBack(c);
                    break;
                }
            }

            Digit singleDigit;
            if (this.digitLexer.TryRead(scanner, out singleDigit))
            {
                // d
                element = new DecimalOctet(singleDigit, context);
                return true;
            }

            element = default(DecimalOctet);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}