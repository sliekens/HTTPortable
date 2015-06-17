namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathRootless : Sequence
    {
        public PathRootless(Sequence sequence)
            : base(sequence)
        {
        }
    }
}