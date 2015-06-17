namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Fragment : Repetition
    {
        public Fragment(Repetition sequence)
            : base(sequence)
        {
        }
    }
}