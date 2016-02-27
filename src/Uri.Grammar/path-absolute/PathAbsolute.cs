namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathAbsolute : Concatenation
    {
        public PathAbsolute(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}