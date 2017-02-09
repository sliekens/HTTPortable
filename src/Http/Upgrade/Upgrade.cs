using JetBrains.Annotations;

namespace Http.Upgrade
{
    public class Upgrade : RequiredDelimitedList
    {
        public Upgrade([NotNull] RequiredDelimitedList requiredDelimitedList)
            : base(requiredDelimitedList)
        {
        }
    }
}
