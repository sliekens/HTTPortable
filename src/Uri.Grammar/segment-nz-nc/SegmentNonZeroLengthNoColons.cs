namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class SegmentNonZeroLengthNoColons : Repetition
    {
        public SegmentNonZeroLengthNoColons(Repetition repetition)
            : base(repetition)
        {
        }
    }
}