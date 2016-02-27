namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathEmpty : Terminal
    {
        public PathEmpty(Terminal pathEmpty)
            : base(pathEmpty)
        {
        }
    }
}