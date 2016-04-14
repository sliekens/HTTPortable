using Txt.ABNF;

namespace Http.status_code
{
    public class StatusCode : Repetition
    {
        public StatusCode(Repetition repetition)
            : base(repetition)
        {
        }
    }
}