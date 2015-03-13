namespace Http.Grammar.Rfc7230
{
    using SLANG;
    using Uri.Grammar;

    public class PartialUriLexer : Lexer<PartialUri>
    {
        private readonly ILexer<RelativePart> relativePartLexer;
        private readonly ILexer<Query> queryLexer;

        public PartialUriLexer(ILexer<RelativePart> relativePartLexer, ILexer<Query> queryLexer)
            : base("partial-URI")
        {
            this.relativePartLexer = relativePartLexer;
            this.queryLexer = queryLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PartialUri element)
        {
            if (scanner.EndOfInput)
            {
                element = default(PartialUri);
                return false;
            }

            var context = scanner.GetContext();
            RelativePart relativePart;
            if (!this.relativePartLexer.TryRead(scanner, out relativePart))
            {
                element = default(PartialUri);
                return false;
            }

            Option<Sequence<Element, Query>> optionalQuery;
            if (!this.TryReadOptionalQuery(scanner, out optionalQuery))
            {
                scanner.PutBack(relativePart.Data);
                element = default(PartialUri);
                return false;
            }

            element = new PartialUri(relativePart, optionalQuery, context);
            return true;
        }

        private bool TryReadOptionalQuery(ITextScanner scanner, out Option<Sequence<Element, Query>> element)
        {
            var context = scanner.GetContext();
            Sequence<Element, Query> queryPart;
            if (this.TryReadQueryPart(scanner, out queryPart))
            {
                element = new Option<Sequence<Element, Query>>(queryPart, context);
            }
            else
            {
                element = new Option<Sequence<Element, Query>>(context);
            }

            return true;
        }

        private bool TryReadQueryPart(ITextScanner scanner, out Sequence<Element, Query> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<Element, Query>);
                return false;
            }

            var context = scanner.GetContext();
            Element separator;
            if (!TryReadTerminal(scanner, @"?", out separator))
            {
                element = default(Sequence<Element, Query>);
                return false;
            }

            Query query;
            if (!this.queryLexer.TryRead(scanner, out query))
            {
                scanner.PutBack(separator.Data);
                element = default(Sequence<Element, Query>);
                return false;
            }

            element = new Sequence<Element, Query>(separator, query, context);
            return true;
        }
    }
}