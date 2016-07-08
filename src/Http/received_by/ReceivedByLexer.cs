using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.received_by
{
    public sealed class ReceivedByLexer : CompositeLexer<Alternation, ReceivedBy>
    {
        public ReceivedByLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
