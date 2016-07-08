using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.t_codings
{
    public sealed class TransferCodingCollectionLexer : CompositeLexer<Alternation, TransferCodingCollection>
    {
        public TransferCodingCollectionLexer([NotNull] ILexer<Alternation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
