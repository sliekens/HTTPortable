namespace Uri.Grammar
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using SLANG;

    public class UriReference : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool isAbsolute;

        public UriReference(Uri uri, ITextContext context)
            : base(uri.Data, context)
        {
            Contract.Requires(uri != null);
            Contract.Requires(context != null);
            this.isAbsolute = true;
        }

        public UriReference(RelativeReference relativeReference, ITextContext context)
            : base(relativeReference.Data, context)
        {
            Contract.Requires(relativeReference != null);
            Contract.Requires(context != null);
            this.isAbsolute = false;
        }

        /// <summary>Gets a value indicating whether the URI reference is an absolute reference..</summary>
        public bool IsAbsolute
        {
            get
            {
                return this.isAbsolute;
            }
        }
    }
}