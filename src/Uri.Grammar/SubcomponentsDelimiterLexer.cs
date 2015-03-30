namespace Uri.Grammar
{
    using SLANG;

    public class SubcomponentsDelimiterLexer : AlternativeLexer<SubcomponentsDelimiter, Element, Element, Element, Element, Element, Element, Element, Element, Element, Element, Element>
    {
        public SubcomponentsDelimiterLexer()
            : base("sub-delims")
        {
        }

        protected override SubcomponentsDelimiter CreateInstance1(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 1, context);
        }

        protected override SubcomponentsDelimiter CreateInstance10(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 10, context);
        }

        protected override SubcomponentsDelimiter CreateInstance11(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 11, context);
        }

        protected override SubcomponentsDelimiter CreateInstance2(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 2, context);
        }

        protected override SubcomponentsDelimiter CreateInstance3(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 3, context);
        }

        protected override SubcomponentsDelimiter CreateInstance4(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 4, context);
        }

        protected override SubcomponentsDelimiter CreateInstance5(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 5, context);
        }

        protected override SubcomponentsDelimiter CreateInstance6(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 6, context);
        }

        protected override SubcomponentsDelimiter CreateInstance7(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 7, context);
        }

        protected override SubcomponentsDelimiter CreateInstance8(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 8, context);
        }

        protected override SubcomponentsDelimiter CreateInstance9(Element element, ITextContext context)
        {
            return new SubcomponentsDelimiter(element, 9, context);
        }

        protected override bool TryRead1(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "!", out element);
        }

        protected override bool TryRead2(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "$", out element);
        }

        protected override bool TryRead3(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "&", out element);
        }

        protected override bool TryRead4(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "'", out element);
        }

        protected override bool TryRead5(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "(", out element);
        }

        protected override bool TryRead6(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, ")", out element);
        }

        protected override bool TryRead7(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "*", out element);
        }

        protected override bool TryRead8(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "+", out element);
        }

        protected override bool TryRead9(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, ",", out element);
        }

        protected override bool TryRead10(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, ";", out element);
        }

        protected override bool TryRead11(ITextScanner scanner, out Element element)
        {
            return TryReadTerminal(scanner, "=", out element);
        }
    }
}