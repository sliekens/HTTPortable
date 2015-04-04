namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class UriReferenceLexer : Lexer<UriReference>
    {
        private readonly ILexer<RelativeReference> relativeReferenceLexer;
        private readonly ILexer<Uri> uriLexer;

        public UriReferenceLexer()
            : this(new UriLexer(), new RelativeReferenceLexer())
        {
        }

        public UriReferenceLexer(ILexer<Uri> uriLexer, ILexer<RelativeReference> relativeReferenceLexer)
            : base("URI-reference")
        {
            Contract.Requires(uriLexer != null);
            Contract.Requires(relativeReferenceLexer != null);
            this.uriLexer = uriLexer;
            this.relativeReferenceLexer = relativeReferenceLexer;
        }

        public override bool TryRead(ITextScanner scanner, out UriReference element)
        {
            if (scanner.EndOfInput)
            {
                element = default(UriReference);
                return false;
            }

            var context = scanner.GetContext();
            Uri uri;
            if (this.uriLexer.TryRead(scanner, out uri))
            {
                element = new UriReference(uri, context);
                return true;
            }

            RelativeReference relativeReference;
            if (this.relativeReferenceLexer.TryRead(scanner, out relativeReference))
            {
                element = new UriReference(relativeReference, context);
                return true;
            }

            element = default(UriReference);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.uriLexer != null);
            Contract.Invariant(this.relativeReferenceLexer != null);
        }
    }
}