namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Port : Repetition
    {
        public Port(Repetition sequence)
            : base(sequence)
        {
        }
    }
}