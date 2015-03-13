namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using Pseudonym = Token;

    public class PseudonymLexer : Lexer<Pseudonym>
    {
        private readonly ILexer<Token> tokenLexer;

        public PseudonymLexer(ILexer<Token> tokenLexer)
            : base("pseudonym")
        {
            this.tokenLexer = tokenLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Pseudonym element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Pseudonym);
                return false;
            }

            if (this.tokenLexer.TryRead(scanner, out element))
            {
                return true;
            }

            element = default(Pseudonym);
            return false;
        }
    }
}