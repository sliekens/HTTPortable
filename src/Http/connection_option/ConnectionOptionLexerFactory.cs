using System;
using Http.token;
using JetBrains.Annotations;
using Txt;

namespace Http.connection_option
{
    public class ConnectionOptionLexerFactory : ILexerFactory<ConnectionOption>
    {
        private readonly ILexer<Token> tokenLexer;

        public ConnectionOptionLexerFactory([NotNull] ILexer<Token> tokenLexer)
        {
            if (tokenLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenLexer));
            }
            this.tokenLexer = tokenLexer;
        }

        public ILexer<ConnectionOption> Create()
        {
            return new ConnectionOptionLexer(tokenLexer);
        }
    }
}
