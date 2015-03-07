namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using QueryPart = Text.Scanning.Sequence<Text.Scanning.Element, Query>;
    using FragmentPart = Text.Scanning.Sequence<Text.Scanning.Element, Fragment>;

    public class UriLexer : Lexer<Uri>
    {
        private readonly ILexer<Scheme> schemeLexer;

        private readonly ILexer<HierarchicalPart> hierarchicalPartLexer;

        private readonly ILexer<Query> queryLexer;

        private readonly ILexer<Fragment> fragmentLexer;

        public UriLexer()
            : this(new SchemeLexer(), new HierarchicalPartLexer(), new QueryLexer(), new FragmentLexer())
        {
        }

        public UriLexer(ILexer<Scheme> schemeLexer, ILexer<HierarchicalPart> hierarchicalPartLexer, ILexer<Query> queryLexer, ILexer<Fragment> fragmentLexer)
            : base("URI")
        {
            Contract.Requires(schemeLexer != null);
            Contract.Requires(hierarchicalPartLexer != null);
            Contract.Requires(queryLexer != null);
            Contract.Requires(fragmentLexer != null);
            this.schemeLexer = schemeLexer;
            this.hierarchicalPartLexer = hierarchicalPartLexer;
            this.queryLexer = queryLexer;
            this.fragmentLexer = fragmentLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Uri element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Uri);
                return false;
            }

            Scheme scheme;
            Element schemeSeparator;
            HierarchicalPart hierarchicalPart;
            Element querySeparator = default(Element);
            Query query = default(Query);
            Element fragmentSeparator = default(Element);
            Fragment fragment = default(Fragment);
            var context = scanner.GetContext();
            if (!this.schemeLexer.TryRead(scanner, out scheme))
            {
                element = default(Uri);
                return false;
            }

            if (!TryReadTerminal(scanner, ':', out schemeSeparator))
            {
                scanner.PutBack(scheme.Data);
                element = default(Uri);
                return false;
            }

            if (!this.hierarchicalPartLexer.TryRead(scanner, out hierarchicalPart))
            {
                scanner.PutBack(schemeSeparator.Data);
                scanner.PutBack(scheme.Data);
                element = default(Uri);
                return false;
            }

            QueryPart queryPart;
            if (this.TryReadQueryPart(scanner, out queryPart))
            {
                querySeparator = queryPart.Element1;
                query = queryPart.Element2;
            }

            FragmentPart fragmentPart;
            if (this.TryReadFragmentPart(scanner, out fragmentPart))
            {
                fragmentSeparator = fragmentPart.Element1;
                fragment = fragmentPart.Element2;
            }

            element = new Uri(
                scheme,
                schemeSeparator,
                hierarchicalPart,
                querySeparator,
                query,
                fragmentSeparator,
                fragment,
                context);
            return true;
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

        private bool TryReadFragmentPart(ITextScanner scanner, out FragmentPart element)
        {
            if (scanner.EndOfInput)
            {
                element = default(FragmentPart);
                return false;
            }

            var context = scanner.GetContext();
            Element numberSign;
            if (!TryReadTerminal(scanner, '#', out numberSign))
            {
                element = default(FragmentPart);
                return false;
            }

            Fragment fragment;
            if (!this.fragmentLexer.TryRead(scanner, out fragment))
            {
                scanner.PutBack(numberSign.Data);
                element = default(FragmentPart);
                return false;
            }

            element = new FragmentPart(numberSign, fragment, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.schemeLexer != null);
            Contract.Invariant(this.hierarchicalPartLexer != null);
            Contract.Invariant(this.queryLexer != null);
            Contract.Invariant(this.fragmentLexer != null);
        }
    }
}