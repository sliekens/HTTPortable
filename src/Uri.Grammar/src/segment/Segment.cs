namespace Uri.Grammar
{
    using SLANG;

    public class Segment : Repetition
    {
        public Segment(Repetition sequence)
            : base(sequence)
        {
        }
    }
}