namespace Uri.Grammar.URI_reference
{
    using System;

    using SLANG;

    using Uri = global::Uri.Grammar.Uri;

    public class UriReferenceLexerFactory : ILexerFactory<UriReference>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<RelativeReference> relativeReferenceLexerFactory;

        private readonly ILexerFactory<Uri> uriLexerFactory;

        public UriReferenceLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<Uri> uriLexerFactory,
            ILexerFactory<RelativeReference> relativeReferenceLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (uriLexerFactory == null)
            {
                throw new ArgumentNullException("uriLexerFactory");
            }

            if (relativeReferenceLexerFactory == null)
            {
                throw new ArgumentNullException("relativeReferenceLexerFactory");
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.uriLexerFactory = uriLexerFactory;
            this.relativeReferenceLexerFactory = relativeReferenceLexerFactory;
        }

        public ILexer<UriReference> Create()
        {
            var uri = this.uriLexerFactory.Create();
            var relativeRef = this.relativeReferenceLexerFactory.Create();
            var innerLexer = this.alternativeLexerFactory.Create(uri, relativeRef);
            return new UriReferenceLexer(innerLexer);
        }
    }
}