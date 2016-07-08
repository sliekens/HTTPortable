using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.protocol
{
    public sealed class ProtocolLexer : CompositeLexer<Concatenation, Protocol>
    {
        public ProtocolLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
