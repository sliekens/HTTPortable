using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.TE
{
    public class TransferEncodingCollection : OptionalDelimitedList
    {
        public TransferEncodingCollection([NotNull] OptionalDelimitedList optionalDelimitedList)
            : base(optionalDelimitedList)
        {
        }
    }
}
