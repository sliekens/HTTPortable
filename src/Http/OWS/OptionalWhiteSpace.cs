using Txt.ABNF;

namespace Http.OWS
{
    public class OptionalWhiteSpace : Repetition
    {
        public OptionalWhiteSpace(Repetition repetition)
            : base(repetition)
        {
        }
    }
}