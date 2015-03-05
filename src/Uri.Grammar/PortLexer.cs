namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Text.Scanning.Core;

    public class PortLexer : Lexer<Port>
    {
        private readonly ILexer<Digit> digitLexer;

        public PortLexer()
            : this(new DigitLexer())
        {
        }

        public PortLexer(ILexer<Digit> digitLexer)
            : base("port")
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Port element)
        {
            var context = scanner.GetContext();
            var digits = new List<Digit>();
            Digit digit;
            while (this.digitLexer.TryRead(scanner, out digit))
            {
                digits.Add(digit);
            }

            element = new Port(digits, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}
