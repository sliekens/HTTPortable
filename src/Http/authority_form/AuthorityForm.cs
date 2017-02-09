using JetBrains.Annotations;
using UriSyntax.authority;

namespace Http.authority_form
{
    public class AuthorityForm : Authority
    {
        public AuthorityForm([NotNull] Authority authority)
            : base(authority)
        {
        }
    }
}
