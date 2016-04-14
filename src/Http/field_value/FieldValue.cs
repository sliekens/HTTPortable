using Txt.ABNF;

namespace Http.field_value
{
    public class FieldValue : Repetition
    {
        public FieldValue(Repetition repetition)
            : base(repetition)
        {
        }
    }
}