using System;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.protocol_version
{
    public sealed class ProtocolVersionLexerFactory : RuleLexerFactory<ProtocolVersion>
    {
        static ProtocolVersionLexerFactory()
        {
            Default = new ProtocolVersionLexerFactory(token.TokenLexerFactory.Default.Singleton());
        }

        public ProtocolVersionLexerFactory([NotNull] ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }
            TokenLexerFactory = tokenLexerFactory;
        }

        [NotNull]
        public static ProtocolVersionLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Token> TokenLexerFactory { get; set; }

        public override ILexer<ProtocolVersion> Create()
        {
            var innerLexer = TokenLexerFactory.Create();
            return new ProtocolVersionLexer(innerLexer);
        }
    }
}
