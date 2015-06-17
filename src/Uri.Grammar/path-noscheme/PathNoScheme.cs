namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathNoScheme : Sequence
    {
        public PathNoScheme(Sequence sequence)
            : base(sequence)
        {
        }
    }
}