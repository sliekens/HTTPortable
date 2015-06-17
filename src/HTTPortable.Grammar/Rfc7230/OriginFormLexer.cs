namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    using Uri.Grammar;

    public class OriginFormLexer : Lexer<OriginForm>
    {
        private readonly ILexer<AbsolutePath> absolutePathLexer;
        private readonly ILexer<Query> queryLexer;

        public OriginFormLexer()
            : this(new AbsolutePathLexer(), new QueryLexer())
        {
        }

        public OriginFormLexer(ILexer<AbsolutePath> absolutePathLexer, ILexer<Query> queryLexer)
            : base("origin-form")
        {
            Contract.Requires(absolutePathLexer != null);
            Contract.Requires(queryLexer != null);
            this.absolutePathLexer = absolutePathLexer;
            this.queryLexer = queryLexer;
        }

        public override bool TryRead(ITextScanner scanner, out OriginForm element)
        {
            if (scanner.EndOfInput)
            {
                element = default(OriginForm);
                return false;
            }

            var context = scanner.GetContext();
            AbsolutePath absolutePath;
            if (!this.absolutePathLexer.TryRead(scanner, out absolutePath))
            {
                element = default(OriginForm);
                return false;
            }

            Option<Sequence<Element, Query>> optionalQuery;
            if (!this.TryReadOptionalQuery(scanner, out optionalQuery))
            {
                scanner.PutBack(absolutePath.Data);
                element = default(OriginForm);
                return false;
            }

            element = new OriginForm(absolutePath, optionalQuery, context);
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

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.absolutePathLexer != null);
            Contract.Invariant(this.queryLexer != null);
        }
    }
}