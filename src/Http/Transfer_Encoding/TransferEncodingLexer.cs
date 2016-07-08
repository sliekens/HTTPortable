using JetBrains.Annotations;
using Txt.Core;

namespace Http.Transfer_Encoding
{
    public sealed class TransferEncodingLexer : CompositeLexer<RequiredDelimitedList, TransferEncoding>
    {
        public TransferEncodingLexer([NotNull] ILexer<RequiredDelimitedList> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
