namespace Uri.Grammar
{
    using Text.Scanning;

    public class Uri : Element
    {
        public Uri(string data, ITextContext context)
            : base(data, context)
        {
        }
    }
}
