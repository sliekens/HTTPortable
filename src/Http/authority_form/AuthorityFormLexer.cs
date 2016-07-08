using JetBrains.Annotations;
using Txt.Core;
using UriSyntax.authority;

namespace Http.authority_form
{
    public sealed class AuthorityFormLexer : CompositeLexer<Authority, AuthorityForm>
    {
        public AuthorityFormLexer([NotNull] ILexer<Authority> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
