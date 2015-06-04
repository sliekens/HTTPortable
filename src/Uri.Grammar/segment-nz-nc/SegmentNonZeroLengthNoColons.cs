namespace Uri.Grammar
{
    using SLANG;

    public class SegmentNonZeroLengthNoColons : Repetition
    {
        public SegmentNonZeroLengthNoColons(Repetition sequence)
            : base(sequence)
        {
        }
    }
}