namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using Pseudonym = Token;

    public class PseudonymLexer : Lexer<Pseudonym>
    {
        private readonly ILexer<Token> tokenLexer;

        public PseudonymLexer()
            : this(new TokenLexer())
        {
        }

        public PseudonymLexer(ILexer<Token> tokenLexer)
            : base("pseudonym")
        {
            this.tokenLexer = tokenLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Pseudonym element)
        {
            return this.tokenLexer.TryRead(scanner, out element);
        }
    }
}