namespace Http.Grammar
{
    using TextFx.ABNF;

    public class OptionalDelimitedList : Repetition
    {
        public OptionalDelimitedList(Repetition repetition)
            : base(repetition)
        {
        }
    }
}