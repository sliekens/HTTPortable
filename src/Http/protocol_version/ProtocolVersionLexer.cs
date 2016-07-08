using Http.token;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.protocol_version
{
    public sealed class ProtocolVersionLexer : CompositeLexer<Token, ProtocolVersion>
    {
        public ProtocolVersionLexer([NotNull] ILexer<Token> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
