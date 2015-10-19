namespace Http.Grammar
{
    using System;

    using TextFx;

    using Uri.Grammar;

    public class AbsoluteFormLexerFactory : ILexerFactory<AbsoluteForm>
    {
        private readonly ILexerFactory<AbsoluteUri> absoluteUriLexerFactory;

        public AbsoluteFormLexerFactory(ILexerFactory<AbsoluteUri> absoluteUriLexerFactory)
        {
            if (absoluteUriLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(absoluteUriLexerFactory));
            }

            this.absoluteUriLexerFactory = absoluteUriLexerFactory;
        }

        public ILexer<AbsoluteForm> Create()
        {
            var innerLexer = this.absoluteUriLexerFactory.Create();
            return new AbsoluteFormLexer(innerLexer);
        }
    }
}