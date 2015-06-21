namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HeaderField : Sequence
    {
        public HeaderField(Sequence sequence)
            : base(sequence)
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
