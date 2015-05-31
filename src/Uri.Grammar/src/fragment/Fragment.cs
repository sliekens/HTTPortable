namespace Uri.Grammar
{
    using SLANG;

    public class Fragment : Repetition
    {
        public Fragment(Repetition sequence)
            : base(sequence)
        {
        }
    }
}