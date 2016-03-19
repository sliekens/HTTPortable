namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UriReferenceLexerFactory : ILexerFactory<UriReference>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<RelativeReference> relativeReferenceLexerFactory;

        private readonly ILexerFactory<UniformResourceIdentifier> uriLexerFactory;

        public UriReferenceLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<UniformResourceIdentifier> uriLexerFactory,
            ILexerFactory<RelativeReference> relativeReferenceLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            if (uriLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(uriLexerFactory));
            }

            if (relativeReferenceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(relativeReferenceLexerFactory));
            }

            this.alternativeLexerFactory = alternativeLexerFactory;
            this.uriLexerFactory = uriLexerFactory;
            this.relativeReferenceLexerFactory = relativeReferenceLexerFactory;
        }

        public ILexer<UriReference> Create()
        {
            var uri = uriLexerFactory.Create();
            var relativeRef = relativeReferenceLexerFactory.Create();
            var innerLexer = alternativeLexerFactory.Create(uri, relativeRef);
            return new UriReferenceLexer(innerLexer);
        }
    }
}