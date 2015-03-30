namespace Uri.Grammar
{
    using SLANG;

    public class GenericDelimiterLexer : AlternativeLexer<GenericDelimiter, Element, Element, Element, Element, Element, Element, Element>
    {
        public GenericDelimiterLexer()
            : base("gen-delims")
        {
        }

        protected override GenericDelimiter CreateInstance1(Element element, ITextContext context)
        {
            return new GenericDelimiter(element, 1, context);
        }

        protected override GenericDelimiter CreateInstance2(Element element, ITextContext context)
        {
            return new GenericDelimiter(element, 2, context);
        }

        protected override GenericDelimiter CreateInstance3(Element element, ITextContext context)
        {
            return new GenericDelimiter(element, 3, context);
        }

        protected override GenericDelimiter CreateInstance4(Element element, ITextContext context)
        {
            return new GenericDelimiter(element, 4, context);
        }

        protected override GenericDelimiter CreateInstance5(Element element, ITextContext context)
        {
            return new GenericDelimiter(element, 5, context);
        }

        protected override GenericDelimiter CreateInstance6(Element element, ITextContext context)
        {
            return new GenericDelimiter(element, 6, context);
        }

        protected override GenericDelimiter CreateInstance7(Element element, ITextContext context)
        {
            return new GenericDelimiter(element, 7, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, ":", out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "/", out element);
        }

        protected override bool TryRead3(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "?", out element);
        }

        protected override bool TryRead4(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "#", out element);
        }

        protected override bool TryRead5(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "[", out element);
        }

        protected override bool TryRead6(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "]", out element);
        }

        protected override bool TryRead7(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "@", out element);
        }
    }
}