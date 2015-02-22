using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class StatusCodeLexer : Lexer<StatusCode>
    {
        private readonly ILexer<Digit> digitLexer;

        public StatusCodeLexer()
            : this(new DigitLexer())
        {
        }

        public StatusCodeLexer(ILexer<Digit> digitLexer)
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        public override StatusCode Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            StatusCode element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'status-code'");
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
                this.digitLexer.PutBack(scanner, digit1);
                element = default(StatusCode);
                return false;
            }

            if (!this.digitLexer.TryRead(scanner, out digit3))
            {
                this.digitLexer.PutBack(scanner, digit2);
                this.digitLexer.PutBack(scanner, digit1);
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