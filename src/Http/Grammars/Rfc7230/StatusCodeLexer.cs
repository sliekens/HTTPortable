namespace Http.Grammars.Rfc7230
{
    using System.Diagnostics.Contracts;
    using Text.Scanning;
    using Text.Scanning.Core;

    public class StatusCodeLexer : Lexer<StatusCodeToken>
    {
        private readonly ILexer<DigitToken> digitLexer;

        public StatusCodeLexer()
            : this(new DigitLexer())
        {
        }

        public StatusCodeLexer(ILexer<DigitToken> digitLexer)
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        public override StatusCodeToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            StatusCodeToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'status-code'");
        }

        public override bool TryRead(ITextScanner scanner, out StatusCodeToken token)
        {
            DigitToken digit1, digit2, digit3;
            var context = scanner.GetContext();
            if (!this.digitLexer.TryRead(scanner, out digit1))
            {
                token = default(StatusCodeToken);
                return false;
            }

            if (!this.digitLexer.TryRead(scanner, out digit2))
            {
                this.digitLexer.PutBack(scanner, digit1);
                token = default(StatusCodeToken);
                return false;
            }

            if (!this.digitLexer.TryRead(scanner, out digit3))
            {
                this.digitLexer.PutBack(scanner, digit2);
                this.digitLexer.PutBack(scanner, digit1);
                token = default(StatusCodeToken);
                return false;
            }

            token = new StatusCodeToken(digit1, digit2, digit3, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}