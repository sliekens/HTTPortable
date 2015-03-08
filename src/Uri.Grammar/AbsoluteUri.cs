namespace Uri.Grammar
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    using SLANG;

    public class AbsoluteUri : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Scheme scheme;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HierarchicalPart hierarchicalPart;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Query query;

        public AbsoluteUri(Scheme scheme, Element schemeSeparator, HierarchicalPart hierarchicalPart, Element querySeparator, Query query, ITextContext context)
            : base(string.Concat(scheme, schemeSeparator, hierarchicalPart, querySeparator, query), context)
        {
            Contract.Requires(scheme != null);
            Contract.Requires(schemeSeparator != null && schemeSeparator.Data == ":");
            Contract.Requires(hierarchicalPart != null);
            Contract.Requires(querySeparator == null || (querySeparator.Data == "?" && query != null));
            Contract.Requires(context != null);
            this.scheme = scheme;
            this.hierarchicalPart = hierarchicalPart;
            this.query = query;
        }

        /// <summary>Gets the scheme. The name of the scheme refers to a specification for assigning identifiers within the scheme.</summary>
        public Scheme Scheme
        {
            get
            {
                return this.scheme;
            }
        }

        /// <summary>Gets the hierarchical part that represents the scheme.</summary>
        public HierarchicalPart HierarchicalPart
        {
            get
            {
                return this.hierarchicalPart;
            }
        }

        /// <summary>Gets the query component. The query serves to identify a resource within the scope of the URI's scheme and naming authority (if any).</summary>
        public Query Query
        {
            get
            {
                return this.query;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.scheme != null);
            Contract.Invariant(this.hierarchicalPart != null);
        }
    }
}
