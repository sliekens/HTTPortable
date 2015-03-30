namespace Http.Grammar.Rfc7230
{
    using SLANG;

    using ChunkExtensionName = Token;

    public class ChunkExtensionNameLexer : Lexer<Token>
    {
        private readonly ILexer<Token> tokenLexer;

        public ChunkExtensionNameLexer(ILexer<Token> tokenLexer)
            : base("chunk-ext-name")
        {
            this.tokenLexer = tokenLexer;
        }

        public override bool TryRead(ITextScanner scanner, out ChunkExtensionName element)
        {
            return this.tokenLexer.TryRead(scanner, out element);
        }
    }
}
