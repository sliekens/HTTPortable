using Txt.ABNF;

namespace Http.reason_phrase
{
    public class ReasonPhrase : Repetition
    {
        public ReasonPhrase(Repetition repetition)
            : base(repetition)
        {
        }
    }
}