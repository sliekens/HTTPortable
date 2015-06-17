namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class MethodLexer : Lexer<Method>
    {
        private readonly ILexer<Token> tokenLexer;

        public MethodLexer()
            : this(new TokenLexer())
        {
        }

        public MethodLexer(ILexer<Token> tokenLexer)
            : base("method")
        {
            Contract.Requires(tokenLexer != null);
            this.tokenLexer = tokenLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Method element)
        {
            var context = scanner.GetContext();
            Token token;
            if (this.tokenLexer.TryRead(scanner, out token))
            {
                element = new Method(token, context);
                return true;
            }

            element = default(Method);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenLexer != null);
        }
    }
}