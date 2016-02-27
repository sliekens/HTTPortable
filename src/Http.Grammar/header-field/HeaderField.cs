namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HeaderField : Concatenation
    {
        public HeaderField(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public FieldName FieldName
        {
            get
            {
                return (FieldName)this.Elements[0];
            }
        }

        public FieldValue FieldValue
        {
            get
            {
                return (FieldValue)this.Elements[3];
            }
        }
    }
}
