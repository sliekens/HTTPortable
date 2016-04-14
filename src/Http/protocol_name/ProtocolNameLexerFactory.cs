using System;
using Http.token;
using Txt;

namespace Http.protocol_name
{
    public class ProtocolNameLexerFactory : ILexerFactory<ProtocolName>
    {
        private readonly ILexerFactory<Token> tokenLexerFactory;

        public ProtocolNameLexerFactory(ILexerFactory<Token> tokenLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }

            this.tokenLexerFactory = tokenLexerFactory;
        }

        public ILexer<ProtocolName> Create()
        {
            var innerLexer = tokenLexerFactory.Create();
            return new ProtocolNameLexer(innerLexer);
        }
    }
}