using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.absolute_path
{
    public class AbsolutePath : Repetition
    {
        public AbsolutePath([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}