namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using ProtocolVersion = Token;

    public class ProtocolVersionLexer : Lexer<ProtocolVersion>
    {
        private readonly ILexer<Token> tokenLexer;

        public ProtocolVersionLexer()
            : this(new TokenLexer())
        {
        }

        public ProtocolVersionLexer(ILexer<Token> tokenLexer)
            : base("protocol-version")
        {
            this.tokenLexer = tokenLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Token element)
        {
            return this.tokenLexer.TryRead(scanner, out element);
        }
    }
}