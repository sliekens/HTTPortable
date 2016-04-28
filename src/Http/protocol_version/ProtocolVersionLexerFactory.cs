using System;
using Http.token;
using JetBrains.Annotations;
using Txt;

namespace Http.protocol_version
{
    public class ProtocolVersionLexerFactory : ILexerFactory<ProtocolVersion>
    {
        private readonly ILexer<Token> tokenLexer;

        public ProtocolVersionLexerFactory([NotNull] ILexer<Token> tokenLexer)
        {
            if (tokenLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenLexer));
            }
            this.tokenLexer = tokenLexer;
        }

        public ILexer<ProtocolVersion> Create()
        {
            return new ProtocolVersionLexer(tokenLexer);
        }
    }
}
