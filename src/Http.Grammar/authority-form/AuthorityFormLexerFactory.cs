namespace Http.Grammar
{
    using System;

    using TextFx;

    using Uri.Grammar;

    public class AuthorityFormLexerFactory : ILexerFactory<AuthorityForm>
    {
        private readonly ILexerFactory<Authority> authorityLexerFactory;

        public AuthorityFormLexerFactory(ILexerFactory<Authority> authorityLexerFactory)
        {
            if (authorityLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(authorityLexerFactory));
            }

            this.authorityLexerFactory = authorityLexerFactory;
        }

        public ILexer<AuthorityForm> Create()
        {
            var innerLexer = this.authorityLexerFactory.Create();
            return new AuthorityFormLexer(innerLexer);
        }
    }
}