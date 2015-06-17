namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    using BadWhiteSpace = OptionalWhiteSpace;
    using ParameterValue = SLANG.Alternative<Token, QuotedString>;

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
            Token parameterName;
            if (!this.tokenLexer.TryRead(scanner, out parameterName))
            {
                element = default(TransferParameter);
                return false;
            }

            BadWhiteSpace badWhiteSpace1;
            if (!this.badWhiteSpaceLexer.TryRead(scanner, out badWhiteSpace1))
            {
                scanner.PutBack(parameterName.Data);
                element = default(TransferParameter);
                return false;
            }

            Element equalsSign;
            if (!TryReadTerminal(scanner, "=", out equalsSign))
            {
                scanner.PutBack(badWhiteSpace1.Data);
                scanner.PutBack(parameterName.Data);
                element = default(TransferParameter);
                return false;
            }

            BadWhiteSpace badWhiteSpace2;
            if (!this.badWhiteSpaceLexer.TryRead(scanner, out badWhiteSpace2))
            {
                scanner.PutBack(equalsSign.Data);
                scanner.PutBack(badWhiteSpace1.Data);
                scanner.PutBack(parameterName.Data);
                element = default(TransferParameter);
                return false;
            }

            ParameterValue parameterValue;
            if (!this.TryReadParameterValue(scanner, out parameterValue))
            {
                scanner.PutBack(badWhiteSpace2.Data);
                scanner.PutBack(equalsSign.Data);
                scanner.PutBack(badWhiteSpace1.Data);
                scanner.PutBack(parameterName.Data);
                element = default(TransferParameter);
                return false;
            }

            element = new TransferParameter(parameterName, badWhiteSpace1, equalsSign, badWhiteSpace2, parameterValue, context);
            return true;
        }

        private bool TryReadParameterValue(ITextScanner scanner, out ParameterValue element)
        {
            if (scanner.EndOfInput)
            {
                element = default(ParameterValue);
                return false;
            }

            var context = scanner.GetContext();
            Token token;
            if (this.tokenLexer.TryRead(scanner, out token))
            {
                element = new ParameterValue(token, 1, context);
                return true;
            }

            QuotedString quotedString;
            if (this.quotedStringLexer.TryRead(scanner, out quotedString))
            {
                element = new ParameterValue(quotedString, 2, context);
                return true;
            }

            element = default(ParameterValue);
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