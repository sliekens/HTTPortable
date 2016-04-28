using Txt.ABNF;

namespace Http.field_vchar
{
    public class FieldVisibleCharacter : Alternation
    {
        public FieldVisibleCharacter(Alternation alternation)
            : base(alternation)
        {
        }
    }
}