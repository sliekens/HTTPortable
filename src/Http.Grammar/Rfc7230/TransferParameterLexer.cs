namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class TransferParameterLexer : Lexer<TransferParameter>
    {
        private readonly ILexer<BadWhiteSpace> badWhiteSpaceLexer;
        private readonly ILexer<QuotedString> quotedStringLexer;
        private readonly ILexer<Token> tokenLexer;

        public TransferParameterLexer()
            : this(new TokenLexer(), new BadWhiteSpaceLexer(), new QuotedStringLexer())
        {
        }

        public TransferParameterLexer(ILexer<Token> tokenLexer, ILexer<BadWhiteSpace> badWhiteSpaceLexer, 
            ILexer<QuotedString> quotedStringLexer)
            : base("transfer-parameter")
        {
            Contract.Requires(tokenLexer != null);
            Contract.Requires(badWhiteSpaceLexer != null);
            Contract.Requires(quotedStringLexer != null);
            this.tokenLexer = tokenLexer;
            this.badWhiteSpaceLexer = badWhiteSpaceLexer;
            this.quotedStringLexer = quotedStringLexer;
        }

        public override bool TryRead(ITextScanner scanner, out TransferParameter element)
        {
            if (scanner.EndOfInput)
            {
                element = default(TransferParameter);
                return false;
            }

            var context = scanner.GetContext();
            Token name;
            if (!this.tokenLexer.TryRead(scanner, out name))
            {
                element = default(TransferParameter);
                return false;
            }

            BadWhiteSpace badWhiteSpace1;
            if (!this.badWhiteSpaceLexer.TryRead(scanner, out badWhiteSpace1))
            {
                scanner.PutBack(name.Data);
                element = default(TransferParameter);
                return false;
            }

            Element equalsSign;
            if (!TryReadTerminal(scanner, '=', out equalsSign))
            {
                scanner.PutBack(badWhiteSpace1.Data);
                scanner.PutBack(name.Data);
                element = default(TransferParameter);
                return false;
            }

            BadWhiteSpace badWhiteSpace2;
            if (!this.badWhiteSpaceLexer.TryRead(scanner, out badWhiteSpace2))
            {
                scanner.PutBack(equalsSign.Data);
                scanner.PutBack(badWhiteSpace1.Data);
                scanner.PutBack(name.Data);
                element = default(TransferParameter);
                return false;
            }

            Token value;
            if (this.tokenLexer.TryRead(scanner, out value))
            {
                element = new TransferParameter(name, value, context);
                return true;
            }

            QuotedString quotedValue;
            if (this.quotedStringLexer.TryRead(scanner, out quotedValue))
            {
                element = new TransferParameter(name, quotedValue, context);
                return true;
            }

            scanner.PutBack(badWhiteSpace2.Data);
            scanner.PutBack(equalsSign.Data);
            scanner.PutBack(badWhiteSpace1.Data);
            scanner.PutBack(name.Data);
            element = default(TransferParameter);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenLexer != null);
            Contract.Invariant(this.badWhiteSpaceLexer != null);
            Contract.Invariant(this.quotedStringLexer != null);
        }
    }
}