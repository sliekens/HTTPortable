using System.Diagnostics.Contracts;

using SLANG.Core;

namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using SLANG.Core;

    public class StatusCodeLexer : Lexer<StatusCode>
    {
        private readonly ILexer<Digit> digitLexer;

        public StatusCodeLexer()
            : this(new DigitLexer())
        {
        }

        public StatusCodeLexer(ILexer<Digit> digitLexer)
            : base("status-code")
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        public override bool TryRead(ITextScanner scanner, out StatusCode element)
        {
            if (scanner.EndOfInput)
            {
                element = default(StatusCode);
                return false;
            }

            Digit digit1, digit2, digit3;
            var context = scanner.GetContext();
            if (!this.digitLexer.TryRead(scanner, out digit1))
            {
                element = default(StatusCode);
                return false;
            }

            if (!this.digitLexer.TryRead(scanner, out digit2))
            {
                scanner.PutBack(digit1.Data);
                element = default(StatusCode);
                return false;
            }

            if (!this.digitLexer.TryRead(scanner, out digit3))
            {
                scanner.PutBack(digit2.Data);
                scanner.PutBack(digit1.Data);
                element = default(StatusCode);
                return false;
            }

            element = new StatusCode(digit1, digit2, digit3, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}