namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using QueryPart = SLANG.Sequence<SLANG.Element, Query>;

    public class AbsoluteUriLexer : Lexer<AbsoluteUri>
    {
        private readonly ILexer<HierarchicalPart> hierarchicalPartLexer;
        private readonly ILexer<Query> queryLexer;
        private readonly ILexer<Scheme> schemeLexer;

        public AbsoluteUriLexer()
            : this(new SchemeLexer(), new HierarchicalPartLexer(), new QueryLexer())
        {
        }

        public AbsoluteUriLexer(ILexer<Scheme> schemeLexer, ILexer<HierarchicalPart> hierarchicalPartLexer, 
            ILexer<Query> queryLexer)
            : base("absolute-URI")
        {
            Contract.Requires(schemeLexer != null);
            Contract.Requires(hierarchicalPartLexer != null);
            Contract.Requires(queryLexer != null);
            this.schemeLexer = schemeLexer;
            this.hierarchicalPartLexer = hierarchicalPartLexer;
            this.queryLexer = queryLexer;
        }

        public override bool TryRead(ITextScanner scanner, out AbsoluteUri element)
        {
            if (scanner.EndOfInput)
            {
                element = default(AbsoluteUri);
                return false;
            }

            Scheme scheme;
            Element schemeSeparator;
            HierarchicalPart hierarchicalPart;
            Element querySeparator = default(Element);
            Query query = default(Query);
            var context = scanner.GetContext();
            if (!this.schemeLexer.TryRead(scanner, out scheme))
            {
                element = default(AbsoluteUri);
                return false;
            }

            if (!TryReadTerminal(scanner, ':', out schemeSeparator))
            {
                scanner.PutBack(scheme.Data);
                element = default(AbsoluteUri);
                return false;
            }

            if (!this.hierarchicalPartLexer.TryRead(scanner, out hierarchicalPart))
            {
                scanner.PutBack(schemeSeparator.Data);
                scanner.PutBack(scheme.Data);
                element = default(AbsoluteUri);
                return false;
            }

            QueryPart queryPart;
            if (this.TryReadQueryPart(scanner, out queryPart))
            {
                querySeparator = queryPart.Element1;
                query = queryPart.Element2;
            }

            element = new AbsoluteUri(scheme, schemeSeparator, hierarchicalPart, querySeparator, query, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.schemeLexer != null);
            Contract.Invariant(this.hierarchicalPartLexer != null);
            Contract.Invariant(this.queryLexer != null);
        }

        private bool TryReadQueryPart(ITextScanner scanner, out QueryPart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(QueryPart);
                return false;
            }

            var context = scanner.GetContext();
            Element questionMark;
            if (!TryReadTerminal(scanner, '?', out questionMark))
            {
                element = default(QueryPart);
                return false;
            }

            Query query;
            if (!this.queryLexer.TryRead(scanner, out query))
            {
                scanner.PutBack(questionMark.Data);
                element = default(QueryPart);
                return false;
            }

            element = new QueryPart(questionMark, query, context);
            return true;
        }
    }
}