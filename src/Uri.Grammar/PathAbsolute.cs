namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class PathAbsolute : Element
    {
        public PathAbsolute(ITextContext context)
            : base("/", context)
        {
            Contract.Requires(context != null);
        }

        public PathAbsolute(PathRootless path, ITextContext context)
            : base(string.Concat("/", path.Data), context)
        {
            Contract.Requires(path != null);
            Contract.Requires(context != null);
        }
    }
}
