using System;
using Http.token;
using Txt;

namespace Http.chunk_ext_name
{
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
            var innerLexer = tokenLexerFactory.Create();
            return new ChunkExtensionNameLexer(innerLexer);
        }
    }
}