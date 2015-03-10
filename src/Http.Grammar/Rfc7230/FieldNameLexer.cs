namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class FieldNameLexer : Lexer<FieldName>
    {
        private readonly ILexer<Token> tokenLexer;

        public FieldNameLexer()
            : this(new TokenLexer())
        {
        }

        public FieldNameLexer(ILexer<Token> tokenLexer)
            : base("field-name")
        {
            Contract.Requires(tokenLexer != null);
            this.tokenLexer = tokenLexer;
        }

        public override bool TryRead(ITextScanner scanner, out FieldName element)
        {
            if (scanner.EndOfInput)
            {
                element = default(FieldName);
                return false;
            }

            var context = scanner.GetContext();
            Token token;
            if (this.tokenLexer.TryRead(scanner, out token))
            {
                element = new FieldName(token, context);
                return true;
            }

            element = default(FieldName);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenLexer != null);
        }
    }
}