namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class Segment : Repetition
    {
        public Segment(Repetition repetition)
            : base(repetition)
        {
        }
    }
}