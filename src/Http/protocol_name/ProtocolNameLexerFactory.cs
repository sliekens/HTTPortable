using System;
using Http.token;
using JetBrains.Annotations;
using Txt;

namespace Http.protocol_name
{
    public class ProtocolNameLexerFactory : ILexerFactory<ProtocolName>
    {
        private readonly ILexer<Token> tokenLexer;

        public ProtocolNameLexerFactory([NotNull] ILexer<Token> tokenLexer)
        {
            if (tokenLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenLexer));
            }
            this.tokenLexer = tokenLexer;
        }

        public ILexer<ProtocolName> Create()
        {
            return new ProtocolNameLexer(tokenLexer);
        }
    }
}
