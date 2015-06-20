namespace Http.Grammar
{
    using TextFx.ABNF;

    public class FieldContent : Sequence
    {
        public FieldContent(Sequence sequence)
            : base(sequence)
        {
        }
    }
}