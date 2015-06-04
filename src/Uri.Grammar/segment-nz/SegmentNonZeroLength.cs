namespace Uri.Grammar
{
    using SLANG;

    public class SegmentNonZeroLength : Repetition
    {
        public SegmentNonZeroLength(Repetition sequence)
            : base(sequence)
        {
        }
    }
}