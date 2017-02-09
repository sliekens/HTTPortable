using System;
using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.connection_option
{
    public sealed class ConnectionOptionLexerFactory : RuleLexerFactory<ConnectionOption>
    {
        static ConnectionOptionLexerFactory()
        {
            Default = new ConnectionOptionLexerFactory(token.TokenLexerFactory.Default.Singleton());
        }

        public ConnectionOptionLexerFactory([NotNull] ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }
            TokenLexerFactory = tokenLexerFactory;
        }

        [NotNull]
        public static ConnectionOptionLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Token> TokenLexerFactory { get; set; }

        public override ILexer<ConnectionOption> Create()
        {
            var innerLexer = TokenLexerFactory.Create();
            return new ConnectionOptionLexer(innerLexer);
        }
    }
}
