using Txt.ABNF;

namespace Http.trailer_part
{
    public class TrailerPart : Repetition
    {
        public TrailerPart(Repetition repetition)
            : base(repetition)
        {
        }
    }
}