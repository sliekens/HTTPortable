using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Via
{
    public class Via : RequiredDelimitedList
    {
        public Via([NotNull] RequiredDelimitedList requiredDelimitedList)
            : base(requiredDelimitedList)
        {
        }
    }
}
