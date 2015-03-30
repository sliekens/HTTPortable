namespace Uri.Grammar
{
    using SLANG;

    public class SubcomponentsDelimiter : Alternative<Element, Element, Element, Element, Element, Element, Element, Element, Element, Element, Element>
    {
        public SubcomponentsDelimiter(Element element, int alternative, ITextContext context)
            : base(element, alternative, context)
        {
        }
    }
}