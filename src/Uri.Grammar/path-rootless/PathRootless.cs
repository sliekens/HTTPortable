namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathRootless : Concatenation
    {
        public PathRootless(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}