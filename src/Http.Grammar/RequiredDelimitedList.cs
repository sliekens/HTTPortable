namespace Http.Grammar
{
    using TextFx.ABNF;

    public class RequiredDelimitedList : Sequence
    {
        public RequiredDelimitedList(Sequence sequence)
            : base(sequence)
        {
        }
    }
}