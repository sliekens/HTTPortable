namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using ConnectionOption = Token;

    public class ConnectionOptionLexer : Lexer<ConnectionOption>
    {
        private readonly ILexer<Token> tokenLexer;

        public ConnectionOptionLexer()
            : this(new TokenLexer())
        {
        }

        public ConnectionOptionLexer(ILexer<Token> tokenLexer)
            : base("connection-option")
        {
            this.tokenLexer = tokenLexer;
        }

        public override bool TryRead(ITextScanner scanner, out ConnectionOption element)
        {
            return this.tokenLexer.TryRead(scanner, out element);
        }
    }
}