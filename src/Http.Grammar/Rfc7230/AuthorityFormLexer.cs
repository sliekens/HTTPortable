namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    using Uri.Grammar;

    public class AuthorityFormLexer : Lexer<AuthorityForm>
    {
        private readonly ILexer<Authority> authorityLexer;

        public AuthorityFormLexer()
            : this(new AuthorityLexer())
        {
        }

        public AuthorityFormLexer(ILexer<Authority> authorityLexer)
            : base("authority-form")
        {
            Contract.Requires(authorityLexer != null);
            this.authorityLexer = authorityLexer;
        }

        public override bool TryRead(ITextScanner scanner, out AuthorityForm element)
        {
            if (scanner.EndOfInput)
            {
                element = default(AuthorityForm);
                return false;
            }

            var context = scanner.GetContext();
            Authority authority;
            if (this.authorityLexer.TryRead(scanner, out authority))
            {
                element = new AuthorityForm(authority, context);
                return true;
            }

            element = default(AuthorityForm);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.authorityLexer != null);
        }
    }
}
