namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class UnreservedLexer : Lexer<Unreserved>
    {
        private readonly ILexer<Alpha> alphaLexer;
        private readonly ILexer<Digit> digitLexer;

        public UnreservedLexer()
            : this(new AlphaLexer(), new DigitLexer())
        {
        }

        public UnreservedLexer(ILexer<Alpha> alphaLexer, ILexer<Digit> digitLexer)
            : base("unreserved")
        {
            Contract.Requires(alphaLexer != null);
            Contract.Requires(digitLexer != null);
            this.alphaLexer = alphaLexer;
            this.digitLexer = digitLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Unreserved element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Unreserved);
                return false;
            }

            var context = scanner.GetContext();
            Alpha alpha;
            if (this.alphaLexer.TryRead(scanner, out alpha))
            {
                element = new Unreserved(alpha, context);
                return true;
            }

            Digit digit;
            if (this.digitLexer.TryRead(scanner, out digit))
            {
                element = new Unreserved(digit, context);
                return true;
            }

            foreach (var c in new[] { '-', '.', '_', '~' })
            {
                Contract.Assert(!scanner.EndOfInput);
                if (scanner.TryMatch(c))
                {
                    element = new Unreserved(c, context);
                    return true;
                }
            }

            element = default(Unreserved);
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