using System;
using JetBrains.Annotations;
using Txt;
using Txt.Core;
using UriSyntax.authority;

namespace Http.authority_form
{
    public class AuthorityFormLexerFactory : ILexerFactory<AuthorityForm>
    {
        private readonly ILexer<Authority> authorityLexer;

        public AuthorityFormLexerFactory([NotNull] ILexer<Authority> authorityLexer)
        {
            if (authorityLexer == null)
            {
                throw new ArgumentNullException(nameof(authorityLexer));
            }
            this.authorityLexer = authorityLexer;
        }

        public ILexer<AuthorityForm> Create()
        {
            return new AuthorityFormLexer(authorityLexer);
        }
    }
}
