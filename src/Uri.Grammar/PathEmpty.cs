namespace Uri.Grammar
{
    using Text.Scanning;

    public class PathEmpty : Element
    {
        public PathEmpty(ITextContext context)
            : base(string.Empty, context)
        {
        }
    }
}
