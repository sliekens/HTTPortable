namespace Uri.Grammar
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    using SLANG;

    public class RelativeReference : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RelativePart relativePart;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Query query;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Fragment fragment;

        /// <summary>Initializes a new instance of the <see cref="RelativeReference"/> class.</summary>
        /// <param name="relativePart">The relative part of the scheme.</param>
        /// <param name="querySeparator">An optional query separator.</param>
        /// <param name="query">An optional query. If a query separator is specified, then the query is required.</param>
        /// <param name="fragmentSeparator">An optional fragment separator.</param>
        /// <param name="fragment">An optional fragment. If a fragment separator is specified, then the fragment is required.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public RelativeReference(RelativePart relativePart, Element querySeparator, Query query, Element fragmentSeparator, Fragment fragment, ITextContext context)
            : base(string.Concat(relativePart, querySeparator, query, fragmentSeparator, fragment), context)
        {
            this.relativePart = relativePart;
            this.query = query;
            this.fragment = fragment;
            Contract.Requires(relativePart != null);
            Contract.Requires(querySeparator == null || (querySeparator.Data == "?" && query != null));
            Contract.Requires(fragmentSeparator == null || (fragmentSeparator.Data == "#" && fragment != null));
            Contract.Requires(context != null);
        }

        /// <summary>Gets the relative part of the scheme.</summary>
        public RelativePart RelativePart
        {
            get
            {
                return this.relativePart;
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

        /// <summary>Gets the fragment identifier component. The fragment allows indirect identification of a secondary resource by reference to a primary resource and additional identifying information.</summary>
        public Fragment Fragment
        {
            get
            {
                return this.fragment;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.relativePart != null);
        }
    }
}
