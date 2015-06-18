namespace Http.Grammar
{
    using TextFx.ABNF;

    public class OptionalWhiteSpace : Repetition
    {
        public OptionalWhiteSpace(Repetition repetition)
            : base(repetition)
        {
        }
    }
}
