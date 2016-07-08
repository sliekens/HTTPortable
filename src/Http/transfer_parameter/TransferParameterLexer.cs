using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_parameter
{
    public sealed class TransferParameterLexer : CompositeLexer<Concatenation, TransferParameter>
    {
        public TransferParameterLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
