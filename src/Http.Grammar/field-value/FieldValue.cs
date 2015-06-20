namespace Http.Grammar
{
    using TextFx.ABNF;

    public class FieldValue : Repetition
    {
        public FieldValue(Repetition repetition)
            : base(repetition)
        {
        }
    }
}