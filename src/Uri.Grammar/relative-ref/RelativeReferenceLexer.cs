﻿namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using FragmentPart = SLANG.Sequence<SLANG.Element, Fragment>;
    using QueryPart = SLANG.Sequence<SLANG.Element, Query>;

    public class RelativeReferenceLexer : Lexer<RelativeReference>
    {
        private readonly ILexer<Fragment> fragmentLexer;
        private readonly ILexer<Query> queryLexer;
        private readonly ILexer<RelativePart> relativePartLexer;

        public RelativeReferenceLexer()
            : this(new RelativePartLexer(), new QueryLexer(), new FragmentLexer())
        {
        }

        public RelativeReferenceLexer(ILexer<RelativePart> relativePartLexer, ILexer<Query> queryLexer, 
            ILexer<Fragment> fragmentLexer)
            : base("relative-ref")
        {
            Contract.Requires(relativePartLexer != null);
            Contract.Requires(queryLexer != null);
            Contract.Requires(fragmentLexer != null);
            this.relativePartLexer = relativePartLexer;
            this.queryLexer = queryLexer;
            this.fragmentLexer = fragmentLexer;
        }

        public override bool TryRead(ITextScanner scanner, out RelativeReference element)
        {
            if (scanner.EndOfInput)
            {
                element = default(RelativeReference);
                return false;
            }

            RelativePart relativePart;
            Element querySeparator = default(Element);
            Query query = default(Query);
            Element fragmentSeparator = default(Element);
            Fragment fragment = default(Fragment);
            var context = scanner.GetContext();
            if (!this.relativePartLexer.TryRead(scanner, out relativePart))
            {
                element = default(RelativeReference);
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

            element = new RelativeReference(relativePart, querySeparator, query, fragmentSeparator, fragment, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.relativePartLexer != null);
            Contract.Invariant(this.queryLexer != null);
            Contract.Invariant(this.fragmentLexer != null);
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