using JetBrains.Annotations;
using Txt.ABNF;

namespace Http.Content_Length
{
    public class ContentLength : Repetition
    {
        public ContentLength([NotNull] Repetition repetition)
            : base(repetition)
        {
        }
    }
}
