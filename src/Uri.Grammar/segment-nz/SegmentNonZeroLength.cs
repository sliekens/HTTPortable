namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class SegmentNonZeroLength : Repetition
    {
        public SegmentNonZeroLength(Repetition repetition)
            : base(repetition)
        {
        }
    }
}