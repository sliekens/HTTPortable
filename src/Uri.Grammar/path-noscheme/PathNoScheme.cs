namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathNoScheme : Concatenation
    {
        public PathNoScheme(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}