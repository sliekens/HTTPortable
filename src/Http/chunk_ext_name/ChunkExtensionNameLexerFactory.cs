using System;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_ext_name
{
    public sealed class ChunkExtensionNameLexerFactory : RuleLexerFactory<ChunkExtensionName>
    {
        static ChunkExtensionNameLexerFactory()
        {
            Default = new ChunkExtensionNameLexerFactory(token.TokenLexerFactory.Default.Singleton());
        }

        public ChunkExtensionNameLexerFactory([NotNull] ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }
            TokenLexerFactory = tokenLexerFactory;
        }

        [NotNull]
        public static ChunkExtensionNameLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Token> TokenLexerFactory { get; }

        public override ILexer<ChunkExtensionName> Create()
        {
            var innerLexer = TokenLexerFactory.Create();
            return new ChunkExtensionNameLexer(innerLexer);
        }
    }
}
