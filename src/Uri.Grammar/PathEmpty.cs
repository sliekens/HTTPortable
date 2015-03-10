namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class PathEmpty : Element
    {
        public PathEmpty(ITextContext context)
            : base(string.Empty, context)
        {
            Contract.Requires(context != null);
        }
    }
}