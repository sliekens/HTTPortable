namespace Uri.Grammar
{
    using SLANG;

    public class GenericDelimiter : Alternative<Element, Element, Element, Element, Element, Element, Element>
    {
        public GenericDelimiter(Element element, int alternative, ITextContext context)
            : base(element, alternative, context)
        {
        }
    }
}