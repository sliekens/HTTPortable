using Txt.ABNF;

namespace Http.RWS
{
    public class RequiredWhiteSpace : Repetition
    {
        public RequiredWhiteSpace(Repetition repetition)
            : base(repetition)
        {
        }
    }
}