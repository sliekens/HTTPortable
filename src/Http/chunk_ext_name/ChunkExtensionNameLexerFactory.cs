using System;
using Http.token;
using JetBrains.Annotations;
using Txt;

namespace Http.chunk_ext_name
{
    public class ChunkExtensionNameLexerFactory : ILexerFactory<ChunkExtensionName>
    {
        private readonly ILexer<Token> tokenLexer;

        public ChunkExtensionNameLexerFactory([NotNull] ILexer<Token> tokenLexer)
        {
            if (tokenLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenLexer));
            }
            this.tokenLexer = tokenLexer;
        }

        public ILexer<ChunkExtensionName> Create()
        {
            return new ChunkExtensionNameLexer(tokenLexer);
        }
    }
}
