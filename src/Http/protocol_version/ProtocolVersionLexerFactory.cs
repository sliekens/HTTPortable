using System;
using Http.token;
using Txt;

namespace Http.protocol_version
{
    public class ProtocolVersionLexerFactory : ILexerFactory<ProtocolVersion>
    {
        private readonly ILexerFactory<Token> tokenLexerFactory;

        public ProtocolVersionLexerFactory(ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }

            this.tokenLexerFactory = tokenLexerFactory;
        }

        public ILexer<ProtocolVersion> Create()
        {
            var innerLexer = tokenLexerFactory.Create();
            return new ProtocolVersionLexer(innerLexer);
        }
    }
}