namespace Http.Grammar
{
    using TextFx.ABNF;

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
