using Txt.ABNF;

namespace Http.absolute_path
{
    public class AbsolutePath : Repetition
    {
        public AbsolutePath(Repetition repetition)
            : base(repetition)
        {
        }
    }
}