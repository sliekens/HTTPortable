namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using Uri.Grammar;

    public class AuthorityForm : Element
    {
        public AuthorityForm(Authority authority, ITextContext context)
            : base(authority.Data, context)
        {
            Contract.Requires(authority != null);
            Contract.Requires(context != null);
        }
    }
}