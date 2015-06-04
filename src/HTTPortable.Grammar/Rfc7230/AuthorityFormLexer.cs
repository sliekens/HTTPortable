namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using Uri.Grammar;

    public class AuthorityFormLexer : Lexer<Authority>
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

        public override bool TryRead(ITextScanner scanner, out Authority element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Authority);
                return false;
            }

            if (this.authorityLexer.TryRead(scanner, out element))
            {
                return true;
            }

            element = default(Authority);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.authorityLexer != null);
        }
    }
}