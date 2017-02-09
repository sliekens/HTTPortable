using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Transfer_Encoding
{
    public class TransferEncoding : RequiredDelimitedList
    {
        public TransferEncoding([NotNull] RequiredDelimitedList requiredDelimitedList)
            : base(requiredDelimitedList)
        {
        }
    }
}
