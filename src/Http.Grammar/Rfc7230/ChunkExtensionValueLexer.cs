namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class ChunkExtensionValueLexer : AlternativeLexer<ChunkExtensionValue, Token, QuotedString>
    {
        private readonly ILexer<Token> element1Lexer;

        private readonly ILexer<QuotedString> element2Lexer;

        public ChunkExtensionValueLexer()
            : this(new TokenLexer(), new QuotedStringLexer())
        {
        }

        public ChunkExtensionValueLexer(ILexer<Token> element1Lexer, ILexer<QuotedString> element2Lexer)
            : base("chunk-ext-value")
        {
            this.element1Lexer = element1Lexer;
            this.element2Lexer = element2Lexer;
        }

        protected override ChunkExtensionValue CreateInstance1(Token element, ITextContext context)
        {
            return new ChunkExtensionValue(element, 1, context);
        }

        protected override ChunkExtensionValue CreateInstance2(QuotedString element, ITextContext context)
        {
            return new ChunkExtensionValue(element, 2, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out Token element)
        {
            return this.element1Lexer.TryRead(scanner, out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out QuotedString element)
        {
            return this.element2Lexer.TryRead(scanner, out element);
        }
    }
}
