namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class SegmentNonZeroLength : Repetition
    {
        public SegmentNonZeroLength(Repetition sequence)
            : base(sequence)
        {
        }
    }
}