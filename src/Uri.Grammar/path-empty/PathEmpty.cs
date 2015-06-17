namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathEmpty : Repetition
    {
        public PathEmpty(Repetition sequence)
            : base(sequence)
        {
        }
    }
}