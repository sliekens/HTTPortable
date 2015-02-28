namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class PathEmpty : Element
    {
        public PathEmpty(ITextContext context)
            : base(string.Empty, context)
        {
            Contract.Requires(context != null);
        }
    }
}
