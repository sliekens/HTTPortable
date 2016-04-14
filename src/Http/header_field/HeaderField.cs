using Http.field_name;
using Http.field_value;
using Txt.ABNF;

namespace Http.header_field
{
    public class HeaderField : Concatenation
    {
        public HeaderField(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public FieldName FieldName => (FieldName)Elements[0];

        public FieldValue FieldValue => (FieldValue)Elements[3];
    }
}
