using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Trailer
{
    public class Trailer : RequiredDelimitedList
    {
        public Trailer([NotNull] RequiredDelimitedList requiredDelimitedList)
            : base(requiredDelimitedList)
        {
        }
    }
}
