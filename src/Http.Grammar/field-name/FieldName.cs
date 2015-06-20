namespace Http.Grammar
{
    using TextFx.ABNF;

    public class FieldName : Token
    {
        public FieldName(Repetition repetition)
            : base(repetition)
        {
        }
    }
}