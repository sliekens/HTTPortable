using Txt.ABNF;

namespace Http.Content_Length
{
    public class ContentLength : Repetition
    {
        public ContentLength(Repetition repetition)
            : base(repetition)
        {
        }
    }
}