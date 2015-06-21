namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class PathEmpty : TerminalString
    {
        public PathEmpty(TerminalString pathEmpty)
            : base(pathEmpty)
        {
        }
    }
}