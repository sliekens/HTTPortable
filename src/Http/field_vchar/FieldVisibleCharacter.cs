using Txt.ABNF;

namespace Http.field_vchar
{
    public class FieldVisibleCharacter : Alternative
    {
        public FieldVisibleCharacter(Alternative alternative)
            : base(alternative)
        {
        }
    }
}