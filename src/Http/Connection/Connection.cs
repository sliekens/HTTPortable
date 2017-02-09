using JetBrains.Annotations;

namespace Http.Connection
{
    public class Connection : RequiredDelimitedList
    {
        public Connection([NotNull] RequiredDelimitedList requiredDelimitedList)
            : base(requiredDelimitedList)
        {
        }
    }
}
