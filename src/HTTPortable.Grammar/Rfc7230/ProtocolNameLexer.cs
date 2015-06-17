namespace Http.Grammar.Rfc7230
{
    using SLANG;

    using ProtocolName = Token;

    public class ProtocolNameLexer : Lexer<ProtocolName>
    {
        private readonly ILexer<Token> tokenLexer;

        public ProtocolNameLexer()
            : this(new TokenLexer())
        {
        }

        public ProtocolNameLexer(ILexer<Token> tokenLexer)
            : base("protocol-name")
        {
            this.tokenLexer = tokenLexer;
        }

        public override bool TryRead(ITextScanner scanner, out ProtocolName element)
        {
            return this.tokenLexer.TryRead(scanner, out element);
        }
    }
}