using Txt.ABNF;

namespace Http.field_content
{
    public class FieldContent : Concatenation
    {
        public FieldContent(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}