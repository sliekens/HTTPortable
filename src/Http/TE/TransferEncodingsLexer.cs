using JetBrains.Annotations;
using Txt.Core;

namespace Http.TE
{
    public sealed class TransferEncodingsLexer : CompositeLexer<OptionalDelimitedList, TransferEncodings>
    {
        public TransferEncodingsLexer([NotNull] ILexer<OptionalDelimitedList> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
