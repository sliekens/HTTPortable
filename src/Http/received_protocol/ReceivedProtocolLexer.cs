using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.received_protocol
{
    public sealed class ReceivedProtocolLexer : CompositeLexer<Concatenation, ReceivedProtocol>
    {
        public ReceivedProtocolLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
