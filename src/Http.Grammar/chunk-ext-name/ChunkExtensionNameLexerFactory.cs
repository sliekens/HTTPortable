namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkExtensionNameLexerFactory : ILexerFactory<ChunkExtensionName>
    {
        private readonly ILexerFactory<Token> tokenLexerFactory;

        public ChunkExtensionNameLexerFactory(ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }

            this.tokenLexerFactory = tokenLexerFactory;
        }

        public ILexer<ChunkExtensionName> Create()
        {
            var innerLexer = this.tokenLexerFactory.Create();
            return new ChunkExtensionNameLexer(innerLexer);
        }
    }
}