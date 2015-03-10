namespace Uri.Grammar
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using SLANG;

    /// <summary>Represents a Uniform Resource Identifier (URI) as described in RFC 3986.</summary>
    /// <remarks>See: <a href="https://www.ietf.org/rfc/rfc3986.txt">RFC 3986</a>.</remarks>
    public class Uri : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Fragment fragment;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HierarchicalPart hierarchicalPart;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Query query;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Scheme scheme;

        /// <summary>Initializes a new instance of the <see cref="Uri"/> class.</summary>
        /// <param name="scheme">The name of the scheme.</param>
        /// <param name="schemeSeparator">The scheme separator.</param>
        /// <param name="hierarchicalPart">The scheme itself.</param>
        /// <param name="querySeparator">An optional query separator.</param>
        /// <param name="query">An optional query. If a query separator is specified, then the query is required.</param>
        /// <param name="fragmentSeparator">An optional fragment separator.</param>
        /// <param name="fragment">An optional fragment. If a fragment separator is specified, then the fragment is required.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Uri(Scheme scheme, Element schemeSeparator, HierarchicalPart hierarchicalPart, Element querySeparator, 
            Query query, Element fragmentSeparator, Fragment fragment, ITextContext context)
            : base(
                string.Concat(scheme, schemeSeparator, hierarchicalPart, querySeparator, query, fragmentSeparator, 
                    fragment), context)
        {
            Contract.Requires(scheme != null);
            Contract.Requires(schemeSeparator != null && schemeSeparator.Data == ":");
            Contract.Requires(hierarchicalPart != null);
            Contract.Requires(querySeparator == null || (querySeparator.Data == "?" && query != null));
            Contract.Requires(fragmentSeparator == null || (fragmentSeparator.Data == "#" && fragment != null));
            Contract.Requires(context != null);
            this.scheme = scheme;
            this.hierarchicalPart = hierarchicalPart;
            this.query = query;
            this.fragment = fragment;
        }

        /// <summary>Gets the fragment identifier component. The fragment allows indirect identification of a secondary resource by reference to a primary resource and additional identifying information.</summary>
        public Fragment Fragment
        {
            get
            {
                return this.fragment;
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

        /// <summary>Gets the scheme. The name of the scheme refers to a specification for assigning identifiers within the scheme.</summary>
        public Scheme Scheme
        {
            get
            {
                return this.scheme;
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