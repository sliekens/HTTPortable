namespace Http.Grammar
{
    using System;
    using TextFx;
    using Uri.Grammar;

    public sealed class AuthorityFormLexer : Lexer<AuthorityForm>
    {
        private readonly ILexer<Authority> innerLexer;

        public AuthorityFormLexer(ILexer<Authority> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<AuthorityForm> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<AuthorityForm>.FromResult(new AuthorityForm(result.Element));
            }
            return ReadResult<AuthorityForm>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
