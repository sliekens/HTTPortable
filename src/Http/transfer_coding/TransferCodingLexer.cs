using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_coding
{
    public sealed class TransferCodingLexer : CompositeLexer<Alternation, TransferCoding>
    {
        public TransferCodingLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
