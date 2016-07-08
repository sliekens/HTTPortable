using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_extension
{
    public sealed class TransferExtensionLexer : CompositeLexer<Concatenation, TransferExtension>
    {
        public TransferExtensionLexer([NotNull] ILexer<Concatenation> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
