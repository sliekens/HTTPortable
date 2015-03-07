namespace Uri.Grammar
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    /// <summary>Represents a URI scheme as a sequence of hierarchical components.</summary>
    public class HierarchicalPart : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool isEmpty;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Authority authority;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Element path;

        /// <summary>Initializes a new instance of the <see cref="HierarchicalPart"/> class.</summary>
        /// <param name="slashes">The double slash that precedes the authority component.</param>
        /// <param name="authority">The authority component.</param>
        /// <param name="path">The path component.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HierarchicalPart(Element slashes, Authority authority, PathAbsoluteOrEmpty path, ITextContext context)
            : base(string.Concat(slashes, authority, path), context)
        {
            Contract.Requires(slashes != null && slashes.Data == "//");
            Contract.Requires(authority != null);
            Contract.Requires(path != null);
            Contract.Requires(context != null);
            this.authority = authority;
            this.path = path;
            this.isEmpty = path.IsEmpty;
        }

        /// <summary>Initializes a new instance of the <see cref="HierarchicalPart"/> class.</summary>
        /// <param name="path">The path component.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HierarchicalPart(PathAbsolute path, ITextContext context)
            : base(path.Data, context)
        {
            Contract.Requires(path != null);
            Contract.Requires(context != null);
            this.path = path;
            this.isEmpty = false;
        }

        /// <summary>Initializes a new instance of the <see cref="HierarchicalPart"/> class.</summary>
        /// <param name="path">The path component.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HierarchicalPart(PathRootless path, ITextContext context)
            : base(path.Data, context)
        {
            Contract.Requires(path != null);
            Contract.Requires(context != null);
            this.path = path;
            this.isEmpty = false;
        }

        /// <summary>Initializes a new instance of the <see cref="HierarchicalPart"/> class.</summary>
        /// <param name="path">The path component.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HierarchicalPart(PathEmpty path, ITextContext context)
            : base(path.Data, context)
        {
            Contract.Requires(path != null);
            Contract.Requires(context != null);
            this.path = path;
            this.isEmpty = true;
        }

        /// <summary>Gets a value indicating whether the path is empty.</summary>
        public bool IsEmpty
        {
            get
            {
                return this.isEmpty;
            }
        }

        /// <summary>Gets the authority. Governance of the name space defined by the remainder of the URI is delegated to the naming authority.</summary>
        public Authority Authority
        {
            get
            {
                return this.authority;
            }
        }

        /// <summary>Gets the path. The path identifies a resource within the scope of the URI's scheme and naming authority (if any).</summary>
        public Element Path
        {
            get
            {
                return this.path;
            }
        }
    }
}