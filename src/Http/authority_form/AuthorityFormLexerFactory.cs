using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.authority;

namespace Http.authority_form
{
    public class AuthorityFormLexerFactory : RuleLexerFactory<AuthorityForm>
    {
        static AuthorityFormLexerFactory()
        {
            Default = new AuthorityFormLexerFactory(AuthorityLexerFactory.Default.Singleton());
        }

        public AuthorityFormLexerFactory([NotNull] ILexerFactory<Authority> authority)
        {
            if (authority == null)
            {
                throw new ArgumentNullException(nameof(authority));
            }
            Authority = authority;
        }

        [NotNull]
        public static AuthorityFormLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Authority> Authority { get; }

        public override ILexer<AuthorityForm> Create()
        {
            var innerLexer = Authority.Create();
            return new AuthorityFormLexer(innerLexer);
        }
    }
}
