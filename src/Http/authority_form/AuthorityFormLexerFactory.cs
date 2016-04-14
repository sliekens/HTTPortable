using System;
using Txt;
using Uri.authority;

namespace Http.authority_form
{
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
            var innerLexer = authorityLexerFactory.Create();
            return new AuthorityFormLexer(innerLexer);
        }
    }
}