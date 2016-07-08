using Http.token;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.protocol_name
{
    public sealed class ProtocolNameLexer : CompositeLexer<Token, ProtocolName>
    {
        public ProtocolNameLexer([NotNull] ILexer<Token> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
